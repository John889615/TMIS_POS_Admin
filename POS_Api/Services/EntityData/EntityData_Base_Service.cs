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

using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.EntityData.POS_PaymentTypes;
using POS_Common.Models.EntityData.Guests;
using POS_Common.Models.EntityData.BookingGuests;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.Models.EntityData.POS_ImageCategories;
using POS_Common.Models.EntityData.POS_PaymentTypeIcons;
using POS_Common.Models.EntityData.POS_Settings;
using POS_Common.Models.EntityData.POS_ExchangeRates;
using POS_Common.Models.EntityData.CurrencyExchangeRates;
using POS_Common.Models.EntityData.GlobalSettings;
using POS_Common.Models.EntityData.POS_SlipTypes;
using POS_Common.Models.EntityData.BookingHeaders;

namespace POS_Api.Services.EntityData
{
    public abstract class EntityData_Base_Service
    {
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
                        Log.Information("Currency found: CurrencyID={CurrencyID}, Currency={Currency}, Name={Name}, ISO2Code={ISO2Code}, Symbol={Symbol}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CurrencyID, resultItem.Currency, resultItem.Name, resultItem.ISO2Code, resultItem.Symbol, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
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
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Symbol", Value = item.Symbol }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Currency>(EntityData_Translator.Translate_Currency);
                        Log.Information("Currency found: CurrencyID={CurrencyID}, Currency={Currency}, Name={Name}, ISO2Code={ISO2Code}, Symbol={Symbol}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CurrencyID, resultItem.Currency, resultItem.Name, resultItem.ISO2Code, resultItem.Symbol, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
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
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Symbol", Value = item.Symbol }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Currency>(EntityData_Translator.Translate_Currency);
                        Log.Information("Currency found: CurrencyID={CurrencyID}, Currency={Currency}, Name={Name}, ISO2Code={ISO2Code}, Symbol={Symbol}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.CurrencyID, resultItem.Currency, resultItem.Name, resultItem.ISO2Code, resultItem.Symbol, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
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

        #region BookingGuests

        public static async Task<BookingGuest> BookingGuests_Select_Single_Transaction(BookingGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingGuests_Select_Single(item, sqlConn);
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

        public static async Task<BookingGuest> BookingGuests_Select_Single(BookingGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingGuests_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingGuest> BookingGuests_Select_Single(BookingGuest item, SqlConnection sqlConn)
        {
            try
            {
                BookingGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "BookingGuests_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingGuestID", Value = item.BookingGuestID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingGuest>(EntityData_Translator.Translate_BookingGuest);
                        Log.Information("BookingGuest found: BookingGuestID={BookingGuestID}, FK_GuestID={FK_GuestID}, FK_BookingHeaderID={FK_BookingHeaderID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingGuestID, resultItem.FK_GuestID, resultItem.FK_BookingHeaderID, resultItem.DateCreated, resultItem.DateUpdated);
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

        public static async Task<BookingGuest> BookingGuests_Insert_Transaction(BookingGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingGuests_Insert(item, sqlConn);
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

        public static async Task<BookingGuest> BookingGuests_Insert(BookingGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingGuests_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingGuest> BookingGuests_Insert(BookingGuest item, SqlConnection sqlConn)
        {
            try
            {
                BookingGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "BookingGuests_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingGuestID", Value = item.BookingGuestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BookingHeaderID", Value = item.FK_BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingGuest>(EntityData_Translator.Translate_BookingGuest);
                        Log.Information("BookingGuest found: BookingGuestID={BookingGuestID}, FK_GuestID={FK_GuestID}, FK_BookingHeaderID={FK_BookingHeaderID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingGuestID, resultItem.FK_GuestID, resultItem.FK_BookingHeaderID, resultItem.DateCreated, resultItem.DateUpdated);
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

        public static async Task<List<BookingGuest>> BookingGuests_Select_All_Transaction(BookingGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingGuests_Select_All(item, sqlConn);
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

        public static async Task<List<BookingGuest>> BookingGuests_Select_All(BookingGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingGuests_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<BookingGuest>> BookingGuests_Select_All(BookingGuest item, SqlConnection sqlConn)
        {
            try
            {
                List<BookingGuest> resultItem = new List<BookingGuest>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "BookingGuests_select_all",
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

        public static async Task<BookingGuest> BookingGuests_Update_Transaction(BookingGuest item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingGuests_Update(item, sqlConn);
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

        public static async Task<BookingGuest> BookingGuests_Update(BookingGuest item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingGuests_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingGuest> BookingGuests_Update(BookingGuest item, SqlConnection sqlConn)
        {
            try
            {
                BookingGuest resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "BookingGuests_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingGuestID", Value = item.BookingGuestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_GuestID", Value = item.FK_GuestID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_BookingHeaderID", Value = item.FK_BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingGuest>(EntityData_Translator.Translate_BookingGuest);
                        Log.Information("BookingGuest found: BookingGuestID={BookingGuestID}, FK_GuestID={FK_GuestID}, FK_BookingHeaderID={FK_BookingHeaderID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.BookingGuestID, resultItem.FK_GuestID, resultItem.FK_BookingHeaderID, resultItem.DateCreated, resultItem.DateUpdated);
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

        #region BookingHeaders

        public static async Task<BookingHeader> BookingHeaders_Select_Single_Transaction(BookingHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingHeaders_Select_Single(item, sqlConn);
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

        public static async Task<BookingHeader> BookingHeaders_Select_Single(BookingHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingHeaders_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingHeader> BookingHeaders_Select_Single(BookingHeader item, SqlConnection sqlConn)
        {
            try
            {
                BookingHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "BookingHeaders_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingHeaderID", Value = item.BookingHeaderID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingHeader>(EntityData_Translator.Translate_BookingHeader);
                        Log.Information("BookingHeader found: BookingHeaderID={BookingHeaderID}, PartyName={PartyName}, BookingReference={BookingReference}, TravelStart={TravelStart}, TravelEnd={TravelEnd}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, IsStaffBooking={IsStaffBooking}", resultItem.BookingHeaderID, resultItem.PartyName, resultItem.BookingReference, resultItem.TravelStart, resultItem.TravelEnd, resultItem.DateCreated, resultItem.DateUpdated, resultItem.IsStaffBooking);
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

        public static async Task<BookingHeader> BookingHeaders_Insert_Transaction(BookingHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingHeaders_Insert(item, sqlConn);
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

        public static async Task<BookingHeader> BookingHeaders_Insert(BookingHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingHeaders_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingHeader> BookingHeaders_Insert(BookingHeader item, SqlConnection sqlConn)
        {
            try
            {
                BookingHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "BookingHeaders_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingHeaderID", Value = item.BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PartyName", Value = item.PartyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BookingReference", Value = item.BookingReference }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@TravelStart", Value = item.TravelStart }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@TravelEnd", Value = item.TravelEnd }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsStaffBooking", Value = item.IsStaffBooking }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingHeader>(EntityData_Translator.Translate_BookingHeader);
                        Log.Information("BookingHeader found: BookingHeaderID={BookingHeaderID}, PartyName={PartyName}, BookingReference={BookingReference}, TravelStart={TravelStart}, TravelEnd={TravelEnd}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, IsStaffBooking={IsStaffBooking}", resultItem.BookingHeaderID, resultItem.PartyName, resultItem.BookingReference, resultItem.TravelStart, resultItem.TravelEnd, resultItem.DateCreated, resultItem.DateUpdated, resultItem.IsStaffBooking);
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

        public static async Task<List<BookingHeader>> BookingHeaders_Select_All_Transaction(BookingHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingHeaders_Select_All(item, sqlConn);
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

        public static async Task<List<BookingHeader>> BookingHeaders_Select_All(BookingHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingHeaders_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<BookingHeader>> BookingHeaders_Select_All(BookingHeader item, SqlConnection sqlConn)
        {
            try
            {
                List<BookingHeader> resultItem = new List<BookingHeader>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "BookingHeaders_select_all",
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

        public static async Task<BookingHeader> BookingHeaders_Update_Transaction(BookingHeader item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingHeaders_Update(item, sqlConn);
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

        public static async Task<BookingHeader> BookingHeaders_Update(BookingHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await BookingHeaders_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<BookingHeader> BookingHeaders_Update(BookingHeader item, SqlConnection sqlConn)
        {
            try
            {
                BookingHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "BookingHeaders_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@BookingHeaderID", Value = item.BookingHeaderID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PartyName", Value = item.PartyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BookingReference", Value = item.BookingReference }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@TravelStart", Value = item.TravelStart }
                        , new SqlParameter() { DbType = DbType.Date, Direction = ParameterDirection.Input, ParameterName = "@TravelEnd", Value = item.TravelEnd }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsStaffBooking", Value = item.IsStaffBooking }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<BookingHeader>(EntityData_Translator.Translate_BookingHeader);
                        Log.Information("BookingHeader found: BookingHeaderID={BookingHeaderID}, PartyName={PartyName}, BookingReference={BookingReference}, TravelStart={TravelStart}, TravelEnd={TravelEnd}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, IsStaffBooking={IsStaffBooking}", resultItem.BookingHeaderID, resultItem.PartyName, resultItem.BookingReference, resultItem.TravelStart, resultItem.TravelEnd, resultItem.DateCreated, resultItem.DateUpdated, resultItem.IsStaffBooking);
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

    }
}
