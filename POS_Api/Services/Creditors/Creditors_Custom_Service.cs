using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.Entities;
using POS_Common.Models.EntityData.EntityAddresses;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.Creditors
{
    public class Creditors_Custom_Service : Creditors_Custom_SP_Service
    {
        #region Methods

        #region Creditors

        public static async Task<List<Creditor>> Creditors_Select_All_Creditors(Creditor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Select_All_Creditors(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Creditor>> Creditors_Select_All_Creditors(Creditor item, SqlConnection sqlConn)
        {
            try
            {
                List<Creditor> resultItem = new List<Creditor>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "creditors_select_all_creditors"))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Creditor>(Creditors_Translator.Translate_Creditor_Creditor));
                        Log.Information("Creditor records found: ", resultItem.Count.ToString());

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

        public static async Task<List<AddressType>> CreditorAddresses_Select_Creditor(AddressType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorAddresses_Select_Creditor(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<AddressType>> CreditorAddresses_Select_Creditor(AddressType item, SqlConnection sqlConn)
        {
            try
            {
                List<AddressType> resultItem = new List<AddressType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "creditorAddressess_select_all_creditor"))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<AddressType>(EntityData_Translator.Translate_AddressType));
                        Log.Information("Address type records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Address type records found.");
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

        public static async Task<Creditor> Creditors_Select_Single_Name(Creditor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Select_Single_Name(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Creditor> Creditors_Select_Single_Name(Creditor item, SqlConnection sqlConn)
        {
            try
            {
                Creditor resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Creditors_select_single_name",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Creditor>(Creditors_Translator.Translate_Creditor);
                        Log.Information("Creditor found: CreditorID={CreditorID}, ShortCode={ShortCode}, Name={Name}, FK_MasterCreditorID={FK_MasterCreditorID}, IsMasterCreditor={IsMasterCreditor}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CreditorID, resultItem.ShortCode, resultItem.Name, resultItem.FK_MasterCreditorID, resultItem.IsMasterCreditor, resultItem.DateCreated, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Creditor found with the given DebtorID.");
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

        public static async Task<Creditor> Creditors_Sync_Insert(Creditor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Creditors_Sync_Insert(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Creditor> Creditors_Sync_Insert(Creditor item, SqlConnection sqlConn)
        {
            try
            {
                Creditor resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Creditors_sync_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MasterCreditorID", Value = item.FK_MasterCreditorID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsMasterCreditor", Value = item.IsMasterCreditor }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }))
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
        #endregion

        #region Creditor Addresses

        public static async Task<EntityAddress> EntityAddresses_Select_Single_AddressID(EntityAddress item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Select_Single_AddressID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntityAddress> EntityAddresses_Select_Single_AddressID(EntityAddress item, SqlConnection sqlConn)
        {
            try
            {
                EntityAddress resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "EntityAddress_select_addressID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AddressID", Value = item.FK_AddressID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntityAddress>(EntityData_Translator.Translate_EntityAddress);
                        Log.Information("Entity Address found");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Entity found with the given EntityID.");
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

        #region Creditor Contacts

        public static async Task<List<Creditor>> CreditorContacts_Select_All(Creditor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CreditorContacts_Select_All(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Creditor>> CreditorContacts_Select_All(Creditor item, SqlConnection sqlConn)
        {
            try
            {
                List<Creditor> resultItem = new List<Creditor>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "creditors_select_all_contacts",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CreditorID", Value = item.CreditorID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityID", Value = item.EntityID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Creditor>(Creditors_Translator.Translate_Creditor_Contact));
                        Log.Information("Creditor records found: ", resultItem.Count.ToString());

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

        public static async Task<Entity> Entity_Select_Creditor(Entity item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entity_Select_Creditor(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Entity> Entity_Select_Creditor(Entity item, SqlConnection sqlConn)
        {
            try
            {
                Entity resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Entities_select_creditor"))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Entity>(EntityData_Translator.Translate_Entity);
                        Log.Information("Entity found: EntityID={EntityID}, Name={Name}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityID, resultItem.Name, resultItem.DateCreated, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Entity found with the given EntityID.");
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

        #endregion
    }
}
