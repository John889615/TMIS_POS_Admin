using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Inventory.Custom.SelectProductCombinationsID;
using POS_Common.Models.Inventory.Custom.DeleteProductCombination;

namespace POS_Api.Translators
{
   public abstract class Inventory_Custom_SP_Translator : Inventory_Base_Translator
   {
       #region Custom Stored Procedure Translators

       
      internal static Res_SelectProductCombinationsID Translate_SelectProductCombinationsID(IDataRecord row)
      {
         return new Res_SelectProductCombinationsID()
         {
            ProductCombinationID = row["ProductCombinationID"].GetType() != typeof(DBNull) ? (int?)row["ProductCombinationID"] : null,
            FKProductID = row["FK_ProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductID"] : null,
            FKProductItemID = row["FK_ProductItemID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductItemID"] : null,
            IsQuantified = row["IsQuantified"].GetType() != typeof(DBNull) ? (bool?)row["IsQuantified"] : null,
            Quantity = row["Quantity"].GetType() != typeof(DBNull) ? (decimal?)row["Quantity"] : null,
            IsOptional = row["IsOptional"].GetType() != typeof(DBNull) ? (bool?)row["IsOptional"] : null,
            IsExtraCharge = row["IsExtraCharge"].GetType() != typeof(DBNull) ? (bool?)row["IsExtraCharge"] : null,
            DisplayOrder = row["DisplayOrder"].GetType() != typeof(DBNull) ? (int?)row["DisplayOrder"] : null,
            FKCreatedUserID = row["FK_CreatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_CreatedUserID"] : null,
            FKUpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }


       #endregion
   }
}
