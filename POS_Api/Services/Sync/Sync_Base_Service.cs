using Microsoft.Data.SqlClient;
using Serilog;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TMIS_Common.Sql;
using POS_Api.Translators;

using POS_Common.Models.Sync.POS_SlipPrinters;
using POS_Common.Models.Sync.POS_InvoiceHeaders;
using POS_Common.Models.Sync.POS_InvoiceLines;
using POS_Common.Models.Sync.POS_RequestFromServer;
using POS_Common.Models.Sync.POS_InvoiceTabs;
using POS_Common.Models.Sync.POS_AccountGuests;
using POS_Common.Models.Sync.POS_Accounts;
using POS_Common.Models.Sync.POS_Arrivals;
using POS_Common.Models.Sync.POS_CashUpHeaders;
using POS_Common.Models.Sync.POS_CashUpLines;
using POS_Common.Models.Sync.POS_InvoicePayments;
using POS_Common.Models.Sync.POS_TabLineCombinations;
using POS_Common.Models.Sync.POS_TabLineExtras;
using POS_Common.Models.Sync.POS_TabLineGuests;
using POS_Common.Models.Sync.POS_TabLinePreparationMethods;
using POS_Common.Models.Sync.POS_TabLines;
using POS_Common.Models.Sync.POS_TablineSubstitutes;
using POS_Common.Models.Sync.POS_Tabs;
using POS_Common.Models.Sync.POS_VoidLogs;
using POS_Common.Models.Sync.SiteSyncStatus;

namespace POS_Api.Services.Sync
{
    public abstract class Sync_Base_Service
    {
        #region POS_SlipPrinters

        public static async Task<SlipPrinter> POS_SlipPrinters_Select_Single_Transaction(SlipPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipPrinters_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipPrinter> POS_SlipPrinters_Select_Single(SlipPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipPrinters_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipPrinter> POS_SlipPrinters_Select_Single(SlipPrinter item, SqlConnection sqlConn)
        {
            try
            {
                SlipPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SlipPrinters_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SlipPrinterID", Value = item.SlipPrinterID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SlipPrinter>(Sync_Translator.Translate_SlipPrinter);
                        Log.Information("SlipPrinter found: SlipPrinterID={SlipPrinterID}, FK_LocationID={FK_LocationID}, CostCenterID={CostCenterID}, Name={Name}, Model={Model}, IpAddress={IpAddress}, Port={Port}, IsDefault={IsDefault}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, AutoCut={AutoCut}", resultItem.SlipPrinterID, resultItem.FK_LocationID, resultItem.CostCenterID, resultItem.Name, resultItem.Model, resultItem.IpAddress, resultItem.Port, resultItem.IsDefault, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.AutoCut);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No SlipPrinter found with the given SlipPrinterID.");
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

        public static async Task<SlipPrinter> POS_SlipPrinters_Insert_Transaction(SlipPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipPrinters_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipPrinter> POS_SlipPrinters_Insert(SlipPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipPrinters_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipPrinter> POS_SlipPrinters_Insert(SlipPrinter item, SqlConnection sqlConn)
        {
            try
            {
                SlipPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SlipPrinters_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterID", Value = item.CostCenterID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Model", Value = item.Model }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@IpAddress", Value = item.IpAddress }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@Port", Value = item.Port }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDefault", Value = item.IsDefault }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@AutoCut", Value = item.AutoCut }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SlipPrinter>(Sync_Translator.Translate_SlipPrinter);
                        Log.Information("SlipPrinter found: SlipPrinterID={SlipPrinterID}, FK_LocationID={FK_LocationID}, CostCenterID={CostCenterID}, Name={Name}, Model={Model}, IpAddress={IpAddress}, Port={Port}, IsDefault={IsDefault}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, AutoCut={AutoCut}", resultItem.SlipPrinterID, resultItem.FK_LocationID, resultItem.CostCenterID, resultItem.Name, resultItem.Model, resultItem.IpAddress, resultItem.Port, resultItem.IsDefault, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.AutoCut);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("SlipPrinter failed to create.");
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

        public static async Task<List<SlipPrinter>> POS_SlipPrinters_Select_All_Transaction(SlipPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipPrinters_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<SlipPrinter>> POS_SlipPrinters_Select_All(SlipPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipPrinters_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<SlipPrinter>> POS_SlipPrinters_Select_All(SlipPrinter item, SqlConnection sqlConn)
        {
            try
            {
                List<SlipPrinter> resultItem = new List<SlipPrinter>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SlipPrinters_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<SlipPrinter>(Sync_Translator.Translate_SlipPrinter));
                        Log.Information("SlipPrinter records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No SlipPrinter records found.");
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

        public static async Task<SlipPrinter> POS_SlipPrinters_Update_Transaction(SlipPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipPrinters_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipPrinter> POS_SlipPrinters_Update(SlipPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipPrinters_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipPrinter> POS_SlipPrinters_Update(SlipPrinter item, SqlConnection sqlConn)
        {
            try
            {
                SlipPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SlipPrinters_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SlipPrinterID", Value = item.SlipPrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterID", Value = item.CostCenterID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Model", Value = item.Model }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@IpAddress", Value = item.IpAddress }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@Port", Value = item.Port }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDefault", Value = item.IsDefault }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@AutoCut", Value = item.AutoCut }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SlipPrinter>(Sync_Translator.Translate_SlipPrinter);
                        Log.Information("SlipPrinter found: SlipPrinterID={SlipPrinterID}, FK_LocationID={FK_LocationID}, CostCenterID={CostCenterID}, Name={Name}, Model={Model}, IpAddress={IpAddress}, Port={Port}, IsDefault={IsDefault}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, AutoCut={AutoCut}", resultItem.SlipPrinterID, resultItem.FK_LocationID, resultItem.CostCenterID, resultItem.Name, resultItem.Model, resultItem.IpAddress, resultItem.Port, resultItem.IsDefault, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.AutoCut);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("SlipPrinter failed to update.");
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

        #region POS_InvoiceHeaders

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Select_Single_Transaction(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceHeaders_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Select_Single(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceHeaders_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Select_Single(InvoiceHeader item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceHeaders_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceHeaderID", Value = item.InvoiceHeaderID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceHeader>(Sync_Translator.Translate_InvoiceHeader);
                        Log.Information("InvoiceHeader found: InvoiceHeaderID={InvoiceHeaderID}, FK_LocationID={FK_LocationID}, FK_AccountID={FK_AccountID}, InvoiceNo={InvoiceNo}, PartyName={PartyName}, BookingReference={BookingReference}, DiscountTotal={DiscountTotal}, GratuityTotal={GratuityTotal}, ExclTotal={ExclTotal}, VatTotal={VatTotal}, InclTotal={InclTotal}, DateCreated={DateCreated}, DatePaid={DatePaid}, FK_CurrencyID={FK_CurrencyID}, IsPaid={IsPaid}, AmountPaid={AmountPaid}, AmountDue={AmountDue}, IsVoided={IsVoided}, VoidReason={VoidReason}, VoidedDate={VoidedDate}, VoidedBy={VoidedBy}", resultItem.InvoiceHeaderID, resultItem.FK_LocationID, resultItem.FK_AccountID, resultItem.InvoiceNo, resultItem.PartyName, resultItem.BookingReference, resultItem.DiscountTotal, resultItem.GratuityTotal, resultItem.ExclTotal, resultItem.VatTotal, resultItem.InclTotal, resultItem.DateCreated, resultItem.DatePaid, resultItem.FK_CurrencyID, resultItem.IsPaid, resultItem.AmountPaid, resultItem.AmountDue, resultItem.IsVoided, resultItem.VoidReason, resultItem.VoidedDate, resultItem.VoidedBy);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InvoiceHeader found with the given InvoiceHeaderID.");
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

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Insert_Transaction(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceHeaders_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Insert(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceHeaders_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Insert(InvoiceHeader item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceHeaders_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceHeaderID", Value = item.InvoiceHeaderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_AccountID", Value = item.FK_AccountID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@InvoiceNo", Value = item.InvoiceNo }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PartyName", Value = item.PartyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BookingReference", Value = item.BookingReference }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@DiscountTotal", Value = item.DiscountTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@GratuityTotal", Value = item.GratuityTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExclTotal", Value = item.ExclTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@VatTotal", Value = item.VatTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@InclTotal", Value = item.InclTotal }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DatePaid", Value = item.DatePaid }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPaid", Value = item.IsPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@AmountPaid", Value = item.AmountPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@AmountDue", Value = item.AmountDue }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVoided", Value = item.IsVoided }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidReason", Value = item.VoidReason }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@VoidedDate", Value = item.VoidedDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidedBy", Value = item.VoidedBy }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceHeader>(Sync_Translator.Translate_InvoiceHeader);
                        Log.Information("InvoiceHeader found: InvoiceHeaderID={InvoiceHeaderID}, FK_LocationID={FK_LocationID}, FK_AccountID={FK_AccountID}, InvoiceNo={InvoiceNo}, PartyName={PartyName}, BookingReference={BookingReference}, DiscountTotal={DiscountTotal}, GratuityTotal={GratuityTotal}, ExclTotal={ExclTotal}, VatTotal={VatTotal}, InclTotal={InclTotal}, DateCreated={DateCreated}, DatePaid={DatePaid}, FK_CurrencyID={FK_CurrencyID}, IsPaid={IsPaid}, AmountPaid={AmountPaid}, AmountDue={AmountDue}, IsVoided={IsVoided}, VoidReason={VoidReason}, VoidedDate={VoidedDate}, VoidedBy={VoidedBy}", resultItem.InvoiceHeaderID, resultItem.FK_LocationID, resultItem.FK_AccountID, resultItem.InvoiceNo, resultItem.PartyName, resultItem.BookingReference, resultItem.DiscountTotal, resultItem.GratuityTotal, resultItem.ExclTotal, resultItem.VatTotal, resultItem.InclTotal, resultItem.DateCreated, resultItem.DatePaid, resultItem.FK_CurrencyID, resultItem.IsPaid, resultItem.AmountPaid, resultItem.AmountDue, resultItem.IsVoided, resultItem.VoidReason, resultItem.VoidedDate, resultItem.VoidedBy);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceHeader failed to create.");
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

        public static async Task<List<InvoiceHeader>> POS_InvoiceHeaders_Select_All_Transaction(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceHeaders_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoiceHeader>> POS_InvoiceHeaders_Select_All(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceHeaders_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoiceHeader>> POS_InvoiceHeaders_Select_All(InvoiceHeader item, SqlConnection sqlConn)
        {
            try
            {
                List<InvoiceHeader> resultItem = new List<InvoiceHeader>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceHeaders_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<InvoiceHeader>(Sync_Translator.Translate_InvoiceHeader));
                        Log.Information("InvoiceHeader records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InvoiceHeader records found.");
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

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Update_Transaction(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceHeaders_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Update(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceHeaders_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceHeader> POS_InvoiceHeaders_Update(InvoiceHeader item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceHeaders_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceHeaderID", Value = item.InvoiceHeaderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_AccountID", Value = item.FK_AccountID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@InvoiceNo", Value = item.InvoiceNo }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PartyName", Value = item.PartyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BookingReference", Value = item.BookingReference }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@DiscountTotal", Value = item.DiscountTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@GratuityTotal", Value = item.GratuityTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExclTotal", Value = item.ExclTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@VatTotal", Value = item.VatTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@InclTotal", Value = item.InclTotal }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DatePaid", Value = item.DatePaid }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPaid", Value = item.IsPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@AmountPaid", Value = item.AmountPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@AmountDue", Value = item.AmountDue }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVoided", Value = item.IsVoided }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidReason", Value = item.VoidReason }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@VoidedDate", Value = item.VoidedDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidedBy", Value = item.VoidedBy }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceHeader>(Sync_Translator.Translate_InvoiceHeader);
                        Log.Information("InvoiceHeader found: InvoiceHeaderID={InvoiceHeaderID}, FK_LocationID={FK_LocationID}, FK_AccountID={FK_AccountID}, InvoiceNo={InvoiceNo}, PartyName={PartyName}, BookingReference={BookingReference}, DiscountTotal={DiscountTotal}, GratuityTotal={GratuityTotal}, ExclTotal={ExclTotal}, VatTotal={VatTotal}, InclTotal={InclTotal}, DateCreated={DateCreated}, DatePaid={DatePaid}, FK_CurrencyID={FK_CurrencyID}, IsPaid={IsPaid}, AmountPaid={AmountPaid}, AmountDue={AmountDue}, IsVoided={IsVoided}, VoidReason={VoidReason}, VoidedDate={VoidedDate}, VoidedBy={VoidedBy}", resultItem.InvoiceHeaderID, resultItem.FK_LocationID, resultItem.FK_AccountID, resultItem.InvoiceNo, resultItem.PartyName, resultItem.BookingReference, resultItem.DiscountTotal, resultItem.GratuityTotal, resultItem.ExclTotal, resultItem.VatTotal, resultItem.InclTotal, resultItem.DateCreated, resultItem.DatePaid, resultItem.FK_CurrencyID, resultItem.IsPaid, resultItem.AmountPaid, resultItem.AmountDue, resultItem.IsVoided, resultItem.VoidReason, resultItem.VoidedDate, resultItem.VoidedBy);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceHeader failed to update.");
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

        #region POS_InvoiceLines

        public static async Task<InvoiceLine> POS_InvoiceLines_Select_Single_Transaction(InvoiceLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceLines_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceLine> POS_InvoiceLines_Select_Single(InvoiceLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceLines_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceLine> POS_InvoiceLines_Select_Single(InvoiceLine item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceLines_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceLineID", Value = item.InvoiceLineID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceLine>(Sync_Translator.Translate_InvoiceLine);
                        Log.Information("InvoiceLine found: InvoiceLineID={InvoiceLineID}, FK_InvoiceTabID={FK_InvoiceTabID}, FK_ProductID={FK_ProductID}, Product={Product}, Quantity={Quantity}, LineDiscount={LineDiscount}, LineTotalExcl={LineTotalExcl}, LineTotalVat={LineTotalVat}, LineTotalIncl={LineTotalIncl}, Guests={Guests}", resultItem.InvoiceLineID, resultItem.FK_InvoiceTabID, resultItem.FK_ProductID, resultItem.Product, resultItem.Quantity, resultItem.LineDiscount, resultItem.LineTotalExcl, resultItem.LineTotalVat, resultItem.LineTotalIncl, resultItem.Guests);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InvoiceLine found with the given InvoiceLineID.");
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

        public static async Task<InvoiceLine> POS_InvoiceLines_Insert_Transaction(InvoiceLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceLines_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceLine> POS_InvoiceLines_Insert(InvoiceLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceLines_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceLine> POS_InvoiceLines_Insert(InvoiceLine item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceLines_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceLineID", Value = item.InvoiceLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceTabID", Value = item.FK_InvoiceTabID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineDiscount", Value = item.LineDiscount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalExcl", Value = item.LineTotalExcl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalVat", Value = item.LineTotalVat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalIncl", Value = item.LineTotalIncl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Guests", Value = item.Guests }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceLine>(Sync_Translator.Translate_InvoiceLine);
                        Log.Information("InvoiceLine found: InvoiceLineID={InvoiceLineID}, FK_InvoiceTabID={FK_InvoiceTabID}, FK_ProductID={FK_ProductID}, Product={Product}, Quantity={Quantity}, LineDiscount={LineDiscount}, LineTotalExcl={LineTotalExcl}, LineTotalVat={LineTotalVat}, LineTotalIncl={LineTotalIncl}, Guests={Guests}", resultItem.InvoiceLineID, resultItem.FK_InvoiceTabID, resultItem.FK_ProductID, resultItem.Product, resultItem.Quantity, resultItem.LineDiscount, resultItem.LineTotalExcl, resultItem.LineTotalVat, resultItem.LineTotalIncl, resultItem.Guests);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceLine failed to create.");
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

        public static async Task<List<InvoiceLine>> POS_InvoiceLines_Select_All_Transaction(InvoiceLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceLines_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoiceLine>> POS_InvoiceLines_Select_All(InvoiceLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceLines_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoiceLine>> POS_InvoiceLines_Select_All(InvoiceLine item, SqlConnection sqlConn)
        {
            try
            {
                List<InvoiceLine> resultItem = new List<InvoiceLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceLines_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<InvoiceLine>(Sync_Translator.Translate_InvoiceLine));
                        Log.Information("InvoiceLine records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InvoiceLine records found.");
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

        public static async Task<InvoiceLine> POS_InvoiceLines_Update_Transaction(InvoiceLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceLines_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceLine> POS_InvoiceLines_Update(InvoiceLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceLines_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceLine> POS_InvoiceLines_Update(InvoiceLine item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceLines_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceLineID", Value = item.InvoiceLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceTabID", Value = item.FK_InvoiceTabID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineDiscount", Value = item.LineDiscount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalExcl", Value = item.LineTotalExcl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalVat", Value = item.LineTotalVat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalIncl", Value = item.LineTotalIncl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Guests", Value = item.Guests }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceLine>(Sync_Translator.Translate_InvoiceLine);
                        Log.Information("InvoiceLine found: InvoiceLineID={InvoiceLineID}, FK_InvoiceTabID={FK_InvoiceTabID}, FK_ProductID={FK_ProductID}, Product={Product}, Quantity={Quantity}, LineDiscount={LineDiscount}, LineTotalExcl={LineTotalExcl}, LineTotalVat={LineTotalVat}, LineTotalIncl={LineTotalIncl}, Guests={Guests}", resultItem.InvoiceLineID, resultItem.FK_InvoiceTabID, resultItem.FK_ProductID, resultItem.Product, resultItem.Quantity, resultItem.LineDiscount, resultItem.LineTotalExcl, resultItem.LineTotalVat, resultItem.LineTotalIncl, resultItem.Guests);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceLine failed to update.");
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

        #region POS_RequestFromServer

        public static async Task<RequestFromServer> POS_RequestFromServer_Select_Single_Transaction(RequestFromServer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_RequestFromServer_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<RequestFromServer> POS_RequestFromServer_Select_Single(RequestFromServer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_RequestFromServer_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<RequestFromServer> POS_RequestFromServer_Select_Single(RequestFromServer item, SqlConnection sqlConn)
        {
            try
            {
                RequestFromServer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_RequestFromServer_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@RequestFromServerID", Value = item.RequestFromServerID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<RequestFromServer>(Sync_Translator.Translate_RequestFromServer);
                        Log.Information("RequestFromServer found: RequestFromServerID={RequestFromServerID}, Type={Type}, LastRequestDate={LastRequestDate}, CallSequence={CallSequence}, SyncFrequency={SyncFrequency}, IsActive={IsActive}, ApiUrl={ApiUrl}", resultItem.RequestFromServerID, resultItem.Type, resultItem.LastRequestDate, resultItem.CallSequence, resultItem.SyncFrequency, resultItem.IsActive, resultItem.ApiUrl);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No RequestFromServer found with the given RequestFromServerID.");
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

        public static async Task<RequestFromServer> POS_RequestFromServer_Insert_Transaction(RequestFromServer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_RequestFromServer_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<RequestFromServer> POS_RequestFromServer_Insert(RequestFromServer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_RequestFromServer_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<RequestFromServer> POS_RequestFromServer_Insert(RequestFromServer item, SqlConnection sqlConn)
        {
            try
            {
                RequestFromServer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_RequestFromServer_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Type", Value = item.Type }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@LastRequestDate", Value = item.LastRequestDate }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CallSequence", Value = item.CallSequence }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SyncFrequency", Value = item.SyncFrequency }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ApiUrl", Value = item.ApiUrl }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<RequestFromServer>(Sync_Translator.Translate_RequestFromServer);
                        Log.Information("RequestFromServer found: RequestFromServerID={RequestFromServerID}, Type={Type}, LastRequestDate={LastRequestDate}, CallSequence={CallSequence}, SyncFrequency={SyncFrequency}, IsActive={IsActive}, ApiUrl={ApiUrl}", resultItem.RequestFromServerID, resultItem.Type, resultItem.LastRequestDate, resultItem.CallSequence, resultItem.SyncFrequency, resultItem.IsActive, resultItem.ApiUrl);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("RequestFromServer failed to create.");
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

        public static async Task<List<RequestFromServer>> POS_RequestFromServer_Select_All_Transaction(RequestFromServer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_RequestFromServer_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<RequestFromServer>> POS_RequestFromServer_Select_All(RequestFromServer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_RequestFromServer_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<RequestFromServer>> POS_RequestFromServer_Select_All(RequestFromServer item, SqlConnection sqlConn)
        {
            try
            {
                List<RequestFromServer> resultItem = new List<RequestFromServer>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_RequestFromServer_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<RequestFromServer>(Sync_Translator.Translate_RequestFromServer));
                        Log.Information("RequestFromServer records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No RequestFromServer records found.");
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

        public static async Task<RequestFromServer> POS_RequestFromServer_Update_Transaction(RequestFromServer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_RequestFromServer_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<RequestFromServer> POS_RequestFromServer_Update(RequestFromServer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_RequestFromServer_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<RequestFromServer> POS_RequestFromServer_Update(RequestFromServer item, SqlConnection sqlConn)
        {
            try
            {
                RequestFromServer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_RequestFromServer_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@RequestFromServerID", Value = item.RequestFromServerID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Type", Value = item.Type }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@LastRequestDate", Value = item.LastRequestDate }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CallSequence", Value = item.CallSequence }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SyncFrequency", Value = item.SyncFrequency }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ApiUrl", Value = item.ApiUrl }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<RequestFromServer>(Sync_Translator.Translate_RequestFromServer);
                        Log.Information("RequestFromServer found: RequestFromServerID={RequestFromServerID}, Type={Type}, LastRequestDate={LastRequestDate}, CallSequence={CallSequence}, SyncFrequency={SyncFrequency}, IsActive={IsActive}, ApiUrl={ApiUrl}", resultItem.RequestFromServerID, resultItem.Type, resultItem.LastRequestDate, resultItem.CallSequence, resultItem.SyncFrequency, resultItem.IsActive, resultItem.ApiUrl);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("RequestFromServer failed to update.");
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

        #region POS_InvoiceTabs

        public static async Task<InvoiceTab> POS_InvoiceTabs_Select_Single_Transaction(InvoiceTab item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceTabs_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceTab> POS_InvoiceTabs_Select_Single(InvoiceTab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceTabs_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceTab> POS_InvoiceTabs_Select_Single(InvoiceTab item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceTab resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceTabs_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceTabID", Value = item.InvoiceTabID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceTab>(Sync_Translator.Translate_InvoiceTab);
                        Log.Information("InvoiceTab found: InvoiceTabID={InvoiceTabID}, FK_InvoiceHeaderID={FK_InvoiceHeaderID}, FK_TabID={FK_TabID}, TabGratuity={TabGratuity}, TabDiscount={TabDiscount}, TabTotalExcl={TabTotalExcl}, TabTotalVat={TabTotalVat}, TabTotalIncl={TabTotalIncl}, TabDateOpened={TabDateOpened}, TabDateClosed={TabDateClosed}", resultItem.InvoiceTabID, resultItem.FK_InvoiceHeaderID, resultItem.FK_TabID, resultItem.TabGratuity, resultItem.TabDiscount, resultItem.TabTotalExcl, resultItem.TabTotalVat, resultItem.TabTotalIncl, resultItem.TabDateOpened, resultItem.TabDateClosed);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InvoiceTab found with the given InvoiceTabID.");
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

        public static async Task<InvoiceTab> POS_InvoiceTabs_Insert_Transaction(InvoiceTab item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceTabs_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceTab> POS_InvoiceTabs_Insert(InvoiceTab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceTabs_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceTab> POS_InvoiceTabs_Insert(InvoiceTab item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceTab resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceTabs_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceTabID", Value = item.InvoiceTabID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceHeaderID", Value = item.FK_InvoiceHeaderID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabID", Value = item.FK_TabID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabGratuity", Value = item.TabGratuity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabDiscount", Value = item.TabDiscount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalExcl", Value = item.TabTotalExcl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalVat", Value = item.TabTotalVat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalIncl", Value = item.TabTotalIncl }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@TabDateOpened", Value = item.TabDateOpened }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@TabDateClosed", Value = item.TabDateClosed }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceTab>(Sync_Translator.Translate_InvoiceTab);
                        Log.Information("InvoiceTab found: InvoiceTabID={InvoiceTabID}, FK_InvoiceHeaderID={FK_InvoiceHeaderID}, FK_TabID={FK_TabID}, TabGratuity={TabGratuity}, TabDiscount={TabDiscount}, TabTotalExcl={TabTotalExcl}, TabTotalVat={TabTotalVat}, TabTotalIncl={TabTotalIncl}, TabDateOpened={TabDateOpened}, TabDateClosed={TabDateClosed}", resultItem.InvoiceTabID, resultItem.FK_InvoiceHeaderID, resultItem.FK_TabID, resultItem.TabGratuity, resultItem.TabDiscount, resultItem.TabTotalExcl, resultItem.TabTotalVat, resultItem.TabTotalIncl, resultItem.TabDateOpened, resultItem.TabDateClosed);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceTab failed to create.");
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

        public static async Task<List<InvoiceTab>> POS_InvoiceTabs_Select_All_Transaction(InvoiceTab item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceTabs_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoiceTab>> POS_InvoiceTabs_Select_All(InvoiceTab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceTabs_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoiceTab>> POS_InvoiceTabs_Select_All(InvoiceTab item, SqlConnection sqlConn)
        {
            try
            {
                List<InvoiceTab> resultItem = new List<InvoiceTab>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceTabs_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<InvoiceTab>(Sync_Translator.Translate_InvoiceTab));
                        Log.Information("InvoiceTab records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InvoiceTab records found.");
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

        public static async Task<InvoiceTab> POS_InvoiceTabs_Update_Transaction(InvoiceTab item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceTabs_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceTab> POS_InvoiceTabs_Update(InvoiceTab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoiceTabs_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceTab> POS_InvoiceTabs_Update(InvoiceTab item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceTab resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoiceTabs_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceTabID", Value = item.InvoiceTabID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceHeaderID", Value = item.FK_InvoiceHeaderID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabID", Value = item.FK_TabID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabGratuity", Value = item.TabGratuity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabDiscount", Value = item.TabDiscount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalExcl", Value = item.TabTotalExcl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalVat", Value = item.TabTotalVat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalIncl", Value = item.TabTotalIncl }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@TabDateOpened", Value = item.TabDateOpened }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@TabDateClosed", Value = item.TabDateClosed }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceTab>(Sync_Translator.Translate_InvoiceTab);
                        Log.Information("InvoiceTab found: InvoiceTabID={InvoiceTabID}, FK_InvoiceHeaderID={FK_InvoiceHeaderID}, FK_TabID={FK_TabID}, TabGratuity={TabGratuity}, TabDiscount={TabDiscount}, TabTotalExcl={TabTotalExcl}, TabTotalVat={TabTotalVat}, TabTotalIncl={TabTotalIncl}, TabDateOpened={TabDateOpened}, TabDateClosed={TabDateClosed}", resultItem.InvoiceTabID, resultItem.FK_InvoiceHeaderID, resultItem.FK_TabID, resultItem.TabGratuity, resultItem.TabDiscount, resultItem.TabTotalExcl, resultItem.TabTotalVat, resultItem.TabTotalIncl, resultItem.TabDateOpened, resultItem.TabDateClosed);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceTab failed to update.");
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

        #region POS_AccountGuests

        public static async Task<AccountGuest> POS_AccountGuests_Select_Single_Transaction(AccountGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_AccountGuests_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AccountGuest> POS_AccountGuests_Select_Single(AccountGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_AccountGuests_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AccountGuest> POS_AccountGuests_Select_Single(AccountGuest item, SqlConnection sqlConn)
        {
            try
            {
                AccountGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_AccountGuests_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@AccountGuestID", Value = item.AccountGuestID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AccountGuest>(Sync_Translator.Translate_AccountGuest);
                        Log.Information("AccountGuest found: AccountGuestID={AccountGuestID}, FK_AccountID={FK_AccountID}, FK_GuestID={FK_GuestID}, IsResponsible={IsResponsible}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AccountGuestID, resultItem.FK_AccountID, resultItem.FK_GuestID, resultItem.IsResponsible, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No AccountGuest found with the given AccountGuestID.");
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

        public static async Task<AccountGuest> POS_AccountGuests_Insert_Transaction(AccountGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_AccountGuests_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AccountGuest> POS_AccountGuests_Insert(AccountGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_AccountGuests_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AccountGuest> POS_AccountGuests_Insert(AccountGuest item, SqlConnection sqlConn)
        {
            try
            {
                AccountGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_AccountGuests_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@AccountGuestID", Value = item.AccountGuestID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_AccountID", Value = item.FK_AccountID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsResponsible", Value = item.IsResponsible }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AccountGuest>(Sync_Translator.Translate_AccountGuest);
                        Log.Information("AccountGuest found: AccountGuestID={AccountGuestID}, FK_AccountID={FK_AccountID}, FK_GuestID={FK_GuestID}, IsResponsible={IsResponsible}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AccountGuestID, resultItem.FK_AccountID, resultItem.FK_GuestID, resultItem.IsResponsible, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("AccountGuest failed to create.");
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

        public static async Task<List<AccountGuest>> POS_AccountGuests_Select_All_Transaction(AccountGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_AccountGuests_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<AccountGuest>> POS_AccountGuests_Select_All(AccountGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_AccountGuests_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<AccountGuest>> POS_AccountGuests_Select_All(AccountGuest item, SqlConnection sqlConn)
        {
            try
            {
                List<AccountGuest> resultItem = new List<AccountGuest>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_AccountGuests_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<AccountGuest>(Sync_Translator.Translate_AccountGuest));
                        Log.Information("AccountGuest records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No AccountGuest records found.");
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

        public static async Task<AccountGuest> POS_AccountGuests_Update_Transaction(AccountGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_AccountGuests_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AccountGuest> POS_AccountGuests_Update(AccountGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_AccountGuests_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AccountGuest> POS_AccountGuests_Update(AccountGuest item, SqlConnection sqlConn)
        {
            try
            {
                AccountGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_AccountGuests_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@AccountGuestID", Value = item.AccountGuestID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_AccountID", Value = item.FK_AccountID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsResponsible", Value = item.IsResponsible }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AccountGuest>(Sync_Translator.Translate_AccountGuest);
                        Log.Information("AccountGuest found: AccountGuestID={AccountGuestID}, FK_AccountID={FK_AccountID}, FK_GuestID={FK_GuestID}, IsResponsible={IsResponsible}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AccountGuestID, resultItem.FK_AccountID, resultItem.FK_GuestID, resultItem.IsResponsible, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("AccountGuest failed to update.");
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

        #region POS_Accounts

        public static async Task<Account> POS_Accounts_Select_Single_Transaction(Account item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Accounts_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Account> POS_Accounts_Select_Single(Account item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Accounts_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Account> POS_Accounts_Select_Single(Account item, SqlConnection sqlConn)
        {
            try
            {
                Account resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Accounts_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@AccountID", Value = item.AccountID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Account>(Sync_Translator.Translate_Account);
                        Log.Information("Account found: AccountID={AccountID}, Name={Name}, FK_BookingHeaderID={FK_BookingHeaderID}, IsClosed={IsClosed}, FK_ResponsibleID={FK_ResponsibleID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AccountID, resultItem.Name, resultItem.FK_BookingHeaderID, resultItem.IsClosed, resultItem.FK_ResponsibleID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Account found with the given AccountID.");
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

        public static async Task<Account> POS_Accounts_Insert_Transaction(Account item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Accounts_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Account> POS_Accounts_Insert(Account item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Accounts_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Account> POS_Accounts_Insert(Account item, SqlConnection sqlConn)
        {
            try
            {
                Account resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Accounts_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@AccountID", Value = item.AccountID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BookingHeaderID", Value = item.FK_BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsClosed", Value = item.IsClosed }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ResponsibleID", Value = item.FK_ResponsibleID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Account>(Sync_Translator.Translate_Account);
                        Log.Information("Account found: AccountID={AccountID}, Name={Name}, FK_BookingHeaderID={FK_BookingHeaderID}, IsClosed={IsClosed}, FK_ResponsibleID={FK_ResponsibleID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AccountID, resultItem.Name, resultItem.FK_BookingHeaderID, resultItem.IsClosed, resultItem.FK_ResponsibleID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Account failed to create.");
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

        public static async Task<List<Account>> POS_Accounts_Select_All_Transaction(Account item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Accounts_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Account>> POS_Accounts_Select_All(Account item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Accounts_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Account>> POS_Accounts_Select_All(Account item, SqlConnection sqlConn)
        {
            try
            {
                List<Account> resultItem = new List<Account>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Accounts_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Account>(Sync_Translator.Translate_Account));
                        Log.Information("Account records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Account records found.");
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

        public static async Task<Account> POS_Accounts_Update_Transaction(Account item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Accounts_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Account> POS_Accounts_Update(Account item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Accounts_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Account> POS_Accounts_Update(Account item, SqlConnection sqlConn)
        {
            try
            {
                Account resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Accounts_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@AccountID", Value = item.AccountID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BookingHeaderID", Value = item.FK_BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsClosed", Value = item.IsClosed }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ResponsibleID", Value = item.FK_ResponsibleID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Account>(Sync_Translator.Translate_Account);
                        Log.Information("Account found: AccountID={AccountID}, Name={Name}, FK_BookingHeaderID={FK_BookingHeaderID}, IsClosed={IsClosed}, FK_ResponsibleID={FK_ResponsibleID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AccountID, resultItem.Name, resultItem.FK_BookingHeaderID, resultItem.IsClosed, resultItem.FK_ResponsibleID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Account failed to update.");
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

        #region POS_Arrivals

        public static async Task<Arrival> POS_Arrivals_Select_Single_Transaction(Arrival item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Arrivals_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Arrival> POS_Arrivals_Select_Single(Arrival item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Arrivals_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Arrival> POS_Arrivals_Select_Single(Arrival item, SqlConnection sqlConn)
        {
            try
            {
                Arrival resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Arrivals_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@ArrivalID", Value = item.ArrivalID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Arrival>(Sync_Translator.Translate_Arrival);
                        Log.Information("Arrival found: ArrivalID={ArrivalID}, FK_GuestID={FK_GuestID}, CheckedInBy={CheckedInBy}, CheckInDate={CheckInDate}, CheckedOutBy={CheckedOutBy}, CheckOutDate={CheckOutDate}", resultItem.ArrivalID, resultItem.FK_GuestID, resultItem.CheckedInBy, resultItem.CheckInDate, resultItem.CheckedOutBy, resultItem.CheckOutDate);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Arrival found with the given ArrivalID.");
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

        public static async Task<Arrival> POS_Arrivals_Insert_Transaction(Arrival item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Arrivals_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Arrival> POS_Arrivals_Insert(Arrival item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Arrivals_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Arrival> POS_Arrivals_Insert(Arrival item, SqlConnection sqlConn)
        {
            try
            {
                Arrival resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Arrivals_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@ArrivalID", Value = item.ArrivalID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CheckedInBy", Value = item.CheckedInBy }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@CheckInDate", Value = item.CheckInDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CheckedOutBy", Value = item.CheckedOutBy }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@CheckOutDate", Value = item.CheckOutDate }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Arrival>(Sync_Translator.Translate_Arrival);
                        Log.Information("Arrival found: ArrivalID={ArrivalID}, FK_GuestID={FK_GuestID}, CheckedInBy={CheckedInBy}, CheckInDate={CheckInDate}, CheckedOutBy={CheckedOutBy}, CheckOutDate={CheckOutDate}", resultItem.ArrivalID, resultItem.FK_GuestID, resultItem.CheckedInBy, resultItem.CheckInDate, resultItem.CheckedOutBy, resultItem.CheckOutDate);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Arrival failed to create.");
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

        public static async Task<List<Arrival>> POS_Arrivals_Select_All_Transaction(Arrival item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Arrivals_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Arrival>> POS_Arrivals_Select_All(Arrival item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Arrivals_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Arrival>> POS_Arrivals_Select_All(Arrival item, SqlConnection sqlConn)
        {
            try
            {
                List<Arrival> resultItem = new List<Arrival>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Arrivals_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Arrival>(Sync_Translator.Translate_Arrival));
                        Log.Information("Arrival records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Arrival records found.");
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

        public static async Task<Arrival> POS_Arrivals_Update_Transaction(Arrival item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Arrivals_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Arrival> POS_Arrivals_Update(Arrival item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Arrivals_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Arrival> POS_Arrivals_Update(Arrival item, SqlConnection sqlConn)
        {
            try
            {
                Arrival resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Arrivals_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@ArrivalID", Value = item.ArrivalID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CheckedInBy", Value = item.CheckedInBy }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@CheckInDate", Value = item.CheckInDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CheckedOutBy", Value = item.CheckedOutBy }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@CheckOutDate", Value = item.CheckOutDate }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Arrival>(Sync_Translator.Translate_Arrival);
                        Log.Information("Arrival found: ArrivalID={ArrivalID}, FK_GuestID={FK_GuestID}, CheckedInBy={CheckedInBy}, CheckInDate={CheckInDate}, CheckedOutBy={CheckedOutBy}, CheckOutDate={CheckOutDate}", resultItem.ArrivalID, resultItem.FK_GuestID, resultItem.CheckedInBy, resultItem.CheckInDate, resultItem.CheckedOutBy, resultItem.CheckOutDate);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Arrival failed to update.");
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

        #region POS_CashUpHeaders

        public static async Task<CashUpHeader> POS_CashUpHeaders_Select_Single_Transaction(CashUpHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpHeaders_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpHeader> POS_CashUpHeaders_Select_Single(CashUpHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpHeaders_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpHeader> POS_CashUpHeaders_Select_Single(CashUpHeader item, SqlConnection sqlConn)
        {
            try
            {
                CashUpHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CashUpHeaders_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@CashUpHeaderID", Value = item.CashUpHeaderID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CashUpHeader>(Sync_Translator.Translate_CashUpHeader);
                        Log.Information("CashUpHeader found: CashUpHeaderID={CashUpHeaderID}, FK_CostCenterID={FK_CostCenterID}, FK_CurrencyID={FK_CurrencyID}, CashUpDate={CashUpDate}, CashUpBy={CashUpBy}, TotalSystemAmount={TotalSystemAmount}, TotalCountedAmount={TotalCountedAmount}, TotalVariance={TotalVariance}, Notes={Notes}, IsFinalised={IsFinalised}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CashUpHeaderID, resultItem.FK_CostCenterID, resultItem.FK_CurrencyID, resultItem.CashUpDate, resultItem.CashUpBy, resultItem.TotalSystemAmount, resultItem.TotalCountedAmount, resultItem.TotalVariance, resultItem.Notes, resultItem.IsFinalised, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CashUpHeader found with the given CashUpHeaderID.");
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

        public static async Task<CashUpHeader> POS_CashUpHeaders_Insert_Transaction(CashUpHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpHeaders_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpHeader> POS_CashUpHeaders_Insert(CashUpHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpHeaders_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpHeader> POS_CashUpHeaders_Insert(CashUpHeader item, SqlConnection sqlConn)
        {
            try
            {
                CashUpHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CashUpHeaders_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@CashUpHeaderID", Value = item.CashUpHeaderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@CashUpDate", Value = item.CashUpDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CashUpBy", Value = item.CashUpBy }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalSystemAmount", Value = item.TotalSystemAmount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCountedAmount", Value = item.TotalCountedAmount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalVariance", Value = item.TotalVariance }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsFinalised", Value = item.IsFinalised }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CashUpHeader>(Sync_Translator.Translate_CashUpHeader);
                        Log.Information("CashUpHeader found: CashUpHeaderID={CashUpHeaderID}, FK_CostCenterID={FK_CostCenterID}, FK_CurrencyID={FK_CurrencyID}, CashUpDate={CashUpDate}, CashUpBy={CashUpBy}, TotalSystemAmount={TotalSystemAmount}, TotalCountedAmount={TotalCountedAmount}, TotalVariance={TotalVariance}, Notes={Notes}, IsFinalised={IsFinalised}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CashUpHeaderID, resultItem.FK_CostCenterID, resultItem.FK_CurrencyID, resultItem.CashUpDate, resultItem.CashUpBy, resultItem.TotalSystemAmount, resultItem.TotalCountedAmount, resultItem.TotalVariance, resultItem.Notes, resultItem.IsFinalised, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CashUpHeader failed to create.");
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

        public static async Task<List<CashUpHeader>> POS_CashUpHeaders_Select_All_Transaction(CashUpHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpHeaders_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CashUpHeader>> POS_CashUpHeaders_Select_All(CashUpHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpHeaders_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CashUpHeader>> POS_CashUpHeaders_Select_All(CashUpHeader item, SqlConnection sqlConn)
        {
            try
            {
                List<CashUpHeader> resultItem = new List<CashUpHeader>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CashUpHeaders_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CashUpHeader>(Sync_Translator.Translate_CashUpHeader));
                        Log.Information("CashUpHeader records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CashUpHeader records found.");
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

        public static async Task<CashUpHeader> POS_CashUpHeaders_Update_Transaction(CashUpHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpHeaders_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpHeader> POS_CashUpHeaders_Update(CashUpHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpHeaders_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpHeader> POS_CashUpHeaders_Update(CashUpHeader item, SqlConnection sqlConn)
        {
            try
            {
                CashUpHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CashUpHeaders_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@CashUpHeaderID", Value = item.CashUpHeaderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@CashUpDate", Value = item.CashUpDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CashUpBy", Value = item.CashUpBy }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalSystemAmount", Value = item.TotalSystemAmount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCountedAmount", Value = item.TotalCountedAmount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalVariance", Value = item.TotalVariance }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsFinalised", Value = item.IsFinalised }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CashUpHeader>(Sync_Translator.Translate_CashUpHeader);
                        Log.Information("CashUpHeader found: CashUpHeaderID={CashUpHeaderID}, FK_CostCenterID={FK_CostCenterID}, FK_CurrencyID={FK_CurrencyID}, CashUpDate={CashUpDate}, CashUpBy={CashUpBy}, TotalSystemAmount={TotalSystemAmount}, TotalCountedAmount={TotalCountedAmount}, TotalVariance={TotalVariance}, Notes={Notes}, IsFinalised={IsFinalised}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CashUpHeaderID, resultItem.FK_CostCenterID, resultItem.FK_CurrencyID, resultItem.CashUpDate, resultItem.CashUpBy, resultItem.TotalSystemAmount, resultItem.TotalCountedAmount, resultItem.TotalVariance, resultItem.Notes, resultItem.IsFinalised, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CashUpHeader failed to update.");
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

        #region POS_CashUpLines

        public static async Task<CashUpLine> POS_CashUpLines_Select_Single_Transaction(CashUpLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpLines_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpLine> POS_CashUpLines_Select_Single(CashUpLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpLines_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpLine> POS_CashUpLines_Select_Single(CashUpLine item, SqlConnection sqlConn)
        {
            try
            {
                CashUpLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CashUpLines_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@CashUpPaymentTypeID", Value = item.CashUpPaymentTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CashUpLine>(Sync_Translator.Translate_CashUpLine);
                        Log.Information("CashUpLine found: CashUpPaymentTypeID={CashUpPaymentTypeID}, FK_CashUpID={FK_CashUpID}, FK_PaymentTypeID={FK_PaymentTypeID}, SystemAmount={SystemAmount}, CountedAmount={CountedAmount}, VarianceAmount={VarianceAmount}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CashUpPaymentTypeID, resultItem.FK_CashUpID, resultItem.FK_PaymentTypeID, resultItem.SystemAmount, resultItem.CountedAmount, resultItem.VarianceAmount, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CashUpLine found with the given CashUpLineID.");
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

        public static async Task<CashUpLine> POS_CashUpLines_Insert_Transaction(CashUpLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpLines_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpLine> POS_CashUpLines_Insert(CashUpLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpLines_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpLine> POS_CashUpLines_Insert(CashUpLine item, SqlConnection sqlConn)
        {
            try
            {
                CashUpLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CashUpLines_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@CashUpPaymentTypeID", Value = item.CashUpPaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_CashUpID", Value = item.FK_CashUpID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentTypeID", Value = item.FK_PaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@SystemAmount", Value = item.SystemAmount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@CountedAmount", Value = item.CountedAmount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@VarianceAmount", Value = item.VarianceAmount }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CashUpLine>(Sync_Translator.Translate_CashUpLine);
                        Log.Information("CashUpLine found: CashUpPaymentTypeID={CashUpPaymentTypeID}, FK_CashUpID={FK_CashUpID}, FK_PaymentTypeID={FK_PaymentTypeID}, SystemAmount={SystemAmount}, CountedAmount={CountedAmount}, VarianceAmount={VarianceAmount}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CashUpPaymentTypeID, resultItem.FK_CashUpID, resultItem.FK_PaymentTypeID, resultItem.SystemAmount, resultItem.CountedAmount, resultItem.VarianceAmount, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CashUpLine failed to create.");
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

        public static async Task<List<CashUpLine>> POS_CashUpLines_Select_All_Transaction(CashUpLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpLines_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CashUpLine>> POS_CashUpLines_Select_All(CashUpLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpLines_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CashUpLine>> POS_CashUpLines_Select_All(CashUpLine item, SqlConnection sqlConn)
        {
            try
            {
                List<CashUpLine> resultItem = new List<CashUpLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CashUpLines_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CashUpLine>(Sync_Translator.Translate_CashUpLine));
                        Log.Information("CashUpLine records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CashUpLine records found.");
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

        public static async Task<CashUpLine> POS_CashUpLines_Update_Transaction(CashUpLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpLines_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpLine> POS_CashUpLines_Update(CashUpLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CashUpLines_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CashUpLine> POS_CashUpLines_Update(CashUpLine item, SqlConnection sqlConn)
        {
            try
            {
                CashUpLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CashUpLines_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@CashUpPaymentTypeID", Value = item.CashUpPaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_CashUpID", Value = item.FK_CashUpID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentTypeID", Value = item.FK_PaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@SystemAmount", Value = item.SystemAmount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@CountedAmount", Value = item.CountedAmount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@VarianceAmount", Value = item.VarianceAmount }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CashUpLine>(Sync_Translator.Translate_CashUpLine);
                        Log.Information("CashUpLine found: CashUpPaymentTypeID={CashUpPaymentTypeID}, FK_CashUpID={FK_CashUpID}, FK_PaymentTypeID={FK_PaymentTypeID}, SystemAmount={SystemAmount}, CountedAmount={CountedAmount}, VarianceAmount={VarianceAmount}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CashUpPaymentTypeID, resultItem.FK_CashUpID, resultItem.FK_PaymentTypeID, resultItem.SystemAmount, resultItem.CountedAmount, resultItem.VarianceAmount, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CashUpLine failed to update.");
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

        #region POS_InvoicePayments

        public static async Task<InvoicePayment> POS_InvoicePayments_Select_Single_Transaction(InvoicePayment item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoicePayments_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoicePayment> POS_InvoicePayments_Select_Single(InvoicePayment item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoicePayments_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoicePayment> POS_InvoicePayments_Select_Single(InvoicePayment item, SqlConnection sqlConn)
        {
            try
            {
                InvoicePayment resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoicePayments_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoicePaymentID", Value = item.InvoicePaymentID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoicePayment>(Sync_Translator.Translate_InvoicePayment);
                        Log.Information("InvoicePayment found: InvoicePaymentID={InvoicePaymentID}, FK_InvoiceID={FK_InvoiceID}, FK_PaymentTypeID={FK_PaymentTypeID}, FK_BaseCurrencyID={FK_BaseCurrencyID}, FK_PaymentCurrencyID={FK_PaymentCurrencyID}, BaseCurrencyCode={BaseCurrencyCode}, PaymentCurrencyCode={PaymentCurrencyCode}, BaseAmountPaid={BaseAmountPaid}, PaymentAmountPaid={PaymentAmountPaid}, ExchangeRate={ExchangeRate}, ExchangeDate={ExchangeDate}, DatePaid={DatePaid}, StaffName={StaffName}, IdempotencyKey={IdempotencyKey}, Reference={Reference}, Notes={Notes}, IsVoided={IsVoided}, VoidReason={VoidReason}, VoidedDate={VoidedDate}, VoidedBy={VoidedBy}, SignatureBase64={SignatureBase64}", resultItem.InvoicePaymentID, resultItem.FK_InvoiceID, resultItem.FK_PaymentTypeID, resultItem.FK_BaseCurrencyID, resultItem.FK_PaymentCurrencyID, resultItem.BaseCurrencyCode, resultItem.PaymentCurrencyCode, resultItem.BaseAmountPaid, resultItem.PaymentAmountPaid, resultItem.ExchangeRate, resultItem.ExchangeDate, resultItem.DatePaid, resultItem.StaffName, resultItem.IdempotencyKey, resultItem.Reference, resultItem.Notes, resultItem.IsVoided, resultItem.VoidReason, resultItem.VoidedDate, resultItem.VoidedBy, resultItem.SignatureBase64);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InvoicePayment found with the given InvoicePaymentID.");
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

        public static async Task<InvoicePayment> POS_InvoicePayments_Insert_Transaction(InvoicePayment item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoicePayments_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoicePayment> POS_InvoicePayments_Insert(InvoicePayment item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoicePayments_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoicePayment> POS_InvoicePayments_Insert(InvoicePayment item, SqlConnection sqlConn)
        {
            try
            {
                InvoicePayment resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoicePayments_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoicePaymentID", Value = item.InvoicePaymentID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceID", Value = item.FK_InvoiceID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentTypeID", Value = item.FK_PaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BaseCurrencyID", Value = item.FK_BaseCurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentCurrencyID", Value = item.FK_PaymentCurrencyID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BaseCurrencyCode", Value = item.BaseCurrencyCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PaymentCurrencyCode", Value = item.PaymentCurrencyCode }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@BaseAmountPaid", Value = item.BaseAmountPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@PaymentAmountPaid", Value = item.PaymentAmountPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExchangeRate", Value = item.ExchangeRate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ExchangeDate", Value = item.ExchangeDate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DatePaid", Value = item.DatePaid }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@StaffName", Value = item.StaffName }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@IdempotencyKey", Value = item.IdempotencyKey }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Reference", Value = item.Reference }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVoided", Value = item.IsVoided }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidReason", Value = item.VoidReason }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@VoidedDate", Value = item.VoidedDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidedBy", Value = item.VoidedBy }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SignatureBase64", Value = item.SignatureBase64 }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoicePayment>(Sync_Translator.Translate_InvoicePayment);
                        Log.Information("InvoicePayment found: InvoicePaymentID={InvoicePaymentID}, FK_InvoiceID={FK_InvoiceID}, FK_PaymentTypeID={FK_PaymentTypeID}, FK_BaseCurrencyID={FK_BaseCurrencyID}, FK_PaymentCurrencyID={FK_PaymentCurrencyID}, BaseCurrencyCode={BaseCurrencyCode}, PaymentCurrencyCode={PaymentCurrencyCode}, BaseAmountPaid={BaseAmountPaid}, PaymentAmountPaid={PaymentAmountPaid}, ExchangeRate={ExchangeRate}, ExchangeDate={ExchangeDate}, DatePaid={DatePaid}, StaffName={StaffName}, IdempotencyKey={IdempotencyKey}, Reference={Reference}, Notes={Notes}, IsVoided={IsVoided}, VoidReason={VoidReason}, VoidedDate={VoidedDate}, VoidedBy={VoidedBy}, SignatureBase64={SignatureBase64}", resultItem.InvoicePaymentID, resultItem.FK_InvoiceID, resultItem.FK_PaymentTypeID, resultItem.FK_BaseCurrencyID, resultItem.FK_PaymentCurrencyID, resultItem.BaseCurrencyCode, resultItem.PaymentCurrencyCode, resultItem.BaseAmountPaid, resultItem.PaymentAmountPaid, resultItem.ExchangeRate, resultItem.ExchangeDate, resultItem.DatePaid, resultItem.StaffName, resultItem.IdempotencyKey, resultItem.Reference, resultItem.Notes, resultItem.IsVoided, resultItem.VoidReason, resultItem.VoidedDate, resultItem.VoidedBy, resultItem.SignatureBase64);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoicePayment failed to create.");
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

        public static async Task<List<InvoicePayment>> POS_InvoicePayments_Select_All_Transaction(InvoicePayment item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoicePayments_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoicePayment>> POS_InvoicePayments_Select_All(InvoicePayment item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoicePayments_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoicePayment>> POS_InvoicePayments_Select_All(InvoicePayment item, SqlConnection sqlConn)
        {
            try
            {
                List<InvoicePayment> resultItem = new List<InvoicePayment>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoicePayments_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<InvoicePayment>(Sync_Translator.Translate_InvoicePayment));
                        Log.Information("InvoicePayment records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InvoicePayment records found.");
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

        public static async Task<InvoicePayment> POS_InvoicePayments_Update_Transaction(InvoicePayment item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoicePayments_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoicePayment> POS_InvoicePayments_Update(InvoicePayment item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InvoicePayments_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoicePayment> POS_InvoicePayments_Update(InvoicePayment item, SqlConnection sqlConn)
        {
            try
            {
                InvoicePayment resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InvoicePayments_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoicePaymentID", Value = item.InvoicePaymentID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceID", Value = item.FK_InvoiceID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentTypeID", Value = item.FK_PaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BaseCurrencyID", Value = item.FK_BaseCurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentCurrencyID", Value = item.FK_PaymentCurrencyID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BaseCurrencyCode", Value = item.BaseCurrencyCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PaymentCurrencyCode", Value = item.PaymentCurrencyCode }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@BaseAmountPaid", Value = item.BaseAmountPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@PaymentAmountPaid", Value = item.PaymentAmountPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExchangeRate", Value = item.ExchangeRate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ExchangeDate", Value = item.ExchangeDate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DatePaid", Value = item.DatePaid }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@StaffName", Value = item.StaffName }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@IdempotencyKey", Value = item.IdempotencyKey }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Reference", Value = item.Reference }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVoided", Value = item.IsVoided }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidReason", Value = item.VoidReason }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@VoidedDate", Value = item.VoidedDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidedBy", Value = item.VoidedBy }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SignatureBase64", Value = item.SignatureBase64 }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoicePayment>(Sync_Translator.Translate_InvoicePayment);
                        Log.Information("InvoicePayment found: InvoicePaymentID={InvoicePaymentID}, FK_InvoiceID={FK_InvoiceID}, FK_PaymentTypeID={FK_PaymentTypeID}, FK_BaseCurrencyID={FK_BaseCurrencyID}, FK_PaymentCurrencyID={FK_PaymentCurrencyID}, BaseCurrencyCode={BaseCurrencyCode}, PaymentCurrencyCode={PaymentCurrencyCode}, BaseAmountPaid={BaseAmountPaid}, PaymentAmountPaid={PaymentAmountPaid}, ExchangeRate={ExchangeRate}, ExchangeDate={ExchangeDate}, DatePaid={DatePaid}, StaffName={StaffName}, IdempotencyKey={IdempotencyKey}, Reference={Reference}, Notes={Notes}, IsVoided={IsVoided}, VoidReason={VoidReason}, VoidedDate={VoidedDate}, VoidedBy={VoidedBy}, SignatureBase64={SignatureBase64}", resultItem.InvoicePaymentID, resultItem.FK_InvoiceID, resultItem.FK_PaymentTypeID, resultItem.FK_BaseCurrencyID, resultItem.FK_PaymentCurrencyID, resultItem.BaseCurrencyCode, resultItem.PaymentCurrencyCode, resultItem.BaseAmountPaid, resultItem.PaymentAmountPaid, resultItem.ExchangeRate, resultItem.ExchangeDate, resultItem.DatePaid, resultItem.StaffName, resultItem.IdempotencyKey, resultItem.Reference, resultItem.Notes, resultItem.IsVoided, resultItem.VoidReason, resultItem.VoidedDate, resultItem.VoidedBy, resultItem.SignatureBase64);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoicePayment failed to update.");
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

        #region POS_TabLineCombinations

        public static async Task<TabLineCombination> POS_TabLineCombinations_Select_Single_Transaction(TabLineCombination item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineCombinations_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineCombination> POS_TabLineCombinations_Select_Single(TabLineCombination item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineCombinations_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineCombination> POS_TabLineCombinations_Select_Single(TabLineCombination item, SqlConnection sqlConn)
        {
            try
            {
                TabLineCombination resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineCombinations_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineCombinationID", Value = item.TabLineCombinationID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineCombination>(Sync_Translator.Translate_TabLineCombination);
                        Log.Information("TabLineCombination found: TabLineCombinationID={TabLineCombinationID}, FK_TabLineID={FK_TabLineID}, FK_ProductCombinationID={FK_ProductCombinationID}, Product={Product}, Hold={Hold}, Notes={Notes}", resultItem.TabLineCombinationID, resultItem.FK_TabLineID, resultItem.FK_ProductCombinationID, resultItem.Product, resultItem.Hold, resultItem.Notes);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLineCombination found with the given TabLineCombinationID.");
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

        public static async Task<TabLineCombination> POS_TabLineCombinations_Insert_Transaction(TabLineCombination item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineCombinations_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineCombination> POS_TabLineCombinations_Insert(TabLineCombination item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineCombinations_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineCombination> POS_TabLineCombinations_Insert(TabLineCombination item, SqlConnection sqlConn)
        {
            try
            {
                TabLineCombination resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineCombinations_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineCombinationID", Value = item.TabLineCombinationID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineID", Value = item.FK_TabLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductCombinationID", Value = item.FK_ProductCombinationID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@Hold", Value = item.Hold }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineCombination>(Sync_Translator.Translate_TabLineCombination);
                        Log.Information("TabLineCombination found: TabLineCombinationID={TabLineCombinationID}, FK_TabLineID={FK_TabLineID}, FK_ProductCombinationID={FK_ProductCombinationID}, Product={Product}, Hold={Hold}, Notes={Notes}", resultItem.TabLineCombinationID, resultItem.FK_TabLineID, resultItem.FK_ProductCombinationID, resultItem.Product, resultItem.Hold, resultItem.Notes);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLineCombination failed to create.");
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

        public static async Task<List<TabLineCombination>> POS_TabLineCombinations_Select_All_Transaction(TabLineCombination item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineCombinations_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLineCombination>> POS_TabLineCombinations_Select_All(TabLineCombination item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineCombinations_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLineCombination>> POS_TabLineCombinations_Select_All(TabLineCombination item, SqlConnection sqlConn)
        {
            try
            {
                List<TabLineCombination> resultItem = new List<TabLineCombination>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineCombinations_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<TabLineCombination>(Sync_Translator.Translate_TabLineCombination));
                        Log.Information("TabLineCombination records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLineCombination records found.");
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

        public static async Task<TabLineCombination> POS_TabLineCombinations_Update_Transaction(TabLineCombination item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineCombinations_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineCombination> POS_TabLineCombinations_Update(TabLineCombination item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineCombinations_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineCombination> POS_TabLineCombinations_Update(TabLineCombination item, SqlConnection sqlConn)
        {
            try
            {
                TabLineCombination resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineCombinations_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineCombinationID", Value = item.TabLineCombinationID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineID", Value = item.FK_TabLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductCombinationID", Value = item.FK_ProductCombinationID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@Hold", Value = item.Hold }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineCombination>(Sync_Translator.Translate_TabLineCombination);
                        Log.Information("TabLineCombination found: TabLineCombinationID={TabLineCombinationID}, FK_TabLineID={FK_TabLineID}, FK_ProductCombinationID={FK_ProductCombinationID}, Product={Product}, Hold={Hold}, Notes={Notes}", resultItem.TabLineCombinationID, resultItem.FK_TabLineID, resultItem.FK_ProductCombinationID, resultItem.Product, resultItem.Hold, resultItem.Notes);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLineCombination failed to update.");
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

        #region POS_TabLineExtras

        public static async Task<TabLineExtra> POS_TabLineExtras_Select_Single_Transaction(TabLineExtra item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineExtras_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineExtra> POS_TabLineExtras_Select_Single(TabLineExtra item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineExtras_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineExtra> POS_TabLineExtras_Select_Single(TabLineExtra item, SqlConnection sqlConn)
        {
            try
            {
                TabLineExtra resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineExtras_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineExtraID", Value = item.TabLineExtraID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineExtra>(Sync_Translator.Translate_TabLineExtra);
                        Log.Information("TabLineExtra found: TabLineExtraID={TabLineExtraID}, FK_TabLineID={FK_TabLineID}, FK_ProductID={FK_ProductID}, Product={Product}", resultItem.TabLineExtraID, resultItem.FK_TabLineID, resultItem.FK_ProductID, resultItem.Product);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLineExtra found with the given TabLineExtraID.");
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

        public static async Task<TabLineExtra> POS_TabLineExtras_Insert_Transaction(TabLineExtra item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineExtras_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineExtra> POS_TabLineExtras_Insert(TabLineExtra item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineExtras_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineExtra> POS_TabLineExtras_Insert(TabLineExtra item, SqlConnection sqlConn)
        {
            try
            {
                TabLineExtra resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineExtras_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineExtraID", Value = item.TabLineExtraID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineID", Value = item.FK_TabLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineExtra>(Sync_Translator.Translate_TabLineExtra);
                        Log.Information("TabLineExtra found: TabLineExtraID={TabLineExtraID}, FK_TabLineID={FK_TabLineID}, FK_ProductID={FK_ProductID}, Product={Product}", resultItem.TabLineExtraID, resultItem.FK_TabLineID, resultItem.FK_ProductID, resultItem.Product);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLineExtra failed to create.");
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

        public static async Task<List<TabLineExtra>> POS_TabLineExtras_Select_All_Transaction(TabLineExtra item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineExtras_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLineExtra>> POS_TabLineExtras_Select_All(TabLineExtra item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineExtras_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLineExtra>> POS_TabLineExtras_Select_All(TabLineExtra item, SqlConnection sqlConn)
        {
            try
            {
                List<TabLineExtra> resultItem = new List<TabLineExtra>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineExtras_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<TabLineExtra>(Sync_Translator.Translate_TabLineExtra));
                        Log.Information("TabLineExtra records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLineExtra records found.");
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

        public static async Task<TabLineExtra> POS_TabLineExtras_Update_Transaction(TabLineExtra item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineExtras_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineExtra> POS_TabLineExtras_Update(TabLineExtra item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineExtras_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineExtra> POS_TabLineExtras_Update(TabLineExtra item, SqlConnection sqlConn)
        {
            try
            {
                TabLineExtra resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineExtras_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineExtraID", Value = item.TabLineExtraID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineID", Value = item.FK_TabLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineExtra>(Sync_Translator.Translate_TabLineExtra);
                        Log.Information("TabLineExtra found: TabLineExtraID={TabLineExtraID}, FK_TabLineID={FK_TabLineID}, FK_ProductID={FK_ProductID}, Product={Product}", resultItem.TabLineExtraID, resultItem.FK_TabLineID, resultItem.FK_ProductID, resultItem.Product);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLineExtra failed to update.");
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

        #region POS_TabLineGuests

        public static async Task<TabLineGuest> POS_TabLineGuests_Select_Single_Transaction(TabLineGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineGuests_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineGuest> POS_TabLineGuests_Select_Single(TabLineGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineGuests_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineGuest> POS_TabLineGuests_Select_Single(TabLineGuest item, SqlConnection sqlConn)
        {
            try
            {
                TabLineGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineGuests_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineGuestID", Value = item.TabLineGuestID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineGuest>(Sync_Translator.Translate_TabLineGuest);
                        Log.Information("TabLineGuest found: TabLineGuestID={TabLineGuestID}, FK_TabLineID={FK_TabLineID}, FK_GuestID={FK_GuestID}, Note={Note}, DateUpdated={DateUpdated}", resultItem.TabLineGuestID, resultItem.FK_TabLineID, resultItem.FK_GuestID, resultItem.Note, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLineGuest found with the given TabLineGuestID.");
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

        public static async Task<TabLineGuest> POS_TabLineGuests_Insert_Transaction(TabLineGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineGuests_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineGuest> POS_TabLineGuests_Insert(TabLineGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineGuests_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineGuest> POS_TabLineGuests_Insert(TabLineGuest item, SqlConnection sqlConn)
        {
            try
            {
                TabLineGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineGuests_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineGuestID", Value = item.TabLineGuestID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineID", Value = item.FK_TabLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Note", Value = item.Note }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineGuest>(Sync_Translator.Translate_TabLineGuest);
                        Log.Information("TabLineGuest found: TabLineGuestID={TabLineGuestID}, FK_TabLineID={FK_TabLineID}, FK_GuestID={FK_GuestID}, Note={Note}, DateUpdated={DateUpdated}", resultItem.TabLineGuestID, resultItem.FK_TabLineID, resultItem.FK_GuestID, resultItem.Note, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLineGuest failed to create.");
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

        public static async Task<List<TabLineGuest>> POS_TabLineGuests_Select_All_Transaction(TabLineGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineGuests_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLineGuest>> POS_TabLineGuests_Select_All(TabLineGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineGuests_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLineGuest>> POS_TabLineGuests_Select_All(TabLineGuest item, SqlConnection sqlConn)
        {
            try
            {
                List<TabLineGuest> resultItem = new List<TabLineGuest>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineGuests_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<TabLineGuest>(Sync_Translator.Translate_TabLineGuest));
                        Log.Information("TabLineGuest records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLineGuest records found.");
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

        public static async Task<TabLineGuest> POS_TabLineGuests_Update_Transaction(TabLineGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineGuests_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineGuest> POS_TabLineGuests_Update(TabLineGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLineGuests_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineGuest> POS_TabLineGuests_Update(TabLineGuest item, SqlConnection sqlConn)
        {
            try
            {
                TabLineGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLineGuests_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineGuestID", Value = item.TabLineGuestID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineID", Value = item.FK_TabLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Note", Value = item.Note }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineGuest>(Sync_Translator.Translate_TabLineGuest);
                        Log.Information("TabLineGuest found: TabLineGuestID={TabLineGuestID}, FK_TabLineID={FK_TabLineID}, FK_GuestID={FK_GuestID}, Note={Note}, DateUpdated={DateUpdated}", resultItem.TabLineGuestID, resultItem.FK_TabLineID, resultItem.FK_GuestID, resultItem.Note, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLineGuest failed to update.");
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

        #region POS_TabLinePreparationMethods

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Select_Single_Transaction(TabLinePreparationMethod item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLinePreparationMethods_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Select_Single(TabLinePreparationMethod item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLinePreparationMethods_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Select_Single(TabLinePreparationMethod item, SqlConnection sqlConn)
        {
            try
            {
                TabLinePreparationMethod resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLinePreparationMethods_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLinePreparationMethodID", Value = item.TabLinePreparationMethodID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLinePreparationMethod>(Sync_Translator.Translate_TabLinePreparationMethod);
                        Log.Information("TabLinePreparationMethod found: TabLinePreparationMethodID={TabLinePreparationMethodID}, FK_TabLineCombinationID={FK_TabLineCombinationID}, FK_PreparationMethodID={FK_PreparationMethodID}, PreparationMethodName={PreparationMethodName}", resultItem.TabLinePreparationMethodID, resultItem.FK_TabLineCombinationID, resultItem.FK_PreparationMethodID, resultItem.PreparationMethodName);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLinePreparationMethod found with the given TabLinePreparationMethodID.");
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

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Insert_Transaction(TabLinePreparationMethod item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLinePreparationMethods_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Insert(TabLinePreparationMethod item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLinePreparationMethods_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Insert(TabLinePreparationMethod item, SqlConnection sqlConn)
        {
            try
            {
                TabLinePreparationMethod resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLinePreparationMethods_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLinePreparationMethodID", Value = item.TabLinePreparationMethodID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineCombinationID", Value = item.FK_TabLineCombinationID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PreparationMethodID", Value = item.FK_PreparationMethodID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PreparationMethodName", Value = item.PreparationMethodName }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLinePreparationMethod>(Sync_Translator.Translate_TabLinePreparationMethod);
                        Log.Information("TabLinePreparationMethod found: TabLinePreparationMethodID={TabLinePreparationMethodID}, FK_TabLineCombinationID={FK_TabLineCombinationID}, FK_PreparationMethodID={FK_PreparationMethodID}, PreparationMethodName={PreparationMethodName}", resultItem.TabLinePreparationMethodID, resultItem.FK_TabLineCombinationID, resultItem.FK_PreparationMethodID, resultItem.PreparationMethodName);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLinePreparationMethod failed to create.");
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

        public static async Task<List<TabLinePreparationMethod>> POS_TabLinePreparationMethods_Select_All_Transaction(TabLinePreparationMethod item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLinePreparationMethods_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLinePreparationMethod>> POS_TabLinePreparationMethods_Select_All(TabLinePreparationMethod item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLinePreparationMethods_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLinePreparationMethod>> POS_TabLinePreparationMethods_Select_All(TabLinePreparationMethod item, SqlConnection sqlConn)
        {
            try
            {
                List<TabLinePreparationMethod> resultItem = new List<TabLinePreparationMethod>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLinePreparationMethods_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<TabLinePreparationMethod>(Sync_Translator.Translate_TabLinePreparationMethod));
                        Log.Information("TabLinePreparationMethod records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLinePreparationMethod records found.");
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

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Update_Transaction(TabLinePreparationMethod item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLinePreparationMethods_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Update(TabLinePreparationMethod item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLinePreparationMethods_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLinePreparationMethod> POS_TabLinePreparationMethods_Update(TabLinePreparationMethod item, SqlConnection sqlConn)
        {
            try
            {
                TabLinePreparationMethod resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLinePreparationMethods_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLinePreparationMethodID", Value = item.TabLinePreparationMethodID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineCombinationID", Value = item.FK_TabLineCombinationID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PreparationMethodID", Value = item.FK_PreparationMethodID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PreparationMethodName", Value = item.PreparationMethodName }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLinePreparationMethod>(Sync_Translator.Translate_TabLinePreparationMethod);
                        Log.Information("TabLinePreparationMethod found: TabLinePreparationMethodID={TabLinePreparationMethodID}, FK_TabLineCombinationID={FK_TabLineCombinationID}, FK_PreparationMethodID={FK_PreparationMethodID}, PreparationMethodName={PreparationMethodName}", resultItem.TabLinePreparationMethodID, resultItem.FK_TabLineCombinationID, resultItem.FK_PreparationMethodID, resultItem.PreparationMethodName);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLinePreparationMethod failed to update.");
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

        #region POS_TabLines

        public static async Task<TabLine> POS_TabLines_Select_Single_Transaction(TabLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLines_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLine> POS_TabLines_Select_Single(TabLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLines_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLine> POS_TabLines_Select_Single(TabLine item, SqlConnection sqlConn)
        {
            try
            {
                TabLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLines_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineID", Value = item.TabLineID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLine>(Sync_Translator.Translate_TabLine);
                        Log.Information("TabLine found: TabLineID={TabLineID}, FK_TabID={FK_TabID}, FK_ProductID={FK_ProductID}, FK_PriceCodeID={FK_PriceCodeID}, FK_PointerID={FK_PointerID}, UnitCostExcl={UnitCostExcl}, Vat={Vat}, UnitCostIncl={UnitCostIncl}, Product={Product}, Quantity={Quantity}, Discount={Discount}, DiscountPerc={DiscountPerc}, IsVoided={IsVoided}, Notes={Notes}, AutoNotes={AutoNotes}, CreatedBy={CreatedBy}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, ServedAs={ServedAs}, ServedAsQuantified={ServedAsQuantified}, ServedAsQuantity={ServedAsQuantity}, FK_MenuID={FK_MenuID}, MenuName={MenuName}, Gratuity={Gratuity}, GratuityPerc={GratuityPerc}, FK_CostCenterID={FK_CostCenterID}", resultItem.TabLineID, resultItem.FK_TabID, resultItem.FK_ProductID, resultItem.FK_PriceCodeID, resultItem.FK_PointerID, resultItem.UnitCostExcl, resultItem.Vat, resultItem.UnitCostIncl, resultItem.Product, resultItem.Quantity, resultItem.Discount, resultItem.DiscountPerc, resultItem.IsVoided, resultItem.Notes, resultItem.AutoNotes, resultItem.CreatedBy, resultItem.DateCreated, resultItem.DateUpdated, resultItem.ServedAs, resultItem.ServedAsQuantified, resultItem.ServedAsQuantity, resultItem.FK_MenuID, resultItem.MenuName, resultItem.Gratuity, resultItem.GratuityPerc, resultItem.FK_CostCenterID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLine found with the given TabLineID.");
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

        public static async Task<TabLine> POS_TabLines_Insert_Transaction(TabLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLines_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLine> POS_TabLines_Insert(TabLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLines_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLine> POS_TabLines_Insert(TabLine item, SqlConnection sqlConn)
        {
            try
            {
                TabLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLines_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineID", Value = item.TabLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabID", Value = item.FK_TabID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PriceCodeID", Value = item.FK_PriceCodeID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_PointerID", Value = item.FK_PointerID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostExcl", Value = item.UnitCostExcl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostIncl", Value = item.UnitCostIncl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Discount", Value = item.Discount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@DiscountPerc", Value = item.DiscountPerc }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVoided", Value = item.IsVoided }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@AutoNotes", Value = item.AutoNotes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CreatedBy", Value = item.CreatedBy }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ServedAs", Value = item.ServedAs }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@ServedAsQuantified", Value = item.ServedAsQuantified }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ServedAsQuantity", Value = item.ServedAsQuantity }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MenuName", Value = item.MenuName }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Gratuity", Value = item.Gratuity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@GratuityPerc", Value = item.GratuityPerc }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLine>(Sync_Translator.Translate_TabLine);
                        Log.Information("TabLine found: TabLineID={TabLineID}, FK_TabID={FK_TabID}, FK_ProductID={FK_ProductID}, FK_PriceCodeID={FK_PriceCodeID}, FK_PointerID={FK_PointerID}, UnitCostExcl={UnitCostExcl}, Vat={Vat}, UnitCostIncl={UnitCostIncl}, Product={Product}, Quantity={Quantity}, Discount={Discount}, DiscountPerc={DiscountPerc}, IsVoided={IsVoided}, Notes={Notes}, AutoNotes={AutoNotes}, CreatedBy={CreatedBy}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, ServedAs={ServedAs}, ServedAsQuantified={ServedAsQuantified}, ServedAsQuantity={ServedAsQuantity}, FK_MenuID={FK_MenuID}, MenuName={MenuName}, Gratuity={Gratuity}, GratuityPerc={GratuityPerc}, FK_CostCenterID={FK_CostCenterID}", resultItem.TabLineID, resultItem.FK_TabID, resultItem.FK_ProductID, resultItem.FK_PriceCodeID, resultItem.FK_PointerID, resultItem.UnitCostExcl, resultItem.Vat, resultItem.UnitCostIncl, resultItem.Product, resultItem.Quantity, resultItem.Discount, resultItem.DiscountPerc, resultItem.IsVoided, resultItem.Notes, resultItem.AutoNotes, resultItem.CreatedBy, resultItem.DateCreated, resultItem.DateUpdated, resultItem.ServedAs, resultItem.ServedAsQuantified, resultItem.ServedAsQuantity, resultItem.FK_MenuID, resultItem.MenuName, resultItem.Gratuity, resultItem.GratuityPerc, resultItem.FK_CostCenterID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLine failed to create.");
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

        public static async Task<List<TabLine>> POS_TabLines_Select_All_Transaction(TabLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLines_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLine>> POS_TabLines_Select_All(TabLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLines_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLine>> POS_TabLines_Select_All(TabLine item, SqlConnection sqlConn)
        {
            try
            {
                List<TabLine> resultItem = new List<TabLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLines_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<TabLine>(Sync_Translator.Translate_TabLine));
                        Log.Information("TabLine records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLine records found.");
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

        public static async Task<TabLine> POS_TabLines_Update_Transaction(TabLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLines_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLine> POS_TabLines_Update(TabLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TabLines_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLine> POS_TabLines_Update(TabLine item, SqlConnection sqlConn)
        {
            try
            {
                TabLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TabLines_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabLineID", Value = item.TabLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabID", Value = item.FK_TabID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PriceCodeID", Value = item.FK_PriceCodeID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_PointerID", Value = item.FK_PointerID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostExcl", Value = item.UnitCostExcl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostIncl", Value = item.UnitCostIncl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Discount", Value = item.Discount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@DiscountPerc", Value = item.DiscountPerc }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVoided", Value = item.IsVoided }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@AutoNotes", Value = item.AutoNotes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CreatedBy", Value = item.CreatedBy }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ServedAs", Value = item.ServedAs }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@ServedAsQuantified", Value = item.ServedAsQuantified }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ServedAsQuantity", Value = item.ServedAsQuantity }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MenuName", Value = item.MenuName }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Gratuity", Value = item.Gratuity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@GratuityPerc", Value = item.GratuityPerc }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLine>(Sync_Translator.Translate_TabLine);
                        Log.Information("TabLine found: TabLineID={TabLineID}, FK_TabID={FK_TabID}, FK_ProductID={FK_ProductID}, FK_PriceCodeID={FK_PriceCodeID}, FK_PointerID={FK_PointerID}, UnitCostExcl={UnitCostExcl}, Vat={Vat}, UnitCostIncl={UnitCostIncl}, Product={Product}, Quantity={Quantity}, Discount={Discount}, DiscountPerc={DiscountPerc}, IsVoided={IsVoided}, Notes={Notes}, AutoNotes={AutoNotes}, CreatedBy={CreatedBy}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, ServedAs={ServedAs}, ServedAsQuantified={ServedAsQuantified}, ServedAsQuantity={ServedAsQuantity}, FK_MenuID={FK_MenuID}, MenuName={MenuName}, Gratuity={Gratuity}, GratuityPerc={GratuityPerc}, FK_CostCenterID={FK_CostCenterID}", resultItem.TabLineID, resultItem.FK_TabID, resultItem.FK_ProductID, resultItem.FK_PriceCodeID, resultItem.FK_PointerID, resultItem.UnitCostExcl, resultItem.Vat, resultItem.UnitCostIncl, resultItem.Product, resultItem.Quantity, resultItem.Discount, resultItem.DiscountPerc, resultItem.IsVoided, resultItem.Notes, resultItem.AutoNotes, resultItem.CreatedBy, resultItem.DateCreated, resultItem.DateUpdated, resultItem.ServedAs, resultItem.ServedAsQuantified, resultItem.ServedAsQuantity, resultItem.FK_MenuID, resultItem.MenuName, resultItem.Gratuity, resultItem.GratuityPerc, resultItem.FK_CostCenterID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLine failed to update.");
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

        #region POS_TablineSubstitutes

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Select_Single_Transaction(TabLineSubstitute item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TablineSubstitutes_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Select_Single(TabLineSubstitute item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TablineSubstitutes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Select_Single(TabLineSubstitute item, SqlConnection sqlConn)
        {
            try
            {
                TabLineSubstitute resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TablineSubstitutes_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TablineSubstituteID", Value = item.TablineSubstituteID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineSubstitute>(Sync_Translator.Translate_TabLineSubstitute);
                        Log.Information("TabLineSubstitute found: TablineSubstituteID={TablineSubstituteID}, FK_ParentTabLineID={FK_ParentTabLineID}, FK_SubstituionTabLineID={FK_SubstituionTabLineID}, FK_ParentTabLineCombinationID={FK_ParentTabLineCombinationID}", resultItem.TablineSubstituteID, resultItem.FK_ParentTabLineID, resultItem.FK_SubstituionTabLineID, resultItem.FK_ParentTabLineCombinationID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLineSubstitute found with the given TabLineSubstituteID.");
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

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Insert_Transaction(TabLineSubstitute item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TablineSubstitutes_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Insert(TabLineSubstitute item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TablineSubstitutes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Insert(TabLineSubstitute item, SqlConnection sqlConn)
        {
            try
            {
                TabLineSubstitute resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TablineSubstitutes_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TablineSubstituteID", Value = item.TablineSubstituteID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_ParentTabLineID", Value = item.FK_ParentTabLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_SubstituionTabLineID", Value = item.FK_SubstituionTabLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_ParentTabLineCombinationID", Value = item.FK_ParentTabLineCombinationID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineSubstitute>(Sync_Translator.Translate_TabLineSubstitute);
                        Log.Information("TabLineSubstitute found: TablineSubstituteID={TablineSubstituteID}, FK_ParentTabLineID={FK_ParentTabLineID}, FK_SubstituionTabLineID={FK_SubstituionTabLineID}, FK_ParentTabLineCombinationID={FK_ParentTabLineCombinationID}", resultItem.TablineSubstituteID, resultItem.FK_ParentTabLineID, resultItem.FK_SubstituionTabLineID, resultItem.FK_ParentTabLineCombinationID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLineSubstitute failed to create.");
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

        public static async Task<List<TabLineSubstitute>> POS_TablineSubstitutes_Select_All_Transaction(TabLineSubstitute item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TablineSubstitutes_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLineSubstitute>> POS_TablineSubstitutes_Select_All(TabLineSubstitute item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TablineSubstitutes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TabLineSubstitute>> POS_TablineSubstitutes_Select_All(TabLineSubstitute item, SqlConnection sqlConn)
        {
            try
            {
                List<TabLineSubstitute> resultItem = new List<TabLineSubstitute>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TablineSubstitutes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<TabLineSubstitute>(Sync_Translator.Translate_TabLineSubstitute));
                        Log.Information("TabLineSubstitute records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TabLineSubstitute records found.");
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

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Update_Transaction(TabLineSubstitute item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TablineSubstitutes_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Update(TabLineSubstitute item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TablineSubstitutes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TabLineSubstitute> POS_TablineSubstitutes_Update(TabLineSubstitute item, SqlConnection sqlConn)
        {
            try
            {
                TabLineSubstitute resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TablineSubstitutes_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TablineSubstituteID", Value = item.TablineSubstituteID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_ParentTabLineID", Value = item.FK_ParentTabLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_SubstituionTabLineID", Value = item.FK_SubstituionTabLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_ParentTabLineCombinationID", Value = item.FK_ParentTabLineCombinationID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TabLineSubstitute>(Sync_Translator.Translate_TabLineSubstitute);
                        Log.Information("TabLineSubstitute found: TablineSubstituteID={TablineSubstituteID}, FK_ParentTabLineID={FK_ParentTabLineID}, FK_SubstituionTabLineID={FK_SubstituionTabLineID}, FK_ParentTabLineCombinationID={FK_ParentTabLineCombinationID}", resultItem.TablineSubstituteID, resultItem.FK_ParentTabLineID, resultItem.FK_SubstituionTabLineID, resultItem.FK_ParentTabLineCombinationID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TabLineSubstitute failed to update.");
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

        #region POS_Tabs

        public static async Task<Tab> POS_Tabs_Select_Single_Transaction(Tab item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Tabs_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Tab> POS_Tabs_Select_Single(Tab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Tabs_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Tab> POS_Tabs_Select_Single(Tab item, SqlConnection sqlConn)
        {
            try
            {
                Tab resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Tabs_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabID", Value = item.TabID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Tab>(Sync_Translator.Translate_Tab);
                        Log.Information("Tab found: TabID={TabID}, FK_LocationID={FK_LocationID}, FK_AccountID={FK_AccountID}, FK_CostCenterID={FK_CostCenterID}, FK_PaymentTypeID={FK_PaymentTypeID}, FK_CurrencyID={FK_CurrencyID}, TabName={TabName}, TableName={TableName}, NoOfGuests={NoOfGuests}, Gratuity={Gratuity}, GratuityPerc={GratuityPerc}, Discount={Discount}, DiscountPerc={DiscountPerc}, IsVoided={IsVoided}, VoidNote={VoidNote}, IsPaid={IsPaid}, AmountPaid={AmountPaid}, AmountDue={AmountDue}, VatTotal={VatTotal}, CurrentExchangeRate={CurrentExchangeRate}, PaymentDate={PaymentDate}, ClosedDate={ClosedDate}, AdditionalInfo={AdditionalInfo}, CreatedBy={CreatedBy}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, TableNumber={TableNumber}", resultItem.TabID, resultItem.FK_LocationID, resultItem.FK_AccountID, resultItem.FK_CostCenterID, resultItem.FK_PaymentTypeID, resultItem.FK_CurrencyID, resultItem.TabName, resultItem.TableName, resultItem.NoOfGuests, resultItem.Gratuity, resultItem.GratuityPerc, resultItem.Discount, resultItem.DiscountPerc, resultItem.IsVoided, resultItem.VoidNote, resultItem.IsPaid, resultItem.AmountPaid, resultItem.AmountDue, resultItem.VatTotal, resultItem.CurrentExchangeRate, resultItem.PaymentDate, resultItem.ClosedDate, resultItem.AdditionalInfo, resultItem.CreatedBy, resultItem.DateCreated, resultItem.DateUpdated, resultItem.TableNumber);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Tab found with the given TabID.");
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

        public static async Task<Tab> POS_Tabs_Insert_Transaction(Tab item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Tabs_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Tab> POS_Tabs_Insert(Tab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Tabs_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Tab> POS_Tabs_Insert(Tab item, SqlConnection sqlConn)
        {
            try
            {
                Tab resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Tabs_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabID", Value = item.TabID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_AccountID", Value = item.FK_AccountID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentTypeID", Value = item.FK_PaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TabName", Value = item.TabName }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TableName", Value = item.TableName }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@NoOfGuests", Value = item.NoOfGuests }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Gratuity", Value = item.Gratuity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@GratuityPerc", Value = item.GratuityPerc }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Discount", Value = item.Discount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@DiscountPerc", Value = item.DiscountPerc }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVoided", Value = item.IsVoided }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidNote", Value = item.VoidNote }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPaid", Value = item.IsPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@AmountPaid", Value = item.AmountPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@AmountDue", Value = item.AmountDue }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@VatTotal", Value = item.VatTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@CurrentExchangeRate", Value = item.CurrentExchangeRate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@PaymentDate", Value = item.PaymentDate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ClosedDate", Value = item.ClosedDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@AdditionalInfo", Value = item.AdditionalInfo }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CreatedBy", Value = item.CreatedBy }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TableNumber", Value = item.TableNumber }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Tab>(Sync_Translator.Translate_Tab);
                        Log.Information("Tab found: TabID={TabID}, FK_LocationID={FK_LocationID}, FK_AccountID={FK_AccountID}, FK_CostCenterID={FK_CostCenterID}, FK_PaymentTypeID={FK_PaymentTypeID}, FK_CurrencyID={FK_CurrencyID}, TabName={TabName}, TableName={TableName}, NoOfGuests={NoOfGuests}, Gratuity={Gratuity}, GratuityPerc={GratuityPerc}, Discount={Discount}, DiscountPerc={DiscountPerc}, IsVoided={IsVoided}, VoidNote={VoidNote}, IsPaid={IsPaid}, AmountPaid={AmountPaid}, AmountDue={AmountDue}, VatTotal={VatTotal}, CurrentExchangeRate={CurrentExchangeRate}, PaymentDate={PaymentDate}, ClosedDate={ClosedDate}, AdditionalInfo={AdditionalInfo}, CreatedBy={CreatedBy}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, TableNumber={TableNumber}", resultItem.TabID, resultItem.FK_LocationID, resultItem.FK_AccountID, resultItem.FK_CostCenterID, resultItem.FK_PaymentTypeID, resultItem.FK_CurrencyID, resultItem.TabName, resultItem.TableName, resultItem.NoOfGuests, resultItem.Gratuity, resultItem.GratuityPerc, resultItem.Discount, resultItem.DiscountPerc, resultItem.IsVoided, resultItem.VoidNote, resultItem.IsPaid, resultItem.AmountPaid, resultItem.AmountDue, resultItem.VatTotal, resultItem.CurrentExchangeRate, resultItem.PaymentDate, resultItem.ClosedDate, resultItem.AdditionalInfo, resultItem.CreatedBy, resultItem.DateCreated, resultItem.DateUpdated, resultItem.TableNumber);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Tab failed to create.");
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

        public static async Task<List<Tab>> POS_Tabs_Select_All_Transaction(Tab item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Tabs_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Tab>> POS_Tabs_Select_All(Tab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Tabs_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Tab>> POS_Tabs_Select_All(Tab item, SqlConnection sqlConn)
        {
            try
            {
                List<Tab> resultItem = new List<Tab>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Tabs_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Tab>(Sync_Translator.Translate_Tab));
                        Log.Information("Tab records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Tab records found.");
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

        public static async Task<Tab> POS_Tabs_Update_Transaction(Tab item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Tabs_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Tab> POS_Tabs_Update(Tab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Tabs_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Tab> POS_Tabs_Update(Tab item, SqlConnection sqlConn)
        {
            try
            {
                Tab resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Tabs_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@TabID", Value = item.TabID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_AccountID", Value = item.FK_AccountID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentTypeID", Value = item.FK_PaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TabName", Value = item.TabName }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TableName", Value = item.TableName }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@NoOfGuests", Value = item.NoOfGuests }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Gratuity", Value = item.Gratuity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@GratuityPerc", Value = item.GratuityPerc }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Discount", Value = item.Discount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@DiscountPerc", Value = item.DiscountPerc }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVoided", Value = item.IsVoided }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidNote", Value = item.VoidNote }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPaid", Value = item.IsPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@AmountPaid", Value = item.AmountPaid }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@AmountDue", Value = item.AmountDue }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@VatTotal", Value = item.VatTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@CurrentExchangeRate", Value = item.CurrentExchangeRate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@PaymentDate", Value = item.PaymentDate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ClosedDate", Value = item.ClosedDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@AdditionalInfo", Value = item.AdditionalInfo }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CreatedBy", Value = item.CreatedBy }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TableNumber", Value = item.TableNumber }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Tab>(Sync_Translator.Translate_Tab);
                        Log.Information("Tab found: TabID={TabID}, FK_LocationID={FK_LocationID}, FK_AccountID={FK_AccountID}, FK_CostCenterID={FK_CostCenterID}, FK_PaymentTypeID={FK_PaymentTypeID}, FK_CurrencyID={FK_CurrencyID}, TabName={TabName}, TableName={TableName}, NoOfGuests={NoOfGuests}, Gratuity={Gratuity}, GratuityPerc={GratuityPerc}, Discount={Discount}, DiscountPerc={DiscountPerc}, IsVoided={IsVoided}, VoidNote={VoidNote}, IsPaid={IsPaid}, AmountPaid={AmountPaid}, AmountDue={AmountDue}, VatTotal={VatTotal}, CurrentExchangeRate={CurrentExchangeRate}, PaymentDate={PaymentDate}, ClosedDate={ClosedDate}, AdditionalInfo={AdditionalInfo}, CreatedBy={CreatedBy}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, TableNumber={TableNumber}", resultItem.TabID, resultItem.FK_LocationID, resultItem.FK_AccountID, resultItem.FK_CostCenterID, resultItem.FK_PaymentTypeID, resultItem.FK_CurrencyID, resultItem.TabName, resultItem.TableName, resultItem.NoOfGuests, resultItem.Gratuity, resultItem.GratuityPerc, resultItem.Discount, resultItem.DiscountPerc, resultItem.IsVoided, resultItem.VoidNote, resultItem.IsPaid, resultItem.AmountPaid, resultItem.AmountDue, resultItem.VatTotal, resultItem.CurrentExchangeRate, resultItem.PaymentDate, resultItem.ClosedDate, resultItem.AdditionalInfo, resultItem.CreatedBy, resultItem.DateCreated, resultItem.DateUpdated, resultItem.TableNumber);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Tab failed to update.");
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

        #region POS_VoidLogs

        public static async Task<VoidLog> POS_VoidLogs_Select_Single_Transaction(VoidLog item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_VoidLogs_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<VoidLog> POS_VoidLogs_Select_Single(VoidLog item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_VoidLogs_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<VoidLog> POS_VoidLogs_Select_Single(VoidLog item, SqlConnection sqlConn)
        {
            try
            {
                VoidLog resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_VoidLogs_select_single",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@VoidLogID", Value = item.VoidLogID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<VoidLog>(Sync_Translator.Translate_VoidLog);
                        Log.Information("VoidLog found: VoidLogID={VoidLogID}, FK_TabID={FK_TabID}, FK_TabLineID={FK_TabLineID}, VoidedBy={VoidedBy}, Note={Note}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.VoidLogID, resultItem.FK_TabID, resultItem.FK_TabLineID, resultItem.VoidedBy, resultItem.Note, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No VoidLog found with the given VoidLogID.");
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

        public static async Task<VoidLog> POS_VoidLogs_Insert_Transaction(VoidLog item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_VoidLogs_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<VoidLog> POS_VoidLogs_Insert(VoidLog item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_VoidLogs_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<VoidLog> POS_VoidLogs_Insert(VoidLog item, SqlConnection sqlConn)
        {
            try
            {
                VoidLog resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_VoidLogs_insert",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@VoidLogID", Value = item.VoidLogID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabID", Value = item.FK_TabID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineID", Value = item.FK_TabLineID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidedBy", Value = item.VoidedBy }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Note", Value = item.Note }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<VoidLog>(Sync_Translator.Translate_VoidLog);
                        Log.Information("VoidLog found: VoidLogID={VoidLogID}, FK_TabID={FK_TabID}, FK_TabLineID={FK_TabLineID}, VoidedBy={VoidedBy}, Note={Note}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.VoidLogID, resultItem.FK_TabID, resultItem.FK_TabLineID, resultItem.VoidedBy, resultItem.Note, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("VoidLog failed to create.");
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

        public static async Task<List<VoidLog>> POS_VoidLogs_Select_All_Transaction(VoidLog item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_VoidLogs_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<VoidLog>> POS_VoidLogs_Select_All(VoidLog item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_VoidLogs_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<VoidLog>> POS_VoidLogs_Select_All(VoidLog item, SqlConnection sqlConn)
        {
            try
            {
                List<VoidLog> resultItem = new List<VoidLog>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_VoidLogs_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<VoidLog>(Sync_Translator.Translate_VoidLog));
                        Log.Information("VoidLog records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No VoidLog records found.");
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

        public static async Task<VoidLog> POS_VoidLogs_Update_Transaction(VoidLog item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_VoidLogs_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<VoidLog> POS_VoidLogs_Update(VoidLog item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_VoidLogs_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<VoidLog> POS_VoidLogs_Update(VoidLog item, SqlConnection sqlConn)
        {
            try
            {
                VoidLog resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_VoidLogs_update",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@VoidLogID", Value = item.VoidLogID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabID", Value = item.FK_TabID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabLineID", Value = item.FK_TabLineID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VoidedBy", Value = item.VoidedBy }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Note", Value = item.Note }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<VoidLog>(Sync_Translator.Translate_VoidLog);
                        Log.Information("VoidLog found: VoidLogID={VoidLogID}, FK_TabID={FK_TabID}, FK_TabLineID={FK_TabLineID}, VoidedBy={VoidedBy}, Note={Note}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.VoidLogID, resultItem.FK_TabID, resultItem.FK_TabLineID, resultItem.VoidedBy, resultItem.Note, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("VoidLog failed to update.");
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

        #region SiteSyncStatus

        public static async Task<SiteSyncStatus> SiteSyncStatus_Select_Single_Transaction(SiteSyncStatus item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SiteSyncStatus_Select_Single(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SiteSyncStatus> SiteSyncStatus_Select_Single(SiteSyncStatus item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SiteSyncStatus_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SiteSyncStatus> SiteSyncStatus_Select_Single(SiteSyncStatus item, SqlConnection sqlConn)
        {
            try
            {
                SiteSyncStatus resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "SiteSyncStatus_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = item.SiteId }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TypeName", Value = item.TypeName }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SiteSyncStatus>(Sync_Translator.Translate_SiteSyncStatus);
                        Log.Information("SiteSyncStatus found: SiteId={SiteId}, TypeName={TypeName}, LastSuccessAt={LastSuccessAt}, LastFailureAt={LastFailureAt}, ConsecutiveFailures={ConsecutiveFailures}, LastErrorMessage={LastErrorMessage}, LastReportedAt={LastReportedAt}, AlertSentAt={AlertSentAt}", resultItem.SiteId, resultItem.TypeName, resultItem.LastSuccessAt, resultItem.LastFailureAt, resultItem.ConsecutiveFailures, resultItem.LastErrorMessage, resultItem.LastReportedAt, resultItem.AlertSentAt);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No SiteSyncStatus found with the given SiteSyncStatusID.");
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

        public static async Task<SiteSyncStatus> SiteSyncStatus_Insert_Transaction(SiteSyncStatus item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SiteSyncStatus_Insert(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SiteSyncStatus> SiteSyncStatus_Insert(SiteSyncStatus item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SiteSyncStatus_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SiteSyncStatus> SiteSyncStatus_Insert(SiteSyncStatus item, SqlConnection sqlConn)
        {
            try
            {
                SiteSyncStatus resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "SiteSyncStatus_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = item.SiteId }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TypeName", Value = item.TypeName }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@LastSuccessAt", Value = item.LastSuccessAt }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@LastFailureAt", Value = item.LastFailureAt }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ConsecutiveFailures", Value = item.ConsecutiveFailures }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LastErrorMessage", Value = item.LastErrorMessage }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@LastReportedAt", Value = item.LastReportedAt }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@AlertSentAt", Value = item.AlertSentAt }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SiteSyncStatus>(Sync_Translator.Translate_SiteSyncStatus);
                        Log.Information("SiteSyncStatus found: SiteId={SiteId}, TypeName={TypeName}, LastSuccessAt={LastSuccessAt}, LastFailureAt={LastFailureAt}, ConsecutiveFailures={ConsecutiveFailures}, LastErrorMessage={LastErrorMessage}, LastReportedAt={LastReportedAt}, AlertSentAt={AlertSentAt}", resultItem.SiteId, resultItem.TypeName, resultItem.LastSuccessAt, resultItem.LastFailureAt, resultItem.ConsecutiveFailures, resultItem.LastErrorMessage, resultItem.LastReportedAt, resultItem.AlertSentAt);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("SiteSyncStatus failed to create.");
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

        public static async Task<List<SiteSyncStatus>> SiteSyncStatus_Select_All_Transaction(SiteSyncStatus item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SiteSyncStatus_Select_All(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<SiteSyncStatus>> SiteSyncStatus_Select_All(SiteSyncStatus item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SiteSyncStatus_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<SiteSyncStatus>> SiteSyncStatus_Select_All(SiteSyncStatus item, SqlConnection sqlConn)
        {
            try
            {
                List<SiteSyncStatus> resultItem = new List<SiteSyncStatus>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "SiteSyncStatus_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<SiteSyncStatus>(Sync_Translator.Translate_SiteSyncStatus));
                        Log.Information("SiteSyncStatus records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No SiteSyncStatus records found.");
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

        public static async Task<SiteSyncStatus> SiteSyncStatus_Update_Transaction(SiteSyncStatus item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SiteSyncStatus_Update(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SiteSyncStatus> SiteSyncStatus_Update(SiteSyncStatus item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SiteSyncStatus_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SiteSyncStatus> SiteSyncStatus_Update(SiteSyncStatus item, SqlConnection sqlConn)
        {
            try
            {
                SiteSyncStatus resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "SiteSyncStatus_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = item.SiteId }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TypeName", Value = item.TypeName }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@LastSuccessAt", Value = item.LastSuccessAt }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@LastFailureAt", Value = item.LastFailureAt }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ConsecutiveFailures", Value = item.ConsecutiveFailures }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LastErrorMessage", Value = item.LastErrorMessage }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@LastReportedAt", Value = item.LastReportedAt }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@AlertSentAt", Value = item.AlertSentAt }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SiteSyncStatus>(Sync_Translator.Translate_SiteSyncStatus);
                        Log.Information("SiteSyncStatus found: SiteId={SiteId}, TypeName={TypeName}, LastSuccessAt={LastSuccessAt}, LastFailureAt={LastFailureAt}, ConsecutiveFailures={ConsecutiveFailures}, LastErrorMessage={LastErrorMessage}, LastReportedAt={LastReportedAt}, AlertSentAt={AlertSentAt}", resultItem.SiteId, resultItem.TypeName, resultItem.LastSuccessAt, resultItem.LastFailureAt, resultItem.ConsecutiveFailures, resultItem.LastErrorMessage, resultItem.LastReportedAt, resultItem.AlertSentAt);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("SiteSyncStatus failed to update.");
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

    }
}
