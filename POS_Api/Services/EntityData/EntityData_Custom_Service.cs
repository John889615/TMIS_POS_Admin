using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.Debtors.POS_LocationCurrencies;
using POS_Common.Models.EntityData.EntityAddresses;
using POS_Common.Models.EntityData.EntityContacts;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.Models.Menu.POS_MenuItems;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.EntityData
{
    public class EntityData_Custom_Service: EntityData_Custom_SP_Service
    {
        public static async Task<List<EntityAddress>> EntityAddresses_Select_All_Entity(EntityAddress item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Select_All_Entity(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<EntityAddress>> EntityAddresses_Select_All_Entity(EntityAddress item, SqlConnection sqlConn)
        {
            try
            {
                List<EntityAddress> resultItem = new List<EntityAddress>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "EntityAddresses_select_all_entity",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityID", Value = item.FK_EntityID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityRecordID", Value = item.EntityRecordID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<EntityAddress>(EntityData_Translator.Translate_EntityAddress_Entity));
                        Log.Information("EntityAddress records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No EntityAddress records found.");
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

        public static async Task<List<EntityContact>> EntityContacts_Select_All_Entity(EntityContact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Select_All_Entity(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<EntityContact>> EntityContacts_Select_All_Entity(EntityContact item, SqlConnection sqlConn)
        {
            try
            {
                List<EntityContact> resultItem = new List<EntityContact>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "EntityContacts_select_all_entity",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityID", Value = item.FK_EntityID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityRecordID", Value = item.EntityRecordID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<EntityContact>(EntityData_Translator.Translate_EntityContact_Entity));
                        Log.Information("EntityAddress records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No EntityAddress records found.");
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

        #region Images

        public static async Task<Image> POS_Images_Insert_Replace(Image item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Insert_Replace(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Image> POS_Images_Insert_Replace(Image item, SqlConnection sqlConn)
        {
            try
            {
                Image resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "POS_Images_insert_replace",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ImageCategoryID", Value = item.FK_ImageCategoryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ItemID", Value = item.FK_ItemID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@FileSystemPath", Value = item.FileSystemPath }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RelativePath", Value = item.RelativePath }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ImageName", Value = item.ImageName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@FileExtension", Value = item.FileExtension }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ImageUrl", Value = item.ImageUrl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LocalUrl", Value = item.LocalUrl }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Image>(EntityData_Translator.Translate_Image);
                        Log.Information("Image found: ImageID={ImageID}, FK_ImageCategoryID={FK_ImageCategoryID}, FK_ItemID={FK_ItemID}, FileSystemPath={FileSystemPath}, RelativePath={RelativePath}, ImageName={ImageName}, FileExtension={FileExtension}, ImageUrl={ImageUrl}, LocalUrl={LocalUrl}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ImageID, resultItem.FK_ImageCategoryID, resultItem.FK_ItemID, resultItem.FileSystemPath, resultItem.RelativePath, resultItem.ImageName, resultItem.FileExtension, resultItem.ImageUrl, resultItem.LocalUrl, resultItem.DateCreated, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Image failed to create.");
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

        #region Location Currency

        public static async Task<List<LocationCurrencies>> Location_Currencies_Select_Active(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Location_Currencies_Select_Active(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<LocationCurrencies>> Location_Currencies_Select_Active(LocationCurrencies item, SqlConnection sqlConn)
        {
            try
            {
                List<LocationCurrencies> resultItem = new List<LocationCurrencies>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Location_Currency_Select_Active",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<LocationCurrencies>(EntityData_Translator.Translate_LocationCurrency));
                        Log.Information("EntityAddress records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No EntityAddress records found.");
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

        public static async Task<LocationCurrencies> Location_Currency_Remove(LocationCurrencies item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Location_Currency_Remove(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<LocationCurrencies> Location_Currency_Remove(LocationCurrencies item, SqlConnection sqlConn)
        {
            try
            {
                LocationCurrencies resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "locationCurrency_remove",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@LocationCurrencyID", Value = item.LocationCurrencyID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<LocationCurrencies>(Debtors_Translator.Translate_LocationCurrencies);
                        Log.Information("POS_MenuItemProduct Removed");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Menu Item Product Removed");
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
