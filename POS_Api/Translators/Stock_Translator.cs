using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Stock.POS_PurchaseOrders;
using POS_Common.Models.Stock.POS_PurchaseOrderLines;
using POS_Common.Models.Stock.POS_StockRequests;
using POS_Common.Models.Stock.POS_StockRequestLines;
using POS_Common.Models.Stock.POS_StockTransfers;
using POS_Common.Models.Stock.POS_DebtorProducts;
using POS_Common.Models.Stock.POS_CostCenterProducts;
using POS_Common.Models.Stock.POS_DebtorProductPrices;
using POS_Common.Models.Stock.POS_StockRequestReviewers;

namespace POS_Api.Translators
{
   public class Stock_Translator : Stock_Custom_SP_Translator
   {
        #region Translators

        internal static PurchaseOrder Translate_PurchaseOrder_PurchaseOrder(IDataRecord row)
        {
            return new PurchaseOrder()
            {
                PurchaseOrderID = (int?)row["PurchaseOrderID"],
                OrderNumber = GetNullableString(row, "OrderNumber"),
                FK_SupplierID = row["FK_SupplierID"].GetType() != typeof(DBNull) ? (int?)row["FK_SupplierID"] : null,
                SupplierName = GetNullableString(row, "SupplierName"),
                FK_DebtorID = row["FK_DebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_DebtorID"] : null,
                DebtorName = GetNullableString(row, "DebtorName"),
                FK_CostCenterID = row["FK_CostCenterID"].GetType() != typeof(DBNull) ? (int?)row["FK_CostCenterID"] : null,
                CostCenterName = GetNullableString(row, "CostCenterName"),
                FK_OrderStatusID = row["OrderStatusID"].GetType() != typeof(DBNull) ? (int?)row["OrderStatusID"] : null,
                OrderStatus = GetNullableString(row, "OrderStatus"),
                CreatedBy = GetNullableString(row, "CreatedBy"),
                Notes = GetNullableString(row, "Notes"),
                ManagerNotes = GetNullableString(row, "ManagerNotes"),
            };
        }

        internal static PurchaseOrder Translate_PurchaseOrder_Supplier(IDataRecord row)
        {
            return new PurchaseOrder()
            {
                PurchaseOrderID = (int?)row["PurchaseOrderID"],
                SupplierProductID = row["SupplierProductID"].GetType() != typeof(DBNull) ? (int?)row["SupplierProductID"] : null,
                UnitCost = row["UnitCost"].GetType() != typeof(DBNull) ? (decimal?)row["UnitCost"] : null,
                TaxTypeID = row["TaxTypeID"].GetType() != typeof(DBNull) ? (int?)row["TaxTypeID"] : null,
                TaxRate = row["TaxRate"].GetType() != typeof(DBNull) ? (int?)row["TaxRate"] : null
            };
        }

        //internal static DebtorProductPrice Translate_DebtorProductPrice_ProductID(IDataRecord row)
        //{
        //    return new DebtorProductPrice()
        //    {
        //        DebtorProductPriceID = (int?)row["DebtorProductPriceID"],
        //        FK_ProductID = (int?)row["FK_ProductID"],
        //        FK_DebtorProductID = (int?)row["FK_DebtorProductID"],
        //        FK_PriceCodeID = (int?)row["FK_PriceCodeID"],
        //        FK_TaxID = (int?)row["FK_TaxID"],
        //        ItemPrice = (decimal?)row["ItemPrice"],
        //        Inclusive = (bool?)row["Inclusive"],
        //        Vat = (decimal?)row["Vat"],
        //        StartDate = row["StartDate"].GetType() != typeof(DBNull) ? (DateTime?)row["StartDate"] : null,
        //        EndDate = row["EndDate"].GetType() != typeof(DBNull) ? (DateTime?)row["EndDate"] : null,
        //        IsActive = (bool?)row["IsActive"],
        //        DateCreated = (DateTime?)row["DateCreated"],
        //        DateUpdated = (DateTime?)row["DateUpdated"],
        //    };
        //}

        internal static PurchaseOrderLine Translate_PurchaseOrderLine_PurchaseOrderLine(IDataRecord row)
        {
            return new PurchaseOrderLine()
            {
                PurchaseOrderLineID = (int?)row["PurchaseOrderLineID"],
                FK_PurchaseOrderID = row["FK_PurchaseOrderID"].GetType() != typeof(DBNull) ? (int?)row["FK_PurchaseOrderID"] : null,
                FK_ProductID = row["FK_ProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductID"] : null,
                ProductName = GetNullableString(row, "ProductName"),
                Quantity = row["Quantity"].GetType() != typeof(DBNull) ? (decimal?)row["Quantity"] : null,
                UnitCostIncl = row["UnitCostIncl"].GetType() != typeof(DBNull) ? (decimal?)row["UnitCostIncl"] : null,
                UnitCostExcl = row["UnitCostExcl"].GetType() != typeof(DBNull) ? (decimal?)row["UnitCostExcl"] : null,
                FK_TaxTypeID = row["FK_TaxTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_TaxTypeID"] : null,
                TaxRate = row["TaxRate"].GetType() != typeof(DBNull) ? (decimal?)row["TaxRate"] : null,
                TotalCostIncl = row["TotalCostIncl"].GetType() != typeof(DBNull) ? (decimal?)row["TotalCostIncl"] : null,
                TotalCostExcl = row["TotalCostExcl"].GetType() != typeof(DBNull) ? (decimal?)row["TotalCostExcl"] : null,
                Notes = GetNullableString(row, "Notes"),
                IsDeclined = GetNullableBool(row, "IsDeclined"),
                ManagerNotes = GetNullableString(row, "ManagerNotes"),
            };
        }

        internal static PurchaseOrderLine Translate_SubmittedPurchaseOrderLine_SubmittedPurchaseOrderLine(IDataRecord row)
        {
            return new PurchaseOrderLine()
            {
                PurchaseOrderLineID = (int?)row["PurchaseOrderLineID"],
                FK_PurchaseOrderID = row["FK_PurchaseOrderID"].GetType() != typeof(DBNull) ? (int?)row["FK_PurchaseOrderID"] : null,
                FK_ProductID = row["FK_ProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductID"] : null,
                ProductName = GetNullableString(row, "ProductName"),
                Quantity = row["Quantity"].GetType() != typeof(DBNull) ? (decimal?)row["Quantity"] : null,
                UnitCostIncl = row["UnitCostIncl"].GetType() != typeof(DBNull) ? (decimal?)row["UnitCostIncl"] : null,
                UnitCostExcl = row["UnitCostExcl"].GetType() != typeof(DBNull) ? (decimal?)row["UnitCostExcl"] : null,
                TaxRate = row["TaxRate"].GetType() != typeof(DBNull) ? (decimal?)row["TaxRate"] : null,
                TotalCostIncl = row["TotalCostIncl"].GetType() != typeof(DBNull) ? (decimal?)row["TotalCostIncl"] : null,
                TotalCostExcl = row["TotalCostExcl"].GetType() != typeof(DBNull) ? (decimal?)row["TotalCostExcl"] : null,
                StockOnHand = row["StockOnHand"].GetType() != typeof(DBNull) ? (decimal?)row["StockOnHand"] : null,
                Notes = GetNullableString(row, "Notes"),
                IsDeclined = GetNullableBool(row, "IsDeclined"),
                ManagerNotes = GetNullableString(row, "ManagerNotes"),
            };
        }

        internal static StockRequest Translate_POS_StockRequest_StockRequest(IDataRecord row)
        {
            return new StockRequest()
            {
                StockRequestID = (int?)row["StockRequestID"],
                RefNumber = row["RefNumber"].GetType() != typeof(DBNull) ? (string)row["RefNumber"] : null,
                FK_FromDebtorID = (int?)row["FK_FromDebtorID"],
                FromDebtorName = GetNullableString(row, "FromDebtorName"),
                FK_ToDebtorID = (int?)row["FK_ToDebtorID"],
                ToDebtorName = GetNullableString(row, "ToDebtorName"),
                FK_OrderStatusID = (int?)row["FK_OrderStatusID"],
                OrderStatus = GetNullableString(row, "OrderStatus"),
                FK_UserID = row["FK_UserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UserID"] : null,
                CreatedBy = GetNullableString(row, "CreatedBy"),
                ManagerNotes = GetNullableString(row,"ManagerNotes"),
                Notes = GetNullableString(row, "Notes"),
                DateOrdered = row["DateOrdered"].GetType() != typeof(DBNull) ? (DateTime?)row["DateOrdered"] : null,
                DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
                FK_ApprovedByUserID = row["FK_ApprovedByUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_ApprovedByUserID"] : null,
                DateApproved = row["DateApproved"].GetType() != typeof(DBNull) ? (DateTime?)row["DateApproved"] : null,
            };
        }

        internal static StockRequestLine Translate_StockRequestLine_StockRequestLine(IDataRecord row)
        {
            return new StockRequestLine()
            {
                StockRequestLineID = (int?)row["StockRequestLineID"],
                FK_StockRequestID = row["FK_StockRequestID"].GetType() != typeof(DBNull) ? (int?)row["FK_StockRequestID"] : null,
                FK_ProductID = row["FK_ProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductID"] : null,
                ProductName = GetNullableString(row, "ProductName"),
                Quantity = row["Quantity"].GetType() != typeof(DBNull) ? (decimal?)row["Quantity"] : null,
                Notes = GetNullableString(row, "Notes"),
                IsDeclined = GetNullableBool(row, "IsDeclined"),
                ManagerNotes = GetNullableString(row, "ManagerNotes"),
                ApprovedQuantity = row["ApprovedQuantity"].GetType() != typeof(DBNull) ? (decimal?)row["ApprovedQuantity"] : null,
            };
        }

        internal static StockRequestReviewer Translate_StockRequestReviewer(IDataRecord row)
        {
            return new StockRequestReviewer()
            {
                POS_StockRequestReviewerID = (int?)row["POS_StockRequestReviewerID"],
                FK_ToDebtorID = (int?)row["FK_ToDebtorID"],
                FK_UserID = row["FK_UserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UserID"] : null,
                Email = (string)row["Email"],
                DisplayName = GetNullableString(row, "DisplayName"),
                Role = (string)row["Role"],
                IsActive = (bool?)row["IsActive"],
                DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            };
        }

        internal static StockTransfer Translate_StockTransfer_StockTransfer(IDataRecord row)
        {
            return new StockTransfer()
            {
                StockTransferID = (int?)row["StockTransferID"],
                RefNumber = GetNullableString(row, "RefNumber"),
                FK_FromDebtorID = row["FK_FromDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_FromDebtorID"] : null,
                FromDebtor = GetNullableString(row, "FromDebtorName"),
                FK_ToDebtorID = row["FK_ToDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_ToDebtorID"] : null,
                ToDebtor = GetNullableString(row, "ToDebtorName"),
                FK_OrderStatusID = row["FK_OrderStatusID"].GetType() != typeof(DBNull) ? (int?)row["FK_OrderStatusID"] : null,
                OrderStatus = GetNullableString(row, "OrderStatus"),
                CreatedBy = GetNullableString(row, "CreatedBy"),
                Notes = GetNullableString(row, "Notes"),
                DateTransfered = GetNullableDate(row, "DateTransfered"),
            };
        }

        internal static DebtorProduct Translate_DebtorProduct_DebtorProduct(IDataRecord row)
        {
            return new DebtorProduct()
            {
                DebtorProductID = (int?)row["DebtorProductID"],
                FK_ProductID = row["FK_ProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductID"] : null,
                ProductName = GetNullableString(row, "ProductName"),
                FK_LocationID = row["FK_LocationID"].GetType() != typeof(DBNull) ? (int?)row["FK_LocationID"] : null,
                Debtor = GetNullableString(row, "Debtor"),
                FK_SellUnitID = row["FK_SellUnitID"].GetType() != typeof(DBNull) ? (int?)row["FK_SellUnitID"] : null,
                Symbol = GetNullableString(row, "Symbol"),
                Unit = GetNullableString(row, "Unit"),
                QuantityOnHand = row["QuantityOnHand"].GetType() != typeof(DBNull) ? (decimal?)row["QuantityOnHand"] : null,
                IsAvailable = GetNullableBool(row, "IsAvailable"),
                IsActive = GetNullableBool(row, "IsActive"),
            };
        }

        internal static CostCenterProduct Translate_CostCenterProduct_CostCenterProduct(IDataRecord row)
        {
            return new CostCenterProduct()
            {
                CostCenterProductID = (int?)row["CostCenterProductID"],
                FK_ProductID = row["FK_ProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductID"] : null,
                ProductName = GetNullableString(row, "ProductName"),
                FK_CostCenterID = row["FK_CostCenterID"].GetType() != typeof(DBNull) ? (int?)row["FK_CostCenterID"] : null,
                CostCenter = GetNullableString(row, "CostCenter"),
                FK_TaxTypeID = row["FK_TaxTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_TaxTypeID"] : null,
                Rate = row["Rate"].GetType() != typeof(DBNull) ? (int?)row["Rate"] : null,
                Value = row["Value"].GetType() != typeof(DBNull) ? (decimal?)row["Value"] : null,
                Vat = row["Vat"].GetType() != typeof(DBNull) ? (decimal?)row["Vat"] : null,
                ItemPrice = row["ItemPrice"].GetType() != typeof(DBNull) ? (decimal?)row["ItemPrice"] : null,
                FK_SellUnitID = row["FK_SellUnitID"].GetType() != typeof(DBNull) ? (int?)row["FK_SellUnitID"] : null,
                Symbol = GetNullableString(row, "Symbol"),
                Unit = GetNullableString(row, "Unit"),
                QuantityOnHand = row["QuantityOnHand"].GetType() != typeof(DBNull) ? (decimal?)row["QuantityOnHand"] : null,
                IsAvailable = GetNullableBool(row, "IsAvailable"),
                IsActive = GetNullableBool(row, "IsActive"),
                CreatedBy = GetNullableString(row, "CreatedBy"),
                UpdatedBy = GetNullableString(row, "UpdatedBy"),
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


