using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_StockRequests
{
  public abstract class StockRequest_Base
  {
       #region Properties
       
      public int? StockRequestID { get; set; }

      public string RefNumber { get; set; }

      public int? FK_FromDebtorID { get; set; }

      public int? FK_ToDebtorID { get; set; }

      public int? FK_OrderStatusID { get; set; }

      public int? FK_UserID { get; set; }

      public string ManagerNotes { get; set; }

      public string Notes { get; set; }

      public DateTime? DateOrdered { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
