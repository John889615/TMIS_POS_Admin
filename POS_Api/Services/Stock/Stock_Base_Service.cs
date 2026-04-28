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

using POS_Common.Models.Stock.POS_PurchaseOrders;
using POS_Common.Models.Stock.POS_PurchaseOrderLines;
using POS_Common.Models.Stock.POS_StockReceive;
using POS_Common.Models.Stock.POS_StockReceiveLines;
using POS_Common.Models.Stock.POS_StockRequests;
using POS_Common.Models.Stock.POS_StockRequestLines;
using POS_Common.Models.Stock.POS_StockTransfers;
using POS_Common.Models.Stock.POS_StockTransferLines;
using POS_Common.Models.Stock.POS_CostCenterProducts;
using POS_Common.Models.Stock.POS_SupplierProducts;
using POS_Common.Models.Stock.POS_DebtorProducts;
using POS_Common.Models.Stock.POS_InternalStockTransfers;
using POS_Common.Models.Stock.POS_InternalStockTransferLines;
using POS_Common.Models.Stock.POS_DebtorProductPriceHistory;
using POS_Common.Models.Stock.POS_CostCenterProductPriceHistory;
using POS_Common.Models.Stock.POS_PriceCodes;
using POS_Common.Models.Stock.POS_DebtorProductPrices;

namespace POS_Api.Services.Stock
{
    public abstract class Stock_Base_Service
    {
        #region POS_PurchaseOrders

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Select_Single_Transaction(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrders_Select_Single(item, sqlConn);
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

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Select_Single(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrders_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Select_Single(PurchaseOrder item, SqlConnection sqlConn)
        {
            try
            {
                PurchaseOrder resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PurchaseOrders_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PurchaseOrderID", Value = item.PurchaseOrderID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PurchaseOrder>(Stock_Translator.Translate_PurchaseOrder);
                        Log.Information("PurchaseOrder found: PurchaseOrderID={PurchaseOrderID}, OrderNumber={OrderNumber}, FK_SupplierID={FK_SupplierID}, FK_DebtorID={FK_DebtorID}, FK_CostCenterID={FK_CostCenterID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, Notes={Notes}, ManagerNotes={ManagerNotes}, DateOrdered={DateOrdered}, DateUpdated={DateUpdated}", resultItem.PurchaseOrderID, resultItem.OrderNumber, resultItem.FK_SupplierID, resultItem.FK_DebtorID, resultItem.FK_CostCenterID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.Notes, resultItem.ManagerNotes, resultItem.DateOrdered, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PurchaseOrder found with the given PurchaseOrderID.");
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

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Insert_Transaction(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrders_Insert(item, sqlConn);
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

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Insert(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrders_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Insert(PurchaseOrder item, SqlConnection sqlConn)
        {
            try
            {
                PurchaseOrder resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PurchaseOrders_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@OrderNumber", Value = item.OrderNumber }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_SupplierID", Value = item.FK_SupplierID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorID", Value = item.FK_DebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderStatusID", Value = item.FK_OrderStatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ManagerNotes", Value = item.ManagerNotes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateOrdered", Value = item.DateOrdered }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PurchaseOrder>(Stock_Translator.Translate_PurchaseOrder);
                        Log.Information("PurchaseOrder found: PurchaseOrderID={PurchaseOrderID}, OrderNumber={OrderNumber}, FK_SupplierID={FK_SupplierID}, FK_DebtorID={FK_DebtorID}, FK_CostCenterID={FK_CostCenterID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, Notes={Notes}, ManagerNotes={ManagerNotes}, DateOrdered={DateOrdered}, DateUpdated={DateUpdated}", resultItem.PurchaseOrderID, resultItem.OrderNumber, resultItem.FK_SupplierID, resultItem.FK_DebtorID, resultItem.FK_CostCenterID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.Notes, resultItem.ManagerNotes, resultItem.DateOrdered, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PurchaseOrder failed to create.");
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

        public static async Task<List<PurchaseOrder>> POS_PurchaseOrders_Select_All_Transaction(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrders_Select_All(item, sqlConn);
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

        public static async Task<List<PurchaseOrder>> POS_PurchaseOrders_Select_All(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrders_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PurchaseOrder>> POS_PurchaseOrders_Select_All(PurchaseOrder item, SqlConnection sqlConn)
        {
            try
            {
                List<PurchaseOrder> resultItem = new List<PurchaseOrder>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PurchaseOrders_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PurchaseOrder>(Stock_Translator.Translate_PurchaseOrder));
                        Log.Information("PurchaseOrder records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PurchaseOrder records found.");
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

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Update_Transaction(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrders_Update(item, sqlConn);
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

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Update(PurchaseOrder item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrders_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrder> POS_PurchaseOrders_Update(PurchaseOrder item, SqlConnection sqlConn)
        {
            try
            {
                PurchaseOrder resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PurchaseOrders_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PurchaseOrderID", Value = item.PurchaseOrderID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@OrderNumber", Value = item.OrderNumber }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_SupplierID", Value = item.FK_SupplierID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorID", Value = item.FK_DebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderStatusID", Value = item.FK_OrderStatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ManagerNotes", Value = item.ManagerNotes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateOrdered", Value = item.DateOrdered }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PurchaseOrder>(Stock_Translator.Translate_PurchaseOrder);
                        Log.Information("PurchaseOrder found: PurchaseOrderID={PurchaseOrderID}, OrderNumber={OrderNumber}, FK_SupplierID={FK_SupplierID}, FK_DebtorID={FK_DebtorID}, FK_CostCenterID={FK_CostCenterID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, Notes={Notes}, ManagerNotes={ManagerNotes}, DateOrdered={DateOrdered}, DateUpdated={DateUpdated}", resultItem.PurchaseOrderID, resultItem.OrderNumber, resultItem.FK_SupplierID, resultItem.FK_DebtorID, resultItem.FK_CostCenterID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.Notes, resultItem.ManagerNotes, resultItem.DateOrdered, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PurchaseOrder failed to update.");
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

        #region POS_PurchaseOrderLines

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Select_Single_Transaction(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrderLines_Select_Single(item, sqlConn);
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

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Select_Single(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrderLines_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Select_Single(PurchaseOrderLine item, SqlConnection sqlConn)
        {
            try
            {
                PurchaseOrderLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PurchaseOrderLines_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PurchaseOrderLineID", Value = item.PurchaseOrderLineID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PurchaseOrderLine>(Stock_Translator.Translate_PurchaseOrderLine);
                        Log.Information("PurchaseOrderLine found: PurchaseOrderLineID={PurchaseOrderLineID}, FK_PurchaseOrderID={FK_PurchaseOrderID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, UnitCostIncl={UnitCostIncl}, UnitCostExcl={UnitCostExcl}, FK_TaxTypeID={FK_TaxTypeID}, TaxRate={TaxRate}, TotalCostIncl={TotalCostIncl}, TotalCostExcl={TotalCostExcl}, Notes={Notes}, ManagerNotes={ManagerNotes}, IsDeclined={IsDeclined}", resultItem.PurchaseOrderLineID, resultItem.FK_PurchaseOrderID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.UnitCostIncl, resultItem.UnitCostExcl, resultItem.FK_TaxTypeID, resultItem.TaxRate, resultItem.TotalCostIncl, resultItem.TotalCostExcl, resultItem.Notes, resultItem.ManagerNotes, resultItem.IsDeclined);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PurchaseOrderLine found with the given PurchaseOrderLineID.");
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

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Insert_Transaction(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrderLines_Insert(item, sqlConn);
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

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Insert(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrderLines_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Insert(PurchaseOrderLine item, SqlConnection sqlConn)
        {
            try
            {
                PurchaseOrderLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PurchaseOrderLines_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PurchaseOrderID", Value = item.FK_PurchaseOrderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostIncl", Value = item.UnitCostIncl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostExcl", Value = item.UnitCostExcl }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxTypeID", Value = item.FK_TaxTypeID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TaxRate", Value = item.TaxRate }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCostIncl", Value = item.TotalCostIncl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCostExcl", Value = item.TotalCostExcl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ManagerNotes", Value = item.ManagerNotes }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDeclined", Value = item.IsDeclined }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PurchaseOrderLine>(Stock_Translator.Translate_PurchaseOrderLine);
                        Log.Information("PurchaseOrderLine found: PurchaseOrderLineID={PurchaseOrderLineID}, FK_PurchaseOrderID={FK_PurchaseOrderID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, UnitCostIncl={UnitCostIncl}, UnitCostExcl={UnitCostExcl}, FK_TaxTypeID={FK_TaxTypeID}, TaxRate={TaxRate}, TotalCostIncl={TotalCostIncl}, TotalCostExcl={TotalCostExcl}, Notes={Notes}, ManagerNotes={ManagerNotes}, IsDeclined={IsDeclined}", resultItem.PurchaseOrderLineID, resultItem.FK_PurchaseOrderID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.UnitCostIncl, resultItem.UnitCostExcl, resultItem.FK_TaxTypeID, resultItem.TaxRate, resultItem.TotalCostIncl, resultItem.TotalCostExcl, resultItem.Notes, resultItem.ManagerNotes, resultItem.IsDeclined);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PurchaseOrderLine failed to create.");
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

        public static async Task<List<PurchaseOrderLine>> POS_PurchaseOrderLines_Select_All_Transaction(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrderLines_Select_All(item, sqlConn);
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

        public static async Task<List<PurchaseOrderLine>> POS_PurchaseOrderLines_Select_All(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrderLines_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PurchaseOrderLine>> POS_PurchaseOrderLines_Select_All(PurchaseOrderLine item, SqlConnection sqlConn)
        {
            try
            {
                List<PurchaseOrderLine> resultItem = new List<PurchaseOrderLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PurchaseOrderLines_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PurchaseOrderLine>(Stock_Translator.Translate_PurchaseOrderLine));
                        Log.Information("PurchaseOrderLine records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PurchaseOrderLine records found.");
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

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Update_Transaction(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrderLines_Update(item, sqlConn);
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

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Update(PurchaseOrderLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PurchaseOrderLines_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PurchaseOrderLine> POS_PurchaseOrderLines_Update(PurchaseOrderLine item, SqlConnection sqlConn)
        {
            try
            {
                PurchaseOrderLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PurchaseOrderLines_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PurchaseOrderLineID", Value = item.PurchaseOrderLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PurchaseOrderID", Value = item.FK_PurchaseOrderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostIncl", Value = item.UnitCostIncl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostExcl", Value = item.UnitCostExcl }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxTypeID", Value = item.FK_TaxTypeID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TaxRate", Value = item.TaxRate }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCostIncl", Value = item.TotalCostIncl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCostExcl", Value = item.TotalCostExcl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ManagerNotes", Value = item.ManagerNotes }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDeclined", Value = item.IsDeclined }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PurchaseOrderLine>(Stock_Translator.Translate_PurchaseOrderLine);
                        Log.Information("PurchaseOrderLine found: PurchaseOrderLineID={PurchaseOrderLineID}, FK_PurchaseOrderID={FK_PurchaseOrderID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, UnitCostIncl={UnitCostIncl}, UnitCostExcl={UnitCostExcl}, FK_TaxTypeID={FK_TaxTypeID}, TaxRate={TaxRate}, TotalCostIncl={TotalCostIncl}, TotalCostExcl={TotalCostExcl}, Notes={Notes}, ManagerNotes={ManagerNotes}, IsDeclined={IsDeclined}", resultItem.PurchaseOrderLineID, resultItem.FK_PurchaseOrderID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.UnitCostIncl, resultItem.UnitCostExcl, resultItem.FK_TaxTypeID, resultItem.TaxRate, resultItem.TotalCostIncl, resultItem.TotalCostExcl, resultItem.Notes, resultItem.ManagerNotes, resultItem.IsDeclined);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PurchaseOrderLine failed to update.");
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

        #region POS_StockReceive

        public static async Task<StockReceive> POS_StockReceive_Select_Single_Transaction(StockReceive item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceive_Select_Single(item, sqlConn);
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

        public static async Task<StockReceive> POS_StockReceive_Select_Single(StockReceive item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceive_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockReceive> POS_StockReceive_Select_Single(StockReceive item, SqlConnection sqlConn)
        {
            try
            {
                StockReceive resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockReceive_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockReceiveID", Value = item.StockReceiveID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockReceive>(Stock_Translator.Translate_StockReceive);
                        Log.Information("StockReceive found: StockReceiveID={StockReceiveID}, FK_PurchaseOrderID={FK_PurchaseOrderID}, FK_StockTransferID={FK_StockTransferID}, FK_UserID={FK_UserID}, Notes={Notes}, DateReceived={DateReceived}", resultItem.StockReceiveID, resultItem.FK_PurchaseOrderID, resultItem.FK_StockTransferID, resultItem.FK_UserID, resultItem.Notes, resultItem.DateReceived);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockReceive found with the given StockReceiveID.");
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

        public static async Task<StockReceive> POS_StockReceive_Insert_Transaction(StockReceive item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceive_Insert(item, sqlConn);
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

        public static async Task<StockReceive> POS_StockReceive_Insert(StockReceive item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceive_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockReceive> POS_StockReceive_Insert(StockReceive item, SqlConnection sqlConn)
        {
            try
            {
                StockReceive resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockReceive_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PurchaseOrderID", Value = item.FK_PurchaseOrderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockTransferID", Value = item.FK_StockTransferID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateReceived", Value = item.DateReceived }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockReceive>(Stock_Translator.Translate_StockReceive);
                        Log.Information("StockReceive found: StockReceiveID={StockReceiveID}, FK_PurchaseOrderID={FK_PurchaseOrderID}, FK_StockTransferID={FK_StockTransferID}, FK_UserID={FK_UserID}, Notes={Notes}, DateReceived={DateReceived}", resultItem.StockReceiveID, resultItem.FK_PurchaseOrderID, resultItem.FK_StockTransferID, resultItem.FK_UserID, resultItem.Notes, resultItem.DateReceived);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockReceive failed to create.");
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

        public static async Task<List<StockReceive>> POS_StockReceive_Select_All_Transaction(StockReceive item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceive_Select_All(item, sqlConn);
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

        public static async Task<List<StockReceive>> POS_StockReceive_Select_All(StockReceive item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceive_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockReceive>> POS_StockReceive_Select_All(StockReceive item, SqlConnection sqlConn)
        {
            try
            {
                List<StockReceive> resultItem = new List<StockReceive>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockReceive_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockReceive>(Stock_Translator.Translate_StockReceive));
                        Log.Information("StockReceive records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockReceive records found.");
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

        public static async Task<StockReceive> POS_StockReceive_Update_Transaction(StockReceive item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceive_Update(item, sqlConn);
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

        public static async Task<StockReceive> POS_StockReceive_Update(StockReceive item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceive_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockReceive> POS_StockReceive_Update(StockReceive item, SqlConnection sqlConn)
        {
            try
            {
                StockReceive resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockReceive_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockReceiveID", Value = item.StockReceiveID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PurchaseOrderID", Value = item.FK_PurchaseOrderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockTransferID", Value = item.FK_StockTransferID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateReceived", Value = item.DateReceived }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockReceive>(Stock_Translator.Translate_StockReceive);
                        Log.Information("StockReceive found: StockReceiveID={StockReceiveID}, FK_PurchaseOrderID={FK_PurchaseOrderID}, FK_StockTransferID={FK_StockTransferID}, FK_UserID={FK_UserID}, Notes={Notes}, DateReceived={DateReceived}", resultItem.StockReceiveID, resultItem.FK_PurchaseOrderID, resultItem.FK_StockTransferID, resultItem.FK_UserID, resultItem.Notes, resultItem.DateReceived);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockReceive failed to update.");
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

        #region POS_StockReceiveLines

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Select_Single_Transaction(StockReceiveLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceiveLines_Select_Single(item, sqlConn);
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

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Select_Single(StockReceiveLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceiveLines_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Select_Single(StockReceiveLine item, SqlConnection sqlConn)
        {
            try
            {
                StockReceiveLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockReceiveLines_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockReceiveLineID", Value = item.StockReceiveLineID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockReceiveLine>(Stock_Translator.Translate_StockReceiveLine);
                        Log.Information("StockReceiveLine found: StockReceiveLineID={StockReceiveLineID}, FK_StockReceiveID={FK_StockReceiveID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, UnitCostIncl={UnitCostIncl}, UnitCostExcl={UnitCostExcl}, FK_TaxTypeID={FK_TaxTypeID}, TaxRate={TaxRate}, TotalCostIncl={TotalCostIncl}, TotalCostExcl={TotalCostExcl}, Notes={Notes}, LineTotal={LineTotal}", resultItem.StockReceiveLineID, resultItem.FK_StockReceiveID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.UnitCostIncl, resultItem.UnitCostExcl, resultItem.FK_TaxTypeID, resultItem.TaxRate, resultItem.TotalCostIncl, resultItem.TotalCostExcl, resultItem.Notes, resultItem.LineTotal);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockReceiveLine found with the given StockReceiveLineID.");
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

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Insert_Transaction(StockReceiveLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceiveLines_Insert(item, sqlConn);
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

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Insert(StockReceiveLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceiveLines_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Insert(StockReceiveLine item, SqlConnection sqlConn)
        {
            try
            {
                StockReceiveLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockReceiveLines_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockReceiveID", Value = item.FK_StockReceiveID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostIncl", Value = item.UnitCostIncl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostExcl", Value = item.UnitCostExcl }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxTypeID", Value = item.FK_TaxTypeID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TaxRate", Value = item.TaxRate }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCostIncl", Value = item.TotalCostIncl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCostExcl", Value = item.TotalCostExcl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotal", Value = item.LineTotal }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockReceiveLine>(Stock_Translator.Translate_StockReceiveLine);
                        Log.Information("StockReceiveLine found: StockReceiveLineID={StockReceiveLineID}, FK_StockReceiveID={FK_StockReceiveID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, UnitCostIncl={UnitCostIncl}, UnitCostExcl={UnitCostExcl}, FK_TaxTypeID={FK_TaxTypeID}, TaxRate={TaxRate}, TotalCostIncl={TotalCostIncl}, TotalCostExcl={TotalCostExcl}, Notes={Notes}, LineTotal={LineTotal}", resultItem.StockReceiveLineID, resultItem.FK_StockReceiveID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.UnitCostIncl, resultItem.UnitCostExcl, resultItem.FK_TaxTypeID, resultItem.TaxRate, resultItem.TotalCostIncl, resultItem.TotalCostExcl, resultItem.Notes, resultItem.LineTotal);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockReceiveLine failed to create.");
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

        public static async Task<List<StockReceiveLine>> POS_StockReceiveLines_Select_All_Transaction(StockReceiveLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceiveLines_Select_All(item, sqlConn);
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

        public static async Task<List<StockReceiveLine>> POS_StockReceiveLines_Select_All(StockReceiveLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceiveLines_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockReceiveLine>> POS_StockReceiveLines_Select_All(StockReceiveLine item, SqlConnection sqlConn)
        {
            try
            {
                List<StockReceiveLine> resultItem = new List<StockReceiveLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockReceiveLines_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockReceiveLine>(Stock_Translator.Translate_StockReceiveLine));
                        Log.Information("StockReceiveLine records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockReceiveLine records found.");
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

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Update_Transaction(StockReceiveLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceiveLines_Update(item, sqlConn);
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

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Update(StockReceiveLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockReceiveLines_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockReceiveLine> POS_StockReceiveLines_Update(StockReceiveLine item, SqlConnection sqlConn)
        {
            try
            {
                StockReceiveLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockReceiveLines_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockReceiveLineID", Value = item.StockReceiveLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockReceiveID", Value = item.FK_StockReceiveID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostIncl", Value = item.UnitCostIncl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitCostExcl", Value = item.UnitCostExcl }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxTypeID", Value = item.FK_TaxTypeID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TaxRate", Value = item.TaxRate }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCostIncl", Value = item.TotalCostIncl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TotalCostExcl", Value = item.TotalCostExcl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotal", Value = item.LineTotal }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockReceiveLine>(Stock_Translator.Translate_StockReceiveLine);
                        Log.Information("StockReceiveLine found: StockReceiveLineID={StockReceiveLineID}, FK_StockReceiveID={FK_StockReceiveID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, UnitCostIncl={UnitCostIncl}, UnitCostExcl={UnitCostExcl}, FK_TaxTypeID={FK_TaxTypeID}, TaxRate={TaxRate}, TotalCostIncl={TotalCostIncl}, TotalCostExcl={TotalCostExcl}, Notes={Notes}, LineTotal={LineTotal}", resultItem.StockReceiveLineID, resultItem.FK_StockReceiveID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.UnitCostIncl, resultItem.UnitCostExcl, resultItem.FK_TaxTypeID, resultItem.TaxRate, resultItem.TotalCostIncl, resultItem.TotalCostExcl, resultItem.Notes, resultItem.LineTotal);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockReceiveLine failed to update.");
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

        #region POS_StockRequests

        public static async Task<StockRequest> POS_StockRequests_Select_Single_Transaction(StockRequest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequests_Select_Single(item, sqlConn);
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

        public static async Task<StockRequest> POS_StockRequests_Select_Single(StockRequest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequests_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockRequest> POS_StockRequests_Select_Single(StockRequest item, SqlConnection sqlConn)
        {
            try
            {
                StockRequest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequests_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockRequestID", Value = item.StockRequestID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockRequest>(Stock_Translator.Translate_StockRequest);
                        Log.Information("StockRequest found: StockRequestID={StockRequestID}, RefNumber={RefNumber}, FK_FromDebtorID={FK_FromDebtorID}, FK_ToDebtorID={FK_ToDebtorID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, ManagerNotes={ManagerNotes}, Notes={Notes}, DateOrdered={DateOrdered}, DateUpdated={DateUpdated}", resultItem.StockRequestID, resultItem.RefNumber, resultItem.FK_FromDebtorID, resultItem.FK_ToDebtorID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.ManagerNotes, resultItem.Notes, resultItem.DateOrdered, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockRequest found with the given StockRequestID.");
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

        public static async Task<StockRequest> POS_StockRequests_Insert_Transaction(StockRequest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequests_Insert(item, sqlConn);
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

        public static async Task<StockRequest> POS_StockRequests_Insert(StockRequest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequests_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockRequest> POS_StockRequests_Insert(StockRequest item, SqlConnection sqlConn)
        {
            try
            {
                StockRequest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequests_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RefNumber", Value = item.RefNumber }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_FromDebtorID", Value = item.FK_FromDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToDebtorID", Value = item.FK_ToDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderStatusID", Value = item.FK_OrderStatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ManagerNotes", Value = item.ManagerNotes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateOrdered", Value = item.DateOrdered }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockRequest>(Stock_Translator.Translate_StockRequest);
                        Log.Information("StockRequest found: StockRequestID={StockRequestID}, RefNumber={RefNumber}, FK_FromDebtorID={FK_FromDebtorID}, FK_ToDebtorID={FK_ToDebtorID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, ManagerNotes={ManagerNotes}, Notes={Notes}, DateOrdered={DateOrdered}, DateUpdated={DateUpdated}", resultItem.StockRequestID, resultItem.RefNumber, resultItem.FK_FromDebtorID, resultItem.FK_ToDebtorID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.ManagerNotes, resultItem.Notes, resultItem.DateOrdered, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockRequest failed to create.");
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

        public static async Task<List<StockRequest>> POS_StockRequests_Select_All_Transaction(StockRequest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequests_Select_All(item, sqlConn);
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

        public static async Task<List<StockRequest>> POS_StockRequests_Select_All(StockRequest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequests_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockRequest>> POS_StockRequests_Select_All(StockRequest item, SqlConnection sqlConn)
        {
            try
            {
                List<StockRequest> resultItem = new List<StockRequest>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequests_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockRequest>(Stock_Translator.Translate_StockRequest));
                        Log.Information("StockRequest records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockRequest records found.");
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

        public static async Task<StockRequest> POS_StockRequests_Update_Transaction(StockRequest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequests_Update(item, sqlConn);
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

        public static async Task<StockRequest> POS_StockRequests_Update(StockRequest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequests_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockRequest> POS_StockRequests_Update(StockRequest item, SqlConnection sqlConn)
        {
            try
            {
                StockRequest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequests_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockRequestID", Value = item.StockRequestID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RefNumber", Value = item.RefNumber }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_FromDebtorID", Value = item.FK_FromDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToDebtorID", Value = item.FK_ToDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderStatusID", Value = item.FK_OrderStatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ManagerNotes", Value = item.ManagerNotes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateOrdered", Value = item.DateOrdered }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockRequest>(Stock_Translator.Translate_StockRequest);
                        Log.Information("StockRequest found: StockRequestID={StockRequestID}, RefNumber={RefNumber}, FK_FromDebtorID={FK_FromDebtorID}, FK_ToDebtorID={FK_ToDebtorID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, ManagerNotes={ManagerNotes}, Notes={Notes}, DateOrdered={DateOrdered}, DateUpdated={DateUpdated}", resultItem.StockRequestID, resultItem.RefNumber, resultItem.FK_FromDebtorID, resultItem.FK_ToDebtorID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.ManagerNotes, resultItem.Notes, resultItem.DateOrdered, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockRequest failed to update.");
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

        #region POS_StockRequestLines

        public static async Task<StockRequestLine> POS_StockRequestLines_Select_Single_Transaction(StockRequestLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequestLines_Select_Single(item, sqlConn);
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

        public static async Task<StockRequestLine> POS_StockRequestLines_Select_Single(StockRequestLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequestLines_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockRequestLine> POS_StockRequestLines_Select_Single(StockRequestLine item, SqlConnection sqlConn)
        {
            try
            {
                StockRequestLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequestLines_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockRequestLineID", Value = item.StockRequestLineID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockRequestLine>(Stock_Translator.Translate_StockRequestLine);
                        Log.Information("StockRequestLine found: StockRequestLineID={StockRequestLineID}, FK_StockRequestID={FK_StockRequestID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, Notes={Notes}, ManagerNotes={ManagerNotes}, IsDeclined={IsDeclined}", resultItem.StockRequestLineID, resultItem.FK_StockRequestID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.Notes, resultItem.ManagerNotes, resultItem.IsDeclined);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockRequestLine found with the given StockRequestLineID.");
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

        public static async Task<StockRequestLine> POS_StockRequestLines_Insert_Transaction(StockRequestLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequestLines_Insert(item, sqlConn);
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

        public static async Task<StockRequestLine> POS_StockRequestLines_Insert(StockRequestLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequestLines_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockRequestLine> POS_StockRequestLines_Insert(StockRequestLine item, SqlConnection sqlConn)
        {
            try
            {
                StockRequestLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequestLines_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockRequestID", Value = item.FK_StockRequestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ManagerNotes", Value = item.ManagerNotes }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDeclined", Value = item.IsDeclined }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockRequestLine>(Stock_Translator.Translate_StockRequestLine);
                        Log.Information("StockRequestLine found: StockRequestLineID={StockRequestLineID}, FK_StockRequestID={FK_StockRequestID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, Notes={Notes}, ManagerNotes={ManagerNotes}, IsDeclined={IsDeclined}", resultItem.StockRequestLineID, resultItem.FK_StockRequestID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.Notes, resultItem.ManagerNotes, resultItem.IsDeclined);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockRequestLine failed to create.");
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

        public static async Task<List<StockRequestLine>> POS_StockRequestLines_Select_All_Transaction(StockRequestLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequestLines_Select_All(item, sqlConn);
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

        public static async Task<List<StockRequestLine>> POS_StockRequestLines_Select_All(StockRequestLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequestLines_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockRequestLine>> POS_StockRequestLines_Select_All(StockRequestLine item, SqlConnection sqlConn)
        {
            try
            {
                List<StockRequestLine> resultItem = new List<StockRequestLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequestLines_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockRequestLine>(Stock_Translator.Translate_StockRequestLine));
                        Log.Information("StockRequestLine records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockRequestLine records found.");
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

        public static async Task<StockRequestLine> POS_StockRequestLines_Update_Transaction(StockRequestLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequestLines_Update(item, sqlConn);
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

        public static async Task<StockRequestLine> POS_StockRequestLines_Update(StockRequestLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockRequestLines_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockRequestLine> POS_StockRequestLines_Update(StockRequestLine item, SqlConnection sqlConn)
        {
            try
            {
                StockRequestLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequestLines_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockRequestLineID", Value = item.StockRequestLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockRequestID", Value = item.FK_StockRequestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ManagerNotes", Value = item.ManagerNotes }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDeclined", Value = item.IsDeclined }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockRequestLine>(Stock_Translator.Translate_StockRequestLine);
                        Log.Information("StockRequestLine found: StockRequestLineID={StockRequestLineID}, FK_StockRequestID={FK_StockRequestID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}, Notes={Notes}, ManagerNotes={ManagerNotes}, IsDeclined={IsDeclined}", resultItem.StockRequestLineID, resultItem.FK_StockRequestID, resultItem.FK_ProductID, resultItem.Quantity, resultItem.Notes, resultItem.ManagerNotes, resultItem.IsDeclined);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockRequestLine failed to update.");
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

        #region POS_StockTransfers

        public static async Task<StockTransfer> POS_StockTransfers_Select_Single_Transaction(StockTransfer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransfers_Select_Single(item, sqlConn);
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

        public static async Task<StockTransfer> POS_StockTransfers_Select_Single(StockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransfers_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockTransfer> POS_StockTransfers_Select_Single(StockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                StockTransfer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockTransfers_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockTransferID", Value = item.StockTransferID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockTransfer>(Stock_Translator.Translate_StockTransfer);
                        Log.Information("StockTransfer found: StockTransferID={StockTransferID}, RefNumber={RefNumber}, FK_FromDebtorID={FK_FromDebtorID}, FK_ToDebtorID={FK_ToDebtorID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, DateTransfered={DateTransfered}, Notes={Notes}", resultItem.StockTransferID, resultItem.RefNumber, resultItem.FK_FromDebtorID, resultItem.FK_ToDebtorID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.DateTransfered, resultItem.Notes);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockTransfer found with the given StockTransferID.");
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

        public static async Task<StockTransfer> POS_StockTransfers_Insert_Transaction(StockTransfer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransfers_Insert(item, sqlConn);
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

        public static async Task<StockTransfer> POS_StockTransfers_Insert(StockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransfers_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockTransfer> POS_StockTransfers_Insert(StockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                StockTransfer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockTransfers_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RefNumber", Value = item.RefNumber }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_FromDebtorID", Value = item.FK_FromDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToDebtorID", Value = item.FK_ToDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderStatusID", Value = item.FK_OrderStatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateTransfered", Value = item.DateTransfered }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockTransfer>(Stock_Translator.Translate_StockTransfer);
                        Log.Information("StockTransfer found: StockTransferID={StockTransferID}, RefNumber={RefNumber}, FK_FromDebtorID={FK_FromDebtorID}, FK_ToDebtorID={FK_ToDebtorID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, DateTransfered={DateTransfered}, Notes={Notes}", resultItem.StockTransferID, resultItem.RefNumber, resultItem.FK_FromDebtorID, resultItem.FK_ToDebtorID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.DateTransfered, resultItem.Notes);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockTransfer failed to create.");
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

        public static async Task<List<StockTransfer>> POS_StockTransfers_Select_All_Transaction(StockTransfer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransfers_Select_All(item, sqlConn);
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

        public static async Task<List<StockTransfer>> POS_StockTransfers_Select_All(StockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransfers_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockTransfer>> POS_StockTransfers_Select_All(StockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                List<StockTransfer> resultItem = new List<StockTransfer>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockTransfers_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockTransfer>(Stock_Translator.Translate_StockTransfer));
                        Log.Information("StockTransfer records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockTransfer records found.");
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

        public static async Task<StockTransfer> POS_StockTransfers_Update_Transaction(StockTransfer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransfers_Update(item, sqlConn);
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

        public static async Task<StockTransfer> POS_StockTransfers_Update(StockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransfers_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockTransfer> POS_StockTransfers_Update(StockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                StockTransfer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockTransfers_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockTransferID", Value = item.StockTransferID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RefNumber", Value = item.RefNumber }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_FromDebtorID", Value = item.FK_FromDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToDebtorID", Value = item.FK_ToDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderStatusID", Value = item.FK_OrderStatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateTransfered", Value = item.DateTransfered }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockTransfer>(Stock_Translator.Translate_StockTransfer);
                        Log.Information("StockTransfer found: StockTransferID={StockTransferID}, RefNumber={RefNumber}, FK_FromDebtorID={FK_FromDebtorID}, FK_ToDebtorID={FK_ToDebtorID}, FK_OrderStatusID={FK_OrderStatusID}, FK_UserID={FK_UserID}, DateTransfered={DateTransfered}, Notes={Notes}", resultItem.StockTransferID, resultItem.RefNumber, resultItem.FK_FromDebtorID, resultItem.FK_ToDebtorID, resultItem.FK_OrderStatusID, resultItem.FK_UserID, resultItem.DateTransfered, resultItem.Notes);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockTransfer failed to update.");
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

        #region POS_StockTransferLines

        public static async Task<StockTransferLine> POS_StockTransferLines_Select_Single_Transaction(StockTransferLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransferLines_Select_Single(item, sqlConn);
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

        public static async Task<StockTransferLine> POS_StockTransferLines_Select_Single(StockTransferLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransferLines_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockTransferLine> POS_StockTransferLines_Select_Single(StockTransferLine item, SqlConnection sqlConn)
        {
            try
            {
                StockTransferLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockTransferLines_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockTransferLineID", Value = item.StockTransferLineID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockTransferLine>(Stock_Translator.Translate_StockTransferLine);
                        Log.Information("StockTransferLine found: StockTransferLineID={StockTransferLineID}, FK_StockTransferID={FK_StockTransferID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}", resultItem.StockTransferLineID, resultItem.FK_StockTransferID, resultItem.FK_ProductID, resultItem.Quantity);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockTransferLine found with the given StockTransferLineID.");
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

        public static async Task<StockTransferLine> POS_StockTransferLines_Insert_Transaction(StockTransferLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransferLines_Insert(item, sqlConn);
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

        public static async Task<StockTransferLine> POS_StockTransferLines_Insert(StockTransferLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransferLines_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockTransferLine> POS_StockTransferLines_Insert(StockTransferLine item, SqlConnection sqlConn)
        {
            try
            {
                StockTransferLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockTransferLines_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockTransferID", Value = item.FK_StockTransferID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockTransferLine>(Stock_Translator.Translate_StockTransferLine);
                        Log.Information("StockTransferLine found: StockTransferLineID={StockTransferLineID}, FK_StockTransferID={FK_StockTransferID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}", resultItem.StockTransferLineID, resultItem.FK_StockTransferID, resultItem.FK_ProductID, resultItem.Quantity);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockTransferLine failed to create.");
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

        public static async Task<List<StockTransferLine>> POS_StockTransferLines_Select_All_Transaction(StockTransferLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransferLines_Select_All(item, sqlConn);
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

        public static async Task<List<StockTransferLine>> POS_StockTransferLines_Select_All(StockTransferLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransferLines_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StockTransferLine>> POS_StockTransferLines_Select_All(StockTransferLine item, SqlConnection sqlConn)
        {
            try
            {
                List<StockTransferLine> resultItem = new List<StockTransferLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockTransferLines_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StockTransferLine>(Stock_Translator.Translate_StockTransferLine));
                        Log.Information("StockTransferLine records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StockTransferLine records found.");
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

        public static async Task<StockTransferLine> POS_StockTransferLines_Update_Transaction(StockTransferLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransferLines_Update(item, sqlConn);
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

        public static async Task<StockTransferLine> POS_StockTransferLines_Update(StockTransferLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_StockTransferLines_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StockTransferLine> POS_StockTransferLines_Update(StockTransferLine item, SqlConnection sqlConn)
        {
            try
            {
                StockTransferLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockTransferLines_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StockTransferLineID", Value = item.StockTransferLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockTransferID", Value = item.FK_StockTransferID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StockTransferLine>(Stock_Translator.Translate_StockTransferLine);
                        Log.Information("StockTransferLine found: StockTransferLineID={StockTransferLineID}, FK_StockTransferID={FK_StockTransferID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}", resultItem.StockTransferLineID, resultItem.FK_StockTransferID, resultItem.FK_ProductID, resultItem.Quantity);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StockTransferLine failed to update.");
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

        #region POS_CostCenterProducts

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Select_Single_Transaction(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProducts_Select_Single(item, sqlConn);
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

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Select_Single(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProducts_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Select_Single(CostCenterProduct item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterProducts_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterProductID", Value = item.CostCenterProductID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterProduct>(Stock_Translator.Translate_CostCenterProduct);
                        Log.Information("CostCenterProduct found: CostCenterProductID={CostCenterProductID}, FK_ProductID={FK_ProductID}, FK_CostCenterID={FK_CostCenterID}, FK_TaxTypeID={FK_TaxTypeID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, FK_SellUnitID={FK_SellUnitID}, QuantityOnHand={QuantityOnHand}, IsAvailable={IsAvailable}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostCenterProductID, resultItem.FK_ProductID, resultItem.FK_CostCenterID, resultItem.FK_TaxTypeID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.FK_SellUnitID, resultItem.QuantityOnHand, resultItem.IsAvailable, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterProduct found with the given CostCenterProductID.");
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

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Insert_Transaction(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProducts_Insert(item, sqlConn);
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

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Insert(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProducts_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Insert(CostCenterProduct item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterProducts_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxTypeID", Value = item.FK_TaxTypeID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Value", Value = item.Value }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ItemPrice", Value = item.ItemPrice }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_SellUnitID", Value = item.FK_SellUnitID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@QuantityOnHand", Value = item.QuantityOnHand }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsAvailable", Value = item.IsAvailable }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterProduct>(Stock_Translator.Translate_CostCenterProduct);
                        Log.Information("CostCenterProduct found: CostCenterProductID={CostCenterProductID}, FK_ProductID={FK_ProductID}, FK_CostCenterID={FK_CostCenterID}, FK_TaxTypeID={FK_TaxTypeID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, FK_SellUnitID={FK_SellUnitID}, QuantityOnHand={QuantityOnHand}, IsAvailable={IsAvailable}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostCenterProductID, resultItem.FK_ProductID, resultItem.FK_CostCenterID, resultItem.FK_TaxTypeID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.FK_SellUnitID, resultItem.QuantityOnHand, resultItem.IsAvailable, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenterProduct failed to create.");
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

        public static async Task<List<CostCenterProduct>> POS_CostCenterProducts_Select_All_Transaction(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProducts_Select_All(item, sqlConn);
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

        public static async Task<List<CostCenterProduct>> POS_CostCenterProducts_Select_All(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProducts_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CostCenterProduct>> POS_CostCenterProducts_Select_All(CostCenterProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<CostCenterProduct> resultItem = new List<CostCenterProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterProducts_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CostCenterProduct>(Stock_Translator.Translate_CostCenterProduct));
                        Log.Information("CostCenterProduct records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterProduct records found.");
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

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Update_Transaction(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProducts_Update(item, sqlConn);
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

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Update(CostCenterProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProducts_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterProduct> POS_CostCenterProducts_Update(CostCenterProduct item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterProducts_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterProductID", Value = item.CostCenterProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxTypeID", Value = item.FK_TaxTypeID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Value", Value = item.Value }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ItemPrice", Value = item.ItemPrice }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_SellUnitID", Value = item.FK_SellUnitID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@QuantityOnHand", Value = item.QuantityOnHand }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsAvailable", Value = item.IsAvailable }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterProduct>(Stock_Translator.Translate_CostCenterProduct);
                        Log.Information("CostCenterProduct found: CostCenterProductID={CostCenterProductID}, FK_ProductID={FK_ProductID}, FK_CostCenterID={FK_CostCenterID}, FK_TaxTypeID={FK_TaxTypeID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, FK_SellUnitID={FK_SellUnitID}, QuantityOnHand={QuantityOnHand}, IsAvailable={IsAvailable}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostCenterProductID, resultItem.FK_ProductID, resultItem.FK_CostCenterID, resultItem.FK_TaxTypeID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.FK_SellUnitID, resultItem.QuantityOnHand, resultItem.IsAvailable, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenterProduct failed to update.");
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

        #region POS_SupplierProducts

        public static async Task<SupplierProduct> POS_SupplierProducts_Select_Single_Transaction(SupplierProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SupplierProducts_Select_Single(item, sqlConn);
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

        public static async Task<SupplierProduct> POS_SupplierProducts_Select_Single(SupplierProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SupplierProducts_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SupplierProduct> POS_SupplierProducts_Select_Single(SupplierProduct item, SqlConnection sqlConn)
        {
            try
            {
                SupplierProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SupplierProducts_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SupplierProductID", Value = item.SupplierProductID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SupplierProduct>(Stock_Translator.Translate_SupplierProduct);
                        Log.Information("SupplierProduct found: SupplierProductID={SupplierProductID}, FK_CreditorID={FK_CreditorID}, FK_ProductID={FK_ProductID}, FK_DebtorID={FK_DebtorID}, SupplierItemCode={SupplierItemCode}, FK_BaseUnitID={FK_BaseUnitID}, FK_PacUnitID={FK_PacUnitID}, UnitsPerPack={UnitsPerPack}, Quantity={Quantity}, TrackPackLevel={TrackPackLevel}, LastPurchasePrice={LastPurchasePrice}, LastPurchaseDate={LastPurchaseDate}, FK_TaxTypeID={FK_TaxTypeID}, LeadTimeDays={LeadTimeDays}, IsPreferred={IsPreferred}, IsActive={IsActive}, DateAdded={DateAdded}, DateUpdated={DateUpdated}", resultItem.SupplierProductID, resultItem.FK_CreditorID, resultItem.FK_ProductID, resultItem.FK_DebtorID, resultItem.SupplierItemCode, resultItem.FK_BaseUnitID, resultItem.FK_PacUnitID, resultItem.UnitsPerPack, resultItem.Quantity, resultItem.TrackPackLevel, resultItem.LastPurchasePrice, resultItem.LastPurchaseDate, resultItem.FK_TaxTypeID, resultItem.LeadTimeDays, resultItem.IsPreferred, resultItem.IsActive, resultItem.DateAdded, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No SupplierProduct found with the given SupplierProductID.");
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

        public static async Task<SupplierProduct> POS_SupplierProducts_Insert_Transaction(SupplierProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SupplierProducts_Insert(item, sqlConn);
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

        public static async Task<SupplierProduct> POS_SupplierProducts_Insert(SupplierProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SupplierProducts_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SupplierProduct> POS_SupplierProducts_Insert(SupplierProduct item, SqlConnection sqlConn)
        {
            try
            {
                SupplierProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SupplierProducts_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreditorID", Value = item.FK_CreditorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorID", Value = item.FK_DebtorID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SupplierItemCode", Value = item.SupplierItemCode }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BaseUnitID", Value = item.FK_BaseUnitID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PacUnitID", Value = item.FK_PacUnitID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitsPerPack", Value = item.UnitsPerPack }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@TrackPackLevel", Value = item.TrackPackLevel }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LastPurchasePrice", Value = item.LastPurchasePrice }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@LastPurchaseDate", Value = item.LastPurchaseDate }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxTypeID", Value = item.FK_TaxTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@LeadTimeDays", Value = item.LeadTimeDays }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@IsPreferred", Value = item.IsPreferred }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateAdded", Value = item.DateAdded }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SupplierProduct>(Stock_Translator.Translate_SupplierProduct);
                        Log.Information("SupplierProduct found: SupplierProductID={SupplierProductID}, FK_CreditorID={FK_CreditorID}, FK_ProductID={FK_ProductID}, FK_DebtorID={FK_DebtorID}, SupplierItemCode={SupplierItemCode}, FK_BaseUnitID={FK_BaseUnitID}, FK_PacUnitID={FK_PacUnitID}, UnitsPerPack={UnitsPerPack}, Quantity={Quantity}, TrackPackLevel={TrackPackLevel}, LastPurchasePrice={LastPurchasePrice}, LastPurchaseDate={LastPurchaseDate}, FK_TaxTypeID={FK_TaxTypeID}, LeadTimeDays={LeadTimeDays}, IsPreferred={IsPreferred}, IsActive={IsActive}, DateAdded={DateAdded}, DateUpdated={DateUpdated}", resultItem.SupplierProductID, resultItem.FK_CreditorID, resultItem.FK_ProductID, resultItem.FK_DebtorID, resultItem.SupplierItemCode, resultItem.FK_BaseUnitID, resultItem.FK_PacUnitID, resultItem.UnitsPerPack, resultItem.Quantity, resultItem.TrackPackLevel, resultItem.LastPurchasePrice, resultItem.LastPurchaseDate, resultItem.FK_TaxTypeID, resultItem.LeadTimeDays, resultItem.IsPreferred, resultItem.IsActive, resultItem.DateAdded, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("SupplierProduct failed to create.");
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

        public static async Task<List<SupplierProduct>> POS_SupplierProducts_Select_All_Transaction(SupplierProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SupplierProducts_Select_All(item, sqlConn);
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

        public static async Task<List<SupplierProduct>> POS_SupplierProducts_Select_All(SupplierProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SupplierProducts_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<SupplierProduct>> POS_SupplierProducts_Select_All(SupplierProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<SupplierProduct> resultItem = new List<SupplierProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SupplierProducts_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<SupplierProduct>(Stock_Translator.Translate_SupplierProduct));
                        Log.Information("SupplierProduct records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No SupplierProduct records found.");
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

        public static async Task<SupplierProduct> POS_SupplierProducts_Update_Transaction(SupplierProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SupplierProducts_Update(item, sqlConn);
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

        public static async Task<SupplierProduct> POS_SupplierProducts_Update(SupplierProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SupplierProducts_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SupplierProduct> POS_SupplierProducts_Update(SupplierProduct item, SqlConnection sqlConn)
        {
            try
            {
                SupplierProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SupplierProducts_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SupplierProductID", Value = item.SupplierProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreditorID", Value = item.FK_CreditorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorID", Value = item.FK_DebtorID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SupplierItemCode", Value = item.SupplierItemCode }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BaseUnitID", Value = item.FK_BaseUnitID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PacUnitID", Value = item.FK_PacUnitID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@UnitsPerPack", Value = item.UnitsPerPack }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@TrackPackLevel", Value = item.TrackPackLevel }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LastPurchasePrice", Value = item.LastPurchasePrice }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@LastPurchaseDate", Value = item.LastPurchaseDate }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxTypeID", Value = item.FK_TaxTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@LeadTimeDays", Value = item.LeadTimeDays }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@IsPreferred", Value = item.IsPreferred }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateAdded", Value = item.DateAdded }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SupplierProduct>(Stock_Translator.Translate_SupplierProduct);
                        Log.Information("SupplierProduct found: SupplierProductID={SupplierProductID}, FK_CreditorID={FK_CreditorID}, FK_ProductID={FK_ProductID}, FK_DebtorID={FK_DebtorID}, SupplierItemCode={SupplierItemCode}, FK_BaseUnitID={FK_BaseUnitID}, FK_PacUnitID={FK_PacUnitID}, UnitsPerPack={UnitsPerPack}, Quantity={Quantity}, TrackPackLevel={TrackPackLevel}, LastPurchasePrice={LastPurchasePrice}, LastPurchaseDate={LastPurchaseDate}, FK_TaxTypeID={FK_TaxTypeID}, LeadTimeDays={LeadTimeDays}, IsPreferred={IsPreferred}, IsActive={IsActive}, DateAdded={DateAdded}, DateUpdated={DateUpdated}", resultItem.SupplierProductID, resultItem.FK_CreditorID, resultItem.FK_ProductID, resultItem.FK_DebtorID, resultItem.SupplierItemCode, resultItem.FK_BaseUnitID, resultItem.FK_PacUnitID, resultItem.UnitsPerPack, resultItem.Quantity, resultItem.TrackPackLevel, resultItem.LastPurchasePrice, resultItem.LastPurchaseDate, resultItem.FK_TaxTypeID, resultItem.LeadTimeDays, resultItem.IsPreferred, resultItem.IsActive, resultItem.DateAdded, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("SupplierProduct failed to update.");
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

        #region POS_DebtorProducts

        public static async Task<DebtorProduct> POS_DebtorProducts_Select_Single_Transaction(DebtorProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProducts_Select_Single(item, sqlConn);
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

        public static async Task<DebtorProduct> POS_DebtorProducts_Select_Single(DebtorProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProducts_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProduct> POS_DebtorProducts_Select_Single(DebtorProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProducts_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorProductID", Value = item.DebtorProductID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProduct>(Stock_Translator.Translate_DebtorProduct);
                        Log.Information("DebtorProduct found: DebtorProductID={DebtorProductID}, FK_ProductID={FK_ProductID}, FK_LocationID={FK_LocationID}, CostPrice={CostPrice}, FK_SellUnitID={FK_SellUnitID}, QuantityOnHand={QuantityOnHand}, IsAvailable={IsAvailable}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorProductID, resultItem.FK_ProductID, resultItem.FK_LocationID, resultItem.CostPrice, resultItem.FK_SellUnitID, resultItem.QuantityOnHand, resultItem.IsAvailable, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorProduct found with the given DebtorProductID.");
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

        public static async Task<DebtorProduct> POS_DebtorProducts_Insert_Transaction(DebtorProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProducts_Insert(item, sqlConn);
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

        public static async Task<DebtorProduct> POS_DebtorProducts_Insert(DebtorProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProducts_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProduct> POS_DebtorProducts_Insert(DebtorProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProducts_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@CostPrice", Value = item.CostPrice }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_SellUnitID", Value = item.FK_SellUnitID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@QuantityOnHand", Value = item.QuantityOnHand }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsAvailable", Value = item.IsAvailable }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProduct>(Stock_Translator.Translate_DebtorProduct);
                        Log.Information("DebtorProduct found: DebtorProductID={DebtorProductID}, FK_ProductID={FK_ProductID}, FK_LocationID={FK_LocationID}, CostPrice={CostPrice}, FK_SellUnitID={FK_SellUnitID}, QuantityOnHand={QuantityOnHand}, IsAvailable={IsAvailable}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorProductID, resultItem.FK_ProductID, resultItem.FK_LocationID, resultItem.CostPrice, resultItem.FK_SellUnitID, resultItem.QuantityOnHand, resultItem.IsAvailable, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorProduct failed to create.");
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

        public static async Task<List<DebtorProduct>> POS_DebtorProducts_Select_All_Transaction(DebtorProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProducts_Select_All(item, sqlConn);
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

        public static async Task<List<DebtorProduct>> POS_DebtorProducts_Select_All(DebtorProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProducts_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorProduct>> POS_DebtorProducts_Select_All(DebtorProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorProduct> resultItem = new List<DebtorProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProducts_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorProduct>(Stock_Translator.Translate_DebtorProduct));
                        Log.Information("DebtorProduct records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorProduct records found.");
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

        public static async Task<DebtorProduct> POS_DebtorProducts_Update_Transaction(DebtorProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProducts_Update(item, sqlConn);
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

        public static async Task<DebtorProduct> POS_DebtorProducts_Update(DebtorProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProducts_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProduct> POS_DebtorProducts_Update(DebtorProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProducts_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorProductID", Value = item.DebtorProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@CostPrice", Value = item.CostPrice }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_SellUnitID", Value = item.FK_SellUnitID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@QuantityOnHand", Value = item.QuantityOnHand }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsAvailable", Value = item.IsAvailable }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProduct>(Stock_Translator.Translate_DebtorProduct);
                        Log.Information("DebtorProduct found: DebtorProductID={DebtorProductID}, FK_ProductID={FK_ProductID}, FK_LocationID={FK_LocationID}, CostPrice={CostPrice}, FK_SellUnitID={FK_SellUnitID}, QuantityOnHand={QuantityOnHand}, IsAvailable={IsAvailable}, IsActive={IsActive}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorProductID, resultItem.FK_ProductID, resultItem.FK_LocationID, resultItem.CostPrice, resultItem.FK_SellUnitID, resultItem.QuantityOnHand, resultItem.IsAvailable, resultItem.IsActive, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorProduct failed to update.");
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

        #region POS_InternalStockTransfers

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Select_Single_Transaction(InternalStockTransfer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransfers_Select_Single(item, sqlConn);
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

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Select_Single(InternalStockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransfers_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Select_Single(InternalStockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                InternalStockTransfer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InternalStockTransfers_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@InternalStockTransferID", Value = item.InternalStockTransferID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InternalStockTransfer>(Stock_Translator.Translate_InternalStockTransfer);
                        Log.Information("InternalStockTransfer found: InternalStockTransferID={InternalStockTransferID}, RefNumber={RefNumber}, FK_DebtorID={FK_DebtorID}, FK_CostCenterID={FK_CostCenterID}, FK_UserID={FK_UserID}, Notes={Notes}, DateTransfered={DateTransfered}", resultItem.InternalStockTransferID, resultItem.RefNumber, resultItem.FK_DebtorID, resultItem.FK_CostCenterID, resultItem.FK_UserID, resultItem.Notes, resultItem.DateTransfered);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InternalStockTransfer found with the given InternalStockTransferID.");
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

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Insert_Transaction(InternalStockTransfer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransfers_Insert(item, sqlConn);
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

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Insert(InternalStockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransfers_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Insert(InternalStockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                InternalStockTransfer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InternalStockTransfers_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RefNumber", Value = item.RefNumber }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorID", Value = item.FK_DebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateTransfered", Value = item.DateTransfered }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InternalStockTransfer>(Stock_Translator.Translate_InternalStockTransfer);
                        Log.Information("InternalStockTransfer found: InternalStockTransferID={InternalStockTransferID}, RefNumber={RefNumber}, FK_DebtorID={FK_DebtorID}, FK_CostCenterID={FK_CostCenterID}, FK_UserID={FK_UserID}, Notes={Notes}, DateTransfered={DateTransfered}", resultItem.InternalStockTransferID, resultItem.RefNumber, resultItem.FK_DebtorID, resultItem.FK_CostCenterID, resultItem.FK_UserID, resultItem.Notes, resultItem.DateTransfered);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InternalStockTransfer failed to create.");
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

        public static async Task<List<InternalStockTransfer>> POS_InternalStockTransfers_Select_All_Transaction(InternalStockTransfer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransfers_Select_All(item, sqlConn);
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

        public static async Task<List<InternalStockTransfer>> POS_InternalStockTransfers_Select_All(InternalStockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransfers_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InternalStockTransfer>> POS_InternalStockTransfers_Select_All(InternalStockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                List<InternalStockTransfer> resultItem = new List<InternalStockTransfer>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InternalStockTransfers_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<InternalStockTransfer>(Stock_Translator.Translate_InternalStockTransfer));
                        Log.Information("InternalStockTransfer records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InternalStockTransfer records found.");
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

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Update_Transaction(InternalStockTransfer item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransfers_Update(item, sqlConn);
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

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Update(InternalStockTransfer item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransfers_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InternalStockTransfer> POS_InternalStockTransfers_Update(InternalStockTransfer item, SqlConnection sqlConn)
        {
            try
            {
                InternalStockTransfer resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InternalStockTransfers_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@InternalStockTransferID", Value = item.InternalStockTransferID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RefNumber", Value = item.RefNumber }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorID", Value = item.FK_DebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateTransfered", Value = item.DateTransfered }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InternalStockTransfer>(Stock_Translator.Translate_InternalStockTransfer);
                        Log.Information("InternalStockTransfer found: InternalStockTransferID={InternalStockTransferID}, RefNumber={RefNumber}, FK_DebtorID={FK_DebtorID}, FK_CostCenterID={FK_CostCenterID}, FK_UserID={FK_UserID}, Notes={Notes}, DateTransfered={DateTransfered}", resultItem.InternalStockTransferID, resultItem.RefNumber, resultItem.FK_DebtorID, resultItem.FK_CostCenterID, resultItem.FK_UserID, resultItem.Notes, resultItem.DateTransfered);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InternalStockTransfer failed to update.");
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

        #region POS_InternalStockTransferLines

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Select_Single_Transaction(InternalStockTransferLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransferLines_Select_Single(item, sqlConn);
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

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Select_Single(InternalStockTransferLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransferLines_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Select_Single(InternalStockTransferLine item, SqlConnection sqlConn)
        {
            try
            {
                InternalStockTransferLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InternalStockTransferLines_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@InternalStockTransferLineID", Value = item.InternalStockTransferLineID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InternalStockTransferLine>(Stock_Translator.Translate_InternalStockTransferLine);
                        Log.Information("InternalStockTransferLine found: InternalStockTransferLineID={InternalStockTransferLineID}, FK_InternalStockTransferID={FK_InternalStockTransferID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}", resultItem.InternalStockTransferLineID, resultItem.FK_InternalStockTransferID, resultItem.FK_ProductID, resultItem.Quantity);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InternalStockTransferLine found with the given InternalStockTransferLineID.");
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

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Insert_Transaction(InternalStockTransferLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransferLines_Insert(item, sqlConn);
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

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Insert(InternalStockTransferLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransferLines_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Insert(InternalStockTransferLine item, SqlConnection sqlConn)
        {
            try
            {
                InternalStockTransferLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InternalStockTransferLines_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_InternalStockTransferID", Value = item.FK_InternalStockTransferID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InternalStockTransferLine>(Stock_Translator.Translate_InternalStockTransferLine);
                        Log.Information("InternalStockTransferLine found: InternalStockTransferLineID={InternalStockTransferLineID}, FK_InternalStockTransferID={FK_InternalStockTransferID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}", resultItem.InternalStockTransferLineID, resultItem.FK_InternalStockTransferID, resultItem.FK_ProductID, resultItem.Quantity);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InternalStockTransferLine failed to create.");
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

        public static async Task<List<InternalStockTransferLine>> POS_InternalStockTransferLines_Select_All_Transaction(InternalStockTransferLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransferLines_Select_All(item, sqlConn);
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

        public static async Task<List<InternalStockTransferLine>> POS_InternalStockTransferLines_Select_All(InternalStockTransferLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransferLines_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InternalStockTransferLine>> POS_InternalStockTransferLines_Select_All(InternalStockTransferLine item, SqlConnection sqlConn)
        {
            try
            {
                List<InternalStockTransferLine> resultItem = new List<InternalStockTransferLine>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InternalStockTransferLines_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<InternalStockTransferLine>(Stock_Translator.Translate_InternalStockTransferLine));
                        Log.Information("InternalStockTransferLine records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No InternalStockTransferLine records found.");
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

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Update_Transaction(InternalStockTransferLine item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransferLines_Update(item, sqlConn);
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

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Update(InternalStockTransferLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_InternalStockTransferLines_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InternalStockTransferLine> POS_InternalStockTransferLines_Update(InternalStockTransferLine item, SqlConnection sqlConn)
        {
            try
            {
                InternalStockTransferLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_InternalStockTransferLines_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@InternalStockTransferLineID", Value = item.InternalStockTransferLineID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_InternalStockTransferID", Value = item.FK_InternalStockTransferID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InternalStockTransferLine>(Stock_Translator.Translate_InternalStockTransferLine);
                        Log.Information("InternalStockTransferLine found: InternalStockTransferLineID={InternalStockTransferLineID}, FK_InternalStockTransferID={FK_InternalStockTransferID}, FK_ProductID={FK_ProductID}, Quantity={Quantity}", resultItem.InternalStockTransferLineID, resultItem.FK_InternalStockTransferID, resultItem.FK_ProductID, resultItem.Quantity);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InternalStockTransferLine failed to update.");
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

        #region POS_DebtorProductPriceHistory

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Select_Single_Transaction(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Select_Single(item, sqlConn);
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

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Select_Single(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Select_Single(DebtorProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProductPriceHistory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProductPriceHistory_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorProductPriceHistoryID", Value = item.DebtorProductPriceHistoryID }                ))
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

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Insert_Transaction(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Insert(item, sqlConn);
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

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Insert(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Insert(DebtorProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProductPriceHistory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProductPriceHistory_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorProductID", Value = item.FK_DebtorProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Value", Value = item.Value }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ItemPrice", Value = item.ItemPrice }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProductPriceHistory>(Stock_Translator.Translate_DebtorProductPriceHistory);
                        Log.Information("DebtorProductPriceHistory found: DebtorProductPriceHistoryID={DebtorProductPriceHistoryID}, FK_DebtorProductID={FK_DebtorProductID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorProductPriceHistoryID, resultItem.FK_DebtorProductID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.ValidFrom, resultItem.ValidTo, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorProductPriceHistory failed to create.");
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

        public static async Task<List<DebtorProductPriceHistory>> POS_DebtorProductPriceHistory_Select_All_Transaction(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Select_All(item, sqlConn);
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

        public static async Task<List<DebtorProductPriceHistory>> POS_DebtorProductPriceHistory_Select_All(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorProductPriceHistory>> POS_DebtorProductPriceHistory_Select_All(DebtorProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorProductPriceHistory> resultItem = new List<DebtorProductPriceHistory>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProductPriceHistory_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorProductPriceHistory>(Stock_Translator.Translate_DebtorProductPriceHistory));
                        Log.Information("DebtorProductPriceHistory records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorProductPriceHistory records found.");
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

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Update_Transaction(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Update(item, sqlConn);
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

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Update(DebtorProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPriceHistory_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProductPriceHistory> POS_DebtorProductPriceHistory_Update(DebtorProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProductPriceHistory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProductPriceHistory_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorProductPriceHistoryID", Value = item.DebtorProductPriceHistoryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorProductID", Value = item.FK_DebtorProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Value", Value = item.Value }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ItemPrice", Value = item.ItemPrice }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProductPriceHistory>(Stock_Translator.Translate_DebtorProductPriceHistory);
                        Log.Information("DebtorProductPriceHistory found: DebtorProductPriceHistoryID={DebtorProductPriceHistoryID}, FK_DebtorProductID={FK_DebtorProductID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorProductPriceHistoryID, resultItem.FK_DebtorProductID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.ValidFrom, resultItem.ValidTo, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorProductPriceHistory failed to update.");
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

        #region POS_CostCenterProductPriceHistory

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Select_Single_Transaction(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProductPriceHistory_Select_Single(item, sqlConn);
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

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Select_Single(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProductPriceHistory_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Select_Single(CostCenterProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterProductPriceHistory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterProductPriceHistory_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostcenterProductPriceHistoryID", Value = item.CostcenterProductPriceHistoryID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterProductPriceHistory>(Stock_Translator.Translate_CostCenterProductPriceHistory);
                        Log.Information("CostCenterProductPriceHistory found: CostcenterProductPriceHistoryID={CostcenterProductPriceHistoryID}, FK_CostCenterProductID={FK_CostCenterProductID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostcenterProductPriceHistoryID, resultItem.FK_CostCenterProductID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.ValidFrom, resultItem.ValidTo, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterProductPriceHistory found with the given CostCenterProductPriceHistoryID.");
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

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Insert_Transaction(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProductPriceHistory_Insert(item, sqlConn);
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

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Insert(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProductPriceHistory_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Insert(CostCenterProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterProductPriceHistory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterProductPriceHistory_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterProductID", Value = item.FK_CostCenterProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Value", Value = item.Value }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ItemPrice", Value = item.ItemPrice }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterProductPriceHistory>(Stock_Translator.Translate_CostCenterProductPriceHistory);
                        Log.Information("CostCenterProductPriceHistory found: CostcenterProductPriceHistoryID={CostcenterProductPriceHistoryID}, FK_CostCenterProductID={FK_CostCenterProductID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostcenterProductPriceHistoryID, resultItem.FK_CostCenterProductID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.ValidFrom, resultItem.ValidTo, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenterProductPriceHistory failed to create.");
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

        public static async Task<List<CostCenterProductPriceHistory>> POS_CostCenterProductPriceHistory_Select_All_Transaction(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProductPriceHistory_Select_All(item, sqlConn);
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

        public static async Task<List<CostCenterProductPriceHistory>> POS_CostCenterProductPriceHistory_Select_All(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProductPriceHistory_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CostCenterProductPriceHistory>> POS_CostCenterProductPriceHistory_Select_All(CostCenterProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                List<CostCenterProductPriceHistory> resultItem = new List<CostCenterProductPriceHistory>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterProductPriceHistory_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CostCenterProductPriceHistory>(Stock_Translator.Translate_CostCenterProductPriceHistory));
                        Log.Information("CostCenterProductPriceHistory records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterProductPriceHistory records found.");
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

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Update_Transaction(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProductPriceHistory_Update(item, sqlConn);
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

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Update(CostCenterProductPriceHistory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterProductPriceHistory_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterProductPriceHistory> POS_CostCenterProductPriceHistory_Update(CostCenterProductPriceHistory item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterProductPriceHistory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterProductPriceHistory_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostcenterProductPriceHistoryID", Value = item.CostcenterProductPriceHistoryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterProductID", Value = item.FK_CostCenterProductID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Value", Value = item.Value }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ItemPrice", Value = item.ItemPrice }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterProductPriceHistory>(Stock_Translator.Translate_CostCenterProductPriceHistory);
                        Log.Information("CostCenterProductPriceHistory found: CostcenterProductPriceHistoryID={CostcenterProductPriceHistoryID}, FK_CostCenterProductID={FK_CostCenterProductID}, Value={Value}, Vat={Vat}, ItemPrice={ItemPrice}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostcenterProductPriceHistoryID, resultItem.FK_CostCenterProductID, resultItem.Value, resultItem.Vat, resultItem.ItemPrice, resultItem.ValidFrom, resultItem.ValidTo, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenterProductPriceHistory failed to update.");
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

        #region POS_PriceCodes

        public static async Task<PriceCodes> POS_PriceCodes_Select_Single_Transaction(PriceCodes item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PriceCodes_Select_Single(item, sqlConn);
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

        public static async Task<PriceCodes> POS_PriceCodes_Select_Single(PriceCodes item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PriceCodes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PriceCodes> POS_PriceCodes_Select_Single(PriceCodes item, SqlConnection sqlConn)
        {
            try
            {
                PriceCodes resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PriceCodes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PriceCodeID", Value = item.PriceCodeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PriceCodes>(Stock_Translator.Translate_PriceCodes);
                        Log.Information("PriceCodes found: PriceCodeID={PriceCodeID}, PriceCode={PriceCode}, Description={Description}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.PriceCodeID, resultItem.PriceCode, resultItem.Description, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PriceCodes found with the given PriceCodesID.");
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

        public static async Task<PriceCodes> POS_PriceCodes_Insert_Transaction(PriceCodes item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PriceCodes_Insert(item, sqlConn);
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

        public static async Task<PriceCodes> POS_PriceCodes_Insert(PriceCodes item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PriceCodes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PriceCodes> POS_PriceCodes_Insert(PriceCodes item, SqlConnection sqlConn)
        {
            try
            {
                PriceCodes resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PriceCodes_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PriceCode", Value = item.PriceCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PriceCodes>(Stock_Translator.Translate_PriceCodes);
                        Log.Information("PriceCodes found: PriceCodeID={PriceCodeID}, PriceCode={PriceCode}, Description={Description}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.PriceCodeID, resultItem.PriceCode, resultItem.Description, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PriceCodes failed to create.");
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

        public static async Task<List<PriceCodes>> POS_PriceCodes_Select_All_Transaction(PriceCodes item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PriceCodes_Select_All(item, sqlConn);
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

        public static async Task<List<PriceCodes>> POS_PriceCodes_Select_All(PriceCodes item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PriceCodes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PriceCodes>> POS_PriceCodes_Select_All(PriceCodes item, SqlConnection sqlConn)
        {
            try
            {
                List<PriceCodes> resultItem = new List<PriceCodes>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PriceCodes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PriceCodes>(Stock_Translator.Translate_PriceCodes));
                        Log.Information("PriceCodes records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PriceCodes records found.");
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

        public static async Task<PriceCodes> POS_PriceCodes_Update_Transaction(PriceCodes item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PriceCodes_Update(item, sqlConn);
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

        public static async Task<PriceCodes> POS_PriceCodes_Update(PriceCodes item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PriceCodes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PriceCodes> POS_PriceCodes_Update(PriceCodes item, SqlConnection sqlConn)
        {
            try
            {
                PriceCodes resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PriceCodes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PriceCodeID", Value = item.PriceCodeID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PriceCode", Value = item.PriceCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PriceCodes>(Stock_Translator.Translate_PriceCodes);
                        Log.Information("PriceCodes found: PriceCodeID={PriceCodeID}, PriceCode={PriceCode}, Description={Description}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.PriceCodeID, resultItem.PriceCode, resultItem.Description, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PriceCodes failed to update.");
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

        #region POS_DebtorProductPrices

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Select_Single_Transaction(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPrices_Select_Single(item, sqlConn);
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

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Select_Single(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPrices_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Select_Single(DebtorProductPrice item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProductPrice resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProductPrices_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorProductPriceID", Value = item.DebtorProductPriceID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProductPrice>(Stock_Translator.Translate_DebtorProductPrice);
                        Log.Information("DebtorProductPrice found: DebtorProductPriceID={DebtorProductPriceID}, FK_DebtorProductID={FK_DebtorProductID}, FK_PriceCodeID={FK_PriceCodeID}, FK_TaxID={FK_TaxID}, ItemPrice={ItemPrice}, Inclusive={Inclusive}, Vat={Vat}, StartDate={StartDate}, EndDate={EndDate}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_DefaultUnitID={FK_DefaultUnitID}", resultItem.DebtorProductPriceID, resultItem.FK_DebtorProductID, resultItem.FK_PriceCodeID, resultItem.FK_TaxID, resultItem.ItemPrice, resultItem.Inclusive, resultItem.Vat, resultItem.StartDate, resultItem.EndDate, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_DefaultUnitID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorProductPrice found with the given DebtorProductPriceID.");
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

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Insert_Transaction(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPrices_Insert(item, sqlConn);
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

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Insert(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPrices_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Insert(DebtorProductPrice item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProductPrice resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProductPrices_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorProductID", Value = item.FK_DebtorProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PriceCodeID", Value = item.FK_PriceCodeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxID", Value = item.FK_TaxID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ItemPrice", Value = item.ItemPrice }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@Inclusive", Value = item.Inclusive }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@StartDate", Value = item.StartDate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@EndDate", Value = item.EndDate }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DefaultUnitID", Value = item.FK_DefaultUnitID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProductPrice>(Stock_Translator.Translate_DebtorProductPrice);
                        Log.Information("DebtorProductPrice found: DebtorProductPriceID={DebtorProductPriceID}, FK_DebtorProductID={FK_DebtorProductID}, FK_PriceCodeID={FK_PriceCodeID}, FK_TaxID={FK_TaxID}, ItemPrice={ItemPrice}, Inclusive={Inclusive}, Vat={Vat}, StartDate={StartDate}, EndDate={EndDate}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_DefaultUnitID={FK_DefaultUnitID}", resultItem.DebtorProductPriceID, resultItem.FK_DebtorProductID, resultItem.FK_PriceCodeID, resultItem.FK_TaxID, resultItem.ItemPrice, resultItem.Inclusive, resultItem.Vat, resultItem.StartDate, resultItem.EndDate, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_DefaultUnitID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorProductPrice failed to create.");
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

        public static async Task<List<DebtorProductPrice>> POS_DebtorProductPrices_Select_All_Transaction(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPrices_Select_All(item, sqlConn);
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

        public static async Task<List<DebtorProductPrice>> POS_DebtorProductPrices_Select_All(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPrices_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorProductPrice>> POS_DebtorProductPrices_Select_All(DebtorProductPrice item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorProductPrice> resultItem = new List<DebtorProductPrice>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProductPrices_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorProductPrice>(Stock_Translator.Translate_DebtorProductPrice));
                        Log.Information("DebtorProductPrice records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorProductPrice records found.");
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

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Update_Transaction(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPrices_Update(item, sqlConn);
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

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Update(DebtorProductPrice item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorProductPrices_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProductPrice> POS_DebtorProductPrices_Update(DebtorProductPrice item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProductPrice resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorProductPrices_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorProductPriceID", Value = item.DebtorProductPriceID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorProductID", Value = item.FK_DebtorProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PriceCodeID", Value = item.FK_PriceCodeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TaxID", Value = item.FK_TaxID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ItemPrice", Value = item.ItemPrice }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@Inclusive", Value = item.Inclusive }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Vat", Value = item.Vat }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@StartDate", Value = item.StartDate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@EndDate", Value = item.EndDate }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DefaultUnitID", Value = item.FK_DefaultUnitID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProductPrice>(Stock_Translator.Translate_DebtorProductPrice);
                        Log.Information("DebtorProductPrice found: DebtorProductPriceID={DebtorProductPriceID}, FK_DebtorProductID={FK_DebtorProductID}, FK_PriceCodeID={FK_PriceCodeID}, FK_TaxID={FK_TaxID}, ItemPrice={ItemPrice}, Inclusive={Inclusive}, Vat={Vat}, StartDate={StartDate}, EndDate={EndDate}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_DefaultUnitID={FK_DefaultUnitID}", resultItem.DebtorProductPriceID, resultItem.FK_DebtorProductID, resultItem.FK_PriceCodeID, resultItem.FK_TaxID, resultItem.ItemPrice, resultItem.Inclusive, resultItem.Vat, resultItem.StartDate, resultItem.EndDate, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_DefaultUnitID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorProductPrice failed to update.");
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
