using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_StockTransfers
{
  public abstract class StockTransfer_Base
  {
       #region Properties
       
      public int? StockTransferID { get; set; }

      public string RefNumber { get; set; }

      public int? FK_FromDebtorID { get; set; }

      public int? FK_ToDebtorID { get; set; }

      public int? FK_OrderStatusID { get; set; }

      public int? FK_UserID { get; set; }

      public DateTime? DateTransfered { get; set; }

      public string Notes { get; set; }
       #endregion
  }
}
