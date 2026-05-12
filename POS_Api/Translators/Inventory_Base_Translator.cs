using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Inventory.POS_ProductCategories;
using POS_Common.Models.Inventory.POS_Units;
using POS_Common.Models.Inventory.POS_ProductTypes;
using POS_Common.Models.Inventory.POS_ProductCombinations;
using POS_Common.Models.Inventory.POS_ProductExtraCategories;
using POS_Common.Models.Inventory.POS_ProductExtras;
using POS_Common.Models.Inventory.POS_ProductPreparation;
using POS_Common.Models.Inventory.POS_ProductPreparationMethods;
using POS_Common.Models.Inventory.POS_ProductSubstitutions;
using POS_Common.Models.Inventory.POS_ServedAs;
using POS_Common.Models.Inventory.POS_ServedAsProducts;
using POS_Common.Models.Inventory.Custom.SelectProductCombinationsID;
using POS_Common.Models.Inventory.Custom.DeleteProductCombination;

namespace POS_Api.Translators
{
   public abstract class Inventory_Base_Translator
   {
       #region Translators
       
      internal static Product Translate_Product(IDataRecord row)
      {
         return new Product()
         {
            ProductID = (int?)row["ProductID"],
            ProductName = (string)row["ProductName"],
            Description = row["Description"].GetType() != typeof(DBNull) ? (string)row["Description"] : null,
            ItemNo = row["ItemNo"].GetType() != typeof(DBNull) ? (string)row["ItemNo"] : null,
            FK_ProductTypeID = (int?)row["FK_ProductTypeID"],
            IsStockTracked = row["IsStockTracked"].GetType() != typeof(DBNull) ? (bool?)row["IsStockTracked"] : null,
            FK_UnitID = (int?)row["FK_UnitID"],
            FK_ProductCategoryID = row["FK_ProductCategoryID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductCategoryID"] : null,
            FK_DefaultUnitID = (int?)row["FK_DefaultUnitID"],
            BC_ID = row["BC_ID"].GetType() != typeof(DBNull) ? (string)row["BC_ID"] : null,
            SKU = row["SKU"].GetType() != typeof(DBNull) ? (string)row["SKU"] : null,
            Barcode = row["Barcode"].GetType() != typeof(DBNull) ? (string)row["Barcode"] : null,
            QrCode = row["QrCode"].GetType() != typeof(DBNull) ? (string)row["QrCode"] : null,
            IsActive = (bool?)row["IsActive"],
            DateAdded = (DateTime?)row["DateAdded"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ProductCategory Translate_ProductCategory(IDataRecord row)
      {
         return new ProductCategory()
         {
            ProductCategoryID = (int?)row["ProductCategoryID"],
            CategoryName = (string)row["CategoryName"],
            FK_ProductCategoryID = row["FK_ProductCategoryID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductCategoryID"] : null,
            BC_ID = row["BC_ID"].GetType() != typeof(DBNull) ? (string)row["BC_ID"] : null,
            IsMaster = (bool?)row["IsMaster"],
            IsActive = (bool?)row["IsActive"],
            DateAdded = (DateTime?)row["DateAdded"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static Unit Translate_Unit(IDataRecord row)
      {
         return new Unit()
         {
            UnitID = (int?)row["UnitID"],
            Unit = (string)row["Unit"],
            Symbol = row["Symbol"].GetType() != typeof(DBNull) ? (string)row["Symbol"] : null,
            BC_ID = row["BC_ID"].GetType() != typeof(DBNull) ? (string)row["BC_ID"] : null,
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ProductType Translate_ProductType(IDataRecord row)
      {
         return new ProductType()
         {
            ProductTypeID = (int?)row["ProductTypeID"],
            ProductType = (string)row["ProductType"],
            IsInventory = (bool?)row["IsInventory"],
            IsManufactured = (bool?)row["IsManufactured"],
            IsService = (bool?)row["IsService"],
            IsComposite = (bool?)row["IsComposite"],
         };
      }

       
      internal static ProductCombination Translate_ProductCombination(IDataRecord row)
      {
         return new ProductCombination()
         {
            ProductCombinationID = (int?)row["ProductCombinationID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            FK_ProductItemID = (int?)row["FK_ProductItemID"],
            IsQuantified = (bool?)row["IsQuantified"],
            Quantity = (decimal?)row["Quantity"],
            IsOptional = (bool?)row["IsOptional"],
            IsExtraCharge = (bool?)row["IsExtraCharge"],
            DisplayOrder = row["DisplayOrder"].GetType() != typeof(DBNull) ? (int?)row["DisplayOrder"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ProductExtraCategory Translate_ProductExtraCategory(IDataRecord row)
      {
         return new ProductExtraCategory()
         {
            ProductExtraCategoryID = (int?)row["ProductExtraCategoryID"],
            Category = (string)row["Category"],
            DisplayOrder = row["DisplayOrder"].GetType() != typeof(DBNull) ? (int?)row["DisplayOrder"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ProductExtra Translate_ProductExtra(IDataRecord row)
      {
         return new ProductExtra()
         {
            ProductExtraID = (int?)row["ProductExtraID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            FK_ProductExtraCategoryID = (int?)row["FK_ProductExtraCategoryID"],
            FK_ProductExtraID = (int?)row["FK_ProductExtraID"],
            IsQuantified = (bool?)row["IsQuantified"],
            Quantity = (decimal?)row["Quantity"],
            IsExtraCharge = (bool?)row["IsExtraCharge"],
            DisplayOrder = row["DisplayOrder"].GetType() != typeof(DBNull) ? (int?)row["DisplayOrder"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ProductPreparation Translate_ProductPreparation(IDataRecord row)
      {
         return new ProductPreparation()
         {
            ProductPreparationID = (int?)row["ProductPreparationID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            FK_ProductPreparationMethodID = (int?)row["FK_ProductPreparationMethodID"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ProductPreparationMethod Translate_ProductPreparationMethod(IDataRecord row)
      {
         return new ProductPreparationMethod()
         {
            ProductPreparationMethodID = (int?)row["ProductPreparationMethodID"],
            ShortCode = (string)row["ShortCode"],
            Method = (string)row["Method"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ProductSubstitution Translate_ProductSubstitution(IDataRecord row)
      {
         return new ProductSubstitution()
         {
            ProductSubstitutionID = (int?)row["ProductSubstitutionID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            FK_ProductSubstitutionID = (int?)row["FK_ProductSubstitutionID"],
            IsQuantified = (bool?)row["IsQuantified"],
            Quantity = (decimal?)row["Quantity"],
            IsExtraCharge = (bool?)row["IsExtraCharge"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ServedAs Translate_ServedAs(IDataRecord row)
      {
         return new ServedAs()
         {
            ServedAsID = (int?)row["ServedAsID"],
            ServedAsType = (string)row["ServedAsType"],
            Name = (string)row["Name"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ServedAsProduct Translate_ServedAsProduct(IDataRecord row)
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
            IsDefault = (bool?)row["IsDefault"],
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
