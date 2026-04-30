using Microsoft.Data.SqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;
using POS_Api.Translators;

using POS_Common.Models.Stock.Custom.StockRequestSelectAllStockRequest;
using POS_Common.Models.Stock.Custom.StockRequestSelectSingleNumber;
using POS_Common.Models.Stock.Custom.StockRequestLinesSelectAllStockRequestLines;
using POS_Common.Models.Stock.Custom.StockRequestReviewersSelectByDebtorRole;

namespace POS_Api.Services.Stock
{
    public abstract class Stock_Custom_SP_Service : Stock_Base_Service
    {
        #region Custom Stored Procedures

        #region StockRequestLinesSelectAllStockRequestLines

        public static async Task<List<Res_StockRequestLinesSelectAllStockRequestLines>> StockRequestLinesSelectAllStockRequestLines_Transaction(Req_StockRequestLinesSelectAllStockRequestLines item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StockRequestLinesSelectAllStockRequestLines(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_StockRequestLinesSelectAllStockRequestLines>> StockRequestLinesSelectAllStockRequestLines(Req_StockRequestLinesSelectAllStockRequestLines item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await StockRequestLinesSelectAllStockRequestLines(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_StockRequestLinesSelectAllStockRequestLines>> StockRequestLinesSelectAllStockRequestLines(Req_StockRequestLinesSelectAllStockRequestLines item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_StockRequestLinesSelectAllStockRequestLines> resultItem = new List<Res_StockRequestLinesSelectAllStockRequestLines>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "stockRequestLines_select_all_stockRequestLines",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StockRequestID", Value = (object)item.FKStockRequestID ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_StockRequestLinesSelectAllStockRequestLines>(Stock_Translator.Translate_StockRequestLinesSelectAllStockRequestLines));
                        Log.Information("StockRequestLinesSelectAllStockRequestLines records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for StockRequestLinesSelectAllStockRequestLines.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        #endregion

        #region StockRequestReviewersSelectByDebtorRole

        public static async Task<List<Res_StockRequestReviewersSelectByDebtorRole>> StockRequestReviewersSelectByDebtorRole_Transaction(Req_StockRequestReviewersSelectByDebtorRole item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StockRequestReviewersSelectByDebtorRole(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_StockRequestReviewersSelectByDebtorRole>> StockRequestReviewersSelectByDebtorRole(Req_StockRequestReviewersSelectByDebtorRole item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await StockRequestReviewersSelectByDebtorRole(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_StockRequestReviewersSelectByDebtorRole>> StockRequestReviewersSelectByDebtorRole(Req_StockRequestReviewersSelectByDebtorRole item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_StockRequestReviewersSelectByDebtorRole> resultItem = new List<Res_StockRequestReviewersSelectByDebtorRole>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_StockRequestReviewers_select_by_debtor_role",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToDebtorID", Value = (object)item.FKToDebtorID ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Role", Value = (object)item.Role ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_StockRequestReviewersSelectByDebtorRole>(Stock_Translator.Translate_StockRequestReviewersSelectByDebtorRole));
                        Log.Information("StockRequestReviewersSelectByDebtorRole records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for StockRequestReviewersSelectByDebtorRole.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        #endregion

        #region StockRequestSelectAllStockRequest

        public static async Task<List<Res_StockRequestSelectAllStockRequest>> StockRequestSelectAllStockRequest_Transaction(Req_StockRequestSelectAllStockRequest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StockRequestSelectAllStockRequest(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_StockRequestSelectAllStockRequest>> StockRequestSelectAllStockRequest(Req_StockRequestSelectAllStockRequest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await StockRequestSelectAllStockRequest(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_StockRequestSelectAllStockRequest>> StockRequestSelectAllStockRequest(Req_StockRequestSelectAllStockRequest item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_StockRequestSelectAllStockRequest> resultItem = new List<Res_StockRequestSelectAllStockRequest>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "stockRequest_select_all_stockRequest",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToDebtorID", Value = (object)item.FKToDebtorID ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_FromDebtorID", Value = (object)item.FKFromDebtorID ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderStatusID", Value = (object)item.FKOrderStatusID ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_StockRequestSelectAllStockRequest>(Stock_Translator.Translate_StockRequestSelectAllStockRequest));
                        Log.Information("StockRequestSelectAllStockRequest records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for StockRequestSelectAllStockRequest.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        #endregion

        #region StockRequestSelectSingleNumber

        public static async Task<List<Res_StockRequestSelectSingleNumber>> StockRequestSelectSingleNumber_Transaction(Req_StockRequestSelectSingleNumber item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StockRequestSelectSingleNumber(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_StockRequestSelectSingleNumber>> StockRequestSelectSingleNumber(Req_StockRequestSelectSingleNumber item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await StockRequestSelectSingleNumber(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_StockRequestSelectSingleNumber>> StockRequestSelectSingleNumber(Req_StockRequestSelectSingleNumber item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_StockRequestSelectSingleNumber> resultItem = new List<Res_StockRequestSelectSingleNumber>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "stockRequest_select_single_number",
                    new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RefNumber", Value = (object)item.RefNumber ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_StockRequestSelectSingleNumber>(Stock_Translator.Translate_StockRequestSelectSingleNumber));
                        Log.Information("StockRequestSelectSingleNumber records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for StockRequestSelectSingleNumber.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        #endregion

        #endregion
    }
}
