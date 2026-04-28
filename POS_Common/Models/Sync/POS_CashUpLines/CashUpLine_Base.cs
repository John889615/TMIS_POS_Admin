using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_CashUpLines
{
  public abstract class CashUpLine_Base
  {
       #region Properties
       
      public Guid? CashUpPaymentTypeID { get; set; }

      public Guid? FK_CashUpID { get; set; }

      public int? FK_PaymentTypeID { get; set; }

      public decimal? SystemAmount { get; set; }

      public decimal? CountedAmount { get; set; }

      public decimal? VarianceAmount { get; set; }

      public string Notes { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
