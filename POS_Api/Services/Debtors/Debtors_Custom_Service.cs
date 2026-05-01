using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.POS_CostCenterPrinters;
using POS_Common.Models.Debtors.POS_CostCenters;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.ContactTypes;
using POS_Common.Models.EntityData.Entities;
using POS_Common.Models.EntityData.EntityAddresses;
using POS_Common.Models.EntityData.EntityContacts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Models.Auth.ApplicationPermissions;
using TMIS_Common.Sql;

namespace POS_Api.Services.Debtors
{
    public class Debtors_Custom_Service: Debtors_Custom_SP_Service
    {
        #region Methods

        #region Debtors

        public static async Task<List<Debtor>> Debtors_Select_All_Debtors(Debtor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Debtors_Select_All_Debtors(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Debtor>> Debtors_Select_All_Debtors(Debtor item, SqlConnection sqlConn)
        {
            try
            {
                List<Debtor> resultItem = new List<Debtor>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtors_select_all_debtors"))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Debtor>(Debtors_Translator.Translate_Debtor_Debtor));
                        Log.Information("Debtor records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Debtor records found.");
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

        public static async Task<Debtor> Debtors_Select_Single_Name(Debtor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Debtors_Select_Single_Name(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Debtor> Debtors_Select_Single_Name(Debtor item, SqlConnection sqlConn)
        {
            try
            {
                Debtor resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Debtors_select_single_name",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Debtor>(Debtors_Translator.Translate_Debtor);
                        Log.Information("Debtor found: DebtorID={DebtorID}, ShortCode={ShortCode}, Name={Name}, FK_MasterDebtorID={FK_MasterDebtorID}, IsMasterDebtor={IsMasterDebtor}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorID, resultItem.ShortCode, resultItem.Name, resultItem.FK_MasterDebtorID, resultItem.IsMasterDebtor, resultItem.DateCreated, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Debtor found with the given DebtorID.");
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

        public static async Task<Debtor> Debtors_Sync_Insert(Debtor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Debtors_Sync_Insert(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Debtor> Debtors_Sync_Insert(Debtor item, SqlConnection sqlConn)
        {
            try
            {
                Debtor resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Debtors_sync_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MasterDebtorID", Value = item.FK_MasterDebtorID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsMasterDebtor", Value = item.IsMasterDebtor }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Debtor>(Debtors_Translator.Translate_Debtor);
                        Log.Information("Debtor found: DebtorID={DebtorID}, ShortCode={ShortCode}, Name={Name}, FK_MasterDebtorID={FK_MasterDebtorID}, IsMasterDebtor={IsMasterDebtor}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, BC_ID={BC_ID}", resultItem.DebtorID, resultItem.ShortCode, resultItem.Name, resultItem.FK_MasterDebtorID, resultItem.IsMasterDebtor, resultItem.DateCreated, resultItem.DateUpdated, resultItem.BC_ID);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Debtor failed to create.");
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

        #region Debtor Addresses

        public static async Task<List<Debtor>> DebtorAddresses_Select_All(Debtor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorAddresses_Select_All(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Debtor>> DebtorAddresses_Select_All(Debtor item, SqlConnection sqlConn)
        {
            try
            {
                List<Debtor> resultItem = new List<Debtor>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtors_select_all_addresses",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorID", Value = item.DebtorID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Debtor>(Debtors_Translator.Translate_Debtor_Address));
                        Log.Information("Debtor records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Debtor records found.");
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

        public static async Task<List<AddressType>> DebtorAddresses_Select_Debtor(AddressType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorAddresses_Select_Debtor(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<AddressType>> DebtorAddresses_Select_Debtor(AddressType item, SqlConnection sqlConn)
        {
            try
            {
                List<AddressType> resultItem = new List<AddressType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorAddressess_select_all_debtor"))
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

        public static async Task<Entity> Entity_Select_Debtor(Entity item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entity_Select_Debtor(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Entity> Entity_Select_Debtor(Entity item, SqlConnection sqlConn)
        {
            try
            {
                Entity resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Entities_select_debtor"))
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

        public static async Task<EntityContact> EntityContacts_Select_Single_ContactID(EntityContact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Select_Single_ContactID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntityContact> EntityContacts_Select_Single_ContactID(EntityContact item, SqlConnection sqlConn)
        {
            try
            {
                EntityContact resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "EntityContact_select_contactID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ContactID", Value = item.FK_ContactID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntityContact>(EntityData_Translator.Translate_EntityContact);
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

        #region Debtor Contacts

        public static async Task<List<Debtor>> DebtorContacts_Select_All(Debtor item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorContacts_Select_All(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Debtor>> DebtorContacts_Select_All(Debtor item, SqlConnection sqlConn)
        {
            try
            {
                List<Debtor> resultItem = new List<Debtor>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtors_select_all_contacts",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorID", Value = item.DebtorID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Debtor>(Debtors_Translator.Translate_Debtor_Contact));
                        Log.Information("Debtor records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Debtor records found.");
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

        #region Cost Centers

        public static async Task<List<CostCenter>> CostCenters_Select_All(CostCenter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CostCenters_Select_All(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CostCenter>> CostCenters_Select_All(CostCenter item, SqlConnection sqlConn)
        {
            try
            {
                List<CostCenter> resultItem = new List<CostCenter>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "CostCenters_select_all",
                        null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CostCenter>(Debtors_Translator.Translate_CostCenter));
                        Log.Information("CostCenter records found: ", resultItem.Count.ToString());

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
        #endregion

        #region Cost Center Printers

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Switch_Link(CostCenterPrinter item, int? fkUserID, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Switch_Link(item, fkUserID, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in custom switch link code");
                return default;
            }
        }

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Switch_Link(CostCenterPrinter item, int? fkUserID, SqlConnection sqlConn)
        {
            try
            {
                CostCenterPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "POS_CostCenterPrinters_switch_link",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PrinterID", Value = item.FK_PrinterID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UserID", Value = (object)fkUserID ?? DBNull.Value }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterPrinter>(Debtors_Translator.Translate_CostCenterPrinter);
                        Log.Information("CostCenterPrinter switch result: CostCenterPrinterID={CostCenterPrinterID}", resultItem.CostCenterPrinterID);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CostCenterPrinter switch result returned.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in custom switch link code");
                return default;
            }
        }

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Delete(CostCenterPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_CostCenterPrinters_Delete(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in custom delete code");
                return default;
            }
        }

        public static async Task<CostCenterPrinter> POS_CostCenterPrinters_Delete(CostCenterPrinter item, SqlConnection sqlConn)
        {
            try
            {
                CostCenterPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "POS_CostCenterPrinters_delete",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CostCenterPrinterID", Value = item.CostCenterPrinterID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CostCenterPrinter>(Debtors_Translator.Translate_CostCenterPrinter);
                        Log.Information("CostCenterPrinter deleted: CostCenterPrinterID={CostCenterPrinterID}", resultItem.CostCenterPrinterID);

                        return resultItem;
                    }
                    else
                    {
                        Log.Information("CostCenterPrinter delete returned no rows (CostCenterPrinterID={CostCenterPrinterID}).", item.CostCenterPrinterID);
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in custom delete code");
                return default;
            }
        }

        #endregion
        #endregion
    }
}
