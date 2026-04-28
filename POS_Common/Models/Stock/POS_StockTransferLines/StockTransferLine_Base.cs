using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_StockTransferLines
{
  public abstract class StockTransferLine_Base
  {
       #region Properties
       
      public int? StockTransferLineID { get; set; }

      public int? FK_StockTransferID { get; set; }

      public int? FK_ProductID { get; set; }

      public decimal? Quantity { get; set; }
       #endregion
  }
}
