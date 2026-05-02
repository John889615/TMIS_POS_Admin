using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Stock.POS_CostCenterProductPriceHistory;
using POS_Common.Models.Stock.POS_CostCenterProducts;
using POS_Common.Models.Stock.POS_DebtorProductPriceHistory;
using POS_Common.Models.Stock.POS_DebtorProductPrices;
using POS_Common.Models.Stock.POS_DebtorProducts;
using POS_Common.Models.Stock.POS_PurchaseOrderLines;
using POS_Common.Models.Stock.POS_PurchaseOrders;
using POS_Common.Models.Stock.POS_StockRequestLines;
using POS_Common.Models.Stock.POS_StockRequests;
using POS_Common.Models.Stock.POS_StockRequestReviewers;
using POS_Common.Models.Stock.POS_StockTransfers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.Stock
{
    public class Stock_Custom_Service: Stock_Custom_SP_Service
    {
        #region Methods

        #region Purchase Orders

        public static async Task<List<PurchaseOrder>> PurchaseOrders_Select_All_PurchaseOrders(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await PurchaseOrders_Select_All_PurchaseOrders(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PurchaseOrder>> PurchaseOrders_Select_All_PurchaseOrders(PurchaseOrder item, SqlConnection sqlConn)
        {
            try
            {
                List<PurchaseOrder> resultItem = new List<PurchaseOrder>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "purchaseorders_select_all_purchaseorders",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_DebtorID", Value = item.FK_DebtorID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PurchaseOrder>(Stock_Translator.Translate_PurchaseOrder_PurchaseOrder));
                        Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrder> PurchaseOrder_Select_Single_Number(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await PurchaseOrder_Select_Single_Number(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrder> PurchaseOrder_Select_Single_Number(PurchaseOrder item, SqlConnection sqlConn)
        {
            try
            {
                PurchaseOrder resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "PurchaseOrder_select_single_number",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@OrderNumber", Value = item.OrderNumber }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PurchaseOrder>(Stock_Translator.Translate_PurchaseOrder);
                        Log.Information("PurchaseOrder found: PurchaseOrderID={PurchaseOrderID}, OrderNumber={OrderNumber}, FK_SupplierID={FK_SupplierID}, FK_CostCenterID={FK_CostCenterID}, FK_OrderStatusID={FK_OrderStatusID}, CreatedBy={CreatedBy}, Notes={Notes}, DateOrdered={DateOrdered}, DateUpdated={DateUpdated}", resultItem.PurchaseOrderID, resultItem.OrderNumber, resultItem.FK_SupplierID, resultItem.FK_CostCenterID, resultItem.FK_OrderStatusID, resultItem.CreatedBy, resultItem.Notes, resultItem.DateOrdered, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrder> PurchaseOrder_Select_Single_ID(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await PurchaseOrder_Select_Single_ID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrder> PurchaseOrder_Select_Single_ID(PurchaseOrder item, SqlConnection sqlConn)
        {
            try
            {
                PurchaseOrder resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "PurchaseOrder_select_single_ID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ProductID", Value = item.FK_ProductID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PurchaseOrderID", Value = item.PurchaseOrderID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PurchaseOrder>(Stock_Translator.Translate_PurchaseOrder_Supplier);
                        Log.Information("PurchaseOrder found");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Purchase Order Lines

        public static async Task<List<PurchaseOrderLine>> PurchaseOrderLines_Select_All_PurchaseOrderLines(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await PurchaseOrderLines_Select_All_PurchaseOrderLines(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PurchaseOrderLine>> PurchaseOrderLines_Select_All_PurchaseOrderLines(PurchaseOrderLine item, SqlConnection sqlConn)
        {
            try
            {
                List<PurchaseOrderLine> resultItem = new List<PurchaseOrderLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "PurchaseOrderLines_select_all_PurchaseOrderLines",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@PurchaseOrderID", Value = item.FK_PurchaseOrderID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PurchaseOrderLine>(Stock_Translator.Translate_PurchaseOrderLine_PurchaseOrderLine));
                        Log.Information("Purchase Order Line records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order Line records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Submitted Purchase Orders

        public static async Task<List<PurchaseOrder>> PurchaseOrders_Select_All_SubmittedPurchaseOrders(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await PurchaseOrders_Select_All_SubmittedPurchaseOrders(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PurchaseOrder>> PurchaseOrders_Select_All_SubmittedPurchaseOrders(PurchaseOrder item, SqlConnection sqlConn)
        {
            try
            {
                List<PurchaseOrder> resultItem = new List<PurchaseOrder>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "purchaseOrders_select_all_submittedPurchaseOrders"))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PurchaseOrder>(Stock_Translator.Translate_PurchaseOrder_PurchaseOrder));
                        Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Submitted Purchase Order Lines

        public static async Task<List<PurchaseOrderLine>> PurchaseOrderLines_Select_All_SubmittedPurchaseOrderLines(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await PurchaseOrderLines_Select_All_SubmittedPurchaseOrderLines(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PurchaseOrderLine>> PurchaseOrderLines_Select_All_SubmittedPurchaseOrderLines(PurchaseOrderLine item, SqlConnection sqlConn)
        {
            try
            {
                List<PurchaseOrderLine> resultItem = new List<PurchaseOrderLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "SubmittedPurchaseOrderLines_select_all_SubmittedPurchaseOrderLines",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@PurchaseOrderID", Value = item.FK_PurchaseOrderID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PurchaseOrderLine>(Stock_Translator.Translate_SubmittedPurchaseOrderLine_SubmittedPurchaseOrderLine));
                        Log.Information("Purchase Order Line records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order Line records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Stock Requests

        public static async Task<List<StockRequest>> StockRequests_Select_All_StockRequests(StockRequest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StockRequests_Select_All_StockRequests(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockRequest>> StockRequests_Select_All_StockRequests(StockRequest item, SqlConnection sqlConn)
        {
            try
            {
                List<StockRequest> resultItem = new List<StockRequest>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "stockRequest_select_all_stockRequest",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToDebtorID", Value = (object)item.FK_ToDebtorID ?? DBNull.Value },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_FromDebtorID", Value = (object)item.FK_FromDebtorID ?? DBNull.Value },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderStatusID", Value = (object)item.FK_OrderStatusID ?? DBNull.Value }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockRequest>(Stock_Translator.Translate_POS_StockRequest_StockRequest));
                        Log.Information("Stock Request records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Stock Request records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockRequest> StockRequest_Select_Single_Number(StockRequest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StockRequest_Select_Single_Number(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockRequest> StockRequest_Select_Single_Number(StockRequest item, SqlConnection sqlConn)
        {
            try
            {
                StockRequest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "stockRequest_select_single_number",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RefNumber", Value = item.RefNumber }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockRequest>(Stock_Translator.Translate_StockRequest);
                        Log.Information("POS_StockRequest found: POS_StockRequestID={POS_StockRequestID}, RefNumber={RefNumber}, FK_FromDebtorID={FK_FromDebtorID}, FK_ToDebtorID={FK_ToDebtorID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, ManagerNotes={ManagerNotes}, Notes={Notes}, DateOrdered={DateOrdered}, DateUpdated={DateUpdated}", resultItem.StockRequestID, resultItem.RefNumber, resultItem.FK_FromDebtorID, resultItem.FK_ToDebtorID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.ManagerNotes, resultItem.Notes, resultItem.DateOrdered, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Stock Request found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Stock Request Lines

        public static async Task<List<StockRequestLine>> StockRequestLines_Select_All_StockRequestLines(StockRequestLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StockRequestLines_Select_All_StockRequestLines(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockRequestLine>> StockRequestLines_Select_All_StockRequestLines(StockRequestLine item, SqlConnection sqlConn)
        {
            try
            {
                List<StockRequestLine> resultItem = new List<StockRequestLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "stockRequestLines_select_all_stockRequestLines",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_StockRequestID", Value = item.FK_StockRequestID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockRequestLine>(Stock_Translator.Translate_StockRequestLine_StockRequestLine));
                        Log.Information("Stock Request Line records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Stock Request Line records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task StockRequestLines_Delete_By_Stock_Request(int stockRequestID, SqlConnection sqlConn)
        {
            try
            {
                await SqlClient.ExecuteNonQueryStoredProcedureAsync(
                    sqlConn,
                    "stockRequestLines_delete_by_stock_request",
                    new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_StockRequestID", Value = stockRequestID });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                throw;
            }
        }
        #endregion

        #region Stock Transfers

        public static async Task<List<StockTransfer>> StockTransfers_Select_All_StockTransfers(StockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StockTransfers_Select_All_StockTransfers(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockTransfer>> StockTransfers_Select_All_StockTransfers(StockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                List<StockTransfer> resultItem = new List<StockTransfer>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "stockTransfer_select_all_stockTransfer",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@DebtorID", Value = item.FK_ToDebtorID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockTransfer>(Stock_Translator.Translate_StockTransfer_StockTransfer));
                        Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Debtor Products

        public static async Task<List<DebtorProduct>> DebtorProducts_Select_All_DebtorProducts(DebtorProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorProducts_Select_All_DebtorProducts(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorProduct>> DebtorProducts_Select_All_DebtorProducts(DebtorProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorProduct> resultItem = new List<DebtorProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorProducts_select_all_debtorProducts",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorProduct>(Stock_Translator.Translate_DebtorProduct_DebtorProduct));
                        Log.Information("Debtor Product records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Debtor Product records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Select_FK_DebtorProductID(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Select_FK_DebtorProductID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Select_FK_DebtorProductID(DebtorProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProductPriceHistory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "POS_DebtorProductPriceHistory_select_FK_DebtorProductID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorProductID", Value = item.FK_DebtorProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProductPriceHistory>(Stock_Translator.Translate_DebtorProductPriceHistory);
                        Log.Information("DebtorProductPriceHistory found: DebtorProductPriceHistoryID={DebtorProductPriceHistoryID}, FK_DebtorProductID={FK_DebtorProductID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorProductPriceHistoryID, resultItem.FK_DebtorProductID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.ValidFrom, resultItem.ValidTo, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorProductPriceHistory found with the given DebtorProductPriceHistoryID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Debtor Product Prices

        public static async Task<List<DebtorProductPrice>> DebtorProductPrices_Select_All_DebtorProducts(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorProductPrices_Select_All_DebtorProducts(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorProductPrice>> DebtorProductPrices_Select_All_DebtorProducts(DebtorProductPrice item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorProductPrice> resultItem = new List<DebtorProductPrice>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorProductPrices_select_all_debtorProducts",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_DebtorProductID", Value = item.FK_DebtorProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorProductPrice>(Stock_Translator.Translate_DebtorProductPrice));
                        Log.Information("Debtor Product records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Debtor Product records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Cost Center Products

        public static async Task<List<CostCenterProduct>> CostCenterProducts_Select_All_CostCenterProducts(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CostCenterProducts_Select_All_CostCenterProducts(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CostCenterProduct>> CostCenterProducts_Select_All_CostCenterProducts(CostCenterProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<CostCenterProduct> resultItem = new List<CostCenterProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "costCenterProducts_select_all_costCenterProducts",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CostCenterProduct>(Stock_Translator.Translate_CostCenterProduct_CostCenterProduct));
                        Log.Information("Cost Center Product records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Cost Center Product records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterProductPriceHistory> CostCenterProductPriceHistory_Select_FK_CostCenterProductID(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CostCenterProductPriceHistory_Select_FK_CostCenterProductID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterProductPriceHistory> CostCenterProductPriceHistory_Select_FK_CostCenterProductID(CostCenterProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterProductPriceHistory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "CostCenterProductPriceHistory_select_FK_CostCenterProductID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterProductID", Value = item.FK_CostCenterProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterProductPriceHistory>(Stock_Translator.Translate_CostCenterProductPriceHistory);
                        Log.Information("DebtorProductPriceHistory found: CostcenterProductPriceHistoryID={CostcenterProductPriceHistoryID}, FK_CostCenterProductID={FK_CostCenterProductID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostcenterProductPriceHistoryID, resultItem.FK_CostCenterProductID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.ValidFrom, resultItem.ValidTo, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorProductPriceHistory found with the given DebtorProductPriceHistoryID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #region Stock Request Reviewers

        public static async Task<List<StockRequestReviewer>> POS_StockRequestReviewers_Select_By_Debtor_Role(int toDebtorID, string role, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await POS_StockRequestReviewers_Select_By_Debtor_Role(toDebtorID, role, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockRequestReviewer>> POS_StockRequestReviewers_Select_By_Debtor_Role(int toDebtorID, string role, SqlConnection sqlConn)
        {
            try
            {
                var resultItem = new List<StockRequestReviewer>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "POS_StockRequestReviewers_select_by_debtor_role",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToDebtorID", Value = toDebtorID },
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Role", Value = role }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockRequestReviewer>(Stock_Translator.Translate_StockRequestReviewer));
                    }
                    return resultItem;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockRequestReviewer>> POS_StockRequestReviewers_Select_By_Role(string role, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await POS_StockRequestReviewers_Select_By_Role(role, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockRequestReviewer>> POS_StockRequestReviewers_Select_By_Role(string role, SqlConnection sqlConn)
        {
            try
            {
                var resultItem = new List<StockRequestReviewer>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "POS_StockRequestReviewers_select_by_role",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Role", Value = role }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockRequestReviewer>(Stock_Translator.Translate_StockRequestReviewer));
                    }
                    return resultItem;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion

        #endregion
    }
}
