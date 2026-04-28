using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Menu.POS_DebtorMenuItemProducts;
using POS_Common.Models.Menu.POS_DebtorMenuItems;
using POS_Common.Models.Menu.POS_DebtorMenus;
using POS_Common.Models.Menu.POS_MenuItemProducts;
using POS_Common.Models.Menu.POS_MenuItems;
using POS_Common.Models.Menu.POS_Menus;
using POS_Common.Models.Stock.POS_PurchaseOrderLines;
using POS_Common.Models.Stock.POS_PurchaseOrders;
using POS_Common.Models.Stock.POS_StockRequestLines;
using POS_Common.Models.Stock.POS_StockRequests;
using POS_Common.ModelsDto.MenuController.DebtorMenuPrinter;

namespace POS_Api.Translators
{
   public class Menu_Translator : Menu_Custom_SP_Translator
   {
        #region Translators

        internal static _Menu Translate_MenuTree(IDataRecord row)
        {
            return new _Menu()
            {
                MenuID = (int?)row["MenuID"],
                MenuName = GetNullableString(row, "MenuName"),
                ItemID = row["ItemID"].GetType() != typeof(DBNull) ? (int?)row["ItemID"] : null,
                Item = GetNullableString(row, "Item"),
                ParentItemID = row["ParentItemID"].GetType() != typeof(DBNull) ? (int?)row["ParentItemID"] : null,
                ParentItem = GetNullableString(row, "ParentItem"),
                MenuItemProductID = row["MenuItemProductID"].GetType() != typeof(DBNull) ? (int?)row["MenuItemProductID"] : null,
                ProductID = row["ProductID"].GetType() != typeof(DBNull) ? (int?)row["ProductID"] : null,
                Product = GetNullableString(row, "Product"),
            };
        }

        internal static _Menu Translate_POS_Menu_Copy(IDataRecord row)
        {
            return new _Menu()
            {
                DebtorMenuID = (int?)row["DebtorMenuID"],
                FK_CostCenterID = row["FK_CostCenterID"].GetType() != typeof(DBNull) ? (int?)row["FK_CostCenterID"] : null,
                FK_MenuID = row["FK_MenuID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuID"] : null,
                MenuName = (string)row["MenuName"],
                ValidFrom = row["ValidFrom"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidFrom"] : null,
                ValidTo = row["ValidTo"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidTo"] : null,
                IsActive = (bool?)row["IsActive"],
                DateCreated = (DateTime?)row["DateCreated"],
                DateUpdated = (DateTime?)row["DateUpdated"],
                FK_LocationID = row["FK_LocationID"].GetType() != typeof(DBNull) ? (int?)row["FK_LocationID"] : null,
            };
        }

        internal static MenuItem Translate_MenuItem_MenuItem(IDataRecord row)
        {
            return new MenuItem()
            {
                MenuItemID = (int?)row["MenuItemID"],
                FK_MenuID = (int?)row["FK_MenuID"],
                MenuName = GetNullableString(row, "MenuName"),
                Item = GetNullableString(row, "Item"),
                Description = GetNullableString(row, "Description"),
                FK_MenuItemID = row["FK_MenuItemID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuItemID"] : null,
                ParentItem = GetNullableString(row, "ParentItem"),
            };
        }

        internal static MenuItemProduct Translate_MenuItemProduct_MenuItemProduct(IDataRecord row)
        {
            return new MenuItemProduct()
            {
                MenuItemProductID = (int?)row["POS_MenuItemProductID"],
                FK_MenuItemID = (int?)row["FK_MenuItemID"],
                Item = GetNullableString(row, "Item"),
                FK_ProductID = (int?)row["FK_ProductID"],
                ProductName = GetNullableString(row, "ProductName"),
            };
        }

        internal static DebtorMenu Translate_DebtorMenu_DebtorMenu(IDataRecord row)
        {
            return new DebtorMenu()
            {
                MenuID = (int?)row["MenuID"],
                MenuName = GetNullableString(row, "MenuName"),
                ValidFrom = GetNullableDate(row, "ValidFrom"),
                ValidTo = GetNullableDate(row, "ValidTo"),
                DateCreated = GetNullableDate(row, "DateCreated"),
                DateUpdated = GetNullableDate(row, "DateUpdated"),
                SourceType = GetNullableString(row, "SourceType"),
                Location = GetNullableString(row, "Location"), 
                IsActive = GetNullableBool(row, "IsActive"),
                ImageUrl = GetNullableString(row, "ImageUrl"),
            };
        }

        internal static DebtorMenu Translate_DebtorMenuTree(IDataRecord row)
        {
            return new DebtorMenu()
            {
                MenuID = (int?)row["MenuID"],
                MenuName = GetNullableString(row, "MenuName"),
                ItemID = row["ItemID"].GetType() != typeof(DBNull) ? (int?)row["ItemID"] : null,
                Item = GetNullableString(row, "Item"),
                ParentItemID = row["ParentItemID"].GetType() != typeof(DBNull) ? (int?)row["ParentItemID"] : null,
                ParentItem = GetNullableString(row, "ParentItem"),
                MenuItemProductID = row["MenuItemProductID"].GetType() != typeof(DBNull) ? (int?)row["MenuItemProductID"] : null,
                ProductID = row["ProductID"].GetType() != typeof(DBNull) ? (int?)row["ProductID"] : null,
                Product = GetNullableString(row, "Product"),
                SourceType = GetNullableString(row, "SourceType"),
                ValidFrom = GetNullableDate(row, "ValidFrom"),
                ValidTo = GetNullableDate(row, "ValidTo"),
            };
        }
        #endregion

        private static string? GetNullableString(IDataRecord record, string columnName)
        {
            return HasColumn(record, columnName) && record[columnName] != DBNull.Value
                ? (string)record[columnName]
                : null;
        }

        private static bool? GetNullableBool(IDataRecord record, string columnName)
        {
            return HasColumn(record, columnName) && record[columnName] != DBNull.Value
                ? (bool?)record[columnName]
                : null;
        }

        private static DateTime? GetNullableDate(IDataRecord record, string columnName)
        {
            return HasColumn(record, columnName) && record[columnName] != DBNull.Value
                ? (DateTime?)record[columnName]
                : null;
        }

        private static bool HasColumn(IDataRecord record, string columnName)
        {
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (record.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        internal static Res_DebtorMenuPrinter_List Translate_DebtorMenuPrinter_List(IDataRecord record)
        {
            return new Res_DebtorMenuPrinter_List()
            {
                DebtorMenuPrinterID = record["DebtorMenuPrinterID"].GetType() != typeof(DBNull) ? (int?)record["DebtorMenuPrinterID"] : null,
                FK_DebtorMenuID = record["FK_DebtorMenuID"].GetType() != typeof(DBNull) ? (int?)record["FK_DebtorMenuID"] : null,
                FK_PrinterID = record["FK_PrinterID"].GetType() != typeof(DBNull) ? (int?)record["FK_PrinterID"] : null,
                PrinterName = GetNullableString(record, "PrinterName"),
                FK_OrderSlipTypeID = record["FK_OrderSlipTypeID"].GetType() != typeof(DBNull) ? (int?)record["FK_OrderSlipTypeID"] : null,
                SlipCode = GetNullableString(record, "SlipCode"),
                SlipDescription = GetNullableString(record, "SlipDescription"),
            };
        }
    }
}






