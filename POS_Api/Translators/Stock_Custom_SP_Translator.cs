using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Stock.Custom.StockRequestSelectAllStockRequest;
using POS_Common.Models.Stock.Custom.StockRequestSelectSingleNumber;
using POS_Common.Models.Stock.Custom.StockRequestLinesSelectAllStockRequestLines;
using POS_Common.Models.Stock.Custom.StockRequestReviewersSelectByDebtorRole;

namespace POS_Api.Translators
{
   public abstract class Stock_Custom_SP_Translator : Stock_Base_Translator
   {
       #region Custom Stored Procedure Translators

       
      internal static Res_StockRequestLinesSelectAllStockRequestLines Translate_StockRequestLinesSelectAllStockRequestLines(IDataRecord row)
      {
         return new Res_StockRequestLinesSelectAllStockRequestLines()
         {
            StockRequestLineID = row["StockRequestLineID"].GetType() != typeof(DBNull) ? (int?)row["StockRequestLineID"] : null,
            FKStockRequestID = row["FK_StockRequestID"].GetType() != typeof(DBNull) ? (int?)row["FK_StockRequestID"] : null,
            FKProductID = row["FK_ProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductID"] : null,
            ProductName = row["ProductName"].GetType() != typeof(DBNull) ? (string)row["ProductName"] : null,
            Quantity = row["Quantity"].GetType() != typeof(DBNull) ? (decimal?)row["Quantity"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            ManagerNotes = row["ManagerNotes"].GetType() != typeof(DBNull) ? (string)row["ManagerNotes"] : null,
            IsDeclined = row["IsDeclined"].GetType() != typeof(DBNull) ? (bool?)row["IsDeclined"] : null,
            ApprovedQuantity = row["ApprovedQuantity"].GetType() != typeof(DBNull) ? (decimal?)row["ApprovedQuantity"] : null,
         };
      }


       
      internal static Res_StockRequestReviewersSelectByDebtorRole Translate_StockRequestReviewersSelectByDebtorRole(IDataRecord row)
      {
         return new Res_StockRequestReviewersSelectByDebtorRole()
         {
            POSStockRequestReviewerID = row["POS_StockRequestReviewerID"].GetType() != typeof(DBNull) ? (int?)row["POS_StockRequestReviewerID"] : null,
            FKToDebtorID = row["FK_ToDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_ToDebtorID"] : null,
            FKUserID = row["FK_UserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UserID"] : null,
            Email = row["Email"].GetType() != typeof(DBNull) ? (string)row["Email"] : null,
            DisplayName = row["DisplayName"].GetType() != typeof(DBNull) ? (string)row["DisplayName"] : null,
            Role = row["Role"].GetType() != typeof(DBNull) ? (string)row["Role"] : null,
            IsActive = row["IsActive"].GetType() != typeof(DBNull) ? (bool?)row["IsActive"] : null,
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
         };
      }


       
      internal static Res_StockRequestSelectAllStockRequest Translate_StockRequestSelectAllStockRequest(IDataRecord row)
      {
         return new Res_StockRequestSelectAllStockRequest()
         {
            StockRequestID = row["StockRequestID"].GetType() != typeof(DBNull) ? (int?)row["StockRequestID"] : null,
            RefNumber = row["RefNumber"].GetType() != typeof(DBNull) ? (string)row["RefNumber"] : null,
            FKFromDebtorID = row["FK_FromDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_FromDebtorID"] : null,
            FromDebtorName = row["FromDebtorName"].GetType() != typeof(DBNull) ? (string)row["FromDebtorName"] : null,
            FKToDebtorID = row["FK_ToDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_ToDebtorID"] : null,
            ToDebtorName = row["ToDebtorName"].GetType() != typeof(DBNull) ? (string)row["ToDebtorName"] : null,
            FKOrderStatusID = row["FK_OrderStatusID"].GetType() != typeof(DBNull) ? (int?)row["FK_OrderStatusID"] : null,
            OrderStatus = row["OrderStatus"].GetType() != typeof(DBNull) ? (string)row["OrderStatus"] : null,
            FKUserID = row["FK_UserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UserID"] : null,
            CreatedBy = row["CreatedBy"].GetType() != typeof(DBNull) ? (string)row["CreatedBy"] : null,
            ManagerNotes = row["ManagerNotes"].GetType() != typeof(DBNull) ? (string)row["ManagerNotes"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateOrdered = row["DateOrdered"].GetType() != typeof(DBNull) ? (DateTime?)row["DateOrdered"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
            FKApprovedByUserID = row["FK_ApprovedByUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_ApprovedByUserID"] : null,
            DateApproved = row["DateApproved"].GetType() != typeof(DBNull) ? (DateTime?)row["DateApproved"] : null,
         };
      }


       
      internal static Res_StockRequestSelectSingleNumber Translate_StockRequestSelectSingleNumber(IDataRecord row)
      {
         return new Res_StockRequestSelectSingleNumber()
         {
            StockRequestID = row["StockRequestID"].GetType() != typeof(DBNull) ? (int?)row["StockRequestID"] : null,
            RefNumber = row["RefNumber"].GetType() != typeof(DBNull) ? (string)row["RefNumber"] : null,
            FKFromDebtorID = row["FK_FromDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_FromDebtorID"] : null,
            FKToDebtorID = row["FK_ToDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_ToDebtorID"] : null,
            FKOrderStatusID = row["FK_OrderStatusID"].GetType() != typeof(DBNull) ? (int?)row["FK_OrderStatusID"] : null,
            FKUserID = row["FK_UserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UserID"] : null,
            ManagerNotes = row["ManagerNotes"].GetType() != typeof(DBNull) ? (string)row["ManagerNotes"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateOrdered = row["DateOrdered"].GetType() != typeof(DBNull) ? (DateTime?)row["DateOrdered"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
            FKApprovedByUserID = row["FK_ApprovedByUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_ApprovedByUserID"] : null,
            DateApproved = row["DateApproved"].GetType() != typeof(DBNull) ? (DateTime?)row["DateApproved"] : null,
         };
      }


       #endregion
   }
}
