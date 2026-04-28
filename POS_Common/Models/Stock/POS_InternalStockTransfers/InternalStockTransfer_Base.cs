using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_InternalStockTransfers
{
  public abstract class InternalStockTransfer_Base
  {
       #region Properties
       
      public int? InternalStockTransferID { get; set; }

      public string RefNumber { get; set; }

      public int? FK_DebtorID { get; set; }

      public int? FK_CostCenterID { get; set; }

      public int? FK_UserID { get; set; }

      public string Notes { get; set; }

      public DateTime? DateTransfered { get; set; }
       #endregion
  }
}
