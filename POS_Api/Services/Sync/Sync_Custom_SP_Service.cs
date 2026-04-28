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

using POS_Common.Models.Sync.Custom.SelectSiteSyncStatus;
using POS_Common.Models.Sync.Custom.SelectSiteSyncStatusForSite;
using POS_Common.Models.Sync.Custom.UpsertSiteSyncStatus;
using POS_Common.Models.Sync.Custom.UpdateLocationsLastSeen;
using POS_Common.Models.Sync.Custom.SelectLocationsSilentSites;
using POS_Common.Models.Sync.Custom.SetLocationsSilentAlert;
using POS_Common.Models.Sync.Custom.SelectLocationRecipients;

namespace POS_Api.Services.Sync
{
    public abstract class Sync_Custom_SP_Service : Sync_Base_Service
    {
        #region Custom Stored Procedures

        #region SelectLocationRecipients

        public static async Task<List<Res_SelectLocationRecipients>> SelectLocationRecipients_Transaction(Req_SelectLocationRecipients item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SelectLocationRecipients(item, sqlConn);
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

        public static async Task<List<Res_SelectLocationRecipients>> SelectLocationRecipients(Req_SelectLocationRecipients item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await SelectLocationRecipients(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_SelectLocationRecipients>> SelectLocationRecipients(Req_SelectLocationRecipients item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_SelectLocationRecipients> resultItem = new List<Res_SelectLocationRecipients>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Locations_select_recipients",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = (object)item.SiteId ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_SelectLocationRecipients>(Sync_Translator.Translate_SelectLocationRecipients));
                        Log.Information("SelectLocationRecipients records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for SelectLocationRecipients.");
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

        #region SelectLocationsSilentSites

        public static async Task<List<Res_SelectLocationsSilentSites>> SelectLocationsSilentSites_Transaction(Req_SelectLocationsSilentSites item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SelectLocationsSilentSites(item, sqlConn);
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

        public static async Task<List<Res_SelectLocationsSilentSites>> SelectLocationsSilentSites(Req_SelectLocationsSilentSites item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await SelectLocationsSilentSites(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_SelectLocationsSilentSites>> SelectLocationsSilentSites(Req_SelectLocationsSilentSites item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_SelectLocationsSilentSites> resultItem = new List<Res_SelectLocationsSilentSites>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Locations_select_silent_sites",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CutoffMinutes", Value = (object)item.CutoffMinutes ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_SelectLocationsSilentSites>(Sync_Translator.Translate_SelectLocationsSilentSites));
                        Log.Information("SelectLocationsSilentSites records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for SelectLocationsSilentSites.");
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

        #region SelectSiteSyncStatus

        public static async Task<List<Res_SelectSiteSyncStatus>> SelectSiteSyncStatus_Transaction(Req_SelectSiteSyncStatus item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SelectSiteSyncStatus(item, sqlConn);
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

        public static async Task<List<Res_SelectSiteSyncStatus>> SelectSiteSyncStatus(Req_SelectSiteSyncStatus item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await SelectSiteSyncStatus(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_SelectSiteSyncStatus>> SelectSiteSyncStatus(Req_SelectSiteSyncStatus item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_SelectSiteSyncStatus> resultItem = new List<Res_SelectSiteSyncStatus>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "SiteSyncStatus_select",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = (object)item.SiteId ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TypeName", Value = (object)item.TypeName ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_SelectSiteSyncStatus>(Sync_Translator.Translate_SelectSiteSyncStatus));
                        Log.Information("SelectSiteSyncStatus records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for SelectSiteSyncStatus.");
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

        #region SelectSiteSyncStatusForSite

        public static async Task<List<Res_SelectSiteSyncStatusForSite>> SelectSiteSyncStatusForSite_Transaction(Req_SelectSiteSyncStatusForSite item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SelectSiteSyncStatusForSite(item, sqlConn);
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

        public static async Task<List<Res_SelectSiteSyncStatusForSite>> SelectSiteSyncStatusForSite(Req_SelectSiteSyncStatusForSite item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await SelectSiteSyncStatusForSite(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_SelectSiteSyncStatusForSite>> SelectSiteSyncStatusForSite(Req_SelectSiteSyncStatusForSite item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_SelectSiteSyncStatusForSite> resultItem = new List<Res_SelectSiteSyncStatusForSite>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "SiteSyncStatus_select_for_site",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = (object)item.SiteId ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_SelectSiteSyncStatusForSite>(Sync_Translator.Translate_SelectSiteSyncStatusForSite));
                        Log.Information("SelectSiteSyncStatusForSite records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for SelectSiteSyncStatusForSite.");
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

        #region SetLocationsSilentAlert

        public static async Task<bool> SetLocationsSilentAlert_Transaction(Req_SetLocationsSilentAlert item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SetLocationsSilentAlert(item, sqlConn);
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

        public static async Task<bool> SetLocationsSilentAlert(Req_SetLocationsSilentAlert item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await SetLocationsSilentAlert(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<bool> SetLocationsSilentAlert(Req_SetLocationsSilentAlert item, SqlConnection sqlConn)
        {
            try
            {
                int rowsAffected = await SqlClient.ExecuteNonQueryStoredProcedureAsync(
                    sqlConn,
                    "Locations_set_silent_alert",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = (object)item.SiteId ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@AlertedAt", Value = (object)item.AlertedAt ?? DBNull.Value }
                );

                if (rowsAffected > 0)
                {
                    Log.Information("SetLocationsSilentAlert records affected: {Count}", rowsAffected);
                    return true;
                }
                else
                {
                    Log.Warning("No records affected for SetLocationsSilentAlert.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        #endregion

        #region UpdateLocationsLastSeen

        public static async Task<List<Res_UpdateLocationsLastSeen>> UpdateLocationsLastSeen_Transaction(Req_UpdateLocationsLastSeen item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await UpdateLocationsLastSeen(item, sqlConn);
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

        public static async Task<List<Res_UpdateLocationsLastSeen>> UpdateLocationsLastSeen(Req_UpdateLocationsLastSeen item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await UpdateLocationsLastSeen(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_UpdateLocationsLastSeen>> UpdateLocationsLastSeen(Req_UpdateLocationsLastSeen item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_UpdateLocationsLastSeen> resultItem = new List<Res_UpdateLocationsLastSeen>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Locations_update_last_seen",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = (object)item.SiteId ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@SeenAt", Value = (object)item.SeenAt ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_UpdateLocationsLastSeen>(Sync_Translator.Translate_UpdateLocationsLastSeen));
                        Log.Information("UpdateLocationsLastSeen records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for UpdateLocationsLastSeen.");
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

        #region UpsertSiteSyncStatus

        public static async Task<List<Res_UpsertSiteSyncStatus>> UpsertSiteSyncStatus_Transaction(Req_UpsertSiteSyncStatus item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await UpsertSiteSyncStatus(item, sqlConn);
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

        public static async Task<List<Res_UpsertSiteSyncStatus>> UpsertSiteSyncStatus(Req_UpsertSiteSyncStatus item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await UpsertSiteSyncStatus(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_UpsertSiteSyncStatus>> UpsertSiteSyncStatus(Req_UpsertSiteSyncStatus item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_UpsertSiteSyncStatus> resultItem = new List<Res_UpsertSiteSyncStatus>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "SiteSyncStatus_upsert",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SiteId", Value = (object)item.SiteId ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TypeName", Value = (object)item.TypeName ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@LastSuccessAt", Value = (object)item.LastSuccessAt ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@LastFailureAt", Value = (object)item.LastFailureAt ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ConsecutiveFailures", Value = (object)item.ConsecutiveFailures ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LastErrorMessage", Value = (object)item.LastErrorMessage ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@LastReportedAt", Value = (object)item.LastReportedAt ?? DBNull.Value }
                    , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@AlertSentAt", Value = (object)item.AlertSentAt ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_UpsertSiteSyncStatus>(Sync_Translator.Translate_UpsertSiteSyncStatus));
                        Log.Information("UpsertSiteSyncStatus records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for UpsertSiteSyncStatus.");
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
