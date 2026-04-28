using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.Menu.POS_DebtorMenuItemProducts;
using POS_Common.Models.Menu.POS_DebtorMenuItems;
using POS_Common.Models.Menu.POS_DebtorMenus;
using POS_Common.Models.Menu.POS_MenuItemProducts;
using POS_Common.Models.Menu.POS_MenuItems;
using POS_Common.Models.Menu.POS_Menus;
using POS_Common.Models.Stock.POS_PurchaseOrders;
using POS_Common.ModelsDto.MenuController.DebtorMenuPrinter;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.Menu
{
    public class Menu_Custom_Service : Menu_Custom_SP_Service
    {
        #region Methods

        #region Menu

        public static async Task<List<DebtorMenu>> Menus_Select_All(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Menus_Select_All(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorMenu>> Menus_Select_All(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorMenu> resultItem = new List<DebtorMenu>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorMenus_select_all_debtorMenus",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenu_DebtorMenu));
                        Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_Menu> Menu_Select_Single_Name(_Menu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Menu_Select_Single_Name(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_Menu> Menu_Select_Single_Name(_Menu item, SqlConnection sqlConn)
        {
            try
            {
                _Menu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menu_select_single_name",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MenuName", Value = item.MenuName }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<_Menu>(Menu_Translator.Translate__Menu);
                        Log.Information("POS_Menu found: POS_MenuID={POS_MenuID}, MenuName={MenuName}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.MenuID, resultItem.MenuName, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<_Menu>> MenuTree_Select(_Menu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await MenuTree_Select(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<_Menu>> MenuTree_Select(_Menu item, SqlConnection sqlConn)
        {
            try
            {
                List<_Menu> resultItem = new List<_Menu>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menuTree_select",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@MenuID", Value = item.MenuID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<_Menu>(Menu_Translator.Translate_MenuTree));
                        Log.Information("Menu Tree records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu Tree records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<_Menu>> MenuTree_Select_All(_Menu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await MenuTree_Select_All(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<_Menu>> MenuTree_Select_All(_Menu item, SqlConnection sqlConn)
        {
            try
            {
                List<_Menu> resultItem = new List<_Menu>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menuTree_select_all"))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<_Menu>(Menu_Translator.Translate_MenuTree));
                        Log.Information("Menu Tree records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu Tree records found.");
                        return default;
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

        #region Menu Item

        public static async Task<List<MenuItem>> MenuItems_Select_All_MenuItems(MenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await MenuItems_Select_All_MenuItems(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<MenuItem>> MenuItems_Select_All_MenuItems(MenuItem item, SqlConnection sqlConn)
        {
            try
            {
                List<MenuItem> resultItem = new List<MenuItem>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menuItems_select_all_menuItems",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<MenuItem>(Menu_Translator.Translate_MenuItem_MenuItem));
                        Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItem> MenuItem_Select_Single_Name(MenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await MenuItem_Select_Single_Name(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItem> MenuItem_Select_Single_Name(MenuItem item, SqlConnection sqlConn)
        {
            try
            {
                MenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menuItem_select_single_name",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Item", Value = item.Item }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItem>(Menu_Translator.Translate_MenuItem);
                        Log.Information("POS_MenuItem found: POS_MenuItemID={POS_MenuItemID}, FK_MenuID={FK_MenuID}, Item={Item}, Description={Description}, FK_POS_MenuItemID={FK_POS_MenuItemID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemID, resultItem.FK_MenuID, resultItem.Item, resultItem.Description, resultItem.FK_MenuItemID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu Item found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItem> MenuItem_Remove(MenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await MenuItem_Remove(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItem> MenuItem_Remove(MenuItem item, SqlConnection sqlConn)
        {
            try
            {
                MenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menuItem_remove",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemID", Value = item.MenuItemID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItem>(Menu_Translator.Translate_MenuItem);
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

        public static async Task<DebtorMenuItem> DebtorMenuItem_Remove(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenuItem_Remove(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItem> DebtorMenuItem_Remove(DebtorMenuItem item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorMenuItem_remove",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemID", Value = item.DebtorMenuItemID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItem>(Menu_Translator.Translate_DebtorMenuItem);
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

        #region Menu Item Product

        public static async Task<List<MenuItemProduct>> MenuItemProducts_Select_All_MenuItemProducts(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await MenuItemProducts_Select_All_MenuItemProducts(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<MenuItemProduct>> MenuItemProducts_Select_All_MenuItemProducts(MenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<MenuItemProduct> resultItem = new List<MenuItemProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menuItemProducts_select_all_menuItemProducts",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<MenuItemProduct>(Menu_Translator.Translate_MenuItemProduct_MenuItemProduct));
                        Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Purchase Order records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItemProduct> MenuItemProduct_Select_Single_ID(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await MenuItemProduct_Select_Single_ID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItemProduct> MenuItemProduct_Select_Single_ID(MenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                MenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menuItemProduct_select_single_id",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItemProduct>(Menu_Translator.Translate_MenuItemProduct);
                        Log.Information("POS_MenuItemProduct found: POS_MenuItemProductID={POS_MenuItemProductID}, FK_MenuItemID={FK_MenuItemID}, FK_ProductID={FK_ProductID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_MenuItemID, resultItem.FK_ProductID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu Item found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItemProduct> MenuItemProducts_Remove(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await MenuItemProducts_Remove(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<MenuItemProduct> MenuItemProducts_Remove(MenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                MenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "menuItemProduct_remove",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemProductID", Value = item.MenuItemProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItemProduct>(Menu_Translator.Translate_MenuItemProduct);
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

        #region Debtor Menu

        public static async Task<List<DebtorMenu>> DebtorMenuTree_Select(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenuTree_Select(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<DebtorMenu>> DebtorMenuTree_Select(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                List<DebtorMenu> resultItem = new List<DebtorMenu>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorMenuTree_select_all",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuID", Value = item.DebtorMenuID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenuTree));
                        Log.Information("Menu Tree records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu Tree records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_Menu> Menu_Copy_To_Debtor(_Menu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Menu_Copy_To_Debtor(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<_Menu> Menu_Copy_To_Debtor(_Menu item, SqlConnection sqlConn)
        {
            try
            {
                _Menu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "CopyMenuToDebtor",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@SourceMenuID", Value = item.MenuID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TargetDebtorID", Value = item.DebtorID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@TargetCostCenterID", Value = item.CostCenterID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@Override", Value = item.Override }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@UserID", Value = item.UserID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<_Menu>(Menu_Translator.Translate_POS_Menu_Copy);
                        Log.Information("Menu Copied");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Menu Copy failed to create.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenu> DebtorMenus_Insert_Custom(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenus_Insert_Custom(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenu> DebtorMenus_Insert_Custom(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "DebtorMenus_insert_custom",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuID", Value = item.DebtorMenuID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@MenuName", Value = item.MenuName }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidFrom", Value = item.ValidFrom }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@ValidTo", Value = item.ValidTo }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenu);
                        Log.Information("DebtorMenu found: DebtorMenuID={DebtorMenuID}, FK_DebtorID={FK_DebtorID}, FK_CostCenterID={FK_CostCenterID}, FK_MenuID={FK_MenuID}, MenuName={MenuName}, ValidFrom={ValidFrom}, ValidTo={ValidTo}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.DebtorMenuID, resultItem.FK_LocationID, resultItem.FK_CostCenterID, resultItem.FK_MenuID, resultItem.MenuName, resultItem.ValidFrom, resultItem.ValidTo, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);

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

        public static async Task<List<MenuItem>> POS_MenuItems_Select_All_MenuID(MenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItems_Select_All_MenuID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<MenuItem>> POS_MenuItems_Select_All_MenuID(MenuItem item, SqlConnection sqlConn)
        {
            try
            {
                List<MenuItem> resultItem = new List<MenuItem>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorMenuItems_select_all_MenuID",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_MenuID", Value = item.FK_MenuID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<MenuItem>(Menu_Translator.Translate_MenuItem));
                        Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItem> DebtorMenuItems_Insert_Custom(DebtorMenuItem item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenuItems_Insert_Custom(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItem> DebtorMenuItems_Insert_Custom(DebtorMenuItem item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "DebtorMenuItems_insert_custom",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuItemID", Value = item.DebtorMenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorMenuID", Value = item.FK_DebtorMenuID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Item", Value = item.Item }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItem>(Menu_Translator.Translate_DebtorMenuItem);
                        Log.Information("DebtorMenuItem found: DebtorMenuItemID={DebtorMenuItemID}, FK_DebtorMenuID={FK_DebtorMenuID}, Item={Item}, Description={Description}, FK_MenuItemID={FK_MenuItemID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.DebtorMenuItemID, resultItem.FK_DebtorMenuID, resultItem.Item, resultItem.Description, resultItem.FK_MenuItemID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);

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

        public static async Task<List<MenuItemProduct>> POS_MenuItemProducts_Select_All_ID(MenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_MenuItemProducts_Select_All_ID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<MenuItemProduct>> POS_MenuItemProducts_Select_All_ID(MenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<MenuItemProduct> resultItem = new List<MenuItemProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorMenuItemProducts_select_all_MenuItemID",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_MenuItemID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<MenuItemProduct>(Menu_Translator.Translate_MenuItemProduct));
                        Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> DebtorMenuItemProducts_Insert_Custom(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenuItemProducts_Insert_Custom(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> DebtorMenuItemProducts_Insert_Custom(DebtorMenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "DebtorMenuItemProducts_insert_custom",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuItemProductID", Value = item.MenuItemProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorMenuItemID", Value = item.FK_DebtorMenuItemID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        //, new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_DebtorProductID", Value = item.FK_DebtorProductID }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CreatedUserID", Value = item.FK_CreatedUserID }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateUpdated", Value = item.DateUpdated }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_UpdatedUserID", Value = item.FK_UpdatedUserID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProduct>(Menu_Translator.Translate_DebtorMenuItemProduct);
                        Log.Information("DebtorMenuItemProduct found: POS_MenuItemProductID={POS_MenuItemProductID}, FK_DebtorMenuItemID={FK_DebtorMenuItemID}, FK_ProductID={FK_ProductID}, FK_DebtorProductID={FK_DebtorProductID}, IsActive={IsActive}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_DebtorMenuItemID, resultItem.FK_ProductID, resultItem.IsActive, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);

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

        public static async Task<DebtorMenu> DebtorMenus_Update_Status(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenus_Update_Status(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenu> DebtorMenus_Update_Status(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtor_menu_update_status",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuID", Value = item.DebtorMenuID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenu);
                        Log.Information("Menu Updated");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenu> DebtorMenus_Update_Status_CostCenter(DebtorMenu item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenus_Update_Status_CostCenter(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenu> DebtorMenus_Update_Status_CostCenter(DebtorMenu item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenu resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtor_menu_update_status_cost_center",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@DebtorMenuID", Value = item.DebtorMenuID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_CostCenterID", Value = item.FK_CostCenterID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenu>(Menu_Translator.Translate_DebtorMenu);
                        Log.Information("Menu Updated");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> DebtorMenuItemProduct_Select_Single_ID(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenuItemProduct_Select_Single_ID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> DebtorMenuItemProduct_Select_Single_ID(DebtorMenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorMenuItemProduct_select_single_id",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_MenuItemID", Value = item.FK_DebtorMenuItemID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProduct>(Menu_Translator.Translate_DebtorMenuItemProduct);
                        Log.Information("POS_MenuItemProduct found: POS_MenuItemProductID={POS_MenuItemProductID}, FK_MenuItemID={FK_MenuItemID}, FK_ProductID={FK_ProductID}, DateCreated={DateCreated}, FK_CreatedUserID={FK_CreatedUserID}, DateUpdated={DateUpdated}, FK_UpdatedUserID={FK_UpdatedUserID}", resultItem.MenuItemProductID, resultItem.FK_ProductID, resultItem.DateCreated, resultItem.FK_CreatedUserID, resultItem.DateUpdated, resultItem.FK_UpdatedUserID);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Menu Item found with the given DebtorID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> DebtorMenuItemProducts_Remove(DebtorMenuItemProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenuItemProducts_Remove(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorMenuItemProduct> DebtorMenuItemProducts_Remove(DebtorMenuItemProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorMenuItemProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "debtorMenuItemProduct_remove",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MenuItemProductID", Value = item.MenuItemProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorMenuItemProduct>(Menu_Translator.Translate_DebtorMenuItemProduct);
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

        #region Debtor Menu Printer 

        public static async Task<List<Res_DebtorMenuPrinter_List>> DebtorMenuPrinters_Select_All_ID(Req_DebtorMenuPrinter_List item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DebtorMenuPrinters_Select_All_ID(item, sqlConn);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Res_DebtorMenuPrinter_List>> DebtorMenuPrinters_Select_All_ID(Req_DebtorMenuPrinter_List item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_DebtorMenuPrinter_List> resultItem = new List<Res_DebtorMenuPrinter_List>();
                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "POS_DebtorMenuPrinters_select_debtor_menu",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_DebtorMenuID", Value = item.DebtorMenuID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_DebtorMenuPrinter_List>(Menu_Translator.Translate_DebtorMenuPrinter_List));
                        Log.Information("Debtor Menu Printer records found: ", resultItem.Count.ToString());
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Debtor Menu Printer records found.");
                        return default;
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
