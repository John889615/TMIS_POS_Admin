using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_InternalStockTransferLines
{
  public abstract class InternalStockTransferLine_Base
  {
       #region Properties
       
      public int? InternalStockTransferLineID { get; set; }

      public int? FK_InternalStockTransferID { get; set; }

      public int? FK_ProductID { get; set; }

      public decimal? Quantity { get; set; }
       #endregion
  }
}
