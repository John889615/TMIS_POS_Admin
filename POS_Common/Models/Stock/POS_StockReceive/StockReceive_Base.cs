using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_StockReceive
{
  public abstract class StockReceive_Base
  {
       #region Properties
       
      public int? StockReceiveID { get; set; }

      public int? FK_PurchaseOrderID { get; set; }

      public int? FK_StockTransferID { get; set; }

      public int? FK_UserID { get; set; }

      public string Notes { get; set; }

      public DateTime? DateReceived { get; set; }
       #endregion
  }
}
