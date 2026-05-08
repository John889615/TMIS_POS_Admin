using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using POS_Api.ServiceInterfaces.BusinessCentral;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.Models.BusinessCentral;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.BusinessCentral
{
    /// <summary>
    /// Spec 3: pushes one paid POS invoice to BC as a Sales Order,
    /// then auto-posts via Microsoft.NAV.shipAndInvoice. Stock decrements
    /// as a side-effect of BC posting (Posted Sales Shipment).
    ///
    /// Idempotent: short-circuits if BC_InvoiceID is already stamped.
    /// On failure, stamps BC_LastError so the operator can see why.
    /// </summary>
    public class Bc_Push_Service : IBc_Push_Service
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<Bc_Push_Service> _logger;
        private readonly IBcTokenProvider _tokenProvider;

        public Bc_Push_Service(
            IHttpClientFactory httpFactory,
            IConfiguration config,
            ILogger<Bc_Push_Service> logger,
            IBcTokenProvider tokenProvider)
        {
            _httpFactory = httpFactory;
            _config = config;
            _logger = logger;
            _tokenProvider = tokenProvider;
        }

        // -------------------------------------------------------------------
        // PushInvoiceAsync
        // -------------------------------------------------------------------

        public async Task<ApiResponse<Bc_Push_Result>> PushInvoiceAsync(Guid invoiceHeaderId, CancellationToken token = default)
        {
            var result = new Bc_Push_Result { InvoiceHeaderID = invoiceHeaderId };

            if (invoiceHeaderId == Guid.Empty)
            {
                return ApiResponse.Fail<Bc_Push_Result>(
                    AppErrorCode.ValidationError,
                    new List<string> { "InvoiceHeaderID is required." }, 400);
            }

            var settings = LoadSettings();
            if (string.IsNullOrWhiteSpace(settings.PosCustomerNo))
            {
                return Fail(result, "BusinessCentral:PosCustomerNo is not configured.");
            }

            // Step 1: load invoice + lines + location code
            HeaderRow header;
            List<LineRow> lines;
            try
            {
                (header, lines) = await LoadForPushAsync(invoiceHeaderId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bc_Push_Service: load failed for {Id}", invoiceHeaderId);
                return Fail(result, $"DB load failed: {ex.Message}");
            }

            if (header == null)
            {
                return Fail(result, "Invoice not found.");
            }

            // Idempotency: a real posted-invoice id (NOT an "ORDER:..."
            // placeholder from a previous AutoPost=false run) means BC has
            // already posted this invoice. Short-circuit.
            bool hasOrderPlaceholder =
                !string.IsNullOrWhiteSpace(header.ExistingBcInvoiceID)
                && header.ExistingBcInvoiceID.StartsWith("ORDER:", StringComparison.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(header.ExistingBcInvoiceID) && !hasOrderPlaceholder)
            {
                result.AlreadyPushed = true;
                result.BC_InvoiceID  = header.ExistingBcInvoiceID;
                return ApiResponse.Success(result);
            }

            if (header.IsPaid != true)
                return Fail(result, "Invoice is not paid.");
            if (header.IsVoided == true)
                return Fail(result, "Invoice is voided.");
            if (string.IsNullOrWhiteSpace(header.LocationBcId))
                return Fail(result, $"Location {header.FK_LocationID} has no BC_ID (BC location GUID required for salesOrderLine.locationId).");
            if (lines == null || lines.Count == 0)
                return Fail(result, "Invoice has no lines.");

            // Defensive: every line must have ProductBcId
            var missing = lines.Where(l => string.IsNullOrWhiteSpace(l.ProductBcId)).ToList();
            if (missing.Any())
            {
                var products = string.Join("; ", missing.Select(l => $"'{l.ProductName}' (ProductID={l.FK_ProductID})"));
                return Fail(result, $"Products missing BC_ID: {products}");
            }

            // Step 2: BC token + URL prefix
            string companyUrl;
            string bearer;
            try
            {
                bearer = await _tokenProvider.GetAccessTokenAsync();
                companyUrl = $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                             $"/api/v2.0/companies({settings.CompanyId})";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bc_Push_Service: token/url failed for {Id}", invoiceHeaderId);
                return Fail(result, $"BC auth failed: {ex.Message}");
            }

            // Step 3..5: create order (or reuse), add lines (or skip), post via shipAndInvoice
            var client = _httpFactory.CreateClient("bc");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            // RESUME MODE: a previous attempt already created the order
            // (BC_SalesOrderID is stamped). Skip header + line creation and
            // jump straight to shipAndInvoice. Avoids piling up zombie orders
            // in BC every time a posting-setup error surfaces.
            bool resume = !string.IsNullOrWhiteSpace(header.ExistingBcSalesOrderID);
            string orderId = header.ExistingBcSalesOrderID;

            try
            {
                if (!resume)
                {
                    // 3. Create the sales order header
                    orderId = await CreateSalesOrderAsync(
                        client, companyUrl, settings.PosCustomerNo, header, token);

                    // Stamp the order id NOW so a downstream failure (line
                    // creation, posting setup, etc.) leaves the order id
                    // recoverable. The next attempt will resume from here.
                    try
                    {
                        await StampResultAsync(invoiceHeaderId, success: true,
                            bcInvoiceId: null, bcSalesOrderId: orderId, errorMessage: null);
                    }
                    catch (Exception stampEx)
                    {
                        _logger.LogWarning(stampEx, "stamp BC_SalesOrderID failed for {Id}; continuing", invoiceHeaderId);
                    }

                    // 4. Add each line (locationId = BC Location GUID).
                    // Wrap each line POST so a BC failure surfaces which product
                    // it choked on (its name + DB ProductID + BC item GUID).
                    foreach (var line in lines)
                    {
                        _logger.LogInformation(
                            "BC push: invoice {Invoice} line {LineId} product '{Product}' (ProductID={ProductID}, BC_ID={BcId}) qty={Qty}",
                            invoiceHeaderId, line.InvoiceLineID, line.ProductName, line.FK_ProductID, line.ProductBcId, line.Quantity);

                        try
                        {
                            await AddSalesOrderLineAsync(client, companyUrl, orderId, header.LocationBcId, line, token);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(
                                $"Line failed for product '{line.ProductName}' " +
                                $"(ProductID={line.FK_ProductID}, BC_ID={line.ProductBcId}): {ex.Message}", ex);
                        }
                    }
                }
                else
                {
                    _logger.LogInformation(
                        "BC push RESUME: invoice {Invoice} reusing existing BC sales order {OrderId} (skipping create + lines).",
                        invoiceHeaderId, orderId);
                }

                string stampedInvoiceId;
                if (settings.AutoPost)
                {
                    // 5. Ship + invoice (atomic action; BC creates Posted Sales Invoice + Posted Sales Shipment)
                    stampedInvoiceId = await ShipAndInvoiceAsync(client, companyUrl, orderId, token);
                }
                else
                {
                    // BYPASS MODE: leave the order Open in BC. Stock does NOT
                    // decrement. Posted invoice does NOT exist. Operator must
                    // post the order manually in BC after fixing General/VAT
                    // Posting Setup. We stamp the order id with an "ORDER:"
                    // prefix on BC_InvoiceID so the sweep treats the row as
                    // done; the prefix is visually distinct from a real
                    // posted-invoice id, and BC_SalesOrderID still holds the
                    // raw order id for direct lookup in BC.
                    stampedInvoiceId = "ORDER:" + orderId;
                    _logger.LogWarning(
                        "BC AutoPost disabled - order {OrderId} created OPEN. Stock NOT deducted. " +
                        "Operator must post manually in BC after fixing posting-setup config.",
                        orderId);
                }

                // Step 6: stamp success (also re-stamps BC_SalesOrderID, no-op if same)
                await StampResultAsync(invoiceHeaderId, success: true,
                    bcInvoiceId: stampedInvoiceId, bcSalesOrderId: orderId, errorMessage: null);

                result.Pushed       = true;
                result.BC_InvoiceID = stampedInvoiceId;
                return ApiResponse.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Bc_Push_Service: BC push failed for {Id}", invoiceHeaderId);
                var msg = Truncate(ex.Message, 4000);
                try
                {
                    // Preserve any orderId we already obtained so a future
                    // retry can resume on the existing BC order.
                    await StampResultAsync(invoiceHeaderId, success: false,
                        bcInvoiceId: null, bcSalesOrderId: orderId, errorMessage: msg);
                }
                catch (Exception stampEx) { _logger.LogError(stampEx, "stamp_result failed for {Id}", invoiceHeaderId); }
                return Fail(result, msg);
            }
        }

        // -------------------------------------------------------------------
        // GetVoidedInvoicesAsync
        // -------------------------------------------------------------------

        public async Task<ApiResponse<Bc_VoidedInvoices_Response>> GetVoidedInvoicesAsync(CancellationToken token = default)
        {
            var rows = new List<Bc_VoidedInvoice_Row>();
            try
            {
                var connectionString = _config.GetConnectionString("ApplicationDb_1");
                using var conn = SqlClient.CreateInstance(connectionString);
                await conn.OpenAsync(token);
                using var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    conn, "POS_InvoiceHeader_BC_select_voided", null);

                while (await reader.ReadAsync(token))
                {
                    rows.Add(new Bc_VoidedInvoice_Row
                    {
                        InvoiceHeaderID  = (Guid)reader["InvoiceHeaderID"],
                        InvoiceNo        = reader["InvoiceNo"]        as string,
                        PartyName        = reader["PartyName"]        as string,
                        BookingReference = reader["BookingReference"] as string,
                        InclTotal        = reader["InclTotal"]        as decimal?,
                        VoidedDate       = reader["VoidedDate"]       as DateTime?,
                        VoidedBy         = reader["VoidedBy"]         as string,
                        VoidReason       = reader["VoidReason"]       as string,
                        BC_InvoiceID     = reader["BC_InvoiceID"]     as string,
                        BC_PushedAt      = reader["BC_PushedAt"]      as DateTime?,
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetVoidedInvoicesAsync failed");
                return ApiResponse.Fail<Bc_VoidedInvoices_Response>(
                    AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            var resp = new Bc_VoidedInvoices_Response
            {
                VoidedAndPushed    = rows.Where(r => !string.IsNullOrEmpty(r.BC_InvoiceID)).ToList(),
                VoidedAndNotPushed = rows.Where(r =>  string.IsNullOrEmpty(r.BC_InvoiceID)).ToList(),
            };
            return ApiResponse.Success(resp);
        }

        // -------------------------------------------------------------------
        // BC OData calls
        // -------------------------------------------------------------------

        private async Task<string> CreateSalesOrderAsync(HttpClient client, string companyUrl, string customerNumber, HeaderRow header, CancellationToken token)
        {
            var url = $"{companyUrl}/salesOrders";

            // BC's UI auto-defaults two fields that the v2.0 API does NOT
            // populate on POST - both are required when we later call
            // Microsoft.NAV.shipAndInvoice:
            //
            //   pricesIncludeTax  - based on the customer's "Prices
            //                       Including VAT" / Customer Price Group.
            //                       Without it the VAT lookup mismatches.
            //   postingDate       - required to post; without it,
            //                       shipAndInvoice fails with
            //                       "Posting Date must have a value".
            //
            // orderDate is the date the customer placed the order;
            // postingDate is the date the financial entries land in
            // the GL. For POS we use today for both.
            var orderDate   = (header.DateCreated ?? DateTime.UtcNow).ToString("yyyy-MM-dd");
            var postingDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var body = JsonSerializer.Serialize(new
            {
                customerNumber         = customerNumber,
                orderDate              = orderDate,
                postingDate            = postingDate,
                externalDocumentNumber = header.InvoiceNo,
                pricesIncludeTax       = true
            });

            using var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
            };
            using var resp = await client.SendAsync(req, token);
            var respBody = await resp.Content.ReadAsStringAsync(token);
            if (!resp.IsSuccessStatusCode)
                throw new Exception($"BC POST salesOrders failed {(int)resp.StatusCode}: {respBody}");

            using var doc = JsonDocument.Parse(respBody);
            if (!doc.RootElement.TryGetProperty("id", out var idEl) || idEl.ValueKind != JsonValueKind.String)
                throw new Exception("BC POST salesOrders response missing 'id'.");

            var orderId = idEl.GetString();
            // Surface the BC order id and the documentNumber (BC's own SO no.)
            // so the operator can find this exact order in BC and compare
            // its VAT Bus. Posting Group / pricesIncludeTax against a manually
            // created order on the same customer.
            string docNumber = doc.RootElement.TryGetProperty("number", out var numEl) && numEl.ValueKind == JsonValueKind.String
                ? numEl.GetString() : null;
            _logger.LogInformation(
                "BC sales order created. id={OrderId} number={DocNumber} customer={Customer} externalDocNo={ExtDoc}",
                orderId, docNumber, customerNumber, header.InvoiceNo);

            return orderId;
        }

        private async Task AddSalesOrderLineAsync(HttpClient client, string companyUrl, string orderId, string locationBcId, LineRow line, CancellationToken token)
        {
            var url = $"{companyUrl}/salesOrders({orderId})/salesOrderLines";

            // Per spec Q6: Unit Price = (LineTotalExcl + LineDiscount) / Quantity.
            // Discount is sent separately so it shows as a real discount in BC.
            decimal qty = line.Quantity ?? 0m;
            decimal lineDiscount = line.LineDiscount ?? 0m;
            decimal unitPrice = qty > 0
                ? Math.Round(((line.LineTotalExcl ?? 0m) + lineDiscount) / qty, 4, MidpointRounding.AwayFromZero)
                : 0m;

            // BC v2.0 salesOrderLine expects:
            //   itemId      = item GUID  (lineObjectNumber works only if lineType=Item is set;
            //                             itemId is the canonical lookup)
            //   locationId  = location GUID (the property "locationCode" does NOT exist
            //                             on this resource - common mistake)
            var body = JsonSerializer.Serialize(new
            {
                lineType       = "Item",
                itemId         = line.ProductBcId,
                quantity       = qty,
                unitPrice      = unitPrice,
                discountAmount = lineDiscount,
                locationId     = locationBcId
            });

            using var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
            };
            using var resp = await client.SendAsync(req, token);
            var respBody = await resp.Content.ReadAsStringAsync(token);
            if (!resp.IsSuccessStatusCode)
                throw new Exception($"BC POST salesOrderLines failed {(int)resp.StatusCode}: {respBody}");
        }

        private async Task<string> ShipAndInvoiceAsync(HttpClient client, string companyUrl, string orderId, CancellationToken token)
        {
            var url = $"{companyUrl}/salesOrders({orderId})/Microsoft.NAV.shipAndInvoice";

            using var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json"),
            };
            using var resp = await client.SendAsync(req, token);
            var respBody = await resp.Content.ReadAsStringAsync(token);
            if (!resp.IsSuccessStatusCode)
                throw new Exception($"BC shipAndInvoice failed {(int)resp.StatusCode}: {respBody}");

            // BC may return the new posted-invoice id as a string body, an
            // object with "value" or "id", or empty (then we fall back to the
            // sales order id as a placeholder marker).
            if (string.IsNullOrWhiteSpace(respBody))
                return orderId;

            try
            {
                using var doc = JsonDocument.Parse(respBody);
                var root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.String) return root.GetString();
                if (root.TryGetProperty("value", out var v) && v.ValueKind == JsonValueKind.String) return v.GetString();
                if (root.TryGetProperty("id",    out var i) && i.ValueKind == JsonValueKind.String) return i.GetString();
            }
            catch
            {
                // Body wasn't JSON; treat as opaque id string
                return respBody.Trim('"');
            }

            return respBody.Trim('"');
        }

        // -------------------------------------------------------------------
        // SQL helpers
        // -------------------------------------------------------------------

        private async Task<(HeaderRow header, List<LineRow> lines)> LoadForPushAsync(Guid invoiceHeaderId)
        {
            HeaderRow header = null;
            var lines = new List<LineRow>();

            var connectionString = _config.GetConnectionString("ApplicationDb_1");
            using var conn = SqlClient.CreateInstance(connectionString);
            await conn.OpenAsync();

            using var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                conn,
                "POS_InvoiceHeader_BC_load_for_push",
                new SqlParameter { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceHeaderID", Value = invoiceHeaderId });

            // Set 0: header
            if (await reader.ReadAsync())
            {
                header = new HeaderRow
                {
                    InvoiceHeaderID         = (Guid)reader["InvoiceHeaderID"],
                    InvoiceNo               = reader["InvoiceNo"]               as string,
                    DateCreated             = reader["DateCreated"]             as DateTime?,
                    IsPaid                  = reader["IsPaid"]                  as bool?,
                    IsVoided                = reader["IsVoided"]                as bool?,
                    FK_LocationID           = reader["FK_LocationID"]           as int?,
                    LocationCode            = reader["LocationCode"]            as string,
                    LocationBcId            = reader["LocationBcId"]            as string,
                    ExistingBcInvoiceID     = reader["ExistingBcInvoiceID"]     as string,
                    ExistingBcSalesOrderID  = reader["ExistingBcSalesOrderID"]  as string,
                };
            }

            // Set 1: lines
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    lines.Add(new LineRow
                    {
                        InvoiceLineID = (Guid)reader["InvoiceLineID"],
                        FK_ProductID  = reader["FK_ProductID"]  as int?,
                        ProductName   = reader["ProductName"]   as string,
                        ProductBcId   = reader["ProductBcId"]   as string,
                        Quantity      = reader["Quantity"]      as decimal?,
                        LineDiscount  = reader["LineDiscount"]  as decimal?,
                        LineTotalExcl = reader["LineTotalExcl"] as decimal?,
                        LineTotalIncl = reader["LineTotalIncl"] as decimal?,
                    });
                }
            }

            return (header, lines);
        }

        private async Task StampResultAsync(Guid invoiceHeaderId, bool success, string bcInvoiceId, string bcSalesOrderId, string errorMessage)
        {
            var connectionString = _config.GetConnectionString("ApplicationDb_1");
            using var conn = SqlClient.CreateInstance(connectionString);
            await conn.OpenAsync();
            await SqlClient.ExecuteNonQueryStoredProcedureAsync(
                conn,
                "POS_InvoiceHeader_BC_stamp_result",
                new SqlParameter { DbType = DbType.Guid,    Direction = ParameterDirection.Input, ParameterName = "@InvoiceHeaderID", Value = invoiceHeaderId },
                new SqlParameter { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@Success",         Value = success },
                new SqlParameter { DbType = DbType.String,  Direction = ParameterDirection.Input, ParameterName = "@BcInvoiceID",     Value = (object)bcInvoiceId    ?? DBNull.Value },
                new SqlParameter { DbType = DbType.String,  Direction = ParameterDirection.Input, ParameterName = "@BcSalesOrderID",  Value = (object)bcSalesOrderId ?? DBNull.Value },
                new SqlParameter { DbType = DbType.String,  Direction = ParameterDirection.Input, ParameterName = "@ErrorMessage",    Value = (object)errorMessage   ?? DBNull.Value });
        }

        // -------------------------------------------------------------------
        // Helpers
        // -------------------------------------------------------------------

        private BusinessCentralSettings LoadSettings()
        {
            var s = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(s);
            return s;
        }

        private ApiResponse<Bc_Push_Result> Fail(Bc_Push_Result result, string message)
        {
            result.ErrorMessage = message;
            // Stamp the failure to the extension table; ignore if it itself fails.
            try
            {
                StampResultAsync(result.InvoiceHeaderID, success: false,
                    bcInvoiceId: null, bcSalesOrderId: null, errorMessage: Truncate(message, 4000))
                    .GetAwaiter().GetResult();
            }
            catch (Exception ex) { _logger.LogWarning(ex, "stamp_result during Fail() threw for {Id}", result.InvoiceHeaderID); }
            return ApiResponse.Fail<Bc_Push_Result>(AppErrorCode.ServerError, new List<string> { message }, 500);
        }

        private static string Truncate(string s, int max) => string.IsNullOrEmpty(s) ? s : (s.Length <= max ? s : s.Substring(0, max));

        // -------------------------------------------------------------------
        // Internal row shapes (mirror the load_for_push result sets)
        // -------------------------------------------------------------------

        private sealed class HeaderRow
        {
            public Guid InvoiceHeaderID { get; set; }
            public string InvoiceNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public bool? IsPaid { get; set; }
            public bool? IsVoided { get; set; }
            public int? FK_LocationID { get; set; }
            public string LocationCode { get; set; }   // POS_Locations.ShortCode  (informational)
            public string LocationBcId { get; set; }   // POS_Locations.BC_ID      (BC location GUID, used for locationId on salesOrderLine)
            public string ExistingBcInvoiceID { get; set; }
            public string ExistingBcSalesOrderID { get; set; }  // BC sales order id, set after a previous successful CreateSalesOrderAsync
        }

        private sealed class LineRow
        {
            public Guid InvoiceLineID { get; set; }
            public int? FK_ProductID { get; set; }
            public string ProductName { get; set; }   // POS_InvoiceLines.Product (snapshot)
            public string ProductBcId { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? LineDiscount { get; set; }
            public decimal? LineTotalExcl { get; set; }
            public decimal? LineTotalIncl { get; set; }
        }
    }
}
