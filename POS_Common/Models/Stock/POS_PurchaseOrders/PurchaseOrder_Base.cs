using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_PurchaseOrders
{
  public abstract class PurchaseOrder_Base
  {
       #region Properties
       
      public int? PurchaseOrderID { get; set; }

      public string OrderNumber { get; set; }

      public int? FK_SupplierID { get; set; }

      public int? FK_DebtorID { get; set; }

      public int? FK_CostCenterID { get; set; }

      public int? FK_OrderStatusID { get; set; }

      public int? FK_UserID { get; set; }

      public string Notes { get; set; }

      public string ManagerNotes { get; set; }

      public DateTime? DateOrdered { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
