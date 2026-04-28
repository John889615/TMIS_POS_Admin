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

using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Creditors.CreditorTypeMappings;
using POS_Common.Models.Creditors.CreditorTypes;

namespace POS_Api.Services.Creditors
{
    public abstract class Creditors_Base_Service
    {
        #region Creditors

        public static async Task<Creditor> Creditors_Select_Single_Transaction(Creditor item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Select_Single(item, sqlConn);
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

        public static async Task<Creditor> Creditors_Select_Single(Creditor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Creditor> Creditors_Select_Single(Creditor item, SqlConnection sqlConn)
        {
            try
            {
                Creditor resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Creditors_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CreditorID", Value = item.CreditorID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Creditor>(Creditors_Translator.Translate_Creditor);
                        Log.Information("Creditor found: CreditorID={CreditorID}, ShortCode={ShortCode}, Name={Name}, FK_MasterCreditorID={FK_MasterCreditorID}, IsMasterCreditor={IsMasterCreditor}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, BC_ID={BC_ID}", resultItem.CreditorID, resultItem.ShortCode, resultItem.Name, resultItem.FK_MasterCreditorID, resultItem.IsMasterCreditor, resultItem.DateCreated, resultItem.DateUpdated, resultItem.BC_ID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Creditor found with the given CreditorID.");
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

        public static async Task<Creditor> Creditors_Insert_Transaction(Creditor item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Insert(item, sqlConn);
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

        public static async Task<Creditor> Creditors_Insert(Creditor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Creditor> Creditors_Insert(Creditor item, SqlConnection sqlConn)
        {
            try
            {
                Creditor resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Creditors_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MasterCreditorID", Value = item.FK_MasterCreditorID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsMasterCreditor", Value = item.IsMasterCreditor }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Creditor>(Creditors_Translator.Translate_Creditor);
                        Log.Information("Creditor found: CreditorID={CreditorID}, ShortCode={ShortCode}, Name={Name}, FK_MasterCreditorID={FK_MasterCreditorID}, IsMasterCreditor={IsMasterCreditor}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, BC_ID={BC_ID}", resultItem.CreditorID, resultItem.ShortCode, resultItem.Name, resultItem.FK_MasterCreditorID, resultItem.IsMasterCreditor, resultItem.DateCreated, resultItem.DateUpdated, resultItem.BC_ID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Creditor failed to create.");
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

        public static async Task<List<Creditor>> Creditors_Select_All_Transaction(Creditor item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Select_All(item, sqlConn);
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

        public static async Task<List<Creditor>> Creditors_Select_All(Creditor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Creditor>> Creditors_Select_All(Creditor item, SqlConnection sqlConn)
        {
            try
            {
                List<Creditor> resultItem = new List<Creditor>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Creditors_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Creditor>(Creditors_Translator.Translate_Creditor));
                        Log.Information("Creditor records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Creditor records found.");
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

        public static async Task<Creditor> Creditors_Update_Transaction(Creditor item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Update(item, sqlConn);
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

        public static async Task<Creditor> Creditors_Update(Creditor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Creditor> Creditors_Update(Creditor item, SqlConnection sqlConn)
        {
            try
            {
                Creditor resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Creditors_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CreditorID", Value = item.CreditorID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MasterCreditorID", Value = item.FK_MasterCreditorID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsMasterCreditor", Value = item.IsMasterCreditor }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Creditor>(Creditors_Translator.Translate_Creditor);
                        Log.Information("Creditor found: CreditorID={CreditorID}, ShortCode={ShortCode}, Name={Name}, FK_MasterCreditorID={FK_MasterCreditorID}, IsMasterCreditor={IsMasterCreditor}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, BC_ID={BC_ID}", resultItem.CreditorID, resultItem.ShortCode, resultItem.Name, resultItem.FK_MasterCreditorID, resultItem.IsMasterCreditor, resultItem.DateCreated, resultItem.DateUpdated, resultItem.BC_ID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Creditor failed to update.");
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

        #region CreditorTypeMappings

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Select_Single_Transaction(CreditorTypeMapping item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypeMappings_Select_Single(item, sqlConn);
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

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Select_Single(CreditorTypeMapping item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypeMappings_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Select_Single(CreditorTypeMapping item, SqlConnection sqlConn)
        {
            try
            {
                CreditorTypeMapping resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CreditorTypeMappings_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CreditorTypeMappingID", Value = item.CreditorTypeMappingID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CreditorTypeMapping>(Creditors_Translator.Translate_CreditorTypeMapping);
                        Log.Information("CreditorTypeMapping found: CreditorTypeMappingID={CreditorTypeMappingID}, FK_CreditorID={FK_CreditorID}, FK_CreditorTypeID={FK_CreditorTypeID}, FK_StatusID={FK_StatusID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CreditorTypeMappingID, resultItem.FK_CreditorID, resultItem.FK_CreditorTypeID, resultItem.FK_StatusID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CreditorTypeMapping found with the given CreditorTypeMappingID.");
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

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Insert_Transaction(CreditorTypeMapping item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypeMappings_Insert(item, sqlConn);
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

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Insert(CreditorTypeMapping item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypeMappings_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Insert(CreditorTypeMapping item, SqlConnection sqlConn)
        {
            try
            {
                CreditorTypeMapping resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CreditorTypeMappings_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreditorID", Value = item.FK_CreditorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreditorTypeID", Value = item.FK_CreditorTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StatusID", Value = item.FK_StatusID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CreditorTypeMapping>(Creditors_Translator.Translate_CreditorTypeMapping);
                        Log.Information("CreditorTypeMapping found: CreditorTypeMappingID={CreditorTypeMappingID}, FK_CreditorID={FK_CreditorID}, FK_CreditorTypeID={FK_CreditorTypeID}, FK_StatusID={FK_StatusID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CreditorTypeMappingID, resultItem.FK_CreditorID, resultItem.FK_CreditorTypeID, resultItem.FK_StatusID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CreditorTypeMapping failed to create.");
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

        public static async Task<List<CreditorTypeMapping>> CreditorTypeMappings_Select_All_Transaction(CreditorTypeMapping item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypeMappings_Select_All(item, sqlConn);
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

        public static async Task<List<CreditorTypeMapping>> CreditorTypeMappings_Select_All(CreditorTypeMapping item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypeMappings_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CreditorTypeMapping>> CreditorTypeMappings_Select_All(CreditorTypeMapping item, SqlConnection sqlConn)
        {
            try
            {
                List<CreditorTypeMapping> resultItem = new List<CreditorTypeMapping>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CreditorTypeMappings_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CreditorTypeMapping>(Creditors_Translator.Translate_CreditorTypeMapping));
                        Log.Information("CreditorTypeMapping records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CreditorTypeMapping records found.");
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

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Update_Transaction(CreditorTypeMapping item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypeMappings_Update(item, sqlConn);
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

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Update(CreditorTypeMapping item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypeMappings_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CreditorTypeMapping> CreditorTypeMappings_Update(CreditorTypeMapping item, SqlConnection sqlConn)
        {
            try
            {
                CreditorTypeMapping resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CreditorTypeMappings_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CreditorTypeMappingID", Value = item.CreditorTypeMappingID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreditorID", Value = item.FK_CreditorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreditorTypeID", Value = item.FK_CreditorTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StatusID", Value = item.FK_StatusID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CreditorTypeMapping>(Creditors_Translator.Translate_CreditorTypeMapping);
                        Log.Information("CreditorTypeMapping found: CreditorTypeMappingID={CreditorTypeMappingID}, FK_CreditorID={FK_CreditorID}, FK_CreditorTypeID={FK_CreditorTypeID}, FK_StatusID={FK_StatusID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CreditorTypeMappingID, resultItem.FK_CreditorID, resultItem.FK_CreditorTypeID, resultItem.FK_StatusID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CreditorTypeMapping failed to update.");
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

        #region CreditorTypes

        public static async Task<CreditorType> CreditorTypes_Select_Single_Transaction(CreditorType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypes_Select_Single(item, sqlConn);
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

        public static async Task<CreditorType> CreditorTypes_Select_Single(CreditorType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CreditorType> CreditorTypes_Select_Single(CreditorType item, SqlConnection sqlConn)
        {
            try
            {
                CreditorType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CreditorTypes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CreditorTypeID", Value = item.CreditorTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CreditorType>(Creditors_Translator.Translate_CreditorType);
                        Log.Information("CreditorType found: CreditorTypeID={CreditorTypeID}, Type={Type}, Description={Description}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CreditorTypeID, resultItem.Type, resultItem.Description, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CreditorType found with the given CreditorTypeID.");
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

        public static async Task<CreditorType> CreditorTypes_Insert_Transaction(CreditorType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypes_Insert(item, sqlConn);
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

        public static async Task<CreditorType> CreditorTypes_Insert(CreditorType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CreditorType> CreditorTypes_Insert(CreditorType item, SqlConnection sqlConn)
        {
            try
            {
                CreditorType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CreditorTypes_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Type", Value = item.Type }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CreditorType>(Creditors_Translator.Translate_CreditorType);
                        Log.Information("CreditorType found: CreditorTypeID={CreditorTypeID}, Type={Type}, Description={Description}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CreditorTypeID, resultItem.Type, resultItem.Description, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CreditorType failed to create.");
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

        public static async Task<List<CreditorType>> CreditorTypes_Select_All_Transaction(CreditorType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypes_Select_All(item, sqlConn);
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

        public static async Task<List<CreditorType>> CreditorTypes_Select_All(CreditorType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CreditorType>> CreditorTypes_Select_All(CreditorType item, SqlConnection sqlConn)
        {
            try
            {
                List<CreditorType> resultItem = new List<CreditorType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CreditorTypes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CreditorType>(Creditors_Translator.Translate_CreditorType));
                        Log.Information("CreditorType records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CreditorType records found.");
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

        public static async Task<CreditorType> CreditorTypes_Update_Transaction(CreditorType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypes_Update(item, sqlConn);
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

        public static async Task<CreditorType> CreditorTypes_Update(CreditorType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorTypes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CreditorType> CreditorTypes_Update(CreditorType item, SqlConnection sqlConn)
        {
            try
            {
                CreditorType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CreditorTypes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CreditorTypeID", Value = item.CreditorTypeID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Type", Value = item.Type }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CreditorType>(Creditors_Translator.Translate_CreditorType);
                        Log.Information("CreditorType found: CreditorTypeID={CreditorTypeID}, Type={Type}, Description={Description}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CreditorTypeID, resultItem.Type, resultItem.Description, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CreditorType failed to update.");
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
