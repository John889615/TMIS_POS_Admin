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

using POS_Common.Models.Menu.POS_DebtorMenuItemProducts;
using POS_Common.Models.Menu.POS_DebtorMenuItems;
using POS_Common.Models.Menu.POS_DebtorMenus;
using POS_Common.Models.Menu.POS_MenuItemProducts;
using POS_Common.Models.Menu.POS_MenuItems;
using POS_Common.Models.Menu.POS_Menus;
using POS_Common.Models.Menu.POS_DebtorMenuPrinters;
using POS_Common.Models.Menu.POS_DebtorMenuItemProductPrinters;

namespace POS_Api.Services.Menu
{
    public abstract class Menu_Base_Service
    {
        #region POS_DebtorMenuItemProducts

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Select_Single_Transaction(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProducts_Select_Single(item, sqlConn);
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

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Select_Single(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProducts_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Select_Single(DebtorMenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItemProducts_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemProductID", Value = item.MenuItemProductID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProduct>(Menu_Translator.Translate_DebtorMenuItemProduct);
                        Log.Information("DebtorMenuItemProduct found: MenuItemProductID={MenuItemProductID}, FK_DebtorMenuItemID={FK_DebtorMenuItemID}, FK_ProductID={FK_ProductID}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_DebtorMenuItemID, resultItem.FK_ProductID, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenuItemProduct found with the given DebtorMenuItemProductID.");
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

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Insert_Transaction(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProducts_Insert(item, sqlConn);
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

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Insert(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProducts_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Insert(DebtorMenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItemProducts_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorMenuItemID", Value = item.FK_DebtorMenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProduct>(Menu_Translator.Translate_DebtorMenuItemProduct);
                        Log.Information("DebtorMenuItemProduct found: MenuItemProductID={MenuItemProductID}, FK_DebtorMenuItemID={FK_DebtorMenuItemID}, FK_ProductID={FK_ProductID}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_DebtorMenuItemID, resultItem.FK_ProductID, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenuItemProduct failed to create.");
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

        public static async Task<List<DebtorMenuItemProduct>> POS_DebtorMenuItemProducts_Select_All_Transaction(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProducts_Select_All(item, sqlConn);
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

        public static async Task<List<DebtorMenuItemProduct>> POS_DebtorMenuItemProducts_Select_All(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProducts_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorMenuItemProduct>> POS_DebtorMenuItemProducts_Select_All(DebtorMenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorMenuItemProduct> resultItem = new List<DebtorMenuItemProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItemProducts_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorMenuItemProduct>(Menu_Translator.Translate_DebtorMenuItemProduct));
                        Log.Information("DebtorMenuItemProduct records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenuItemProduct records found.");
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

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Update_Transaction(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProducts_Update(item, sqlConn);
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

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Update(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProducts_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> POS_DebtorMenuItemProducts_Update(DebtorMenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItemProducts_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemProductID", Value = item.MenuItemProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorMenuItemID", Value = item.FK_DebtorMenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProduct>(Menu_Translator.Translate_DebtorMenuItemProduct);
                        Log.Information("DebtorMenuItemProduct found: MenuItemProductID={MenuItemProductID}, FK_DebtorMenuItemID={FK_DebtorMenuItemID}, FK_ProductID={FK_ProductID}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_DebtorMenuItemID, resultItem.FK_ProductID, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenuItemProduct failed to update.");
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

        #region POS_DebtorMenuItems

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Select_Single_Transaction(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItems_Select_Single(item, sqlConn);
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

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Select_Single(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItems_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Select_Single(DebtorMenuItem item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItems_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuItemID", Value = item.DebtorMenuItemID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItem>(Menu_Translator.Translate_DebtorMenuItem);
                        Log.Information("DebtorMenuItem found: DebtorMenuItemID={DebtorMenuItemID}, FK_DebtorMenuID={FK_DebtorMenuID}, Item={Item}, Description={Description}, FK_MenuItemID={FK_MenuItemID}, FK_ReferenceInsertID={FK_ReferenceInsertID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.DebtorMenuItemID, resultItem.FK_DebtorMenuID, resultItem.Item, resultItem.Description, resultItem.FK_MenuItemID, resultItem.FK_ReferenceInsertID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenuItem found with the given DebtorMenuItemID.");
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

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Insert_Transaction(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItems_Insert(item, sqlConn);
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

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Insert(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItems_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Insert(DebtorMenuItem item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItems_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorMenuID", Value = item.FK_DebtorMenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Item", Value = item.Item }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ReferenceInsertID", Value = item.FK_ReferenceInsertID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItem>(Menu_Translator.Translate_DebtorMenuItem);
                        Log.Information("DebtorMenuItem found: DebtorMenuItemID={DebtorMenuItemID}, FK_DebtorMenuID={FK_DebtorMenuID}, Item={Item}, Description={Description}, FK_MenuItemID={FK_MenuItemID}, FK_ReferenceInsertID={FK_ReferenceInsertID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.DebtorMenuItemID, resultItem.FK_DebtorMenuID, resultItem.Item, resultItem.Description, resultItem.FK_MenuItemID, resultItem.FK_ReferenceInsertID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenuItem failed to create.");
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

        public static async Task<List<DebtorMenuItem>> POS_DebtorMenuItems_Select_All_Transaction(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItems_Select_All(item, sqlConn);
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

        public static async Task<List<DebtorMenuItem>> POS_DebtorMenuItems_Select_All(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItems_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorMenuItem>> POS_DebtorMenuItems_Select_All(DebtorMenuItem item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorMenuItem> resultItem = new List<DebtorMenuItem>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItems_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorMenuItem>(Menu_Translator.Translate_DebtorMenuItem));
                        Log.Information("DebtorMenuItem records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenuItem records found.");
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

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Update_Transaction(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItems_Update(item, sqlConn);
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

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Update(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItems_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItem> POS_DebtorMenuItems_Update(DebtorMenuItem item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItems_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuItemID", Value = item.DebtorMenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorMenuID", Value = item.FK_DebtorMenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Item", Value = item.Item }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ReferenceInsertID", Value = item.FK_ReferenceInsertID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItem>(Menu_Translator.Translate_DebtorMenuItem);
                        Log.Information("DebtorMenuItem found: DebtorMenuItemID={DebtorMenuItemID}, FK_DebtorMenuID={FK_DebtorMenuID}, Item={Item}, Description={Description}, FK_MenuItemID={FK_MenuItemID}, FK_ReferenceInsertID={FK_ReferenceInsertID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.DebtorMenuItemID, resultItem.FK_DebtorMenuID, resultItem.Item, resultItem.Description, resultItem.FK_MenuItemID, resultItem.FK_ReferenceInsertID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenuItem failed to update.");
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

        #region POS_DebtorMenus

        public static async Task<DebtorMenu> POS_DebtorMenus_Select_Single_Transaction(DebtorMenu item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenus_Select_Single(item, sqlConn);
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

        public static async Task<DebtorMenu> POS_DebtorMenus_Select_Single(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenus_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenu> POS_DebtorMenus_Select_Single(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenus_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuID", Value = item.DebtorMenuID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenu);
                        Log.Information("DebtorMenu found: DebtorMenuID={DebtorMenuID}, FK_LocationID={FK_LocationID}, FK_CostCenterID={FK_CostCenterID}, FK_MenuID={FK_MenuID}, MenuName={MenuName}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorMenuID, resultItem.FK_LocationID, resultItem.FK_CostCenterID, resultItem.FK_MenuID, resultItem.MenuName, resultItem.ValidFrom, resultItem.ValidTo, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenu found with the given DebtorMenuID.");
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

        public static async Task<DebtorMenu> POS_DebtorMenus_Insert_Transaction(DebtorMenu item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenus_Insert(item, sqlConn);
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

        public static async Task<DebtorMenu> POS_DebtorMenus_Insert(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenus_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenu> POS_DebtorMenus_Insert(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenus_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MenuName", Value = item.MenuName }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenu);
                        Log.Information("DebtorMenu found: DebtorMenuID={DebtorMenuID}, FK_LocationID={FK_LocationID}, FK_CostCenterID={FK_CostCenterID}, FK_MenuID={FK_MenuID}, MenuName={MenuName}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorMenuID, resultItem.FK_LocationID, resultItem.FK_CostCenterID, resultItem.FK_MenuID, resultItem.MenuName, resultItem.ValidFrom, resultItem.ValidTo, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenu failed to create.");
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

        public static async Task<List<DebtorMenu>> POS_DebtorMenus_Select_All_Transaction(DebtorMenu item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenus_Select_All(item, sqlConn);
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

        public static async Task<List<DebtorMenu>> POS_DebtorMenus_Select_All(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenus_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorMenu>> POS_DebtorMenus_Select_All(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorMenu> resultItem = new List<DebtorMenu>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenus_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenu));
                        Log.Information("DebtorMenu records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenu records found.");
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

        public static async Task<DebtorMenu> POS_DebtorMenus_Update_Transaction(DebtorMenu item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenus_Update(item, sqlConn);
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

        public static async Task<DebtorMenu> POS_DebtorMenus_Update(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenus_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenu> POS_DebtorMenus_Update(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenus_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuID", Value = item.DebtorMenuID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MenuName", Value = item.MenuName }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenu);
                        Log.Information("DebtorMenu found: DebtorMenuID={DebtorMenuID}, FK_LocationID={FK_LocationID}, FK_CostCenterID={FK_CostCenterID}, FK_MenuID={FK_MenuID}, MenuName={MenuName}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorMenuID, resultItem.FK_LocationID, resultItem.FK_CostCenterID, resultItem.FK_MenuID, resultItem.MenuName, resultItem.ValidFrom, resultItem.ValidTo, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenu failed to update.");
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

        #region POS_MenuItemProducts

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Select_Single_Transaction(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Select_Single(item, sqlConn);
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

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Select_Single(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Select_Single(MenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                MenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_MenuItemProducts_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemProductID", Value = item.MenuItemProductID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItemProduct>(Menu_Translator.Translate_MenuItemProduct);
                        Log.Information("MenuItemProduct found: MenuItemProductID={MenuItemProductID}, FK_MenuItemID={FK_MenuItemID}, FK_ProductID={FK_ProductID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_MenuItemID, resultItem.FK_ProductID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No MenuItemProduct found with the given MenuItemProductID.");
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

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Insert_Transaction(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Insert(item, sqlConn);
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

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Insert(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Insert(MenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                MenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_MenuItemProducts_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItemProduct>(Menu_Translator.Translate_MenuItemProduct);
                        Log.Information("MenuItemProduct found: MenuItemProductID={MenuItemProductID}, FK_MenuItemID={FK_MenuItemID}, FK_ProductID={FK_ProductID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_MenuItemID, resultItem.FK_ProductID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("MenuItemProduct failed to create.");
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

        public static async Task<List<MenuItemProduct>> POS_MenuItemProducts_Select_All_Transaction(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Select_All(item, sqlConn);
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

        public static async Task<List<MenuItemProduct>> POS_MenuItemProducts_Select_All(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<MenuItemProduct>> POS_MenuItemProducts_Select_All(MenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<MenuItemProduct> resultItem = new List<MenuItemProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_MenuItemProducts_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<MenuItemProduct>(Menu_Translator.Translate_MenuItemProduct));
                        Log.Information("MenuItemProduct records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No MenuItemProduct records found.");
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

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Update_Transaction(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Update(item, sqlConn);
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

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Update(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItemProduct> POS_MenuItemProducts_Update(MenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                MenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_MenuItemProducts_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemProductID", Value = item.MenuItemProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItemProduct>(Menu_Translator.Translate_MenuItemProduct);
                        Log.Information("MenuItemProduct found: MenuItemProductID={MenuItemProductID}, FK_MenuItemID={FK_MenuItemID}, FK_ProductID={FK_ProductID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_MenuItemID, resultItem.FK_ProductID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("MenuItemProduct failed to update.");
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

        #region POS_MenuItems

        public static async Task<MenuItem> POS_MenuItems_Select_Single_Transaction(MenuItem item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Select_Single(item, sqlConn);
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

        public static async Task<MenuItem> POS_MenuItems_Select_Single(MenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItem> POS_MenuItems_Select_Single(MenuItem item, SqlConnection sqlConn)
        {
            try
            {
                MenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_MenuItems_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemID", Value = item.MenuItemID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItem>(Menu_Translator.Translate_MenuItem);
                        Log.Information("MenuItem found: MenuItemID={MenuItemID}, FK_MenuID={FK_MenuID}, Item={Item}, Description={Description}, FK_MenuItemID={FK_MenuItemID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemID, resultItem.FK_MenuID, resultItem.Item, resultItem.Description, resultItem.FK_MenuItemID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No MenuItem found with the given MenuItemID.");
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

        public static async Task<MenuItem> POS_MenuItems_Insert_Transaction(MenuItem item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Insert(item, sqlConn);
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

        public static async Task<MenuItem> POS_MenuItems_Insert(MenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItem> POS_MenuItems_Insert(MenuItem item, SqlConnection sqlConn)
        {
            try
            {
                MenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_MenuItems_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Item", Value = item.Item }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItem>(Menu_Translator.Translate_MenuItem);
                        Log.Information("MenuItem found: MenuItemID={MenuItemID}, FK_MenuID={FK_MenuID}, Item={Item}, Description={Description}, FK_MenuItemID={FK_MenuItemID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemID, resultItem.FK_MenuID, resultItem.Item, resultItem.Description, resultItem.FK_MenuItemID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("MenuItem failed to create.");
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

        public static async Task<List<MenuItem>> POS_MenuItems_Select_All_Transaction(MenuItem item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Select_All(item, sqlConn);
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

        public static async Task<List<MenuItem>> POS_MenuItems_Select_All(MenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<MenuItem>> POS_MenuItems_Select_All(MenuItem item, SqlConnection sqlConn)
        {
            try
            {
                List<MenuItem> resultItem = new List<MenuItem>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_MenuItems_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<MenuItem>(Menu_Translator.Translate_MenuItem));
                        Log.Information("MenuItem records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No MenuItem records found.");
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

        public static async Task<MenuItem> POS_MenuItems_Update_Transaction(MenuItem item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Update(item, sqlConn);
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

        public static async Task<MenuItem> POS_MenuItems_Update(MenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItem> POS_MenuItems_Update(MenuItem item, SqlConnection sqlConn)
        {
            try
            {
                MenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_MenuItems_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemID", Value = item.MenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Item", Value = item.Item }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItem>(Menu_Translator.Translate_MenuItem);
                        Log.Information("MenuItem found: MenuItemID={MenuItemID}, FK_MenuID={FK_MenuID}, Item={Item}, Description={Description}, FK_MenuItemID={FK_MenuItemID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemID, resultItem.FK_MenuID, resultItem.Item, resultItem.Description, resultItem.FK_MenuItemID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("MenuItem failed to update.");
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

        #region POS_Menus

        public static async Task<_Menu> POS_Menus_Select_Single_Transaction(_Menu item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Menus_Select_Single(item, sqlConn);
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

        public static async Task<_Menu> POS_Menus_Select_Single(_Menu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Menus_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_Menu> POS_Menus_Select_Single(_Menu item, SqlConnection sqlConn)
        {
            try
            {
                _Menu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Menus_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuID", Value = item.MenuID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<_Menu>(Menu_Translator.Translate__Menu);
                        Log.Information("_Menu found: MenuID={MenuID}, MenuName={MenuName}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.MenuID, resultItem.MenuName, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No _Menu found with the given _MenuID.");
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

        public static async Task<_Menu> POS_Menus_Insert_Transaction(_Menu item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Menus_Insert(item, sqlConn);
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

        public static async Task<_Menu> POS_Menus_Insert(_Menu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Menus_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_Menu> POS_Menus_Insert(_Menu item, SqlConnection sqlConn)
        {
            try
            {
                _Menu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Menus_insert",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MenuName", Value = item.MenuName }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<_Menu>(Menu_Translator.Translate__Menu);
                        Log.Information("_Menu found: MenuID={MenuID}, MenuName={MenuName}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.MenuID, resultItem.MenuName, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("_Menu failed to create.");
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

        public static async Task<List<_Menu>> POS_Menus_Select_All_Transaction(_Menu item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Menus_Select_All(item, sqlConn);
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

        public static async Task<List<_Menu>> POS_Menus_Select_All(_Menu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Menus_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<_Menu>> POS_Menus_Select_All(_Menu item, SqlConnection sqlConn)
        {
            try
            {
                List<_Menu> resultItem = new List<_Menu>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Menus_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<_Menu>(Menu_Translator.Translate__Menu));
                        Log.Information("_Menu records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No _Menu records found.");
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

        public static async Task<_Menu> POS_Menus_Update_Transaction(_Menu item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Menus_Update(item, sqlConn);
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

        public static async Task<_Menu> POS_Menus_Update(_Menu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_Menus_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_Menu> POS_Menus_Update(_Menu item, SqlConnection sqlConn)
        {
            try
            {
                _Menu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_Menus_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuID", Value = item.MenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MenuName", Value = item.MenuName }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<_Menu>(Menu_Translator.Translate__Menu);
                        Log.Information("_Menu found: MenuID={MenuID}, MenuName={MenuName}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.MenuID, resultItem.MenuName, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("_Menu failed to update.");
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

        #region POS_DebtorMenuPrinters

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Select_Single_Transaction(DebtorMenuPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuPrinters_Select_Single(item, sqlConn);
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

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Select_Single(DebtorMenuPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuPrinters_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Select_Single(DebtorMenuPrinter item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuPrinters_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuPrinterID", Value = item.DebtorMenuPrinterID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuPrinter>(Menu_Translator.Translate_DebtorMenuPrinter);
                        Log.Information("DebtorMenuPrinter found: DebtorMenuPrinterID={DebtorMenuPrinterID}, FK_DebtorMenuID={FK_DebtorMenuID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_OrderSlipTypeID={FK_OrderSlipTypeID}", resultItem.DebtorMenuPrinterID, resultItem.FK_DebtorMenuID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_OrderSlipTypeID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenuPrinter found with the given DebtorMenuPrinterID.");
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

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Insert_Transaction(DebtorMenuPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuPrinters_Insert(item, sqlConn);
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

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Insert(DebtorMenuPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuPrinters_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Insert(DebtorMenuPrinter item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuPrinters_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorMenuID", Value = item.FK_DebtorMenuID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PrinterID", Value = item.FK_PrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderSlipTypeID", Value = item.FK_OrderSlipTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuPrinter>(Menu_Translator.Translate_DebtorMenuPrinter);
                        Log.Information("DebtorMenuPrinter found: DebtorMenuPrinterID={DebtorMenuPrinterID}, FK_DebtorMenuID={FK_DebtorMenuID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_OrderSlipTypeID={FK_OrderSlipTypeID}", resultItem.DebtorMenuPrinterID, resultItem.FK_DebtorMenuID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_OrderSlipTypeID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenuPrinter failed to create.");
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

        public static async Task<List<DebtorMenuPrinter>> POS_DebtorMenuPrinters_Select_All_Transaction(DebtorMenuPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuPrinters_Select_All(item, sqlConn);
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

        public static async Task<List<DebtorMenuPrinter>> POS_DebtorMenuPrinters_Select_All(DebtorMenuPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuPrinters_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorMenuPrinter>> POS_DebtorMenuPrinters_Select_All(DebtorMenuPrinter item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorMenuPrinter> resultItem = new List<DebtorMenuPrinter>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuPrinters_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorMenuPrinter>(Menu_Translator.Translate_DebtorMenuPrinter));
                        Log.Information("DebtorMenuPrinter records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenuPrinter records found.");
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

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Update_Transaction(DebtorMenuPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuPrinters_Update(item, sqlConn);
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

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Update(DebtorMenuPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuPrinters_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuPrinter> POS_DebtorMenuPrinters_Update(DebtorMenuPrinter item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuPrinters_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuPrinterID", Value = item.DebtorMenuPrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorMenuID", Value = item.FK_DebtorMenuID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PrinterID", Value = item.FK_PrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_OrderSlipTypeID", Value = item.FK_OrderSlipTypeID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuPrinter>(Menu_Translator.Translate_DebtorMenuPrinter);
                        Log.Information("DebtorMenuPrinter found: DebtorMenuPrinterID={DebtorMenuPrinterID}, FK_DebtorMenuID={FK_DebtorMenuID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}, FK_OrderSlipTypeID={FK_OrderSlipTypeID}", resultItem.DebtorMenuPrinterID, resultItem.FK_DebtorMenuID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated, resultItem.FK_OrderSlipTypeID);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenuPrinter failed to update.");
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

        #region POS_DebtorMenuItemProductPrinters

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Select_Single_Transaction(DebtorMenuItemProductPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProductPrinters_Select_Single(item, sqlConn);
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

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Select_Single(DebtorMenuItemProductPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProductPrinters_Select_Single(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Select_Single(DebtorMenuItemProductPrinter item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProductPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItemProductPrinters_select_single",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuItemProductPrinterID", Value = item.DebtorMenuItemProductPrinterID }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProductPrinter>(Menu_Translator.Translate_DebtorMenuItemProductPrinter);
                        Log.Information("DebtorMenuItemProductPrinter found: DebtorMenuItemProductPrinterID={DebtorMenuItemProductPrinterID}, FK_MenuItemProductID={FK_MenuItemProductID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorMenuItemProductPrinterID, resultItem.FK_MenuItemProductID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenuItemProductPrinter found with the given DebtorMenuItemProductPrinterID.");
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

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Insert_Transaction(DebtorMenuItemProductPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProductPrinters_Insert(item, sqlConn);
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

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Insert(DebtorMenuItemProductPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProductPrinters_Insert(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Insert(DebtorMenuItemProductPrinter item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProductPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItemProductPrinters_insert",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemProductID", Value = item.FK_MenuItemProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PrinterID", Value = item.FK_PrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProductPrinter>(Menu_Translator.Translate_DebtorMenuItemProductPrinter);
                        Log.Information("DebtorMenuItemProductPrinter found: DebtorMenuItemProductPrinterID={DebtorMenuItemProductPrinterID}, FK_MenuItemProductID={FK_MenuItemProductID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorMenuItemProductPrinterID, resultItem.FK_MenuItemProductID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenuItemProductPrinter failed to create.");
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

        public static async Task<List<DebtorMenuItemProductPrinter>> POS_DebtorMenuItemProductPrinters_Select_All_Transaction(DebtorMenuItemProductPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProductPrinters_Select_All(item, sqlConn);
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

        public static async Task<List<DebtorMenuItemProductPrinter>> POS_DebtorMenuItemProductPrinters_Select_All(DebtorMenuItemProductPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProductPrinters_Select_All(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorMenuItemProductPrinter>> POS_DebtorMenuItemProductPrinters_Select_All(DebtorMenuItemProductPrinter item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorMenuItemProductPrinter> resultItem = new List<DebtorMenuItemProductPrinter>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItemProductPrinters_select_all",
                    null))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorMenuItemProductPrinter>(Menu_Translator.Translate_DebtorMenuItemProductPrinter));
                        Log.Information("DebtorMenuItemProductPrinter records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No DebtorMenuItemProductPrinter records found.");
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

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Update_Transaction(DebtorMenuItemProductPrinter item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProductPrinters_Update(item, sqlConn);
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

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Update(DebtorMenuItemProductPrinter item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_DebtorMenuItemProductPrinters_Update(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProductPrinter> POS_DebtorMenuItemProductPrinters_Update(DebtorMenuItemProductPrinter item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProductPrinter resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DebtorMenuItemProductPrinters_update",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuItemProductPrinterID", Value = item.DebtorMenuItemProductPrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemProductID", Value = item.FK_MenuItemProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_PrinterID", Value = item.FK_PrinterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProductPrinter>(Menu_Translator.Translate_DebtorMenuItemProductPrinter);
                        Log.Information("DebtorMenuItemProductPrinter found: DebtorMenuItemProductPrinterID={DebtorMenuItemProductPrinterID}, FK_MenuItemProductID={FK_MenuItemProductID}, FK_PrinterID={FK_PrinterID}, FK_CreatedUserID={FK_CreatedUserID}, FK_UpdatedUserID={FK_UpdatedUserID}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorMenuItemProductPrinterID, resultItem.FK_MenuItemProductID, resultItem.FK_PrinterID, resultItem.FK_CreatedUserID, resultItem.FK_UpdatedUserID, resultItem.DateCreated, resultItem.DateUpdated);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("DebtorMenuItemProductPrinter failed to update.");
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
