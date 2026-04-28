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

using POS_Common.Models.Logs.POS_Logs;

namespace POS_Api.Services.Logs
{
    public abstract class Logs_Base_Service
    {
        #region POS_Logs

        public static async Task<POS_Log> POS_Logs_Select_Single_Transaction(POS_Log item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Logs_Select_Single(item, sqlConn);
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

        public static async Task<POS_Log> POS_Logs_Select_Single(POS_Log item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Logs_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<POS_Log> POS_Logs_Select_Single(POS_Log item, SqlConnection sqlConn)
        {
            try
            {
                POS_Log resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Logs_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@AuditLogID", Value = item.AuditLogID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<POS_Log>(Logs_Translator.Translate_POS_Log);
                        Log.Information("POS_Log found: AuditLogID={AuditLogID}, Action={Action}, ItemID={ItemID}, Item={Item}, FK_UserID={FK_UserID}, ActionDate={ActionDate}", resultItem.AuditLogID, resultItem.Action, resultItem.ItemID, resultItem.Item, resultItem.FK_UserID, resultItem.ActionDate);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No POS_Log found with the given POS_LogID.");
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

        public static async Task<POS_Log> POS_Logs_Insert_Transaction(POS_Log item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Logs_Insert(item, sqlConn);
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

        public static async Task<POS_Log> POS_Logs_Insert(POS_Log item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Logs_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<POS_Log> POS_Logs_Insert(POS_Log item, SqlConnection sqlConn)
        {
            try
            {
                POS_Log resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Logs_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Action", Value = item.Action }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ItemID", Value = item.ItemID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Item", Value = item.Item }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ActionDate", Value = item.ActionDate }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<POS_Log>(Logs_Translator.Translate_POS_Log);
                        Log.Information("POS_Log found: AuditLogID={AuditLogID}, Action={Action}, ItemID={ItemID}, Item={Item}, FK_UserID={FK_UserID}, ActionDate={ActionDate}", resultItem.AuditLogID, resultItem.Action, resultItem.ItemID, resultItem.Item, resultItem.FK_UserID, resultItem.ActionDate);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("POS_Log failed to create.");
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

        public static async Task<List<POS_Log>> POS_Logs_Select_All_Transaction(POS_Log item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Logs_Select_All(item, sqlConn);
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

        public static async Task<List<POS_Log>> POS_Logs_Select_All(POS_Log item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Logs_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<POS_Log>> POS_Logs_Select_All(POS_Log item, SqlConnection sqlConn)
        {
            try
            {
                List<POS_Log> resultItem = new List<POS_Log>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Logs_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<POS_Log>(Logs_Translator.Translate_POS_Log));
                        Log.Information("POS_Log records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No POS_Log records found.");
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

        public static async Task<POS_Log> POS_Logs_Update_Transaction(POS_Log item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Logs_Update(item, sqlConn);
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

        public static async Task<POS_Log> POS_Logs_Update(POS_Log item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Logs_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<POS_Log> POS_Logs_Update(POS_Log item, SqlConnection sqlConn)
        {
            try
            {
                POS_Log resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Logs_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@AuditLogID", Value = item.AuditLogID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Action", Value = item.Action }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ItemID", Value = item.ItemID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Item", Value = item.Item }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = item.FK_UserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ActionDate", Value = item.ActionDate }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<POS_Log>(Logs_Translator.Translate_POS_Log);
                        Log.Information("POS_Log found: AuditLogID={AuditLogID}, Action={Action}, ItemID={ItemID}, Item={Item}, FK_UserID={FK_UserID}, ActionDate={ActionDate}", resultItem.AuditLogID, resultItem.Action, resultItem.ItemID, resultItem.Item, resultItem.FK_UserID, resultItem.ActionDate);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("POS_Log failed to update.");
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
