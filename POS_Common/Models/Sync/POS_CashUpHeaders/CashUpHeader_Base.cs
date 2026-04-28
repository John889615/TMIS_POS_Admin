using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_CashUpHeaders
{
  public abstract class CashUpHeader_Base
  {
       #region Properties
       
      public Guid? CashUpHeaderID { get; set; }

      public int? FK_CostCenterID { get; set; }

      public int? FK_CurrencyID { get; set; }

      public DateTime? CashUpDate { get; set; }

      public string CashUpBy { get; set; }

      public decimal? TotalSystemAmount { get; set; }

      public decimal? TotalCountedAmount { get; set; }

      public decimal? TotalVariance { get; set; }

      public string Notes { get; set; }

      public bool? IsFinalised { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
