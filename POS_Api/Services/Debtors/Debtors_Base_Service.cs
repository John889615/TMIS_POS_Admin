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

using POS_Common.Models.Debtors.POS_CostCenters;
using POS_Common.Models.Debtors.POS_CostCenterTypes;
using POS_Common.Models.Debtors.POS_Locations;
using POS_Common.Models.Debtors.POS_LocationCurrencies;
using POS_Common.Models.Debtors.POS_CostCenterPrinters;

namespace POS_Api.Services.Debtors
{
    public abstract class Debtors_Base_Service
    {
        #region POS_CostCenters

        public static async Task<CostCenter> POS_CostCenters_Select_Single_Transaction(CostCenter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenters_Select_Single(item, sqlConn);
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

        public static async Task<CostCenter> POS_CostCenters_Select_Single(CostCenter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenters_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenter> POS_CostCenters_Select_Single(CostCenter item, SqlConnection sqlConn)
        {
            try
            {
                CostCenter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenters_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterID", Value = item.CostCenterID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenter>(Debtors_Translator.Translate_CostCenter);
                        Log.Information("CostCenter found: CostCenterID={CostCenterID}, FK_LocationID={FK_LocationID}, Name={Name}, BillingReference={BillingReference}, FK_StatusID={FK_StatusID}, FK_CostCenterTypeID={FK_CostCenterTypeID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, BC_ID={BC_ID}", resultItem.CostCenterID, resultItem.FK_LocationID, resultItem.Name, resultItem.BillingReference, resultItem.FK_StatusID, resultItem.FK_CostCenterTypeID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.BC_ID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenter found with the given CostCenterID.");
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

        public static async Task<CostCenter> POS_CostCenters_Insert_Transaction(CostCenter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenters_Insert(item, sqlConn);
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

        public static async Task<CostCenter> POS_CostCenters_Insert(CostCenter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenters_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenter> POS_CostCenters_Insert(CostCenter item, SqlConnection sqlConn)
        {
            try
            {
                CostCenter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenters_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BillingReference", Value = item.BillingReference }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StatusID", Value = item.FK_StatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterTypeID", Value = item.FK_CostCenterTypeID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenter>(Debtors_Translator.Translate_CostCenter);
                        Log.Information("CostCenter found: CostCenterID={CostCenterID}, FK_LocationID={FK_LocationID}, Name={Name}, BillingReference={BillingReference}, FK_StatusID={FK_StatusID}, FK_CostCenterTypeID={FK_CostCenterTypeID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, BC_ID={BC_ID}", resultItem.CostCenterID, resultItem.FK_LocationID, resultItem.Name, resultItem.BillingReference, resultItem.FK_StatusID, resultItem.FK_CostCenterTypeID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.BC_ID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenter failed to create.");
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

        public static async Task<List<CostCenter>> POS_CostCenters_Select_All_Transaction(CostCenter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenters_Select_All(item, sqlConn);
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

        public static async Task<List<CostCenter>> POS_CostCenters_Select_All(CostCenter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenters_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CostCenter>> POS_CostCenters_Select_All(CostCenter item, SqlConnection sqlConn)
        {
            try
            {
                List<CostCenter> resultItem = new List<CostCenter>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenters_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CostCenter>(Debtors_Translator.Translate_CostCenter));
                        Log.Information("CostCenter records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenter records found.");
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

        public static async Task<CostCenter> POS_CostCenters_Update_Transaction(CostCenter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenters_Update(item, sqlConn);
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

        public static async Task<CostCenter> POS_CostCenters_Update(CostCenter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenters_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenter> POS_CostCenters_Update(CostCenter item, SqlConnection sqlConn)
        {
            try
            {
                CostCenter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenters_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterID", Value = item.CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BillingReference", Value = item.BillingReference }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StatusID", Value = item.FK_StatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterTypeID", Value = item.FK_CostCenterTypeID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenter>(Debtors_Translator.Translate_CostCenter);
                        Log.Information("CostCenter found: CostCenterID={CostCenterID}, FK_LocationID={FK_LocationID}, Name={Name}, BillingReference={BillingReference}, FK_StatusID={FK_StatusID}, FK_CostCenterTypeID={FK_CostCenterTypeID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, BC_ID={BC_ID}", resultItem.CostCenterID, resultItem.FK_LocationID, resultItem.Name, resultItem.BillingReference, resultItem.FK_StatusID, resultItem.FK_CostCenterTypeID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.BC_ID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenter failed to update.");
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

        #region POS_CostCenterTypes

        public static async Task<CostCenterType> POS_CostCenterTypes_Select_Single_Transaction(CostCenterType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterTypes_Select_Single(item, sqlConn);
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

        public static async Task<CostCenterType> POS_CostCenterTypes_Select_Single(CostCenterType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterTypes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterType> POS_CostCenterTypes_Select_Single(CostCenterType item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterTypes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterTypeID", Value = item.CostCenterTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterType>(Debtors_Translator.Translate_CostCenterType);
                        Log.Information("CostCenterType found: CostCenterTypeID={CostCenterTypeID}, Name={Name}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostCenterTypeID, resultItem.Name, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterType found with the given CostCenterTypeID.");
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

        public static async Task<CostCenterType> POS_CostCenterTypes_Insert_Transaction(CostCenterType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterTypes_Insert(item, sqlConn);
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

        public static async Task<CostCenterType> POS_CostCenterTypes_Insert(CostCenterType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterTypes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterType> POS_CostCenterTypes_Insert(CostCenterType item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterTypes_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterType>(Debtors_Translator.Translate_CostCenterType);
                        Log.Information("CostCenterType found: CostCenterTypeID={CostCenterTypeID}, Name={Name}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostCenterTypeID, resultItem.Name, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenterType failed to create.");
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

        public static async Task<List<CostCenterType>> POS_CostCenterTypes_Select_All_Transaction(CostCenterType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterTypes_Select_All(item, sqlConn);
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

        public static async Task<List<CostCenterType>> POS_CostCenterTypes_Select_All(CostCenterType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterTypes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CostCenterType>> POS_CostCenterTypes_Select_All(CostCenterType item, SqlConnection sqlConn)
        {
            try
            {
                List<CostCenterType> resultItem = new List<CostCenterType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterTypes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CostCenterType>(Debtors_Translator.Translate_CostCenterType));
                        Log.Information("CostCenterType records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterType records found.");
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

        public static async Task<CostCenterType> POS_CostCenterTypes_Update_Transaction(CostCenterType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterTypes_Update(item, sqlConn);
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

        public static async Task<CostCenterType> POS_CostCenterTypes_Update(CostCenterType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterTypes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterType> POS_CostCenterTypes_Update(CostCenterType item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterTypes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterTypeID", Value = item.CostCenterTypeID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterType>(Debtors_Translator.Translate_CostCenterType);
                        Log.Information("CostCenterType found: CostCenterTypeID={CostCenterTypeID}, Name={Name}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CostCenterTypeID, resultItem.Name, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenterType failed to update.");
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

        #region POS_Locations

        public static async Task<Location> POS_Locations_Select_Single_Transaction(Location item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Locations_Select_Single(item, sqlConn);
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

        public static async Task<Location> POS_Locations_Select_Single(Location item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Locations_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Location> POS_Locations_Select_Single(Location item, SqlConnection sqlConn)
        {
            try
            {
                Location resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Locations_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@LocationID", Value = item.LocationID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Location>(Debtors_Translator.Translate_Location);
                        Log.Information("Location found: LocationID={LocationID}, FK_CurrencyID={FK_CurrencyID}, BC_ID={BC_ID}, ShortCode={ShortCode}, Name={Name}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}, ContactEmail={ContactEmail}, SupportEmail={SupportEmail}, LastSyncSeenAt={LastSyncSeenAt}, SilentAlertSentAt={SilentAlertSentAt}", resultItem.LocationID, resultItem.FK_CurrencyID, resultItem.BC_ID, resultItem.ShortCode, resultItem.Name, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID, resultItem.ContactEmail, resultItem.SupportEmail, resultItem.LastSyncSeenAt, resultItem.SilentAlertSentAt);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Location found with the given LocationID.");
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

        public static async Task<Location> POS_Locations_Insert_Transaction(Location item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Locations_Insert(item, sqlConn);
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

        public static async Task<Location> POS_Locations_Insert(Location item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Locations_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Location> POS_Locations_Insert(Location item, SqlConnection sqlConn)
        {
            try
            {
                Location resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Locations_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ContactEmail", Value = item.ContactEmail }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SupportEmail", Value = item.SupportEmail }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@LastSyncSeenAt", Value = item.LastSyncSeenAt }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@SilentAlertSentAt", Value = item.SilentAlertSentAt }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Location>(Debtors_Translator.Translate_Location);
                        Log.Information("Location found: LocationID={LocationID}, FK_CurrencyID={FK_CurrencyID}, BC_ID={BC_ID}, ShortCode={ShortCode}, Name={Name}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}, ContactEmail={ContactEmail}, SupportEmail={SupportEmail}, LastSyncSeenAt={LastSyncSeenAt}, SilentAlertSentAt={SilentAlertSentAt}", resultItem.LocationID, resultItem.FK_CurrencyID, resultItem.BC_ID, resultItem.ShortCode, resultItem.Name, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID, resultItem.ContactEmail, resultItem.SupportEmail, resultItem.LastSyncSeenAt, resultItem.SilentAlertSentAt);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Location failed to create.");
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

        public static async Task<List<Location>> POS_Locations_Select_All_Transaction(Location item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Locations_Select_All(item, sqlConn);
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

        public static async Task<List<Location>> POS_Locations_Select_All(Location item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Locations_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Location>> POS_Locations_Select_All(Location item, SqlConnection sqlConn)
        {
            try
            {
                List<Location> resultItem = new List<Location>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Locations_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Location>(Debtors_Translator.Translate_Location));
                        Log.Information("Location records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Location records found.");
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

        public static async Task<Location> POS_Locations_Update_Transaction(Location item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Locations_Update(item, sqlConn);
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

        public static async Task<Location> POS_Locations_Update(Location item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Locations_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Location> POS_Locations_Update(Location item, SqlConnection sqlConn)
        {
            try
            {
                Location resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Locations_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@LocationID", Value = item.LocationID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ContactEmail", Value = item.ContactEmail }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SupportEmail", Value = item.SupportEmail }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@LastSyncSeenAt", Value = item.LastSyncSeenAt }
                        , new SqlParameter() { DbType = DbType.Object, Direction = ParameterDirection.Input, ParameterName = "@SilentAlertSentAt", Value = item.SilentAlertSentAt }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Location>(Debtors_Translator.Translate_Location);
                        Log.Information("Location found: LocationID={LocationID}, FK_CurrencyID={FK_CurrencyID}, BC_ID={BC_ID}, ShortCode={ShortCode}, Name={Name}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}, ContactEmail={ContactEmail}, SupportEmail={SupportEmail}, LastSyncSeenAt={LastSyncSeenAt}, SilentAlertSentAt={SilentAlertSentAt}", resultItem.LocationID, resultItem.FK_CurrencyID, resultItem.BC_ID, resultItem.ShortCode, resultItem.Name, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID, resultItem.ContactEmail, resultItem.SupportEmail, resultItem.LastSyncSeenAt, resultItem.SilentAlertSentAt);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Location failed to update.");
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

        #region POS_LocationCurrencies

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Select_Single_Transaction(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_LocationCurrencies_Select_Single(item, sqlConn);
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

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Select_Single(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_LocationCurrencies_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Select_Single(LocationCurrencies item, SqlConnection sqlConn)
        {
            try
            {
                LocationCurrencies resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_LocationCurrencies_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@LocationCurrencyID", Value = item.LocationCurrencyID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<LocationCurrencies>(Debtors_Translator.Translate_LocationCurrencies);
                        Log.Information("LocationCurrencies found: LocationCurrencyID={LocationCurrencyID}, FK_CurrencyID={FK_CurrencyID}, FK_LocationID={FK_LocationID}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.LocationCurrencyID, resultItem.FK_CurrencyID, resultItem.FK_LocationID, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No LocationCurrencies found with the given LocationCurrenciesID.");
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

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Insert_Transaction(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_LocationCurrencies_Insert(item, sqlConn);
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

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Insert(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_LocationCurrencies_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Insert(LocationCurrencies item, SqlConnection sqlConn)
        {
            try
            {
                LocationCurrencies resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_LocationCurrencies_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<LocationCurrencies>(Debtors_Translator.Translate_LocationCurrencies);
                        Log.Information("LocationCurrencies found: LocationCurrencyID={LocationCurrencyID}, FK_CurrencyID={FK_CurrencyID}, FK_LocationID={FK_LocationID}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.LocationCurrencyID, resultItem.FK_CurrencyID, resultItem.FK_LocationID, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("LocationCurrencies failed to create.");
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

        public static async Task<List<LocationCurrencies>> POS_LocationCurrencies_Select_All_Transaction(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_LocationCurrencies_Select_All(item, sqlConn);
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

        public static async Task<List<LocationCurrencies>> POS_LocationCurrencies_Select_All(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_LocationCurrencies_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<LocationCurrencies>> POS_LocationCurrencies_Select_All(LocationCurrencies item, SqlConnection sqlConn)
        {
            try
            {
                List<LocationCurrencies> resultItem = new List<LocationCurrencies>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_LocationCurrencies_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<LocationCurrencies>(Debtors_Translator.Translate_LocationCurrencies));
                        Log.Information("LocationCurrencies records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No LocationCurrencies records found.");
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

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Update_Transaction(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_LocationCurrencies_Update(item, sqlConn);
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

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Update(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_LocationCurrencies_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<LocationCurrencies> POS_LocationCurrencies_Update(LocationCurrencies item, SqlConnection sqlConn)
        {
            try
            {
                LocationCurrencies resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_LocationCurrencies_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@LocationCurrencyID", Value = item.LocationCurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<LocationCurrencies>(Debtors_Translator.Translate_LocationCurrencies);
                        Log.Information("LocationCurrencies found: LocationCurrencyID={LocationCurrencyID}, FK_CurrencyID={FK_CurrencyID}, FK_LocationID={FK_LocationID}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.LocationCurrencyID, resultItem.FK_CurrencyID, resultItem.FK_LocationID, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("LocationCurrencies failed to update.");
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

        #region POS_CostCenterPrinters

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Select_Single_Transaction(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Select_Single(item, sqlConn);
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

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Select_Single(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Select_Single(CostCenterPrinter item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterPrinters_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterPrinterID", Value = item.CostCenterPrinterID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterPrinter>(Debtors_Translator.Translate_CostCenterPrinter);
                        Log.Information("CostCenterPrinter found: CostCenterPrinterID={CostCenterPrinterID}, FK_CostCenterID={FK_CostCenterID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_InvoiceSlipTypeID={FK_InvoiceSlipTypeID}, FK_TabSlipTypeID={FK_TabSlipTypeID}", resultItem.CostCenterPrinterID, resultItem.FK_CostCenterID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_InvoiceSlipTypeID, resultItem.FK_TabSlipTypeID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterPrinter found with the given CostCenterPrinterID.");
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

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Insert_Transaction(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Insert(item, sqlConn);
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

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Insert(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Insert(CostCenterPrinter item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterPrinters_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PrinterID", Value = item.FK_PrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceSlipTypeID", Value = item.FK_InvoiceSlipTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TabSlipTypeID", Value = item.FK_TabSlipTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterPrinter>(Debtors_Translator.Translate_CostCenterPrinter);
                        Log.Information("CostCenterPrinter found: CostCenterPrinterID={CostCenterPrinterID}, FK_CostCenterID={FK_CostCenterID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_InvoiceSlipTypeID={FK_InvoiceSlipTypeID}, FK_TabSlipTypeID={FK_TabSlipTypeID}", resultItem.CostCenterPrinterID, resultItem.FK_CostCenterID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_InvoiceSlipTypeID, resultItem.FK_TabSlipTypeID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenterPrinter failed to create.");
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

        public static async Task<List<CostCenterPrinter>> POS_CostCenterPrinters_Select_All_Transaction(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Select_All(item, sqlConn);
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

        public static async Task<List<CostCenterPrinter>> POS_CostCenterPrinters_Select_All(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CostCenterPrinter>> POS_CostCenterPrinters_Select_All(CostCenterPrinter item, SqlConnection sqlConn)
        {
            try
            {
                List<CostCenterPrinter> resultItem = new List<CostCenterPrinter>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterPrinters_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CostCenterPrinter>(Debtors_Translator.Translate_CostCenterPrinter));
                        Log.Information("CostCenterPrinter records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterPrinter records found.");
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

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Update_Transaction(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Update(item, sqlConn);
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

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Update(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Update(CostCenterPrinter item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_CostCenterPrinters_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterPrinterID", Value = item.CostCenterPrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PrinterID", Value = item.FK_PrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceSlipTypeID", Value = item.FK_InvoiceSlipTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TabSlipTypeID", Value = item.FK_TabSlipTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterPrinter>(Debtors_Translator.Translate_CostCenterPrinter);
                        Log.Information("CostCenterPrinter found: CostCenterPrinterID={CostCenterPrinterID}, FK_CostCenterID={FK_CostCenterID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_InvoiceSlipTypeID={FK_InvoiceSlipTypeID}, FK_TabSlipTypeID={FK_TabSlipTypeID}", resultItem.CostCenterPrinterID, resultItem.FK_CostCenterID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_InvoiceSlipTypeID, resultItem.FK_TabSlipTypeID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CostCenterPrinter failed to update.");
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
