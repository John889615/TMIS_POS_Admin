using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_StockRequestLines
{
  public abstract class StockRequestLine_Base
  {
       #region Properties
       
      public int? StockRequestLineID { get; set; }

      public int? FK_StockRequestID { get; set; }

      public int? FK_ProductID { get; set; }

      public decimal? Quantity { get; set; }

      public string Notes { get; set; }

      public string ManagerNotes { get; set; }

      public bool? IsDeclined { get; set; }

      public decimal? ApprovedQuantity { get; set; }
       #endregion
  }
}
