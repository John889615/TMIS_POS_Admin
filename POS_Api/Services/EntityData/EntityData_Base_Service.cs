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

using POS_Common.Models.EntityData.Addresses;
using POS_Common.Models.EntityData.AddressRegions;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.Contacts;
using POS_Common.Models.EntityData.ContactTypes;
using POS_Common.Models.EntityData.Continents;
using POS_Common.Models.EntityData.Countries;
using POS_Common.Models.EntityData.CountryProvinces;
using POS_Common.Models.EntityData.CountrySubregions;
using POS_Common.Models.EntityData.CountryRegions;
using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.DialingCodes;
using POS_Common.Models.EntityData.Entities;
using POS_Common.Models.EntityData.EntityAddresses;
using POS_Common.Models.EntityData.EntityContacts;
using POS_Common.Models.EntityData.Statuses;
using POS_Common.Models.EntityData.StatusGroups;
using POS_Common.Models.EntityData.TimeZones;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.EntityData.POS_PaymentTypes;
using POS_Common.Models.EntityData.TH_BookingHeaders;
using POS_Common.Models.EntityData.Guests;
using POS_Common.Models.EntityData.TH_BookingGuests;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.Models.EntityData.POS_ImageCategories;
using POS_Common.Models.EntityData.POS_PaymentTypeIcons;
using POS_Common.Models.EntityData.POS_Settings;
using POS_Common.Models.EntityData.POS_ExchangeRates;
using POS_Common.Models.EntityData.CurrencyExchangeRates;
using POS_Common.Models.EntityData.GlobalSettings;
using POS_Common.Models.EntityData.POS_SlipTypes;
using POS_Common.Models.EntityData.EntitySettings;

namespace POS_Api.Services.EntityData
{
    public abstract class EntityData_Base_Service
    {
        #region Addresses

        public static async Task<Address> Addresses_Select_Single_Transaction(Address item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Addresses_Select_Single(item, sqlConn);
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

        public static async Task<Address> Addresses_Select_Single(Address item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Addresses_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Address> Addresses_Select_Single(Address item, SqlConnection sqlConn)
        {
            try
            {
                Address resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Addresses_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@AddressID", Value = item.AddressID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Address>(EntityData_Translator.Translate_Address);
                        Log.Information("Address found: AddressID={AddressID}, FK_CountryID={FK_CountryID}, FK_ProvinceID={FK_ProvinceID}, FK_AddressRegionID={FK_AddressRegionID}, StreetAddress={StreetAddress}, Locality={Locality}, PostalCode={PostalCode}, Landmark={Landmark}, Latitude={Latitude}, Longitude={Longitude}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressID, resultItem.FK_CountryID, resultItem.FK_ProvinceID, resultItem.FK_AddressRegionID, resultItem.StreetAddress, resultItem.Locality, resultItem.PostalCode, resultItem.Landmark, resultItem.Latitude, resultItem.Longitude, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Address found with the given AddressID.");
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

        public static async Task<Address> Addresses_Insert_Transaction(Address item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Addresses_Insert(item, sqlConn);
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

        public static async Task<Address> Addresses_Insert(Address item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Addresses_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Address> Addresses_Insert(Address item, SqlConnection sqlConn)
        {
            try
            {
                Address resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Addresses_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryID", Value = item.FK_CountryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProvinceID", Value = item.FK_ProvinceID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AddressRegionID", Value = item.FK_AddressRegionID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@StreetAddress", Value = item.StreetAddress }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Locality", Value = item.Locality }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PostalCode", Value = item.PostalCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Landmark", Value = item.Landmark }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Latitude", Value = item.Latitude }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Longitude", Value = item.Longitude }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Address>(EntityData_Translator.Translate_Address);
                        Log.Information("Address found: AddressID={AddressID}, FK_CountryID={FK_CountryID}, FK_ProvinceID={FK_ProvinceID}, FK_AddressRegionID={FK_AddressRegionID}, StreetAddress={StreetAddress}, Locality={Locality}, PostalCode={PostalCode}, Landmark={Landmark}, Latitude={Latitude}, Longitude={Longitude}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressID, resultItem.FK_CountryID, resultItem.FK_ProvinceID, resultItem.FK_AddressRegionID, resultItem.StreetAddress, resultItem.Locality, resultItem.PostalCode, resultItem.Landmark, resultItem.Latitude, resultItem.Longitude, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Address failed to create.");
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

        public static async Task<List<Address>> Addresses_Select_All_Transaction(Address item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Addresses_Select_All(item, sqlConn);
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

        public static async Task<List<Address>> Addresses_Select_All(Address item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Addresses_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Address>> Addresses_Select_All(Address item, SqlConnection sqlConn)
        {
            try
            {
                List<Address> resultItem = new List<Address>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Addresses_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Address>(EntityData_Translator.Translate_Address));
                        Log.Information("Address records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Address records found.");
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

        public static async Task<Address> Addresses_Update_Transaction(Address item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Addresses_Update(item, sqlConn);
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

        public static async Task<Address> Addresses_Update(Address item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Addresses_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Address> Addresses_Update(Address item, SqlConnection sqlConn)
        {
            try
            {
                Address resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Addresses_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@AddressID", Value = item.AddressID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryID", Value = item.FK_CountryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProvinceID", Value = item.FK_ProvinceID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AddressRegionID", Value = item.FK_AddressRegionID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@StreetAddress", Value = item.StreetAddress }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Locality", Value = item.Locality }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PostalCode", Value = item.PostalCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Landmark", Value = item.Landmark }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Latitude", Value = item.Latitude }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Longitude", Value = item.Longitude }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Address>(EntityData_Translator.Translate_Address);
                        Log.Information("Address found: AddressID={AddressID}, FK_CountryID={FK_CountryID}, FK_ProvinceID={FK_ProvinceID}, FK_AddressRegionID={FK_AddressRegionID}, StreetAddress={StreetAddress}, Locality={Locality}, PostalCode={PostalCode}, Landmark={Landmark}, Latitude={Latitude}, Longitude={Longitude}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressID, resultItem.FK_CountryID, resultItem.FK_ProvinceID, resultItem.FK_AddressRegionID, resultItem.StreetAddress, resultItem.Locality, resultItem.PostalCode, resultItem.Landmark, resultItem.Latitude, resultItem.Longitude, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Address failed to update.");
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

        #region AddressRegions

        public static async Task<AddressRegion> AddressRegions_Select_Single_Transaction(AddressRegion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressRegions_Select_Single(item, sqlConn);
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

        public static async Task<AddressRegion> AddressRegions_Select_Single(AddressRegion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressRegions_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AddressRegion> AddressRegions_Select_Single(AddressRegion item, SqlConnection sqlConn)
        {
            try
            {
                AddressRegion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "AddressRegions_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@AddressRegionID", Value = item.AddressRegionID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AddressRegion>(EntityData_Translator.Translate_AddressRegion);
                        Log.Information("AddressRegion found: AddressRegionID={AddressRegionID}, RegionName={RegionName}, Description={Description}, FK_CountryID={FK_CountryID}, FK_ProvinceID={FK_ProvinceID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressRegionID, resultItem.RegionName, resultItem.Description, resultItem.FK_CountryID, resultItem.FK_ProvinceID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No AddressRegion found with the given AddressRegionID.");
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

        public static async Task<AddressRegion> AddressRegions_Insert_Transaction(AddressRegion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressRegions_Insert(item, sqlConn);
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

        public static async Task<AddressRegion> AddressRegions_Insert(AddressRegion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressRegions_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AddressRegion> AddressRegions_Insert(AddressRegion item, SqlConnection sqlConn)
        {
            try
            {
                AddressRegion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "AddressRegions_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RegionName", Value = item.RegionName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryID", Value = item.FK_CountryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProvinceID", Value = item.FK_ProvinceID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AddressRegion>(EntityData_Translator.Translate_AddressRegion);
                        Log.Information("AddressRegion found: AddressRegionID={AddressRegionID}, RegionName={RegionName}, Description={Description}, FK_CountryID={FK_CountryID}, FK_ProvinceID={FK_ProvinceID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressRegionID, resultItem.RegionName, resultItem.Description, resultItem.FK_CountryID, resultItem.FK_ProvinceID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("AddressRegion failed to create.");
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

        public static async Task<List<AddressRegion>> AddressRegions_Select_All_Transaction(AddressRegion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressRegions_Select_All(item, sqlConn);
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

        public static async Task<List<AddressRegion>> AddressRegions_Select_All(AddressRegion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressRegions_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<AddressRegion>> AddressRegions_Select_All(AddressRegion item, SqlConnection sqlConn)
        {
            try
            {
                List<AddressRegion> resultItem = new List<AddressRegion>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "AddressRegions_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<AddressRegion>(EntityData_Translator.Translate_AddressRegion));
                        Log.Information("AddressRegion records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No AddressRegion records found.");
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

        public static async Task<AddressRegion> AddressRegions_Update_Transaction(AddressRegion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressRegions_Update(item, sqlConn);
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

        public static async Task<AddressRegion> AddressRegions_Update(AddressRegion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressRegions_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AddressRegion> AddressRegions_Update(AddressRegion item, SqlConnection sqlConn)
        {
            try
            {
                AddressRegion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "AddressRegions_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@AddressRegionID", Value = item.AddressRegionID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RegionName", Value = item.RegionName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryID", Value = item.FK_CountryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProvinceID", Value = item.FK_ProvinceID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AddressRegion>(EntityData_Translator.Translate_AddressRegion);
                        Log.Information("AddressRegion found: AddressRegionID={AddressRegionID}, RegionName={RegionName}, Description={Description}, FK_CountryID={FK_CountryID}, FK_ProvinceID={FK_ProvinceID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressRegionID, resultItem.RegionName, resultItem.Description, resultItem.FK_CountryID, resultItem.FK_ProvinceID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("AddressRegion failed to update.");
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

        #region AddressTypes

        public static async Task<AddressType> AddressTypes_Select_Single_Transaction(AddressType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressTypes_Select_Single(item, sqlConn);
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

        public static async Task<AddressType> AddressTypes_Select_Single(AddressType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressTypes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AddressType> AddressTypes_Select_Single(AddressType item, SqlConnection sqlConn)
        {
            try
            {
                AddressType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "AddressTypes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@AddressTypeID", Value = item.AddressTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AddressType>(EntityData_Translator.Translate_AddressType);
                        Log.Information("AddressType found: AddressTypeID={AddressTypeID}, FK_EntityID={FK_EntityID}, Type={Type}, IsRequired={IsRequired}, CanEdit={CanEdit}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressTypeID, resultItem.FK_EntityID, resultItem.Type, resultItem.IsRequired, resultItem.CanEdit, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No AddressType found with the given AddressTypeID.");
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

        public static async Task<AddressType> AddressTypes_Insert_Transaction(AddressType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressTypes_Insert(item, sqlConn);
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

        public static async Task<AddressType> AddressTypes_Insert(AddressType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressTypes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AddressType> AddressTypes_Insert(AddressType item, SqlConnection sqlConn)
        {
            try
            {
                AddressType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "AddressTypes_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Type", Value = item.Type }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsRequired", Value = item.IsRequired }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@CanEdit", Value = item.CanEdit }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AddressType>(EntityData_Translator.Translate_AddressType);
                        Log.Information("AddressType found: AddressTypeID={AddressTypeID}, FK_EntityID={FK_EntityID}, Type={Type}, IsRequired={IsRequired}, CanEdit={CanEdit}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressTypeID, resultItem.FK_EntityID, resultItem.Type, resultItem.IsRequired, resultItem.CanEdit, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("AddressType failed to create.");
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

        public static async Task<List<AddressType>> AddressTypes_Select_All_Transaction(AddressType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressTypes_Select_All(item, sqlConn);
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

        public static async Task<List<AddressType>> AddressTypes_Select_All(AddressType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressTypes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<AddressType>> AddressTypes_Select_All(AddressType item, SqlConnection sqlConn)
        {
            try
            {
                List<AddressType> resultItem = new List<AddressType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "AddressTypes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<AddressType>(EntityData_Translator.Translate_AddressType));
                        Log.Information("AddressType records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No AddressType records found.");
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

        public static async Task<AddressType> AddressTypes_Update_Transaction(AddressType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressTypes_Update(item, sqlConn);
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

        public static async Task<AddressType> AddressTypes_Update(AddressType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await AddressTypes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<AddressType> AddressTypes_Update(AddressType item, SqlConnection sqlConn)
        {
            try
            {
                AddressType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "AddressTypes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@AddressTypeID", Value = item.AddressTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Type", Value = item.Type }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsRequired", Value = item.IsRequired }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@CanEdit", Value = item.CanEdit }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<AddressType>(EntityData_Translator.Translate_AddressType);
                        Log.Information("AddressType found: AddressTypeID={AddressTypeID}, FK_EntityID={FK_EntityID}, Type={Type}, IsRequired={IsRequired}, CanEdit={CanEdit}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.AddressTypeID, resultItem.FK_EntityID, resultItem.Type, resultItem.IsRequired, resultItem.CanEdit, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("AddressType failed to update.");
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

        #region Contacts

        public static async Task<Contact> Contacts_Select_Single_Transaction(Contact item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Contacts_Select_Single(item, sqlConn);
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

        public static async Task<Contact> Contacts_Select_Single(Contact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Contacts_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Contact> Contacts_Select_Single(Contact item, SqlConnection sqlConn)
        {
            try
            {
                Contact resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Contacts_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ContactID", Value = item.ContactID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Contact>(EntityData_Translator.Translate_Contact);
                        Log.Information("Contact found: ContactID={ContactID}, ContactValue={ContactValue}, FK_ContactTypeID={FK_ContactTypeID}, FK_DialingCodeID={FK_DialingCodeID}, IsVerified={IsVerified}, VerificationToken={VerificationToken}, VerifiedAt={VerifiedAt}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ContactID, resultItem.ContactValue, resultItem.FK_ContactTypeID, resultItem.FK_DialingCodeID, resultItem.IsVerified, resultItem.VerificationToken, resultItem.VerifiedAt, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Contact found with the given ContactID.");
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

        public static async Task<Contact> Contacts_Insert_Transaction(Contact item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Contacts_Insert(item, sqlConn);
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

        public static async Task<Contact> Contacts_Insert(Contact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Contacts_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Contact> Contacts_Insert(Contact item, SqlConnection sqlConn)
        {
            try
            {
                Contact resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Contacts_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ContactValue", Value = item.ContactValue }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ContactTypeID", Value = item.FK_ContactTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DialingCodeID", Value = item.FK_DialingCodeID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVerified", Value = item.IsVerified }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VerificationToken", Value = item.VerificationToken }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@VerifiedAt", Value = item.VerifiedAt }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Contact>(EntityData_Translator.Translate_Contact);
                        Log.Information("Contact found: ContactID={ContactID}, ContactValue={ContactValue}, FK_ContactTypeID={FK_ContactTypeID}, FK_DialingCodeID={FK_DialingCodeID}, IsVerified={IsVerified}, VerificationToken={VerificationToken}, VerifiedAt={VerifiedAt}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ContactID, resultItem.ContactValue, resultItem.FK_ContactTypeID, resultItem.FK_DialingCodeID, resultItem.IsVerified, resultItem.VerificationToken, resultItem.VerifiedAt, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Contact failed to create.");
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

        public static async Task<List<Contact>> Contacts_Select_All_Transaction(Contact item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Contacts_Select_All(item, sqlConn);
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

        public static async Task<List<Contact>> Contacts_Select_All(Contact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Contacts_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Contact>> Contacts_Select_All(Contact item, SqlConnection sqlConn)
        {
            try
            {
                List<Contact> resultItem = new List<Contact>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Contacts_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Contact>(EntityData_Translator.Translate_Contact));
                        Log.Information("Contact records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Contact records found.");
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

        public static async Task<Contact> Contacts_Update_Transaction(Contact item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Contacts_Update(item, sqlConn);
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

        public static async Task<Contact> Contacts_Update(Contact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Contacts_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Contact> Contacts_Update(Contact item, SqlConnection sqlConn)
        {
            try
            {
                Contact resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Contacts_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ContactID", Value = item.ContactID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ContactValue", Value = item.ContactValue }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ContactTypeID", Value = item.FK_ContactTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DialingCodeID", Value = item.FK_DialingCodeID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsVerified", Value = item.IsVerified }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@VerificationToken", Value = item.VerificationToken }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@VerifiedAt", Value = item.VerifiedAt }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Contact>(EntityData_Translator.Translate_Contact);
                        Log.Information("Contact found: ContactID={ContactID}, ContactValue={ContactValue}, FK_ContactTypeID={FK_ContactTypeID}, FK_DialingCodeID={FK_DialingCodeID}, IsVerified={IsVerified}, VerificationToken={VerificationToken}, VerifiedAt={VerifiedAt}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ContactID, resultItem.ContactValue, resultItem.FK_ContactTypeID, resultItem.FK_DialingCodeID, resultItem.IsVerified, resultItem.VerificationToken, resultItem.VerifiedAt, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Contact failed to update.");
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

        #region ContactTypes

        public static async Task<ContactType> ContactTypes_Select_Single_Transaction(ContactType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ContactTypes_Select_Single(item, sqlConn);
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

        public static async Task<ContactType> ContactTypes_Select_Single(ContactType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ContactTypes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ContactType> ContactTypes_Select_Single(ContactType item, SqlConnection sqlConn)
        {
            try
            {
                ContactType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "ContactTypes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ContactTypeID", Value = item.ContactTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ContactType>(EntityData_Translator.Translate_ContactType);
                        Log.Information("ContactType found: ContactTypeID={ContactTypeID}, Type={Type}, IsPhoneNumberType={IsPhoneNumberType}, IsEmailType={IsEmailType}", resultItem.ContactTypeID, resultItem.Type, resultItem.IsPhoneNumberType, resultItem.IsEmailType);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No ContactType found with the given ContactTypeID.");
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

        public static async Task<ContactType> ContactTypes_Insert_Transaction(ContactType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ContactTypes_Insert(item, sqlConn);
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

        public static async Task<ContactType> ContactTypes_Insert(ContactType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ContactTypes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ContactType> ContactTypes_Insert(ContactType item, SqlConnection sqlConn)
        {
            try
            {
                ContactType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "ContactTypes_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Type", Value = item.Type }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPhoneNumberType", Value = item.IsPhoneNumberType }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsEmailType", Value = item.IsEmailType }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ContactType>(EntityData_Translator.Translate_ContactType);
                        Log.Information("ContactType found: ContactTypeID={ContactTypeID}, Type={Type}, IsPhoneNumberType={IsPhoneNumberType}, IsEmailType={IsEmailType}", resultItem.ContactTypeID, resultItem.Type, resultItem.IsPhoneNumberType, resultItem.IsEmailType);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("ContactType failed to create.");
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

        public static async Task<List<ContactType>> ContactTypes_Select_All_Transaction(ContactType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ContactTypes_Select_All(item, sqlConn);
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

        public static async Task<List<ContactType>> ContactTypes_Select_All(ContactType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ContactTypes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<ContactType>> ContactTypes_Select_All(ContactType item, SqlConnection sqlConn)
        {
            try
            {
                List<ContactType> resultItem = new List<ContactType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "ContactTypes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<ContactType>(EntityData_Translator.Translate_ContactType));
                        Log.Information("ContactType records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No ContactType records found.");
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

        public static async Task<ContactType> ContactTypes_Update_Transaction(ContactType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ContactTypes_Update(item, sqlConn);
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

        public static async Task<ContactType> ContactTypes_Update(ContactType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ContactTypes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ContactType> ContactTypes_Update(ContactType item, SqlConnection sqlConn)
        {
            try
            {
                ContactType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "ContactTypes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ContactTypeID", Value = item.ContactTypeID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Type", Value = item.Type }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPhoneNumberType", Value = item.IsPhoneNumberType }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsEmailType", Value = item.IsEmailType }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ContactType>(EntityData_Translator.Translate_ContactType);
                        Log.Information("ContactType found: ContactTypeID={ContactTypeID}, Type={Type}, IsPhoneNumberType={IsPhoneNumberType}, IsEmailType={IsEmailType}", resultItem.ContactTypeID, resultItem.Type, resultItem.IsPhoneNumberType, resultItem.IsEmailType);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("ContactType failed to update.");
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

        #region Continents

        public static async Task<Continent> Continents_Select_Single_Transaction(Continent item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Continents_Select_Single(item, sqlConn);
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

        public static async Task<Continent> Continents_Select_Single(Continent item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Continents_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Continent> Continents_Select_Single(Continent item, SqlConnection sqlConn)
        {
            try
            {
                Continent resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Continents_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ContinentID", Value = item.ContinentID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Continent>(EntityData_Translator.Translate_Continent);
                        Log.Information("Continent found: ContinentID={ContinentID}, Name={Name}, ShortCode={ShortCode}", resultItem.ContinentID, resultItem.Name, resultItem.ShortCode);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Continent found with the given ContinentID.");
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

        public static async Task<Continent> Continents_Insert_Transaction(Continent item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Continents_Insert(item, sqlConn);
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

        public static async Task<Continent> Continents_Insert(Continent item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Continents_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Continent> Continents_Insert(Continent item, SqlConnection sqlConn)
        {
            try
            {
                Continent resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Continents_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Continent>(EntityData_Translator.Translate_Continent);
                        Log.Information("Continent found: ContinentID={ContinentID}, Name={Name}, ShortCode={ShortCode}", resultItem.ContinentID, resultItem.Name, resultItem.ShortCode);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Continent failed to create.");
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

        public static async Task<List<Continent>> Continents_Select_All_Transaction(Continent item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Continents_Select_All(item, sqlConn);
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

        public static async Task<List<Continent>> Continents_Select_All(Continent item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Continents_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Continent>> Continents_Select_All(Continent item, SqlConnection sqlConn)
        {
            try
            {
                List<Continent> resultItem = new List<Continent>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Continents_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Continent>(EntityData_Translator.Translate_Continent));
                        Log.Information("Continent records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Continent records found.");
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

        public static async Task<Continent> Continents_Update_Transaction(Continent item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Continents_Update(item, sqlConn);
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

        public static async Task<Continent> Continents_Update(Continent item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Continents_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Continent> Continents_Update(Continent item, SqlConnection sqlConn)
        {
            try
            {
                Continent resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Continents_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ContinentID", Value = item.ContinentID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Continent>(EntityData_Translator.Translate_Continent);
                        Log.Information("Continent found: ContinentID={ContinentID}, Name={Name}, ShortCode={ShortCode}", resultItem.ContinentID, resultItem.Name, resultItem.ShortCode);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Continent failed to update.");
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

        #region Countries

        public static async Task<Country> Countries_Select_Single_Transaction(Country item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Countries_Select_Single(item, sqlConn);
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

        public static async Task<Country> Countries_Select_Single(Country item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Countries_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Country> Countries_Select_Single(Country item, SqlConnection sqlConn)
        {
            try
            {
                Country resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Countries_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CountryID", Value = item.CountryID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Country>(EntityData_Translator.Translate_Country);
                        Log.Information("Country found: CountryID={CountryID}, CountryName={CountryName}, NativeName={NativeName}, OfficialName={OfficialName}, ISO2Code={ISO2Code}, ISO3Code={ISO3Code}, PrimaryLanguageCode={PrimaryLanguageCode}, NumericCode={NumericCode}, FK_DialingCodeID={FK_DialingCodeID}, FK_CurrencyID={FK_CurrencyID}, FK_CountryRegionID={FK_CountryRegionID}, FK_CountrySubregionID={FK_CountrySubregionID}, FK_TimeZoneID={FK_TimeZoneID}", resultItem.CountryID, resultItem.CountryName, resultItem.NativeName, resultItem.OfficialName, resultItem.ISO2Code, resultItem.ISO3Code, resultItem.PrimaryLanguageCode, resultItem.NumericCode, resultItem.FK_DialingCodeID, resultItem.FK_CurrencyID, resultItem.FK_CountryRegionID, resultItem.FK_CountrySubregionID, resultItem.FK_TimeZoneID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Country found with the given CountryID.");
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

        public static async Task<Country> Countries_Insert_Transaction(Country item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Countries_Insert(item, sqlConn);
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

        public static async Task<Country> Countries_Insert(Country item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Countries_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Country> Countries_Insert(Country item, SqlConnection sqlConn)
        {
            try
            {
                Country resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Countries_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CountryName", Value = item.CountryName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@NativeName", Value = item.NativeName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@OfficialName", Value = item.OfficialName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO2Code", Value = item.ISO2Code }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO3Code", Value = item.ISO3Code }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PrimaryLanguageCode", Value = item.PrimaryLanguageCode }
                        , new SqlParameter() { DbType = DbType.Int16, Direction = ParameterDirection.Input, ParameterName = "@NumericCode", Value = item.NumericCode }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DialingCodeID", Value = item.FK_DialingCodeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryRegionID", Value = item.FK_CountryRegionID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountrySubregionID", Value = item.FK_CountrySubregionID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TimeZoneID", Value = item.FK_TimeZoneID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Country>(EntityData_Translator.Translate_Country);
                        Log.Information("Country found: CountryID={CountryID}, CountryName={CountryName}, NativeName={NativeName}, OfficialName={OfficialName}, ISO2Code={ISO2Code}, ISO3Code={ISO3Code}, PrimaryLanguageCode={PrimaryLanguageCode}, NumericCode={NumericCode}, FK_DialingCodeID={FK_DialingCodeID}, FK_CurrencyID={FK_CurrencyID}, FK_CountryRegionID={FK_CountryRegionID}, FK_CountrySubregionID={FK_CountrySubregionID}, FK_TimeZoneID={FK_TimeZoneID}", resultItem.CountryID, resultItem.CountryName, resultItem.NativeName, resultItem.OfficialName, resultItem.ISO2Code, resultItem.ISO3Code, resultItem.PrimaryLanguageCode, resultItem.NumericCode, resultItem.FK_DialingCodeID, resultItem.FK_CurrencyID, resultItem.FK_CountryRegionID, resultItem.FK_CountrySubregionID, resultItem.FK_TimeZoneID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Country failed to create.");
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

        public static async Task<List<Country>> Countries_Select_All_Transaction(Country item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Countries_Select_All(item, sqlConn);
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

        public static async Task<List<Country>> Countries_Select_All(Country item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Countries_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Country>> Countries_Select_All(Country item, SqlConnection sqlConn)
        {
            try
            {
                List<Country> resultItem = new List<Country>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Countries_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Country>(EntityData_Translator.Translate_Country));
                        Log.Information("Country records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Country records found.");
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

        public static async Task<Country> Countries_Update_Transaction(Country item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Countries_Update(item, sqlConn);
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

        public static async Task<Country> Countries_Update(Country item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Countries_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Country> Countries_Update(Country item, SqlConnection sqlConn)
        {
            try
            {
                Country resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Countries_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CountryID", Value = item.CountryID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CountryName", Value = item.CountryName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@NativeName", Value = item.NativeName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@OfficialName", Value = item.OfficialName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO2Code", Value = item.ISO2Code }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO3Code", Value = item.ISO3Code }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PrimaryLanguageCode", Value = item.PrimaryLanguageCode }
                        , new SqlParameter() { DbType = DbType.Int16, Direction = ParameterDirection.Input, ParameterName = "@NumericCode", Value = item.NumericCode }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DialingCodeID", Value = item.FK_DialingCodeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryRegionID", Value = item.FK_CountryRegionID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountrySubregionID", Value = item.FK_CountrySubregionID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_TimeZoneID", Value = item.FK_TimeZoneID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Country>(EntityData_Translator.Translate_Country);
                        Log.Information("Country found: CountryID={CountryID}, CountryName={CountryName}, NativeName={NativeName}, OfficialName={OfficialName}, ISO2Code={ISO2Code}, ISO3Code={ISO3Code}, PrimaryLanguageCode={PrimaryLanguageCode}, NumericCode={NumericCode}, FK_DialingCodeID={FK_DialingCodeID}, FK_CurrencyID={FK_CurrencyID}, FK_CountryRegionID={FK_CountryRegionID}, FK_CountrySubregionID={FK_CountrySubregionID}, FK_TimeZoneID={FK_TimeZoneID}", resultItem.CountryID, resultItem.CountryName, resultItem.NativeName, resultItem.OfficialName, resultItem.ISO2Code, resultItem.ISO3Code, resultItem.PrimaryLanguageCode, resultItem.NumericCode, resultItem.FK_DialingCodeID, resultItem.FK_CurrencyID, resultItem.FK_CountryRegionID, resultItem.FK_CountrySubregionID, resultItem.FK_TimeZoneID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Country failed to update.");
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

        #region CountryProvinces

        public static async Task<CountryProvince> CountryProvinces_Select_Single_Transaction(CountryProvince item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryProvinces_Select_Single(item, sqlConn);
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

        public static async Task<CountryProvince> CountryProvinces_Select_Single(CountryProvince item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryProvinces_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountryProvince> CountryProvinces_Select_Single(CountryProvince item, SqlConnection sqlConn)
        {
            try
            {
                CountryProvince resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountryProvinces_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CountryProvinceID", Value = item.CountryProvinceID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountryProvince>(EntityData_Translator.Translate_CountryProvince);
                        Log.Information("CountryProvince found: CountryProvinceID={CountryProvinceID}, ProvinceName={ProvinceName}, ISO2Code={ISO2Code}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_CountryID={FK_CountryID}", resultItem.CountryProvinceID, resultItem.ProvinceName, resultItem.ISO2Code, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_CountryID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CountryProvince found with the given CountryProvinceID.");
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

        public static async Task<CountryProvince> CountryProvinces_Insert_Transaction(CountryProvince item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryProvinces_Insert(item, sqlConn);
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

        public static async Task<CountryProvince> CountryProvinces_Insert(CountryProvince item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryProvinces_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountryProvince> CountryProvinces_Insert(CountryProvince item, SqlConnection sqlConn)
        {
            try
            {
                CountryProvince resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountryProvinces_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ProvinceName", Value = item.ProvinceName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO2Code", Value = item.ISO2Code }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryID", Value = item.FK_CountryID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountryProvince>(EntityData_Translator.Translate_CountryProvince);
                        Log.Information("CountryProvince found: CountryProvinceID={CountryProvinceID}, ProvinceName={ProvinceName}, ISO2Code={ISO2Code}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_CountryID={FK_CountryID}", resultItem.CountryProvinceID, resultItem.ProvinceName, resultItem.ISO2Code, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_CountryID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CountryProvince failed to create.");
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

        public static async Task<List<CountryProvince>> CountryProvinces_Select_All_Transaction(CountryProvince item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryProvinces_Select_All(item, sqlConn);
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

        public static async Task<List<CountryProvince>> CountryProvinces_Select_All(CountryProvince item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryProvinces_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CountryProvince>> CountryProvinces_Select_All(CountryProvince item, SqlConnection sqlConn)
        {
            try
            {
                List<CountryProvince> resultItem = new List<CountryProvince>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountryProvinces_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CountryProvince>(EntityData_Translator.Translate_CountryProvince));
                        Log.Information("CountryProvince records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CountryProvince records found.");
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

        public static async Task<CountryProvince> CountryProvinces_Update_Transaction(CountryProvince item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryProvinces_Update(item, sqlConn);
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

        public static async Task<CountryProvince> CountryProvinces_Update(CountryProvince item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryProvinces_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountryProvince> CountryProvinces_Update(CountryProvince item, SqlConnection sqlConn)
        {
            try
            {
                CountryProvince resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountryProvinces_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CountryProvinceID", Value = item.CountryProvinceID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ProvinceName", Value = item.ProvinceName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO2Code", Value = item.ISO2Code }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryID", Value = item.FK_CountryID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountryProvince>(EntityData_Translator.Translate_CountryProvince);
                        Log.Information("CountryProvince found: CountryProvinceID={CountryProvinceID}, ProvinceName={ProvinceName}, ISO2Code={ISO2Code}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_CountryID={FK_CountryID}", resultItem.CountryProvinceID, resultItem.ProvinceName, resultItem.ISO2Code, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_CountryID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CountryProvince failed to update.");
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

        #region CountrySubregions

        public static async Task<CountrySubregion> CountrySubregions_Select_Single_Transaction(CountrySubregion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountrySubregions_Select_Single(item, sqlConn);
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

        public static async Task<CountrySubregion> CountrySubregions_Select_Single(CountrySubregion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountrySubregions_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountrySubregion> CountrySubregions_Select_Single(CountrySubregion item, SqlConnection sqlConn)
        {
            try
            {
                CountrySubregion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountrySubregions_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CountrySubregionID", Value = item.CountrySubregionID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountrySubregion>(EntityData_Translator.Translate_CountrySubregion);
                        Log.Information("CountrySubregion found: CountrySubregionID={CountrySubregionID}, Subregion={Subregion}, FK_CountryRegionID={FK_CountryRegionID}", resultItem.CountrySubregionID, resultItem.Subregion, resultItem.FK_CountryRegionID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CountrySubregion found with the given CountrySubregionID.");
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

        public static async Task<CountrySubregion> CountrySubregions_Insert_Transaction(CountrySubregion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountrySubregions_Insert(item, sqlConn);
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

        public static async Task<CountrySubregion> CountrySubregions_Insert(CountrySubregion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountrySubregions_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountrySubregion> CountrySubregions_Insert(CountrySubregion item, SqlConnection sqlConn)
        {
            try
            {
                CountrySubregion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountrySubregions_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Subregion", Value = item.Subregion }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryRegionID", Value = item.FK_CountryRegionID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountrySubregion>(EntityData_Translator.Translate_CountrySubregion);
                        Log.Information("CountrySubregion found: CountrySubregionID={CountrySubregionID}, Subregion={Subregion}, FK_CountryRegionID={FK_CountryRegionID}", resultItem.CountrySubregionID, resultItem.Subregion, resultItem.FK_CountryRegionID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CountrySubregion failed to create.");
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

        public static async Task<List<CountrySubregion>> CountrySubregions_Select_All_Transaction(CountrySubregion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountrySubregions_Select_All(item, sqlConn);
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

        public static async Task<List<CountrySubregion>> CountrySubregions_Select_All(CountrySubregion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountrySubregions_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CountrySubregion>> CountrySubregions_Select_All(CountrySubregion item, SqlConnection sqlConn)
        {
            try
            {
                List<CountrySubregion> resultItem = new List<CountrySubregion>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountrySubregions_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CountrySubregion>(EntityData_Translator.Translate_CountrySubregion));
                        Log.Information("CountrySubregion records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CountrySubregion records found.");
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

        public static async Task<CountrySubregion> CountrySubregions_Update_Transaction(CountrySubregion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountrySubregions_Update(item, sqlConn);
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

        public static async Task<CountrySubregion> CountrySubregions_Update(CountrySubregion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountrySubregions_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountrySubregion> CountrySubregions_Update(CountrySubregion item, SqlConnection sqlConn)
        {
            try
            {
                CountrySubregion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountrySubregions_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CountrySubregionID", Value = item.CountrySubregionID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Subregion", Value = item.Subregion }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CountryRegionID", Value = item.FK_CountryRegionID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountrySubregion>(EntityData_Translator.Translate_CountrySubregion);
                        Log.Information("CountrySubregion found: CountrySubregionID={CountrySubregionID}, Subregion={Subregion}, FK_CountryRegionID={FK_CountryRegionID}", resultItem.CountrySubregionID, resultItem.Subregion, resultItem.FK_CountryRegionID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CountrySubregion failed to update.");
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

        #region CountryRegions

        public static async Task<CountryRegion> CountryRegions_Select_Single_Transaction(CountryRegion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryRegions_Select_Single(item, sqlConn);
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

        public static async Task<CountryRegion> CountryRegions_Select_Single(CountryRegion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryRegions_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountryRegion> CountryRegions_Select_Single(CountryRegion item, SqlConnection sqlConn)
        {
            try
            {
                CountryRegion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountryRegions_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CountryRegionID", Value = item.CountryRegionID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountryRegion>(EntityData_Translator.Translate_CountryRegion);
                        Log.Information("CountryRegion found: CountryRegionID={CountryRegionID}, Region={Region}, FK_ContinentID={FK_ContinentID}", resultItem.CountryRegionID, resultItem.Region, resultItem.FK_ContinentID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CountryRegion found with the given CountryRegionID.");
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

        public static async Task<CountryRegion> CountryRegions_Insert_Transaction(CountryRegion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryRegions_Insert(item, sqlConn);
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

        public static async Task<CountryRegion> CountryRegions_Insert(CountryRegion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryRegions_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountryRegion> CountryRegions_Insert(CountryRegion item, SqlConnection sqlConn)
        {
            try
            {
                CountryRegion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountryRegions_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Region", Value = item.Region }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ContinentID", Value = item.FK_ContinentID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountryRegion>(EntityData_Translator.Translate_CountryRegion);
                        Log.Information("CountryRegion found: CountryRegionID={CountryRegionID}, Region={Region}, FK_ContinentID={FK_ContinentID}", resultItem.CountryRegionID, resultItem.Region, resultItem.FK_ContinentID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CountryRegion failed to create.");
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

        public static async Task<List<CountryRegion>> CountryRegions_Select_All_Transaction(CountryRegion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryRegions_Select_All(item, sqlConn);
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

        public static async Task<List<CountryRegion>> CountryRegions_Select_All(CountryRegion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryRegions_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CountryRegion>> CountryRegions_Select_All(CountryRegion item, SqlConnection sqlConn)
        {
            try
            {
                List<CountryRegion> resultItem = new List<CountryRegion>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountryRegions_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CountryRegion>(EntityData_Translator.Translate_CountryRegion));
                        Log.Information("CountryRegion records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CountryRegion records found.");
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

        public static async Task<CountryRegion> CountryRegions_Update_Transaction(CountryRegion item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryRegions_Update(item, sqlConn);
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

        public static async Task<CountryRegion> CountryRegions_Update(CountryRegion item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CountryRegions_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CountryRegion> CountryRegions_Update(CountryRegion item, SqlConnection sqlConn)
        {
            try
            {
                CountryRegion resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CountryRegions_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CountryRegionID", Value = item.CountryRegionID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Region", Value = item.Region }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ContinentID", Value = item.FK_ContinentID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CountryRegion>(EntityData_Translator.Translate_CountryRegion);
                        Log.Information("CountryRegion found: CountryRegionID={CountryRegionID}, Region={Region}, FK_ContinentID={FK_ContinentID}", resultItem.CountryRegionID, resultItem.Region, resultItem.FK_ContinentID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CountryRegion failed to update.");
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

        #region Currencies

        public static async Task<Currency> Currencies_Select_Single_Transaction(Currency item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Currencies_Select_Single(item, sqlConn);
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

        public static async Task<Currency> Currencies_Select_Single(Currency item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Currencies_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Currency> Currencies_Select_Single(Currency item, SqlConnection sqlConn)
        {
            try
            {
                Currency resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Currencies_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CurrencyID", Value = item.CurrencyID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Currency>(EntityData_Translator.Translate_Currency);
                        Log.Information("Currency found: CurrencyID={CurrencyID}, Currency={Currency}, Name={Name}, ISO2Code={ISO2Code}, Symbol={Symbol}", resultItem.CurrencyID, resultItem.Currency, resultItem.Name, resultItem.ISO2Code, resultItem.Symbol);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Currency found with the given CurrencyID.");
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

        public static async Task<Currency> Currencies_Insert_Transaction(Currency item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Currencies_Insert(item, sqlConn);
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

        public static async Task<Currency> Currencies_Insert(Currency item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Currencies_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Currency> Currencies_Insert(Currency item, SqlConnection sqlConn)
        {
            try
            {
                Currency resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Currencies_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Currency", Value = item.Currency }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO2Code", Value = item.ISO2Code }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Symbol", Value = item.Symbol }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Currency>(EntityData_Translator.Translate_Currency);
                        Log.Information("Currency found: CurrencyID={CurrencyID}, Currency={Currency}, Name={Name}, ISO2Code={ISO2Code}, Symbol={Symbol}", resultItem.CurrencyID, resultItem.Currency, resultItem.Name, resultItem.ISO2Code, resultItem.Symbol);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Currency failed to create.");
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

        public static async Task<List<Currency>> Currencies_Select_All_Transaction(Currency item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Currencies_Select_All(item, sqlConn);
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

        public static async Task<List<Currency>> Currencies_Select_All(Currency item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Currencies_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Currency>> Currencies_Select_All(Currency item, SqlConnection sqlConn)
        {
            try
            {
                List<Currency> resultItem = new List<Currency>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Currencies_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Currency>(EntityData_Translator.Translate_Currency));
                        Log.Information("Currency records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Currency records found.");
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

        public static async Task<Currency> Currencies_Update_Transaction(Currency item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Currencies_Update(item, sqlConn);
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

        public static async Task<Currency> Currencies_Update(Currency item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Currencies_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Currency> Currencies_Update(Currency item, SqlConnection sqlConn)
        {
            try
            {
                Currency resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Currencies_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CurrencyID", Value = item.CurrencyID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Currency", Value = item.Currency }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO2Code", Value = item.ISO2Code }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Symbol", Value = item.Symbol }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Currency>(EntityData_Translator.Translate_Currency);
                        Log.Information("Currency found: CurrencyID={CurrencyID}, Currency={Currency}, Name={Name}, ISO2Code={ISO2Code}, Symbol={Symbol}", resultItem.CurrencyID, resultItem.Currency, resultItem.Name, resultItem.ISO2Code, resultItem.Symbol);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Currency failed to update.");
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

        #region DialingCodes

        public static async Task<DialingCode> DialingCodes_Select_Single_Transaction(DialingCode item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DialingCodes_Select_Single(item, sqlConn);
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

        public static async Task<DialingCode> DialingCodes_Select_Single(DialingCode item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DialingCodes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DialingCode> DialingCodes_Select_Single(DialingCode item, SqlConnection sqlConn)
        {
            try
            {
                DialingCode resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "DialingCodes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DialingCodeID", Value = item.DialingCodeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DialingCode>(EntityData_Translator.Translate_DialingCode);
                        Log.Information("DialingCode found: DialingCodeID={DialingCodeID}, DialingCode={DialingCode}, ISO2Code={ISO2Code}", resultItem.DialingCodeID, resultItem.DialingCode, resultItem.ISO2Code);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DialingCode found with the given DialingCodeID.");
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

        public static async Task<DialingCode> DialingCodes_Insert_Transaction(DialingCode item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DialingCodes_Insert(item, sqlConn);
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

        public static async Task<DialingCode> DialingCodes_Insert(DialingCode item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DialingCodes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DialingCode> DialingCodes_Insert(DialingCode item, SqlConnection sqlConn)
        {
            try
            {
                DialingCode resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "DialingCodes_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@DialingCode", Value = item.DialingCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO2Code", Value = item.ISO2Code }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DialingCode>(EntityData_Translator.Translate_DialingCode);
                        Log.Information("DialingCode found: DialingCodeID={DialingCodeID}, DialingCode={DialingCode}, ISO2Code={ISO2Code}", resultItem.DialingCodeID, resultItem.DialingCode, resultItem.ISO2Code);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DialingCode failed to create.");
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

        public static async Task<List<DialingCode>> DialingCodes_Select_All_Transaction(DialingCode item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DialingCodes_Select_All(item, sqlConn);
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

        public static async Task<List<DialingCode>> DialingCodes_Select_All(DialingCode item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DialingCodes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DialingCode>> DialingCodes_Select_All(DialingCode item, SqlConnection sqlConn)
        {
            try
            {
                List<DialingCode> resultItem = new List<DialingCode>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "DialingCodes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DialingCode>(EntityData_Translator.Translate_DialingCode));
                        Log.Information("DialingCode records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DialingCode records found.");
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

        public static async Task<DialingCode> DialingCodes_Update_Transaction(DialingCode item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DialingCodes_Update(item, sqlConn);
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

        public static async Task<DialingCode> DialingCodes_Update(DialingCode item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DialingCodes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DialingCode> DialingCodes_Update(DialingCode item, SqlConnection sqlConn)
        {
            try
            {
                DialingCode resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "DialingCodes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DialingCodeID", Value = item.DialingCodeID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@DialingCode", Value = item.DialingCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ISO2Code", Value = item.ISO2Code }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DialingCode>(EntityData_Translator.Translate_DialingCode);
                        Log.Information("DialingCode found: DialingCodeID={DialingCodeID}, DialingCode={DialingCode}, ISO2Code={ISO2Code}", resultItem.DialingCodeID, resultItem.DialingCode, resultItem.ISO2Code);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DialingCode failed to update.");
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

        #region Entities

        public static async Task<Entity> Entities_Select_Single_Transaction(Entity item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entities_Select_Single(item, sqlConn);
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

        public static async Task<Entity> Entities_Select_Single(Entity item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entities_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Entity> Entities_Select_Single(Entity item, SqlConnection sqlConn)
        {
            try
            {
                Entity resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Entities_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityID", Value = item.EntityID }                ))
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

        public static async Task<Entity> Entities_Insert_Transaction(Entity item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entities_Insert(item, sqlConn);
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

        public static async Task<Entity> Entities_Insert(Entity item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entities_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Entity> Entities_Insert(Entity item, SqlConnection sqlConn)
        {
            try
            {
                Entity resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Entities_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Entity>(EntityData_Translator.Translate_Entity);
                        Log.Information("Entity found: EntityID={EntityID}, Name={Name}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityID, resultItem.Name, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Entity failed to create.");
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

        public static async Task<List<Entity>> Entities_Select_All_Transaction(Entity item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entities_Select_All(item, sqlConn);
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

        public static async Task<List<Entity>> Entities_Select_All(Entity item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entities_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Entity>> Entities_Select_All(Entity item, SqlConnection sqlConn)
        {
            try
            {
                List<Entity> resultItem = new List<Entity>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Entities_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Entity>(EntityData_Translator.Translate_Entity));
                        Log.Information("Entity records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Entity records found.");
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

        public static async Task<Entity> Entities_Update_Transaction(Entity item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entities_Update(item, sqlConn);
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

        public static async Task<Entity> Entities_Update(Entity item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Entities_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Entity> Entities_Update(Entity item, SqlConnection sqlConn)
        {
            try
            {
                Entity resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Entities_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityID", Value = item.EntityID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Entity>(EntityData_Translator.Translate_Entity);
                        Log.Information("Entity found: EntityID={EntityID}, Name={Name}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityID, resultItem.Name, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Entity failed to update.");
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

        #region EntityAddresses

        public static async Task<EntityAddress> EntityAddresses_Select_Single_Transaction(EntityAddress item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Select_Single(item, sqlConn);
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

        public static async Task<EntityAddress> EntityAddresses_Select_Single(EntityAddress item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntityAddress> EntityAddresses_Select_Single(EntityAddress item, SqlConnection sqlConn)
        {
            try
            {
                EntityAddress resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntityAddresses_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityAddressID", Value = item.EntityAddressID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntityAddress>(EntityData_Translator.Translate_EntityAddress);
                        Log.Information("EntityAddress found: EntityAddressID={EntityAddressID}, FK_EntityID={FK_EntityID}, EntityRecordID={EntityRecordID}, FK_AddressID={FK_AddressID}, FK_AddressTypeID={FK_AddressTypeID}, IsPrimary={IsPrimary}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityAddressID, resultItem.FK_EntityID, resultItem.EntityRecordID, resultItem.FK_AddressID, resultItem.FK_AddressTypeID, resultItem.IsPrimary, resultItem.ValidFrom, resultItem.ValidTo, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No EntityAddress found with the given EntityAddressID.");
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

        public static async Task<EntityAddress> EntityAddresses_Insert_Transaction(EntityAddress item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Insert(item, sqlConn);
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

        public static async Task<EntityAddress> EntityAddresses_Insert(EntityAddress item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntityAddress> EntityAddresses_Insert(EntityAddress item, SqlConnection sqlConn)
        {
            try
            {
                EntityAddress resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntityAddresses_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityRecordID", Value = item.EntityRecordID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AddressID", Value = item.FK_AddressID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AddressTypeID", Value = item.FK_AddressTypeID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPrimary", Value = item.IsPrimary }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntityAddress>(EntityData_Translator.Translate_EntityAddress);
                        Log.Information("EntityAddress found: EntityAddressID={EntityAddressID}, FK_EntityID={FK_EntityID}, EntityRecordID={EntityRecordID}, FK_AddressID={FK_AddressID}, FK_AddressTypeID={FK_AddressTypeID}, IsPrimary={IsPrimary}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityAddressID, resultItem.FK_EntityID, resultItem.EntityRecordID, resultItem.FK_AddressID, resultItem.FK_AddressTypeID, resultItem.IsPrimary, resultItem.ValidFrom, resultItem.ValidTo, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("EntityAddress failed to create.");
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

        public static async Task<List<EntityAddress>> EntityAddresses_Select_All_Transaction(EntityAddress item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Select_All(item, sqlConn);
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

        public static async Task<List<EntityAddress>> EntityAddresses_Select_All(EntityAddress item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<EntityAddress>> EntityAddresses_Select_All(EntityAddress item, SqlConnection sqlConn)
        {
            try
            {
                List<EntityAddress> resultItem = new List<EntityAddress>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntityAddresses_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<EntityAddress>(EntityData_Translator.Translate_EntityAddress));
                        Log.Information("EntityAddress records found: {Count}", resultItem.Count);
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

        public static async Task<EntityAddress> EntityAddresses_Update_Transaction(EntityAddress item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Update(item, sqlConn);
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

        public static async Task<EntityAddress> EntityAddresses_Update(EntityAddress item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityAddresses_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntityAddress> EntityAddresses_Update(EntityAddress item, SqlConnection sqlConn)
        {
            try
            {
                EntityAddress resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntityAddresses_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityAddressID", Value = item.EntityAddressID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityRecordID", Value = item.EntityRecordID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AddressID", Value = item.FK_AddressID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AddressTypeID", Value = item.FK_AddressTypeID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPrimary", Value = item.IsPrimary }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntityAddress>(EntityData_Translator.Translate_EntityAddress);
                        Log.Information("EntityAddress found: EntityAddressID={EntityAddressID}, FK_EntityID={FK_EntityID}, EntityRecordID={EntityRecordID}, FK_AddressID={FK_AddressID}, FK_AddressTypeID={FK_AddressTypeID}, IsPrimary={IsPrimary}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityAddressID, resultItem.FK_EntityID, resultItem.EntityRecordID, resultItem.FK_AddressID, resultItem.FK_AddressTypeID, resultItem.IsPrimary, resultItem.ValidFrom, resultItem.ValidTo, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("EntityAddress failed to update.");
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

        #region EntityContacts

        public static async Task<EntityContact> EntityContacts_Select_Single_Transaction(EntityContact item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Select_Single(item, sqlConn);
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

        public static async Task<EntityContact> EntityContacts_Select_Single(EntityContact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntityContact> EntityContacts_Select_Single(EntityContact item, SqlConnection sqlConn)
        {
            try
            {
                EntityContact resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntityContacts_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityContactID", Value = item.EntityContactID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntityContact>(EntityData_Translator.Translate_EntityContact);
                        Log.Information("EntityContact found: EntityContactID={EntityContactID}, FK_EntityID={FK_EntityID}, EntityRecordID={EntityRecordID}, FK_ContactID={FK_ContactID}, IsPrimary={IsPrimary}, IsMarketing={IsMarketing}, IsEmergency={IsEmergency}, PreferredContactTime={PreferredContactTime}, PreferredLanguageCode={PreferredLanguageCode}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityContactID, resultItem.FK_EntityID, resultItem.EntityRecordID, resultItem.FK_ContactID, resultItem.IsPrimary, resultItem.IsMarketing, resultItem.IsEmergency, resultItem.PreferredContactTime, resultItem.PreferredLanguageCode, resultItem.ValidFrom, resultItem.ValidTo, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No EntityContact found with the given EntityContactID.");
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

        public static async Task<EntityContact> EntityContacts_Insert_Transaction(EntityContact item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Insert(item, sqlConn);
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

        public static async Task<EntityContact> EntityContacts_Insert(EntityContact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntityContact> EntityContacts_Insert(EntityContact item, SqlConnection sqlConn)
        {
            try
            {
                EntityContact resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntityContacts_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityRecordID", Value = item.EntityRecordID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ContactID", Value = item.FK_ContactID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPrimary", Value = item.IsPrimary }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsMarketing", Value = item.IsMarketing }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsEmergency", Value = item.IsEmergency }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PreferredContactTime", Value = item.PreferredContactTime }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PreferredLanguageCode", Value = item.PreferredLanguageCode }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntityContact>(EntityData_Translator.Translate_EntityContact);
                        Log.Information("EntityContact found: EntityContactID={EntityContactID}, FK_EntityID={FK_EntityID}, EntityRecordID={EntityRecordID}, FK_ContactID={FK_ContactID}, IsPrimary={IsPrimary}, IsMarketing={IsMarketing}, IsEmergency={IsEmergency}, PreferredContactTime={PreferredContactTime}, PreferredLanguageCode={PreferredLanguageCode}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityContactID, resultItem.FK_EntityID, resultItem.EntityRecordID, resultItem.FK_ContactID, resultItem.IsPrimary, resultItem.IsMarketing, resultItem.IsEmergency, resultItem.PreferredContactTime, resultItem.PreferredLanguageCode, resultItem.ValidFrom, resultItem.ValidTo, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("EntityContact failed to create.");
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

        public static async Task<List<EntityContact>> EntityContacts_Select_All_Transaction(EntityContact item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Select_All(item, sqlConn);
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

        public static async Task<List<EntityContact>> EntityContacts_Select_All(EntityContact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<EntityContact>> EntityContacts_Select_All(EntityContact item, SqlConnection sqlConn)
        {
            try
            {
                List<EntityContact> resultItem = new List<EntityContact>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntityContacts_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<EntityContact>(EntityData_Translator.Translate_EntityContact));
                        Log.Information("EntityContact records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No EntityContact records found.");
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

        public static async Task<EntityContact> EntityContacts_Update_Transaction(EntityContact item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Update(item, sqlConn);
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

        public static async Task<EntityContact> EntityContacts_Update(EntityContact item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntityContacts_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntityContact> EntityContacts_Update(EntityContact item, SqlConnection sqlConn)
        {
            try
            {
                EntityContact resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntityContacts_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityContactID", Value = item.EntityContactID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntityRecordID", Value = item.EntityRecordID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ContactID", Value = item.FK_ContactID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPrimary", Value = item.IsPrimary }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsMarketing", Value = item.IsMarketing }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsEmergency", Value = item.IsEmergency }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PreferredContactTime", Value = item.PreferredContactTime }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PreferredLanguageCode", Value = item.PreferredLanguageCode }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntityContact>(EntityData_Translator.Translate_EntityContact);
                        Log.Information("EntityContact found: EntityContactID={EntityContactID}, FK_EntityID={FK_EntityID}, EntityRecordID={EntityRecordID}, FK_ContactID={FK_ContactID}, IsPrimary={IsPrimary}, IsMarketing={IsMarketing}, IsEmergency={IsEmergency}, PreferredContactTime={PreferredContactTime}, PreferredLanguageCode={PreferredLanguageCode}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntityContactID, resultItem.FK_EntityID, resultItem.EntityRecordID, resultItem.FK_ContactID, resultItem.IsPrimary, resultItem.IsMarketing, resultItem.IsEmergency, resultItem.PreferredContactTime, resultItem.PreferredLanguageCode, resultItem.ValidFrom, resultItem.ValidTo, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("EntityContact failed to update.");
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

        #region Statuses

        public static async Task<Status> Statuses_Select_Single_Transaction(Status item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Statuses_Select_Single(item, sqlConn);
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

        public static async Task<Status> Statuses_Select_Single(Status item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Statuses_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Status> Statuses_Select_Single(Status item, SqlConnection sqlConn)
        {
            try
            {
                Status resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Statuses_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StatusID", Value = item.StatusID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Status>(EntityData_Translator.Translate_Status);
                        Log.Information("Status found: StatusID={StatusID}, FK_EntityID={FK_EntityID}, FK_StatusGroupID={FK_StatusGroupID}, SystemCode={SystemCode}, DisplayName={DisplayName}, IsActive={IsActive}, CanEdit={CanEdit}, ShowInUI={ShowInUI}, SortOrder={SortOrder}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.StatusID, resultItem.FK_EntityID, resultItem.FK_StatusGroupID, resultItem.SystemCode, resultItem.DisplayName, resultItem.IsActive, resultItem.CanEdit, resultItem.ShowInUI, resultItem.SortOrder, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Status found with the given StatusID.");
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

        public static async Task<Status> Statuses_Insert_Transaction(Status item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Statuses_Insert(item, sqlConn);
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

        public static async Task<Status> Statuses_Insert(Status item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Statuses_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Status> Statuses_Insert(Status item, SqlConnection sqlConn)
        {
            try
            {
                Status resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Statuses_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StatusGroupID", Value = item.FK_StatusGroupID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SystemCode", Value = item.SystemCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@DisplayName", Value = item.DisplayName }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@CanEdit", Value = item.CanEdit }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@ShowInUI", Value = item.ShowInUI }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SortOrder", Value = item.SortOrder }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Status>(EntityData_Translator.Translate_Status);
                        Log.Information("Status found: StatusID={StatusID}, FK_EntityID={FK_EntityID}, FK_StatusGroupID={FK_StatusGroupID}, SystemCode={SystemCode}, DisplayName={DisplayName}, IsActive={IsActive}, CanEdit={CanEdit}, ShowInUI={ShowInUI}, SortOrder={SortOrder}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.StatusID, resultItem.FK_EntityID, resultItem.FK_StatusGroupID, resultItem.SystemCode, resultItem.DisplayName, resultItem.IsActive, resultItem.CanEdit, resultItem.ShowInUI, resultItem.SortOrder, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Status failed to create.");
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

        public static async Task<List<Status>> Statuses_Select_All_Transaction(Status item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Statuses_Select_All(item, sqlConn);
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

        public static async Task<List<Status>> Statuses_Select_All(Status item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Statuses_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Status>> Statuses_Select_All(Status item, SqlConnection sqlConn)
        {
            try
            {
                List<Status> resultItem = new List<Status>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Statuses_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Status>(EntityData_Translator.Translate_Status));
                        Log.Information("Status records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Status records found.");
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

        public static async Task<Status> Statuses_Update_Transaction(Status item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Statuses_Update(item, sqlConn);
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

        public static async Task<Status> Statuses_Update(Status item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Statuses_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Status> Statuses_Update(Status item, SqlConnection sqlConn)
        {
            try
            {
                Status resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Statuses_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StatusID", Value = item.StatusID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_StatusGroupID", Value = item.FK_StatusGroupID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SystemCode", Value = item.SystemCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@DisplayName", Value = item.DisplayName }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@CanEdit", Value = item.CanEdit }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@ShowInUI", Value = item.ShowInUI }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SortOrder", Value = item.SortOrder }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Status>(EntityData_Translator.Translate_Status);
                        Log.Information("Status found: StatusID={StatusID}, FK_EntityID={FK_EntityID}, FK_StatusGroupID={FK_StatusGroupID}, SystemCode={SystemCode}, DisplayName={DisplayName}, IsActive={IsActive}, CanEdit={CanEdit}, ShowInUI={ShowInUI}, SortOrder={SortOrder}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.StatusID, resultItem.FK_EntityID, resultItem.FK_StatusGroupID, resultItem.SystemCode, resultItem.DisplayName, resultItem.IsActive, resultItem.CanEdit, resultItem.ShowInUI, resultItem.SortOrder, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Status failed to update.");
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

        #region StatusGroups

        public static async Task<StatusGroup> StatusGroups_Select_Single_Transaction(StatusGroup item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StatusGroups_Select_Single(item, sqlConn);
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

        public static async Task<StatusGroup> StatusGroups_Select_Single(StatusGroup item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StatusGroups_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StatusGroup> StatusGroups_Select_Single(StatusGroup item, SqlConnection sqlConn)
        {
            try
            {
                StatusGroup resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "StatusGroups_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StatusGroupID", Value = item.StatusGroupID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StatusGroup>(EntityData_Translator.Translate_StatusGroup);
                        Log.Information("StatusGroup found: StatusGroupID={StatusGroupID}, GroupName={GroupName}, Description={Description}", resultItem.StatusGroupID, resultItem.GroupName, resultItem.Description);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StatusGroup found with the given StatusGroupID.");
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

        public static async Task<StatusGroup> StatusGroups_Insert_Transaction(StatusGroup item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StatusGroups_Insert(item, sqlConn);
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

        public static async Task<StatusGroup> StatusGroups_Insert(StatusGroup item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StatusGroups_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StatusGroup> StatusGroups_Insert(StatusGroup item, SqlConnection sqlConn)
        {
            try
            {
                StatusGroup resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "StatusGroups_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@GroupName", Value = item.GroupName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StatusGroup>(EntityData_Translator.Translate_StatusGroup);
                        Log.Information("StatusGroup found: StatusGroupID={StatusGroupID}, GroupName={GroupName}, Description={Description}", resultItem.StatusGroupID, resultItem.GroupName, resultItem.Description);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StatusGroup failed to create.");
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

        public static async Task<List<StatusGroup>> StatusGroups_Select_All_Transaction(StatusGroup item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StatusGroups_Select_All(item, sqlConn);
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

        public static async Task<List<StatusGroup>> StatusGroups_Select_All(StatusGroup item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StatusGroups_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<StatusGroup>> StatusGroups_Select_All(StatusGroup item, SqlConnection sqlConn)
        {
            try
            {
                List<StatusGroup> resultItem = new List<StatusGroup>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "StatusGroups_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<StatusGroup>(EntityData_Translator.Translate_StatusGroup));
                        Log.Information("StatusGroup records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No StatusGroup records found.");
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

        public static async Task<StatusGroup> StatusGroups_Update_Transaction(StatusGroup item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StatusGroups_Update(item, sqlConn);
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

        public static async Task<StatusGroup> StatusGroups_Update(StatusGroup item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await StatusGroups_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<StatusGroup> StatusGroups_Update(StatusGroup item, SqlConnection sqlConn)
        {
            try
            {
                StatusGroup resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "StatusGroups_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@StatusGroupID", Value = item.StatusGroupID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@GroupName", Value = item.GroupName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<StatusGroup>(EntityData_Translator.Translate_StatusGroup);
                        Log.Information("StatusGroup found: StatusGroupID={StatusGroupID}, GroupName={GroupName}, Description={Description}", resultItem.StatusGroupID, resultItem.GroupName, resultItem.Description);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("StatusGroup failed to update.");
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

        #region TimeZones

        public static async Task<_TimeZone> TimeZones_Select_Single_Transaction(_TimeZone item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TimeZones_Select_Single(item, sqlConn);
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

        public static async Task<_TimeZone> TimeZones_Select_Single(_TimeZone item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TimeZones_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_TimeZone> TimeZones_Select_Single(_TimeZone item, SqlConnection sqlConn)
        {
            try
            {
                _TimeZone resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TimeZones_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TimeZoneID", Value = item.TimeZoneID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<_TimeZone>(EntityData_Translator.Translate__TimeZone);
                        Log.Information("_TimeZone found: TimeZoneID={TimeZoneID}, TimeZone={TimeZone}, UTCOffset={UTCOffset}, ObservesDST={ObservesDST}", resultItem.TimeZoneID, resultItem.TimeZone, resultItem.UTCOffset, resultItem.ObservesDST);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No _TimeZone found with the given _TimeZoneID.");
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

        public static async Task<_TimeZone> TimeZones_Insert_Transaction(_TimeZone item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TimeZones_Insert(item, sqlConn);
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

        public static async Task<_TimeZone> TimeZones_Insert(_TimeZone item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TimeZones_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_TimeZone> TimeZones_Insert(_TimeZone item, SqlConnection sqlConn)
        {
            try
            {
                _TimeZone resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TimeZones_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TimeZone", Value = item.TimeZone }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@UTCOffset", Value = item.UTCOffset }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@ObservesDST", Value = item.ObservesDST }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<_TimeZone>(EntityData_Translator.Translate__TimeZone);
                        Log.Information("_TimeZone found: TimeZoneID={TimeZoneID}, TimeZone={TimeZone}, UTCOffset={UTCOffset}, ObservesDST={ObservesDST}", resultItem.TimeZoneID, resultItem.TimeZone, resultItem.UTCOffset, resultItem.ObservesDST);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("_TimeZone failed to create.");
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

        public static async Task<List<_TimeZone>> TimeZones_Select_All_Transaction(_TimeZone item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TimeZones_Select_All(item, sqlConn);
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

        public static async Task<List<_TimeZone>> TimeZones_Select_All(_TimeZone item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TimeZones_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<_TimeZone>> TimeZones_Select_All(_TimeZone item, SqlConnection sqlConn)
        {
            try
            {
                List<_TimeZone> resultItem = new List<_TimeZone>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TimeZones_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<_TimeZone>(EntityData_Translator.Translate__TimeZone));
                        Log.Information("_TimeZone records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No _TimeZone records found.");
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

        public static async Task<_TimeZone> TimeZones_Update_Transaction(_TimeZone item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TimeZones_Update(item, sqlConn);
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

        public static async Task<_TimeZone> TimeZones_Update(_TimeZone item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TimeZones_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_TimeZone> TimeZones_Update(_TimeZone item, SqlConnection sqlConn)
        {
            try
            {
                _TimeZone resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TimeZones_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TimeZoneID", Value = item.TimeZoneID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TimeZone", Value = item.TimeZone }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@UTCOffset", Value = item.UTCOffset }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@ObservesDST", Value = item.ObservesDST }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<_TimeZone>(EntityData_Translator.Translate__TimeZone);
                        Log.Information("_TimeZone found: TimeZoneID={TimeZoneID}, TimeZone={TimeZone}, UTCOffset={UTCOffset}, ObservesDST={ObservesDST}", resultItem.TimeZoneID, resultItem.TimeZone, resultItem.UTCOffset, resultItem.ObservesDST);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("_TimeZone failed to update.");
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

        #region POS_TaxTypes

        public static async Task<TaxType> POS_TaxTypes_Select_Single_Transaction(TaxType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TaxTypes_Select_Single(item, sqlConn);
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

        public static async Task<TaxType> POS_TaxTypes_Select_Single(TaxType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TaxTypes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TaxType> POS_TaxTypes_Select_Single(TaxType item, SqlConnection sqlConn)
        {
            try
            {
                TaxType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TaxTypes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TaxTypeID", Value = item.TaxTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TaxType>(EntityData_Translator.Translate_TaxType);
                        Log.Information("TaxType found: TaxTypeID={TaxTypeID}, TaxName={TaxName}, TaxPercentage={TaxPercentage}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.TaxTypeID, resultItem.TaxName, resultItem.TaxPercentage, resultItem.ValidFrom, resultItem.ValidTo, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TaxType found with the given TaxTypeID.");
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

        public static async Task<TaxType> POS_TaxTypes_Insert_Transaction(TaxType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TaxTypes_Insert(item, sqlConn);
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

        public static async Task<TaxType> POS_TaxTypes_Insert(TaxType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TaxTypes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TaxType> POS_TaxTypes_Insert(TaxType item, SqlConnection sqlConn)
        {
            try
            {
                TaxType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TaxTypes_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TaxName", Value = item.TaxName }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TaxPercentage", Value = item.TaxPercentage }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TaxType>(EntityData_Translator.Translate_TaxType);
                        Log.Information("TaxType found: TaxTypeID={TaxTypeID}, TaxName={TaxName}, TaxPercentage={TaxPercentage}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.TaxTypeID, resultItem.TaxName, resultItem.TaxPercentage, resultItem.ValidFrom, resultItem.ValidTo, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TaxType failed to create.");
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

        public static async Task<List<TaxType>> POS_TaxTypes_Select_All_Transaction(TaxType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TaxTypes_Select_All(item, sqlConn);
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

        public static async Task<List<TaxType>> POS_TaxTypes_Select_All(TaxType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TaxTypes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<TaxType>> POS_TaxTypes_Select_All(TaxType item, SqlConnection sqlConn)
        {
            try
            {
                List<TaxType> resultItem = new List<TaxType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TaxTypes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<TaxType>(EntityData_Translator.Translate_TaxType));
                        Log.Information("TaxType records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No TaxType records found.");
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

        public static async Task<TaxType> POS_TaxTypes_Update_Transaction(TaxType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TaxTypes_Update(item, sqlConn);
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

        public static async Task<TaxType> POS_TaxTypes_Update(TaxType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_TaxTypes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<TaxType> POS_TaxTypes_Update(TaxType item, SqlConnection sqlConn)
        {
            try
            {
                TaxType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_TaxTypes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TaxTypeID", Value = item.TaxTypeID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@TaxName", Value = item.TaxName }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TaxPercentage", Value = item.TaxPercentage }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<TaxType>(EntityData_Translator.Translate_TaxType);
                        Log.Information("TaxType found: TaxTypeID={TaxTypeID}, TaxName={TaxName}, TaxPercentage={TaxPercentage}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.TaxTypeID, resultItem.TaxName, resultItem.TaxPercentage, resultItem.ValidFrom, resultItem.ValidTo, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("TaxType failed to update.");
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

        #region Users

        public static async Task<User> Users_Select_Single_Transaction(User item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Users_Select_Single(item, sqlConn);
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

        public static async Task<User> Users_Select_Single(User item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Users_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<User> Users_Select_Single(User item, SqlConnection sqlConn)
        {
            try
            {
                User resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Users_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@UserID", Value = item.UserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<User>(EntityData_Translator.Translate_User);
                        Log.Information("User found: UserID={UserID}, Firstname={Firstname}, Lastname={Lastname}, Username={Username}", resultItem.UserID, resultItem.Firstname, resultItem.Lastname, resultItem.Username);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No User found with the given UserID.");
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

        public static async Task<User> Users_Insert_Transaction(User item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Users_Insert(item, sqlConn);
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

        public static async Task<User> Users_Insert(User item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Users_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<User> Users_Insert(User item, SqlConnection sqlConn)
        {
            try
            {
                User resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Users_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@UserID", Value = item.UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Firstname", Value = item.Firstname }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Lastname", Value = item.Lastname }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Username", Value = item.Username }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<User>(EntityData_Translator.Translate_User);
                        Log.Information("User found: UserID={UserID}, Firstname={Firstname}, Lastname={Lastname}, Username={Username}", resultItem.UserID, resultItem.Firstname, resultItem.Lastname, resultItem.Username);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("User failed to create.");
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

        public static async Task<List<User>> Users_Select_All_Transaction(User item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Users_Select_All(item, sqlConn);
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

        public static async Task<List<User>> Users_Select_All(User item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Users_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<User>> Users_Select_All(User item, SqlConnection sqlConn)
        {
            try
            {
                List<User> resultItem = new List<User>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Users_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<User>(EntityData_Translator.Translate_User));
                        Log.Information("User records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No User records found.");
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

        public static async Task<User> Users_Update_Transaction(User item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Users_Update(item, sqlConn);
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

        public static async Task<User> Users_Update(User item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Users_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<User> Users_Update(User item, SqlConnection sqlConn)
        {
            try
            {
                User resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Users_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@UserID", Value = item.UserID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Firstname", Value = item.Firstname }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Lastname", Value = item.Lastname }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Username", Value = item.Username }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<User>(EntityData_Translator.Translate_User);
                        Log.Information("User found: UserID={UserID}, Firstname={Firstname}, Lastname={Lastname}, Username={Username}", resultItem.UserID, resultItem.Firstname, resultItem.Lastname, resultItem.Username);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("User failed to update.");
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

        #region POS_PaymentTypes

        public static async Task<PaymentType> POS_PaymentTypes_Select_Single_Transaction(PaymentType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypes_Select_Single(item, sqlConn);
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

        public static async Task<PaymentType> POS_PaymentTypes_Select_Single(PaymentType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PaymentType> POS_PaymentTypes_Select_Single(PaymentType item, SqlConnection sqlConn)
        {
            try
            {
                PaymentType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PaymentTypes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PaymentTypeID", Value = item.PaymentTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PaymentType>(EntityData_Translator.Translate_PaymentType);
                        Log.Information("PaymentType found: PaymentTypeID={PaymentTypeID}, FK_PaymentTypeIcon={FK_PaymentTypeIcon}, Name={Name}, IsActive={IsActive}, IsPrimary={IsPrimary}, IsSecondary={IsSecondary}, SettlePayment={SettlePayment}, RequireAdditionalInfo={RequireAdditionalInfo}, RequireElevation={RequireElevation}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.PaymentTypeID, resultItem.FK_PaymentTypeIcon, resultItem.Name, resultItem.IsActive, resultItem.IsPrimary, resultItem.IsSecondary, resultItem.SettlePayment, resultItem.RequireAdditionalInfo, resultItem.RequireElevation, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PaymentType found with the given PaymentTypeID.");
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

        public static async Task<PaymentType> POS_PaymentTypes_Insert_Transaction(PaymentType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypes_Insert(item, sqlConn);
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

        public static async Task<PaymentType> POS_PaymentTypes_Insert(PaymentType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PaymentType> POS_PaymentTypes_Insert(PaymentType item, SqlConnection sqlConn)
        {
            try
            {
                PaymentType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PaymentTypes_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentTypeIcon", Value = item.FK_PaymentTypeIcon }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPrimary", Value = item.IsPrimary }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsSecondary", Value = item.IsSecondary }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@SettlePayment", Value = item.SettlePayment }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@RequireAdditionalInfo", Value = item.RequireAdditionalInfo }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@RequireElevation", Value = item.RequireElevation }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PaymentType>(EntityData_Translator.Translate_PaymentType);
                        Log.Information("PaymentType found: PaymentTypeID={PaymentTypeID}, FK_PaymentTypeIcon={FK_PaymentTypeIcon}, Name={Name}, IsActive={IsActive}, IsPrimary={IsPrimary}, IsSecondary={IsSecondary}, SettlePayment={SettlePayment}, RequireAdditionalInfo={RequireAdditionalInfo}, RequireElevation={RequireElevation}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.PaymentTypeID, resultItem.FK_PaymentTypeIcon, resultItem.Name, resultItem.IsActive, resultItem.IsPrimary, resultItem.IsSecondary, resultItem.SettlePayment, resultItem.RequireAdditionalInfo, resultItem.RequireElevation, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PaymentType failed to create.");
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

        public static async Task<List<PaymentType>> POS_PaymentTypes_Select_All_Transaction(PaymentType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypes_Select_All(item, sqlConn);
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

        public static async Task<List<PaymentType>> POS_PaymentTypes_Select_All(PaymentType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PaymentType>> POS_PaymentTypes_Select_All(PaymentType item, SqlConnection sqlConn)
        {
            try
            {
                List<PaymentType> resultItem = new List<PaymentType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PaymentTypes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PaymentType>(EntityData_Translator.Translate_PaymentType));
                        Log.Information("PaymentType records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PaymentType records found.");
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

        public static async Task<PaymentType> POS_PaymentTypes_Update_Transaction(PaymentType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypes_Update(item, sqlConn);
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

        public static async Task<PaymentType> POS_PaymentTypes_Update(PaymentType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PaymentType> POS_PaymentTypes_Update(PaymentType item, SqlConnection sqlConn)
        {
            try
            {
                PaymentType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PaymentTypes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PaymentTypeID", Value = item.PaymentTypeID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PaymentTypeIcon", Value = item.FK_PaymentTypeIcon }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsPrimary", Value = item.IsPrimary }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsSecondary", Value = item.IsSecondary }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@SettlePayment", Value = item.SettlePayment }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@RequireAdditionalInfo", Value = item.RequireAdditionalInfo }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@RequireElevation", Value = item.RequireElevation }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PaymentType>(EntityData_Translator.Translate_PaymentType);
                        Log.Information("PaymentType found: PaymentTypeID={PaymentTypeID}, FK_PaymentTypeIcon={FK_PaymentTypeIcon}, Name={Name}, IsActive={IsActive}, IsPrimary={IsPrimary}, IsSecondary={IsSecondary}, SettlePayment={SettlePayment}, RequireAdditionalInfo={RequireAdditionalInfo}, RequireElevation={RequireElevation}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.PaymentTypeID, resultItem.FK_PaymentTypeIcon, resultItem.Name, resultItem.IsActive, resultItem.IsPrimary, resultItem.IsSecondary, resultItem.SettlePayment, resultItem.RequireAdditionalInfo, resultItem.RequireElevation, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PaymentType failed to update.");
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

        #region TH_BookingHeaders

        public static async Task<BookingHeader> TH_BookingHeaders_Select_Single_Transaction(BookingHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingHeaders_Select_Single(item, sqlConn);
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

        public static async Task<BookingHeader> TH_BookingHeaders_Select_Single(BookingHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingHeaders_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingHeader> TH_BookingHeaders_Select_Single(BookingHeader item, SqlConnection sqlConn)
        {
            try
            {
                BookingHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TH_BookingHeaders_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingHeaderID", Value = item.BookingHeaderID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingHeader>(EntityData_Translator.Translate_BookingHeader);
                        Log.Information("BookingHeader found: BookingHeaderID={BookingHeaderID}, PartyName={PartyName}, BookingReference={BookingReference}, FK_AgentDebtorID={FK_AgentDebtorID}, FK_BranchID={FK_BranchID}, FK_DepartmentID={FK_DepartmentID}, FK_CurrencyID={FK_CurrencyID}, QuoteTotal={QuoteTotal}, BookingTotal={BookingTotal}, FK_BookingStatusID={FK_BookingStatusID}, TravelStart={TravelStart}, TravelEnd={TravelEnd}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingHeaderID, resultItem.PartyName, resultItem.BookingReference, resultItem.FK_AgentDebtorID, resultItem.FK_BranchID, resultItem.FK_DepartmentID, resultItem.FK_CurrencyID, resultItem.QuoteTotal, resultItem.BookingTotal, resultItem.FK_BookingStatusID, resultItem.TravelStart, resultItem.TravelEnd, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No BookingHeader found with the given BookingHeaderID.");
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

        public static async Task<BookingHeader> TH_BookingHeaders_Insert_Transaction(BookingHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingHeaders_Insert(item, sqlConn);
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

        public static async Task<BookingHeader> TH_BookingHeaders_Insert(BookingHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingHeaders_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingHeader> TH_BookingHeaders_Insert(BookingHeader item, SqlConnection sqlConn)
        {
            try
            {
                BookingHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TH_BookingHeaders_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PartyName", Value = item.PartyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BookingReference", Value = item.BookingReference }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AgentDebtorID", Value = item.FK_AgentDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BranchID", Value = item.FK_BranchID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DepartmentID", Value = item.FK_DepartmentID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@QuoteTotal", Value = item.QuoteTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@BookingTotal", Value = item.BookingTotal }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BookingStatusID", Value = item.FK_BookingStatusID }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@TravelStart", Value = item.TravelStart }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@TravelEnd", Value = item.TravelEnd }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingHeader>(EntityData_Translator.Translate_BookingHeader);
                        Log.Information("BookingHeader found: BookingHeaderID={BookingHeaderID}, PartyName={PartyName}, BookingReference={BookingReference}, FK_AgentDebtorID={FK_AgentDebtorID}, FK_BranchID={FK_BranchID}, FK_DepartmentID={FK_DepartmentID}, FK_CurrencyID={FK_CurrencyID}, QuoteTotal={QuoteTotal}, BookingTotal={BookingTotal}, FK_BookingStatusID={FK_BookingStatusID}, TravelStart={TravelStart}, TravelEnd={TravelEnd}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingHeaderID, resultItem.PartyName, resultItem.BookingReference, resultItem.FK_AgentDebtorID, resultItem.FK_BranchID, resultItem.FK_DepartmentID, resultItem.FK_CurrencyID, resultItem.QuoteTotal, resultItem.BookingTotal, resultItem.FK_BookingStatusID, resultItem.TravelStart, resultItem.TravelEnd, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("BookingHeader failed to create.");
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

        public static async Task<List<BookingHeader>> TH_BookingHeaders_Select_All_Transaction(BookingHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingHeaders_Select_All(item, sqlConn);
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

        public static async Task<List<BookingHeader>> TH_BookingHeaders_Select_All(BookingHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingHeaders_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<BookingHeader>> TH_BookingHeaders_Select_All(BookingHeader item, SqlConnection sqlConn)
        {
            try
            {
                List<BookingHeader> resultItem = new List<BookingHeader>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TH_BookingHeaders_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<BookingHeader>(EntityData_Translator.Translate_BookingHeader));
                        Log.Information("BookingHeader records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No BookingHeader records found.");
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

        public static async Task<BookingHeader> TH_BookingHeaders_Update_Transaction(BookingHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingHeaders_Update(item, sqlConn);
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

        public static async Task<BookingHeader> TH_BookingHeaders_Update(BookingHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingHeaders_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingHeader> TH_BookingHeaders_Update(BookingHeader item, SqlConnection sqlConn)
        {
            try
            {
                BookingHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TH_BookingHeaders_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingHeaderID", Value = item.BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PartyName", Value = item.PartyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BookingReference", Value = item.BookingReference }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_AgentDebtorID", Value = item.FK_AgentDebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BranchID", Value = item.FK_BranchID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DepartmentID", Value = item.FK_DepartmentID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@QuoteTotal", Value = item.QuoteTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@BookingTotal", Value = item.BookingTotal }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BookingStatusID", Value = item.FK_BookingStatusID }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@TravelStart", Value = item.TravelStart }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@TravelEnd", Value = item.TravelEnd }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingHeader>(EntityData_Translator.Translate_BookingHeader);
                        Log.Information("BookingHeader found: BookingHeaderID={BookingHeaderID}, PartyName={PartyName}, BookingReference={BookingReference}, FK_AgentDebtorID={FK_AgentDebtorID}, FK_BranchID={FK_BranchID}, FK_DepartmentID={FK_DepartmentID}, FK_CurrencyID={FK_CurrencyID}, QuoteTotal={QuoteTotal}, BookingTotal={BookingTotal}, FK_BookingStatusID={FK_BookingStatusID}, TravelStart={TravelStart}, TravelEnd={TravelEnd}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingHeaderID, resultItem.PartyName, resultItem.BookingReference, resultItem.FK_AgentDebtorID, resultItem.FK_BranchID, resultItem.FK_DepartmentID, resultItem.FK_CurrencyID, resultItem.QuoteTotal, resultItem.BookingTotal, resultItem.FK_BookingStatusID, resultItem.TravelStart, resultItem.TravelEnd, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("BookingHeader failed to update.");
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

        #region Guests

        public static async Task<Guest> Guests_Select_Single_Transaction(Guest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Guests_Select_Single(item, sqlConn);
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

        public static async Task<Guest> Guests_Select_Single(Guest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Guests_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Guest> Guests_Select_Single(Guest item, SqlConnection sqlConn)
        {
            try
            {
                Guest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Guests_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@GuestID", Value = item.GuestID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Guest>(EntityData_Translator.Translate_Guest);
                        Log.Information("Guest found: GuestID={GuestID}, Title={Title}, FirstName={FirstName}, MiddleName={MiddleName}, LastName={LastName}, DateOfBirth={DateOfBirth}, Gender={Gender}, Nationality={Nationality}, PreferredLanguage={PreferredLanguage}, SpecialRequests={SpecialRequests}, LoyaltyNumber={LoyaltyNumber}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.GuestID, resultItem.Title, resultItem.FirstName, resultItem.MiddleName, resultItem.LastName, resultItem.DateOfBirth, resultItem.Gender, resultItem.Nationality, resultItem.PreferredLanguage, resultItem.SpecialRequests, resultItem.LoyaltyNumber, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Guest found with the given GuestID.");
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

        public static async Task<Guest> Guests_Insert_Transaction(Guest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Guests_Insert(item, sqlConn);
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

        public static async Task<Guest> Guests_Insert(Guest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Guests_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Guest> Guests_Insert(Guest item, SqlConnection sqlConn)
        {
            try
            {
                Guest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Guests_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Title", Value = item.Title }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@FirstName", Value = item.FirstName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MiddleName", Value = item.MiddleName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LastName", Value = item.LastName }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@DateOfBirth", Value = item.DateOfBirth }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Gender", Value = item.Gender }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Nationality", Value = item.Nationality }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PreferredLanguage", Value = item.PreferredLanguage }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SpecialRequests", Value = item.SpecialRequests }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LoyaltyNumber", Value = item.LoyaltyNumber }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Guest>(EntityData_Translator.Translate_Guest);
                        Log.Information("Guest found: GuestID={GuestID}, Title={Title}, FirstName={FirstName}, MiddleName={MiddleName}, LastName={LastName}, DateOfBirth={DateOfBirth}, Gender={Gender}, Nationality={Nationality}, PreferredLanguage={PreferredLanguage}, SpecialRequests={SpecialRequests}, LoyaltyNumber={LoyaltyNumber}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.GuestID, resultItem.Title, resultItem.FirstName, resultItem.MiddleName, resultItem.LastName, resultItem.DateOfBirth, resultItem.Gender, resultItem.Nationality, resultItem.PreferredLanguage, resultItem.SpecialRequests, resultItem.LoyaltyNumber, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Guest failed to create.");
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

        public static async Task<List<Guest>> Guests_Select_All_Transaction(Guest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Guests_Select_All(item, sqlConn);
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

        public static async Task<List<Guest>> Guests_Select_All(Guest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Guests_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Guest>> Guests_Select_All(Guest item, SqlConnection sqlConn)
        {
            try
            {
                List<Guest> resultItem = new List<Guest>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Guests_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Guest>(EntityData_Translator.Translate_Guest));
                        Log.Information("Guest records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Guest records found.");
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

        public static async Task<Guest> Guests_Update_Transaction(Guest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Guests_Update(item, sqlConn);
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

        public static async Task<Guest> Guests_Update(Guest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Guests_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Guest> Guests_Update(Guest item, SqlConnection sqlConn)
        {
            try
            {
                Guest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Guests_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@GuestID", Value = item.GuestID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Title", Value = item.Title }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@FirstName", Value = item.FirstName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MiddleName", Value = item.MiddleName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LastName", Value = item.LastName }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@DateOfBirth", Value = item.DateOfBirth }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Gender", Value = item.Gender }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Nationality", Value = item.Nationality }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PreferredLanguage", Value = item.PreferredLanguage }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SpecialRequests", Value = item.SpecialRequests }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LoyaltyNumber", Value = item.LoyaltyNumber }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Guest>(EntityData_Translator.Translate_Guest);
                        Log.Information("Guest found: GuestID={GuestID}, Title={Title}, FirstName={FirstName}, MiddleName={MiddleName}, LastName={LastName}, DateOfBirth={DateOfBirth}, Gender={Gender}, Nationality={Nationality}, PreferredLanguage={PreferredLanguage}, SpecialRequests={SpecialRequests}, LoyaltyNumber={LoyaltyNumber}, Notes={Notes}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.GuestID, resultItem.Title, resultItem.FirstName, resultItem.MiddleName, resultItem.LastName, resultItem.DateOfBirth, resultItem.Gender, resultItem.Nationality, resultItem.PreferredLanguage, resultItem.SpecialRequests, resultItem.LoyaltyNumber, resultItem.Notes, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Guest failed to update.");
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

        #region TH_BookingGuests

        public static async Task<BookingGuest> TH_BookingGuests_Select_Single_Transaction(BookingGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingGuests_Select_Single(item, sqlConn);
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

        public static async Task<BookingGuest> TH_BookingGuests_Select_Single(BookingGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingGuests_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingGuest> TH_BookingGuests_Select_Single(BookingGuest item, SqlConnection sqlConn)
        {
            try
            {
                BookingGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TH_BookingGuests_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingGuestID", Value = item.BookingGuestID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingGuest>(EntityData_Translator.Translate_BookingGuest);
                        Log.Information("BookingGuest found: BookingGuestID={BookingGuestID}, FK_BookingHeaderID={FK_BookingHeaderID}, FK_GuestID={FK_GuestID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingGuestID, resultItem.FK_BookingHeaderID, resultItem.FK_GuestID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No BookingGuest found with the given BookingGuestID.");
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

        public static async Task<BookingGuest> TH_BookingGuests_Insert_Transaction(BookingGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingGuests_Insert(item, sqlConn);
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

        public static async Task<BookingGuest> TH_BookingGuests_Insert(BookingGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingGuests_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingGuest> TH_BookingGuests_Insert(BookingGuest item, SqlConnection sqlConn)
        {
            try
            {
                BookingGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TH_BookingGuests_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BookingHeaderID", Value = item.FK_BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingGuest>(EntityData_Translator.Translate_BookingGuest);
                        Log.Information("BookingGuest found: BookingGuestID={BookingGuestID}, FK_BookingHeaderID={FK_BookingHeaderID}, FK_GuestID={FK_GuestID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingGuestID, resultItem.FK_BookingHeaderID, resultItem.FK_GuestID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("BookingGuest failed to create.");
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

        public static async Task<List<BookingGuest>> TH_BookingGuests_Select_All_Transaction(BookingGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingGuests_Select_All(item, sqlConn);
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

        public static async Task<List<BookingGuest>> TH_BookingGuests_Select_All(BookingGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingGuests_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<BookingGuest>> TH_BookingGuests_Select_All(BookingGuest item, SqlConnection sqlConn)
        {
            try
            {
                List<BookingGuest> resultItem = new List<BookingGuest>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TH_BookingGuests_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<BookingGuest>(EntityData_Translator.Translate_BookingGuest));
                        Log.Information("BookingGuest records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No BookingGuest records found.");
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

        public static async Task<BookingGuest> TH_BookingGuests_Update_Transaction(BookingGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingGuests_Update(item, sqlConn);
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

        public static async Task<BookingGuest> TH_BookingGuests_Update(BookingGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await TH_BookingGuests_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingGuest> TH_BookingGuests_Update(BookingGuest item, SqlConnection sqlConn)
        {
            try
            {
                BookingGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "TH_BookingGuests_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingGuestID", Value = item.BookingGuestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BookingHeaderID", Value = item.FK_BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingGuest>(EntityData_Translator.Translate_BookingGuest);
                        Log.Information("BookingGuest found: BookingGuestID={BookingGuestID}, FK_BookingHeaderID={FK_BookingHeaderID}, FK_GuestID={FK_GuestID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingGuestID, resultItem.FK_BookingHeaderID, resultItem.FK_GuestID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("BookingGuest failed to update.");
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

        #region POS_Images

        public static async Task<Image> POS_Images_Select_Single_Transaction(Image item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Select_Single(item, sqlConn);
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

        public static async Task<Image> POS_Images_Select_Single(Image item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Image> POS_Images_Select_Single(Image item, SqlConnection sqlConn)
        {
            try
            {
                Image resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Images_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ImageID", Value = item.ImageID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Image>(EntityData_Translator.Translate_Image);
                        Log.Information("Image found: ImageID={ImageID}, FK_ImageCategoryID={FK_ImageCategoryID}, FK_ItemID={FK_ItemID}, FileSystemPath={FileSystemPath}, RelativePath={RelativePath}, ImageName={ImageName}, FileExtension={FileExtension}, ImageUrl={ImageUrl}, LocalUrl={LocalUrl}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ImageID, resultItem.FK_ImageCategoryID, resultItem.FK_ItemID, resultItem.FileSystemPath, resultItem.RelativePath, resultItem.ImageName, resultItem.FileExtension, resultItem.ImageUrl, resultItem.LocalUrl, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Image found with the given ImageID.");
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

        public static async Task<Image> POS_Images_Insert_Transaction(Image item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Insert(item, sqlConn);
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

        public static async Task<Image> POS_Images_Insert(Image item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Image> POS_Images_Insert(Image item, SqlConnection sqlConn)
        {
            try
            {
                Image resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Images_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ImageCategoryID", Value = item.FK_ImageCategoryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ItemID", Value = item.FK_ItemID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@FileSystemPath", Value = item.FileSystemPath }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RelativePath", Value = item.RelativePath }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ImageName", Value = item.ImageName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@FileExtension", Value = item.FileExtension }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ImageUrl", Value = item.ImageUrl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LocalUrl", Value = item.LocalUrl }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
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

        public static async Task<List<Image>> POS_Images_Select_All_Transaction(Image item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Select_All(item, sqlConn);
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

        public static async Task<List<Image>> POS_Images_Select_All(Image item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Image>> POS_Images_Select_All(Image item, SqlConnection sqlConn)
        {
            try
            {
                List<Image> resultItem = new List<Image>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Images_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Image>(EntityData_Translator.Translate_Image));
                        Log.Information("Image records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Image records found.");
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

        public static async Task<Image> POS_Images_Update_Transaction(Image item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Update(item, sqlConn);
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

        public static async Task<Image> POS_Images_Update(Image item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Images_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Image> POS_Images_Update(Image item, SqlConnection sqlConn)
        {
            try
            {
                Image resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Images_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ImageID", Value = item.ImageID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ImageCategoryID", Value = item.FK_ImageCategoryID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ItemID", Value = item.FK_ItemID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@FileSystemPath", Value = item.FileSystemPath }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@RelativePath", Value = item.RelativePath }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ImageName", Value = item.ImageName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@FileExtension", Value = item.FileExtension }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ImageUrl", Value = item.ImageUrl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@LocalUrl", Value = item.LocalUrl }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Image>(EntityData_Translator.Translate_Image);
                        Log.Information("Image found: ImageID={ImageID}, FK_ImageCategoryID={FK_ImageCategoryID}, FK_ItemID={FK_ItemID}, FileSystemPath={FileSystemPath}, RelativePath={RelativePath}, ImageName={ImageName}, FileExtension={FileExtension}, ImageUrl={ImageUrl}, LocalUrl={LocalUrl}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ImageID, resultItem.FK_ImageCategoryID, resultItem.FK_ItemID, resultItem.FileSystemPath, resultItem.RelativePath, resultItem.ImageName, resultItem.FileExtension, resultItem.ImageUrl, resultItem.LocalUrl, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Image failed to update.");
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

        #region POS_ImageCategories

        public static async Task<ImageCategory> POS_ImageCategories_Select_Single_Transaction(ImageCategory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ImageCategories_Select_Single(item, sqlConn);
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

        public static async Task<ImageCategory> POS_ImageCategories_Select_Single(ImageCategory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ImageCategories_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ImageCategory> POS_ImageCategories_Select_Single(ImageCategory item, SqlConnection sqlConn)
        {
            try
            {
                ImageCategory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_ImageCategories_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ImageCategoryID", Value = item.ImageCategoryID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ImageCategory>(EntityData_Translator.Translate_ImageCategory);
                        Log.Information("ImageCategory found: ImageCategoryID={ImageCategoryID}, Category={Category}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ImageCategoryID, resultItem.Category, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No ImageCategory found with the given ImageCategoryID.");
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

        public static async Task<ImageCategory> POS_ImageCategories_Insert_Transaction(ImageCategory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ImageCategories_Insert(item, sqlConn);
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

        public static async Task<ImageCategory> POS_ImageCategories_Insert(ImageCategory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ImageCategories_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ImageCategory> POS_ImageCategories_Insert(ImageCategory item, SqlConnection sqlConn)
        {
            try
            {
                ImageCategory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_ImageCategories_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Category", Value = item.Category }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ImageCategory>(EntityData_Translator.Translate_ImageCategory);
                        Log.Information("ImageCategory found: ImageCategoryID={ImageCategoryID}, Category={Category}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ImageCategoryID, resultItem.Category, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("ImageCategory failed to create.");
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

        public static async Task<List<ImageCategory>> POS_ImageCategories_Select_All_Transaction(ImageCategory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ImageCategories_Select_All(item, sqlConn);
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

        public static async Task<List<ImageCategory>> POS_ImageCategories_Select_All(ImageCategory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ImageCategories_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<ImageCategory>> POS_ImageCategories_Select_All(ImageCategory item, SqlConnection sqlConn)
        {
            try
            {
                List<ImageCategory> resultItem = new List<ImageCategory>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_ImageCategories_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<ImageCategory>(EntityData_Translator.Translate_ImageCategory));
                        Log.Information("ImageCategory records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No ImageCategory records found.");
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

        public static async Task<ImageCategory> POS_ImageCategories_Update_Transaction(ImageCategory item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ImageCategories_Update(item, sqlConn);
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

        public static async Task<ImageCategory> POS_ImageCategories_Update(ImageCategory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ImageCategories_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ImageCategory> POS_ImageCategories_Update(ImageCategory item, SqlConnection sqlConn)
        {
            try
            {
                ImageCategory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_ImageCategories_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ImageCategoryID", Value = item.ImageCategoryID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Category", Value = item.Category }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ImageCategory>(EntityData_Translator.Translate_ImageCategory);
                        Log.Information("ImageCategory found: ImageCategoryID={ImageCategoryID}, Category={Category}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ImageCategoryID, resultItem.Category, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("ImageCategory failed to update.");
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

        #region POS_PaymentTypeIcons

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Select_Single_Transaction(PaymentTypeIcon item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypeIcons_Select_Single(item, sqlConn);
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

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Select_Single(PaymentTypeIcon item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypeIcons_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Select_Single(PaymentTypeIcon item, SqlConnection sqlConn)
        {
            try
            {
                PaymentTypeIcon resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PaymentTypeIcons_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PaymentTypeIconID", Value = item.PaymentTypeIconID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PaymentTypeIcon>(EntityData_Translator.Translate_PaymentTypeIcon);
                        Log.Information("PaymentTypeIcon found: PaymentTypeIconID={PaymentTypeIconID}, IconPath={IconPath}, Category={Category}, DateCreated={DateCreated}", resultItem.PaymentTypeIconID, resultItem.IconPath, resultItem.Category, resultItem.DateCreated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PaymentTypeIcon found with the given PaymentTypeIconID.");
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

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Insert_Transaction(PaymentTypeIcon item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypeIcons_Insert(item, sqlConn);
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

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Insert(PaymentTypeIcon item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypeIcons_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Insert(PaymentTypeIcon item, SqlConnection sqlConn)
        {
            try
            {
                PaymentTypeIcon resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PaymentTypeIcons_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@IconPath", Value = item.IconPath }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Category", Value = item.Category }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PaymentTypeIcon>(EntityData_Translator.Translate_PaymentTypeIcon);
                        Log.Information("PaymentTypeIcon found: PaymentTypeIconID={PaymentTypeIconID}, IconPath={IconPath}, Category={Category}, DateCreated={DateCreated}", resultItem.PaymentTypeIconID, resultItem.IconPath, resultItem.Category, resultItem.DateCreated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PaymentTypeIcon failed to create.");
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

        public static async Task<List<PaymentTypeIcon>> POS_PaymentTypeIcons_Select_All_Transaction(PaymentTypeIcon item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypeIcons_Select_All(item, sqlConn);
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

        public static async Task<List<PaymentTypeIcon>> POS_PaymentTypeIcons_Select_All(PaymentTypeIcon item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypeIcons_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<PaymentTypeIcon>> POS_PaymentTypeIcons_Select_All(PaymentTypeIcon item, SqlConnection sqlConn)
        {
            try
            {
                List<PaymentTypeIcon> resultItem = new List<PaymentTypeIcon>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PaymentTypeIcons_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<PaymentTypeIcon>(EntityData_Translator.Translate_PaymentTypeIcon));
                        Log.Information("PaymentTypeIcon records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No PaymentTypeIcon records found.");
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

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Update_Transaction(PaymentTypeIcon item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypeIcons_Update(item, sqlConn);
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

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Update(PaymentTypeIcon item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_PaymentTypeIcons_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PaymentTypeIcon> POS_PaymentTypeIcons_Update(PaymentTypeIcon item, SqlConnection sqlConn)
        {
            try
            {
                PaymentTypeIcon resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_PaymentTypeIcons_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PaymentTypeIconID", Value = item.PaymentTypeIconID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@IconPath", Value = item.IconPath }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Category", Value = item.Category }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PaymentTypeIcon>(EntityData_Translator.Translate_PaymentTypeIcon);
                        Log.Information("PaymentTypeIcon found: PaymentTypeIconID={PaymentTypeIconID}, IconPath={IconPath}, Category={Category}, DateCreated={DateCreated}", resultItem.PaymentTypeIconID, resultItem.IconPath, resultItem.Category, resultItem.DateCreated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("PaymentTypeIcon failed to update.");
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

        #region POS_Settings

        public static async Task<Settings> POS_Settings_Select_Single_Transaction(Settings item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Settings_Select_Single(item, sqlConn);
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

        public static async Task<Settings> POS_Settings_Select_Single(Settings item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Settings_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Settings> POS_Settings_Select_Single(Settings item, SqlConnection sqlConn)
        {
            try
            {
                Settings resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Settings_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SettingID", Value = item.SettingID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Settings>(EntityData_Translator.Translate_Settings);
                        Log.Information("Settings found: SettingID={SettingID}, CompanyName={CompanyName}, Email={Email}, HeadOfficeNo={HeadOfficeNo}, FK_CurrencyID={FK_CurrencyID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.SettingID, resultItem.CompanyName, resultItem.Email, resultItem.HeadOfficeNo, resultItem.FK_CurrencyID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Settings found with the given SettingsID.");
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

        public static async Task<Settings> POS_Settings_Insert_Transaction(Settings item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Settings_Insert(item, sqlConn);
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

        public static async Task<Settings> POS_Settings_Insert(Settings item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Settings_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Settings> POS_Settings_Insert(Settings item, SqlConnection sqlConn)
        {
            try
            {
                Settings resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Settings_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CompanyName", Value = item.CompanyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Email", Value = item.Email }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@HeadOfficeNo", Value = item.HeadOfficeNo }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Settings>(EntityData_Translator.Translate_Settings);
                        Log.Information("Settings found: SettingID={SettingID}, CompanyName={CompanyName}, Email={Email}, HeadOfficeNo={HeadOfficeNo}, FK_CurrencyID={FK_CurrencyID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.SettingID, resultItem.CompanyName, resultItem.Email, resultItem.HeadOfficeNo, resultItem.FK_CurrencyID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Settings failed to create.");
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

        public static async Task<List<Settings>> POS_Settings_Select_All_Transaction(Settings item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Settings_Select_All(item, sqlConn);
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

        public static async Task<List<Settings>> POS_Settings_Select_All(Settings item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Settings_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Settings>> POS_Settings_Select_All(Settings item, SqlConnection sqlConn)
        {
            try
            {
                List<Settings> resultItem = new List<Settings>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Settings_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Settings>(EntityData_Translator.Translate_Settings));
                        Log.Information("Settings records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Settings records found.");
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

        public static async Task<Settings> POS_Settings_Update_Transaction(Settings item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Settings_Update(item, sqlConn);
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

        public static async Task<Settings> POS_Settings_Update(Settings item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Settings_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Settings> POS_Settings_Update(Settings item, SqlConnection sqlConn)
        {
            try
            {
                Settings resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Settings_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SettingID", Value = item.SettingID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CompanyName", Value = item.CompanyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Email", Value = item.Email }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@HeadOfficeNo", Value = item.HeadOfficeNo }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Settings>(EntityData_Translator.Translate_Settings);
                        Log.Information("Settings found: SettingID={SettingID}, CompanyName={CompanyName}, Email={Email}, HeadOfficeNo={HeadOfficeNo}, FK_CurrencyID={FK_CurrencyID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.SettingID, resultItem.CompanyName, resultItem.Email, resultItem.HeadOfficeNo, resultItem.FK_CurrencyID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Settings failed to update.");
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

        #region POS_ExchangeRates

        public static async Task<ExchangeRate> POS_ExchangeRates_Select_Single_Transaction(ExchangeRate item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ExchangeRates_Select_Single(item, sqlConn);
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

        public static async Task<ExchangeRate> POS_ExchangeRates_Select_Single(ExchangeRate item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ExchangeRates_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ExchangeRate> POS_ExchangeRates_Select_Single(ExchangeRate item, SqlConnection sqlConn)
        {
            try
            {
                ExchangeRate resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_ExchangeRates_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ExchangeRateID", Value = item.ExchangeRateID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ExchangeRate>(EntityData_Translator.Translate_ExchangeRate);
                        Log.Information("ExchangeRate found: ExchangeRateID={ExchangeRateID}, FK_CurrencyID={FK_CurrencyID}, ExchangeRate={ExchangeRate}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ExchangeRateID, resultItem.FK_CurrencyID, resultItem.ExchangeRate, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No ExchangeRate found with the given ExchangeRateID.");
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

        public static async Task<ExchangeRate> POS_ExchangeRates_Insert_Transaction(ExchangeRate item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ExchangeRates_Insert(item, sqlConn);
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

        public static async Task<ExchangeRate> POS_ExchangeRates_Insert(ExchangeRate item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ExchangeRates_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ExchangeRate> POS_ExchangeRates_Insert(ExchangeRate item, SqlConnection sqlConn)
        {
            try
            {
                ExchangeRate resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_ExchangeRates_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExchangeRate", Value = item.ExchangeRate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ExchangeRate>(EntityData_Translator.Translate_ExchangeRate);
                        Log.Information("ExchangeRate found: ExchangeRateID={ExchangeRateID}, FK_CurrencyID={FK_CurrencyID}, ExchangeRate={ExchangeRate}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ExchangeRateID, resultItem.FK_CurrencyID, resultItem.ExchangeRate, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("ExchangeRate failed to create.");
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

        public static async Task<List<ExchangeRate>> POS_ExchangeRates_Select_All_Transaction(ExchangeRate item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ExchangeRates_Select_All(item, sqlConn);
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

        public static async Task<List<ExchangeRate>> POS_ExchangeRates_Select_All(ExchangeRate item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ExchangeRates_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<ExchangeRate>> POS_ExchangeRates_Select_All(ExchangeRate item, SqlConnection sqlConn)
        {
            try
            {
                List<ExchangeRate> resultItem = new List<ExchangeRate>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_ExchangeRates_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<ExchangeRate>(EntityData_Translator.Translate_ExchangeRate));
                        Log.Information("ExchangeRate records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No ExchangeRate records found.");
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

        public static async Task<ExchangeRate> POS_ExchangeRates_Update_Transaction(ExchangeRate item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ExchangeRates_Update(item, sqlConn);
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

        public static async Task<ExchangeRate> POS_ExchangeRates_Update(ExchangeRate item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ExchangeRates_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ExchangeRate> POS_ExchangeRates_Update(ExchangeRate item, SqlConnection sqlConn)
        {
            try
            {
                ExchangeRate resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_ExchangeRates_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ExchangeRateID", Value = item.ExchangeRateID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExchangeRate", Value = item.ExchangeRate }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ExchangeRate>(EntityData_Translator.Translate_ExchangeRate);
                        Log.Information("ExchangeRate found: ExchangeRateID={ExchangeRateID}, FK_CurrencyID={FK_CurrencyID}, ExchangeRate={ExchangeRate}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.ExchangeRateID, resultItem.FK_CurrencyID, resultItem.ExchangeRate, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("ExchangeRate failed to update.");
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

        #region CurrencyExchangeRates

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Select_Single_Transaction(CurrencyExchangeRate item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CurrencyExchangeRates_Select_Single(item, sqlConn);
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

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Select_Single(CurrencyExchangeRate item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CurrencyExchangeRates_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Select_Single(CurrencyExchangeRate item, SqlConnection sqlConn)
        {
            try
            {
                CurrencyExchangeRate resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CurrencyExchangeRates_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CurrencyExchangeRateID", Value = item.CurrencyExchangeRateID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CurrencyExchangeRate>(EntityData_Translator.Translate_CurrencyExchangeRate);
                        Log.Information("CurrencyExchangeRate found: CurrencyExchangeRateID={CurrencyExchangeRateID}, FK_FromCurrencyID={FK_FromCurrencyID}, FK_ToCurrencyID={FK_ToCurrencyID}, ExchangeRate={ExchangeRate}, ConversionMethod={ConversionMethod}, EffectiveDate={EffectiveDate}, Notes={Notes}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CurrencyExchangeRateID, resultItem.FK_FromCurrencyID, resultItem.FK_ToCurrencyID, resultItem.ExchangeRate, resultItem.ConversionMethod, resultItem.EffectiveDate, resultItem.Notes, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CurrencyExchangeRate found with the given CurrencyExchangeRateID.");
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

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Insert_Transaction(CurrencyExchangeRate item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CurrencyExchangeRates_Insert(item, sqlConn);
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

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Insert(CurrencyExchangeRate item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CurrencyExchangeRates_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Insert(CurrencyExchangeRate item, SqlConnection sqlConn)
        {
            try
            {
                CurrencyExchangeRate resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CurrencyExchangeRates_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_FromCurrencyID", Value = item.FK_FromCurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToCurrencyID", Value = item.FK_ToCurrencyID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExchangeRate", Value = item.ExchangeRate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ConversionMethod", Value = item.ConversionMethod }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@EffectiveDate", Value = item.EffectiveDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CurrencyExchangeRate>(EntityData_Translator.Translate_CurrencyExchangeRate);
                        Log.Information("CurrencyExchangeRate found: CurrencyExchangeRateID={CurrencyExchangeRateID}, FK_FromCurrencyID={FK_FromCurrencyID}, FK_ToCurrencyID={FK_ToCurrencyID}, ExchangeRate={ExchangeRate}, ConversionMethod={ConversionMethod}, EffectiveDate={EffectiveDate}, Notes={Notes}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CurrencyExchangeRateID, resultItem.FK_FromCurrencyID, resultItem.FK_ToCurrencyID, resultItem.ExchangeRate, resultItem.ConversionMethod, resultItem.EffectiveDate, resultItem.Notes, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CurrencyExchangeRate failed to create.");
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

        public static async Task<List<CurrencyExchangeRate>> CurrencyExchangeRates_Select_All_Transaction(CurrencyExchangeRate item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CurrencyExchangeRates_Select_All(item, sqlConn);
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

        public static async Task<List<CurrencyExchangeRate>> CurrencyExchangeRates_Select_All(CurrencyExchangeRate item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CurrencyExchangeRates_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<CurrencyExchangeRate>> CurrencyExchangeRates_Select_All(CurrencyExchangeRate item, SqlConnection sqlConn)
        {
            try
            {
                List<CurrencyExchangeRate> resultItem = new List<CurrencyExchangeRate>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CurrencyExchangeRates_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<CurrencyExchangeRate>(EntityData_Translator.Translate_CurrencyExchangeRate));
                        Log.Information("CurrencyExchangeRate records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No CurrencyExchangeRate records found.");
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

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Update_Transaction(CurrencyExchangeRate item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CurrencyExchangeRates_Update(item, sqlConn);
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

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Update(CurrencyExchangeRate item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await CurrencyExchangeRates_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<CurrencyExchangeRate> CurrencyExchangeRates_Update(CurrencyExchangeRate item, SqlConnection sqlConn)
        {
            try
            {
                CurrencyExchangeRate resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "CurrencyExchangeRates_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@CurrencyExchangeRateID", Value = item.CurrencyExchangeRateID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_FromCurrencyID", Value = item.FK_FromCurrencyID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ToCurrencyID", Value = item.FK_ToCurrencyID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExchangeRate", Value = item.ExchangeRate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ConversionMethod", Value = item.ConversionMethod }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@EffectiveDate", Value = item.EffectiveDate }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Notes", Value = item.Notes }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<CurrencyExchangeRate>(EntityData_Translator.Translate_CurrencyExchangeRate);
                        Log.Information("CurrencyExchangeRate found: CurrencyExchangeRateID={CurrencyExchangeRateID}, FK_FromCurrencyID={FK_FromCurrencyID}, FK_ToCurrencyID={FK_ToCurrencyID}, ExchangeRate={ExchangeRate}, ConversionMethod={ConversionMethod}, EffectiveDate={EffectiveDate}, Notes={Notes}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CurrencyExchangeRateID, resultItem.FK_FromCurrencyID, resultItem.FK_ToCurrencyID, resultItem.ExchangeRate, resultItem.ConversionMethod, resultItem.EffectiveDate, resultItem.Notes, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("CurrencyExchangeRate failed to update.");
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

        #region GlobalSettings

        public static async Task<GlobalSettings> GlobalSettings_Select_Single_Transaction(GlobalSettings item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await GlobalSettings_Select_Single(item, sqlConn);
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

        public static async Task<GlobalSettings> GlobalSettings_Select_Single(GlobalSettings item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await GlobalSettings_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<GlobalSettings> GlobalSettings_Select_Single(GlobalSettings item, SqlConnection sqlConn)
        {
            try
            {
                GlobalSettings resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "GlobalSettings_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@GlobalSettingID", Value = item.GlobalSettingID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<GlobalSettings>(EntityData_Translator.Translate_GlobalSettings);
                        Log.Information("GlobalSettings found: GlobalSettingID={GlobalSettingID}, Key={Key}, Value={Value}, Environment={Environment}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.GlobalSettingID, resultItem.Key, resultItem.Value, resultItem.Environment, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No GlobalSettings found with the given GlobalSettingsID.");
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

        public static async Task<GlobalSettings> GlobalSettings_Insert_Transaction(GlobalSettings item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await GlobalSettings_Insert(item, sqlConn);
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

        public static async Task<GlobalSettings> GlobalSettings_Insert(GlobalSettings item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await GlobalSettings_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<GlobalSettings> GlobalSettings_Insert(GlobalSettings item, SqlConnection sqlConn)
        {
            try
            {
                GlobalSettings resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "GlobalSettings_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Key", Value = item.Key }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Value", Value = item.Value }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Environment", Value = item.Environment }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<GlobalSettings>(EntityData_Translator.Translate_GlobalSettings);
                        Log.Information("GlobalSettings found: GlobalSettingID={GlobalSettingID}, Key={Key}, Value={Value}, Environment={Environment}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.GlobalSettingID, resultItem.Key, resultItem.Value, resultItem.Environment, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("GlobalSettings failed to create.");
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

        public static async Task<List<GlobalSettings>> GlobalSettings_Select_All_Transaction(GlobalSettings item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await GlobalSettings_Select_All(item, sqlConn);
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

        public static async Task<List<GlobalSettings>> GlobalSettings_Select_All(GlobalSettings item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await GlobalSettings_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<GlobalSettings>> GlobalSettings_Select_All(GlobalSettings item, SqlConnection sqlConn)
        {
            try
            {
                List<GlobalSettings> resultItem = new List<GlobalSettings>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "GlobalSettings_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<GlobalSettings>(EntityData_Translator.Translate_GlobalSettings));
                        Log.Information("GlobalSettings records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No GlobalSettings records found.");
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

        public static async Task<GlobalSettings> GlobalSettings_Update_Transaction(GlobalSettings item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await GlobalSettings_Update(item, sqlConn);
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

        public static async Task<GlobalSettings> GlobalSettings_Update(GlobalSettings item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await GlobalSettings_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<GlobalSettings> GlobalSettings_Update(GlobalSettings item, SqlConnection sqlConn)
        {
            try
            {
                GlobalSettings resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "GlobalSettings_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@GlobalSettingID", Value = item.GlobalSettingID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Key", Value = item.Key }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Value", Value = item.Value }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Environment", Value = item.Environment }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<GlobalSettings>(EntityData_Translator.Translate_GlobalSettings);
                        Log.Information("GlobalSettings found: GlobalSettingID={GlobalSettingID}, Key={Key}, Value={Value}, Environment={Environment}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.GlobalSettingID, resultItem.Key, resultItem.Value, resultItem.Environment, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("GlobalSettings failed to update.");
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

        #region POS_SlipTypes

        public static async Task<SlipType> POS_SlipTypes_Select_Single_Transaction(SlipType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipTypes_Select_Single(item, sqlConn);
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

        public static async Task<SlipType> POS_SlipTypes_Select_Single(SlipType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipTypes_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipType> POS_SlipTypes_Select_Single(SlipType item, SqlConnection sqlConn)
        {
            try
            {
                SlipType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SlipTypes_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SlipTypeID", Value = item.SlipTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SlipType>(EntityData_Translator.Translate_SlipType);
                        Log.Information("SlipType found: SlipTypeID={SlipTypeID}, SlipType={SlipType}, SlipCode={SlipCode}, Description={Description}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.SlipTypeID, resultItem.SlipType, resultItem.SlipCode, resultItem.Description, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No SlipType found with the given SlipTypeID.");
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

        public static async Task<SlipType> POS_SlipTypes_Insert_Transaction(SlipType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipTypes_Insert(item, sqlConn);
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

        public static async Task<SlipType> POS_SlipTypes_Insert(SlipType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipTypes_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipType> POS_SlipTypes_Insert(SlipType item, SqlConnection sqlConn)
        {
            try
            {
                SlipType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SlipTypes_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SlipType", Value = item.SlipType }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SlipCode", Value = item.SlipCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SlipType>(EntityData_Translator.Translate_SlipType);
                        Log.Information("SlipType found: SlipTypeID={SlipTypeID}, SlipType={SlipType}, SlipCode={SlipCode}, Description={Description}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.SlipTypeID, resultItem.SlipType, resultItem.SlipCode, resultItem.Description, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("SlipType failed to create.");
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

        public static async Task<List<SlipType>> POS_SlipTypes_Select_All_Transaction(SlipType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipTypes_Select_All(item, sqlConn);
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

        public static async Task<List<SlipType>> POS_SlipTypes_Select_All(SlipType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipTypes_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<SlipType>> POS_SlipTypes_Select_All(SlipType item, SqlConnection sqlConn)
        {
            try
            {
                List<SlipType> resultItem = new List<SlipType>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SlipTypes_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<SlipType>(EntityData_Translator.Translate_SlipType));
                        Log.Information("SlipType records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No SlipType records found.");
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

        public static async Task<SlipType> POS_SlipTypes_Update_Transaction(SlipType item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipTypes_Update(item, sqlConn);
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

        public static async Task<SlipType> POS_SlipTypes_Update(SlipType item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_SlipTypes_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<SlipType> POS_SlipTypes_Update(SlipType item, SqlConnection sqlConn)
        {
            try
            {
                SlipType resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_SlipTypes_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SlipTypeID", Value = item.SlipTypeID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SlipType", Value = item.SlipType }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@SlipCode", Value = item.SlipCode }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<SlipType>(EntityData_Translator.Translate_SlipType);
                        Log.Information("SlipType found: SlipTypeID={SlipTypeID}, SlipType={SlipType}, SlipCode={SlipCode}, Description={Description}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.SlipTypeID, resultItem.SlipType, resultItem.SlipCode, resultItem.Description, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("SlipType failed to update.");
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

        #region EntitySettings

        public static async Task<EntitySetting> EntitySettings_Select_Single_Transaction(EntitySetting item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntitySettings_Select_Single(item, sqlConn);
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

        public static async Task<EntitySetting> EntitySettings_Select_Single(EntitySetting item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntitySettings_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntitySetting> EntitySettings_Select_Single(EntitySetting item, SqlConnection sqlConn)
        {
            try
            {
                EntitySetting resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntitySettings_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntitySettingID", Value = item.EntitySettingID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntitySetting>(EntityData_Translator.Translate_EntitySetting);
                        Log.Information("EntitySetting found: EntitySettingID={EntitySettingID}, FK_EntityID={FK_EntityID}, IsCreditor={IsCreditor}, IsDebtor={IsDebtor}, IsBranch={IsBranch}, IsDepartment={IsDepartment}, IsUser={IsUser}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntitySettingID, resultItem.FK_EntityID, resultItem.IsCreditor, resultItem.IsDebtor, resultItem.IsBranch, resultItem.IsDepartment, resultItem.IsUser, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No EntitySetting found with the given EntitySettingID.");
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

        public static async Task<EntitySetting> EntitySettings_Insert_Transaction(EntitySetting item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntitySettings_Insert(item, sqlConn);
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

        public static async Task<EntitySetting> EntitySettings_Insert(EntitySetting item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntitySettings_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntitySetting> EntitySettings_Insert(EntitySetting item, SqlConnection sqlConn)
        {
            try
            {
                EntitySetting resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntitySettings_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsCreditor", Value = item.IsCreditor }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDebtor", Value = item.IsDebtor }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsBranch", Value = item.IsBranch }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDepartment", Value = item.IsDepartment }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsUser", Value = item.IsUser }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntitySetting>(EntityData_Translator.Translate_EntitySetting);
                        Log.Information("EntitySetting found: EntitySettingID={EntitySettingID}, FK_EntityID={FK_EntityID}, IsCreditor={IsCreditor}, IsDebtor={IsDebtor}, IsBranch={IsBranch}, IsDepartment={IsDepartment}, IsUser={IsUser}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntitySettingID, resultItem.FK_EntityID, resultItem.IsCreditor, resultItem.IsDebtor, resultItem.IsBranch, resultItem.IsDepartment, resultItem.IsUser, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("EntitySetting failed to create.");
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

        public static async Task<List<EntitySetting>> EntitySettings_Select_All_Transaction(EntitySetting item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntitySettings_Select_All(item, sqlConn);
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

        public static async Task<List<EntitySetting>> EntitySettings_Select_All(EntitySetting item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntitySettings_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<EntitySetting>> EntitySettings_Select_All(EntitySetting item, SqlConnection sqlConn)
        {
            try
            {
                List<EntitySetting> resultItem = new List<EntitySetting>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntitySettings_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<EntitySetting>(EntityData_Translator.Translate_EntitySetting));
                        Log.Information("EntitySetting records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No EntitySetting records found.");
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

        public static async Task<EntitySetting> EntitySettings_Update_Transaction(EntitySetting item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntitySettings_Update(item, sqlConn);
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

        public static async Task<EntitySetting> EntitySettings_Update(EntitySetting item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await EntitySettings_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<EntitySetting> EntitySettings_Update(EntitySetting item, SqlConnection sqlConn)
        {
            try
            {
                EntitySetting resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "EntitySettings_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@EntitySettingID", Value = item.EntitySettingID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_EntityID", Value = item.FK_EntityID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsCreditor", Value = item.IsCreditor }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDebtor", Value = item.IsDebtor }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsBranch", Value = item.IsBranch }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDepartment", Value = item.IsDepartment }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsUser", Value = item.IsUser }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<EntitySetting>(EntityData_Translator.Translate_EntitySetting);
                        Log.Information("EntitySetting found: EntitySettingID={EntitySettingID}, FK_EntityID={FK_EntityID}, IsCreditor={IsCreditor}, IsDebtor={IsDebtor}, IsBranch={IsBranch}, IsDepartment={IsDepartment}, IsUser={IsUser}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.EntitySettingID, resultItem.FK_EntityID, resultItem.IsCreditor, resultItem.IsDebtor, resultItem.IsBranch, resultItem.IsDepartment, resultItem.IsUser, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("EntitySetting failed to update.");
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
