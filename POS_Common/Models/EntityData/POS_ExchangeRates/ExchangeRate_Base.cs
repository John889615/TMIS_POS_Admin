using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.POS_ExchangeRates
{
  public abstract class ExchangeRate_Base
  {
       #region Properties
       
      public int? ExchangeRateID { get; set; }

      public int? FK_CurrencyID { get; set; }

      public decimal? ExchangeRate { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
