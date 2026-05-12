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
using POS_Common.Models.Menu.POS_DebtorMenuPrinters;
using POS_Common.Models.Menu.POS_DebtorMenuItemProductPrinters;

namespace POS_Api.Translators
{
   public abstract class Menu_Base_Translator
   {
       #region Translators
       
      internal static DebtorMenuItemProduct Translate_DebtorMenuItemProduct(IDataRecord row)
      {
         return new DebtorMenuItemProduct()
         {
            MenuItemProductID = (int?)row["MenuItemProductID"],
            FK_DebtorMenuItemID = row["FK_DebtorMenuItemID"].GetType() != typeof(DBNull) ? (int?)row["FK_DebtorMenuItemID"] : null,
            FK_ProductID = (int?)row["FK_ProductID"],
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
            FK_UpdatedUserID = (int?)row["FK_UpdatedUserID"],
            DisplayOrder = (int?)row["DisplayOrder"],
         };
      }

       
      internal static DebtorMenuItem Translate_DebtorMenuItem(IDataRecord row)
      {
         return new DebtorMenuItem()
         {
            DebtorMenuItemID = (int?)row["DebtorMenuItemID"],
            FK_DebtorMenuID = row["FK_DebtorMenuID"].GetType() != typeof(DBNull) ? (int?)row["FK_DebtorMenuID"] : null,
            Item = (string)row["Item"],
            Description = row["Description"].GetType() != typeof(DBNull) ? (string)row["Description"] : null,
            FK_MenuItemID = row["FK_MenuItemID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuItemID"] : null,
            FK_ReferenceInsertID = row["FK_ReferenceInsertID"].GetType() != typeof(DBNull) ? (int?)row["FK_ReferenceInsertID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
            FK_UpdatedUserID = (int?)row["FK_UpdatedUserID"],
         };
      }

       
      internal static DebtorMenu Translate_DebtorMenu(IDataRecord row)
      {
         return new DebtorMenu()
         {
            DebtorMenuID = (int?)row["DebtorMenuID"],
            FK_LocationID = (int?)row["FK_LocationID"],
            FK_CostCenterID = row["FK_CostCenterID"].GetType() != typeof(DBNull) ? (int?)row["FK_CostCenterID"] : null,
            FK_MenuID = row["FK_MenuID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuID"] : null,
            MenuName = (string)row["MenuName"],
            ValidFrom = row["ValidFrom"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidFrom"] : null,
            ValidTo = row["ValidTo"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidTo"] : null,
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static MenuItemProduct Translate_MenuItemProduct(IDataRecord row)
      {
         return new MenuItemProduct()
         {
            MenuItemProductID = (int?)row["MenuItemProductID"],
            FK_MenuItemID = row["FK_MenuItemID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuItemID"] : null,
            FK_ProductID = (int?)row["FK_ProductID"],
            DateCreated = (DateTime?)row["DateCreated"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
            FK_UpdatedUserID = (int?)row["FK_UpdatedUserID"],
            DisplayOrder = (int?)row["DisplayOrder"],
         };
      }

       
      internal static MenuItem Translate_MenuItem(IDataRecord row)
      {
         return new MenuItem()
         {
            MenuItemID = (int?)row["MenuItemID"],
            FK_MenuID = row["FK_MenuID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuID"] : null,
            Item = (string)row["Item"],
            Description = row["Description"].GetType() != typeof(DBNull) ? (string)row["Description"] : null,
            FK_MenuItemID = row["FK_MenuItemID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuItemID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
            FK_UpdatedUserID = (int?)row["FK_UpdatedUserID"],
         };
      }

       
      internal static _Menu Translate__Menu(IDataRecord row)
      {
         return new _Menu()
         {
            MenuID = (int?)row["MenuID"],
            MenuName = (string)row["MenuName"],
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static DebtorMenuPrinter Translate_DebtorMenuPrinter(IDataRecord row)
      {
         return new DebtorMenuPrinter()
         {
            DebtorMenuPrinterID = (int?)row["DebtorMenuPrinterID"],
            FK_DebtorMenuID = row["FK_DebtorMenuID"].GetType() != typeof(DBNull) ? (int?)row["FK_DebtorMenuID"] : null,
            FK_PrinterID = (int?)row["FK_PrinterID"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
            FK_OrderSlipTypeID = row["FK_OrderSlipTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_OrderSlipTypeID"] : null,
         };
      }

       
      internal static DebtorMenuItemProductPrinter Translate_DebtorMenuItemProductPrinter(IDataRecord row)
      {
         return new DebtorMenuItemProductPrinter()
         {
            DebtorMenuItemProductPrinterID = (int?)row["DebtorMenuItemProductPrinterID"],
            FK_MenuItemProductID = row["FK_MenuItemProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuItemProductID"] : null,
            FK_PrinterID = (int?)row["FK_PrinterID"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       #endregion

       protected static string GetNullableString(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (string)record[columnName]
               : null;
       }

       protected static bool? GetNullableBool(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (bool?)record[columnName]
               : null;
       }

       protected static DateTime? GetNullableDate(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (DateTime?)record[columnName]
               : null;
       }

       protected static bool HasColumn(IDataRecord record, string columnName)
       {
           for (int i = 0; i < record.FieldCount; i++)
           {
               if (record.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                   return true;
           }

           return false;
       }
   }
}
