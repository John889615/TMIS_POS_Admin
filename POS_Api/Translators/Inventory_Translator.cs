using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Inventory.POS_ServedAsProducts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace POS_Api.Translators
{
   public class Inventory_Translator : Inventory_Custom_SP_Translator
   {
        #region Translators

        internal static Product Translate_Product_Product(IDataRecord row)
        {
            return new Product()
            {
                ProductID = (int?)row["ProductID"],
                ProductName = (string)row["ProductName"],
                Description = GetNullableString(row, "Description"),
                FK_ProductTypeID = (int?)row["FK_ProductTypeID"],
                ProductType = GetNullableString(row, "ProductType"),
                IsInventory = GetNullableBool(row, "IsInventory"),
                IsManufactured = GetNullableBool(row, "IsManufactured"),
                IsService = GetNullableBool(row, "IsService"),
                IsComposite = GetNullableBool(row, "IsComposite"),
                IsStockTracked = GetNullableBool(row, "IsStockTracked"),
                FK_UnitID = (int?)row["FK_UnitID"],
                Unit = GetNullableString(row, "Unit"),
                Symbol = GetNullableString(row, "Symbol"),
                FK_ProductCategoryID = (int?)row["FK_ProductCategoryID"],
                ProductCategory = GetNullableString(row, "CategoryName"),
                FK_DefaultUnitID = (int?)row["FK_DefaultUnitID"],
                DefaultUnit = GetNullableString(row, "DefaultUnit"),
                DefaultSymbol = GetNullableString(row, "DefaultSymbol"),
                SKU = GetNullableString(row, "SKU"),
                Barcode = GetNullableString(row, "Barcode"),
                QrCode = GetNullableString(row, "QrCode"),
                ImageUrl = GetNullableString(row, "ImageUrl"),
            };
        }

        internal static ServedAsProduct Translate_ServedAsProduct_Product(IDataRecord row)
        {
            return new ServedAsProduct()
            {
                ServedAsProductID = (int?)row["ServedAsProductID"],
                FK_ProductID = (int?)row["FK_ProductID"],
                FK_ServedAsID = (int?)row["FK_ServedAsID"],
                IsQuantified = (bool?)row["IsQuantified"],
                Quantity = (decimal?)row["Quantity"],
                FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
                FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
                DateCreated = (DateTime?)row["DateCreated"],
                DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
                IsDefault = GetNullableBool(row, "IsDefault"),

                ServedAsType = GetNullableString(row, "ServedAsType"),
                Name = GetNullableString(row, "Name")
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
    }
}





