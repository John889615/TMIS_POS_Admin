using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Stock.POS_PurchaseOrders;
using POS_Common.Models.Stock.POS_PurchaseOrderLines;
using POS_Common.Models.Stock.POS_StockReceive;
using POS_Common.Models.Stock.POS_StockReceiveLines;
using POS_Common.Models.Stock.POS_StockRequests;
using POS_Common.Models.Stock.POS_StockRequestLines;
using POS_Common.Models.Stock.POS_StockTransfers;
using POS_Common.Models.Stock.POS_StockTransferLines;
using POS_Common.Models.Stock.POS_CostCenterProducts;
using POS_Common.Models.Stock.POS_SupplierProducts;
using POS_Common.Models.Stock.POS_DebtorProducts;
using POS_Common.Models.Stock.POS_InternalStockTransfers;
using POS_Common.Models.Stock.POS_InternalStockTransferLines;
using POS_Common.Models.Stock.POS_DebtorProductPriceHistory;
using POS_Common.Models.Stock.POS_CostCenterProductPriceHistory;
using POS_Common.Models.Stock.POS_PriceCodes;
using POS_Common.Models.Stock.POS_DebtorProductPrices;
using POS_Common.Models.Stock.POS_OrderStatus;
using POS_Common.Models.Stock.Custom.StockRequestSelectAllStockRequest;
using POS_Common.Models.Stock.Custom.StockRequestSelectSingleNumber;
using POS_Common.Models.Stock.Custom.StockRequestLinesSelectAllStockRequestLines;
using POS_Common.Models.Stock.Custom.StockRequestReviewersSelectByDebtorRole;

namespace POS_Api.Translators
{
   public abstract class Stock_Base_Translator
   {
       #region Translators
       
      internal static PurchaseOrder Translate_PurchaseOrder(IDataRecord row)
      {
         return new PurchaseOrder()
         {
            PurchaseOrderID = (int?)row["PurchaseOrderID"],
            OrderNumber = (string)row["OrderNumber"],
            FK_SupplierID = (int?)row["FK_SupplierID"],
            FK_DebtorID = (int?)row["FK_DebtorID"],
            FK_CostCenterID = row["FK_CostCenterID"].GetType() != typeof(DBNull) ? (int?)row["FK_CostCenterID"] : null,
            FK_OrderStatusID = (int?)row["FK_OrderStatusID"],
            FK_UserID = (int?)row["FK_UserID"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            ManagerNotes = row["ManagerNotes"].GetType() != typeof(DBNull) ? (string)row["ManagerNotes"] : null,
            DateOrdered = (DateTime?)row["DateOrdered"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static PurchaseOrderLine Translate_PurchaseOrderLine(IDataRecord row)
      {
         return new PurchaseOrderLine()
         {
            PurchaseOrderLineID = (int?)row["PurchaseOrderLineID"],
            FK_PurchaseOrderID = (int?)row["FK_PurchaseOrderID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            Quantity = (decimal?)row["Quantity"],
            UnitCostIncl = (decimal?)row["UnitCostIncl"],
            UnitCostExcl = (decimal?)row["UnitCostExcl"],
            FK_TaxTypeID = (int?)row["FK_TaxTypeID"],
            TaxRate = (decimal?)row["TaxRate"],
            TotalCostIncl = (decimal?)row["TotalCostIncl"],
            TotalCostExcl = (decimal?)row["TotalCostExcl"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            ManagerNotes = row["ManagerNotes"].GetType() != typeof(DBNull) ? (string)row["ManagerNotes"] : null,
            IsDeclined = (bool?)row["IsDeclined"],
         };
      }

       
      internal static StockReceive Translate_StockReceive(IDataRecord row)
      {
         return new StockReceive()
         {
            StockReceiveID = (int?)row["StockReceiveID"],
            FK_PurchaseOrderID = row["FK_PurchaseOrderID"].GetType() != typeof(DBNull) ? (int?)row["FK_PurchaseOrderID"] : null,
            FK_StockTransferID = row["FK_StockTransferID"].GetType() != typeof(DBNull) ? (int?)row["FK_StockTransferID"] : null,
            FK_UserID = (int?)row["FK_UserID"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateReceived = (DateTime?)row["DateReceived"],
         };
      }

       
      internal static StockReceiveLine Translate_StockReceiveLine(IDataRecord row)
      {
         return new StockReceiveLine()
         {
            StockReceiveLineID = (int?)row["StockReceiveLineID"],
            FK_StockReceiveID = (int?)row["FK_StockReceiveID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            Quantity = (decimal?)row["Quantity"],
            UnitCostIncl = row["UnitCostIncl"].GetType() != typeof(DBNull) ? (decimal?)row["UnitCostIncl"] : null,
            UnitCostExcl = row["UnitCostExcl"].GetType() != typeof(DBNull) ? (decimal?)row["UnitCostExcl"] : null,
            FK_TaxTypeID = row["FK_TaxTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_TaxTypeID"] : null,
            TaxRate = row["TaxRate"].GetType() != typeof(DBNull) ? (decimal?)row["TaxRate"] : null,
            TotalCostIncl = row["TotalCostIncl"].GetType() != typeof(DBNull) ? (decimal?)row["TotalCostIncl"] : null,
            TotalCostExcl = row["TotalCostExcl"].GetType() != typeof(DBNull) ? (decimal?)row["TotalCostExcl"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            LineTotal = row["LineTotal"].GetType() != typeof(DBNull) ? (decimal?)row["LineTotal"] : null,
         };
      }

       
      internal static StockRequest Translate_StockRequest(IDataRecord row)
      {
         return new StockRequest()
         {
            StockRequestID = (int?)row["StockRequestID"],
            RefNumber = row["RefNumber"].GetType() != typeof(DBNull) ? (string)row["RefNumber"] : null,
            FK_FromDebtorID = (int?)row["FK_FromDebtorID"],
            FK_ToDebtorID = (int?)row["FK_ToDebtorID"],
            FK_OrderStatusID = (int?)row["FK_OrderStatusID"],
            FK_UserID = (int?)row["FK_UserID"],
            ManagerNotes = row["ManagerNotes"].GetType() != typeof(DBNull) ? (string)row["ManagerNotes"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateOrdered = (DateTime?)row["DateOrdered"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            FK_ApprovedByUserID = row["FK_ApprovedByUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_ApprovedByUserID"] : null,
            DateApproved = row["DateApproved"].GetType() != typeof(DBNull) ? (DateTime?)row["DateApproved"] : null,
         };
      }

       
      internal static StockRequestLine Translate_StockRequestLine(IDataRecord row)
      {
         return new StockRequestLine()
         {
            StockRequestLineID = (int?)row["StockRequestLineID"],
            FK_StockRequestID = (int?)row["FK_StockRequestID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            Quantity = (decimal?)row["Quantity"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            ManagerNotes = row["ManagerNotes"].GetType() != typeof(DBNull) ? (string)row["ManagerNotes"] : null,
            IsDeclined = (bool?)row["IsDeclined"],
            ApprovedQuantity = row["ApprovedQuantity"].GetType() != typeof(DBNull) ? (decimal?)row["ApprovedQuantity"] : null,
         };
      }

       
      internal static StockTransfer Translate_StockTransfer(IDataRecord row)
      {
         return new StockTransfer()
         {
            StockTransferID = (int?)row["StockTransferID"],
            RefNumber = row["RefNumber"].GetType() != typeof(DBNull) ? (string)row["RefNumber"] : null,
            FK_FromDebtorID = (int?)row["FK_FromDebtorID"],
            FK_ToDebtorID = (int?)row["FK_ToDebtorID"],
            FK_OrderStatusID = (int?)row["FK_OrderStatusID"],
            FK_UserID = (int?)row["FK_UserID"],
            DateTransfered = (DateTime?)row["DateTransfered"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
         };
      }

       
      internal static StockTransferLine Translate_StockTransferLine(IDataRecord row)
      {
         return new StockTransferLine()
         {
            StockTransferLineID = (int?)row["StockTransferLineID"],
            FK_StockTransferID = (int?)row["FK_StockTransferID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            Quantity = (decimal?)row["Quantity"],
         };
      }

       
      internal static CostCenterProduct Translate_CostCenterProduct(IDataRecord row)
      {
         return new CostCenterProduct()
         {
            CostCenterProductID = (int?)row["CostCenterProductID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            FK_CostCenterID = (int?)row["FK_CostCenterID"],
            FK_TaxTypeID = (int?)row["FK_TaxTypeID"],
            Value = (decimal?)row["Value"],
            Vat = (decimal?)row["Vat"],
            ItemPrice = (decimal?)row["ItemPrice"],
            FK_SellUnitID = (int?)row["FK_SellUnitID"],
            QuantityOnHand = (decimal?)row["QuantityOnHand"],
            IsAvailable = (bool?)row["IsAvailable"],
            IsActive = (bool?)row["IsActive"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static SupplierProduct Translate_SupplierProduct(IDataRecord row)
      {
         return new SupplierProduct()
         {
            SupplierProductID = (int?)row["SupplierProductID"],
            FK_CreditorID = (int?)row["FK_CreditorID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            FK_DebtorID = (int?)row["FK_DebtorID"],
            SupplierItemCode = (string)row["SupplierItemCode"],
            FK_BaseUnitID = (int?)row["FK_BaseUnitID"],
            FK_PacUnitID = row["FK_PacUnitID"].GetType() != typeof(DBNull) ? (int?)row["FK_PacUnitID"] : null,
            UnitsPerPack = row["UnitsPerPack"].GetType() != typeof(DBNull) ? (decimal?)row["UnitsPerPack"] : null,
            Quantity = (decimal?)row["Quantity"],
            TrackPackLevel = (bool?)row["TrackPackLevel"],
            LastPurchasePrice = row["LastPurchasePrice"].GetType() != typeof(DBNull) ? (decimal?)row["LastPurchasePrice"] : null,
            LastPurchaseDate = row["LastPurchaseDate"].GetType() != typeof(DBNull) ? (DateTime?)row["LastPurchaseDate"] : null,
            FK_TaxTypeID = (int?)row["FK_TaxTypeID"],
            LeadTimeDays = row["LeadTimeDays"].GetType() != typeof(DBNull) ? (int?)row["LeadTimeDays"] : null,
            IsPreferred = (int?)row["IsPreferred"],
            IsActive = (bool?)row["IsActive"],
            DateAdded = (DateTime?)row["DateAdded"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static DebtorProduct Translate_DebtorProduct(IDataRecord row)
      {
         return new DebtorProduct()
         {
            DebtorProductID = (int?)row["DebtorProductID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            FK_LocationID = (int?)row["FK_LocationID"],
            CostPrice = (decimal?)row["CostPrice"],
            FK_SellUnitID = (int?)row["FK_SellUnitID"],
            QuantityOnHand = (decimal?)row["QuantityOnHand"],
            IsAvailable = (bool?)row["IsAvailable"],
            IsActive = (bool?)row["IsActive"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static InternalStockTransfer Translate_InternalStockTransfer(IDataRecord row)
      {
         return new InternalStockTransfer()
         {
            InternalStockTransferID = (int?)row["InternalStockTransferID"],
            RefNumber = row["RefNumber"].GetType() != typeof(DBNull) ? (string)row["RefNumber"] : null,
            FK_DebtorID = (int?)row["FK_DebtorID"],
            FK_CostCenterID = (int?)row["FK_CostCenterID"],
            FK_UserID = (int?)row["FK_UserID"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateTransfered = (DateTime?)row["DateTransfered"],
         };
      }

       
      internal static InternalStockTransferLine Translate_InternalStockTransferLine(IDataRecord row)
      {
         return new InternalStockTransferLine()
         {
            InternalStockTransferLineID = (int?)row["InternalStockTransferLineID"],
            FK_InternalStockTransferID = (int?)row["FK_InternalStockTransferID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            Quantity = (decimal?)row["Quantity"],
         };
      }

       
      internal static DebtorProductPriceHistory Translate_DebtorProductPriceHistory(IDataRecord row)
      {
         return new DebtorProductPriceHistory()
         {
            DebtorProductPriceHistoryID = (int?)row["DebtorProductPriceHistoryID"],
            FK_DebtorProductID = (int?)row["FK_DebtorProductID"],
            Value = (decimal?)row["Value"],
            Vat = (decimal?)row["Vat"],
            ItemPrice = (decimal?)row["ItemPrice"],
            ValidFrom = (DateTime?)row["ValidFrom"],
            ValidTo = row["ValidTo"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidTo"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static CostCenterProductPriceHistory Translate_CostCenterProductPriceHistory(IDataRecord row)
      {
         return new CostCenterProductPriceHistory()
         {
            CostcenterProductPriceHistoryID = (int?)row["CostcenterProductPriceHistoryID"],
            FK_CostCenterProductID = (int?)row["FK_CostCenterProductID"],
            Value = (decimal?)row["Value"],
            Vat = (decimal?)row["Vat"],
            ItemPrice = (decimal?)row["ItemPrice"],
            ValidFrom = (DateTime?)row["ValidFrom"],
            ValidTo = row["ValidTo"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidTo"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static PriceCodes Translate_PriceCodes(IDataRecord row)
      {
         return new PriceCodes()
         {
            PriceCodeID = (int?)row["PriceCodeID"],
            PriceCode = (string)row["PriceCode"],
            Description = row["Description"].GetType() != typeof(DBNull) ? (string)row["Description"] : null,
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static DebtorProductPrice Translate_DebtorProductPrice(IDataRecord row)
      {
         return new DebtorProductPrice()
         {
            DebtorProductPriceID = (int?)row["DebtorProductPriceID"],
            FK_DebtorProductID = (int?)row["FK_DebtorProductID"],
            FK_PriceCodeID = (int?)row["FK_PriceCodeID"],
            FK_TaxID = (int?)row["FK_TaxID"],
            ItemPrice = (decimal?)row["ItemPrice"],
            Inclusive = (bool?)row["Inclusive"],
            Vat = (decimal?)row["Vat"],
            StartDate = row["StartDate"].GetType() != typeof(DBNull) ? (DateTime?)row["StartDate"] : null,
            EndDate = row["EndDate"].GetType() != typeof(DBNull) ? (DateTime?)row["EndDate"] : null,
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            FK_DefaultUnitID = row["FK_DefaultUnitID"].GetType() != typeof(DBNull) ? (int?)row["FK_DefaultUnitID"] : null,
         };
      }

       
      internal static OrderStatus Translate_OrderStatus(IDataRecord row)
      {
         return new OrderStatus()
         {
            OrderStatusID = (int?)row["OrderStatusID"],
            OrderStatus = (string)row["OrderStatus"],
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
