using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.CurrencyExchangeRates
{
  public abstract class CurrencyExchangeRate_Base
  {
       #region Properties
       
      public int? CurrencyExchangeRateID { get; set; }

      public int? FK_FromCurrencyID { get; set; }

      public int? FK_ToCurrencyID { get; set; }

      public decimal? ExchangeRate { get; set; }

      public string ConversionMethod { get; set; }

      public DateTime? EffectiveDate { get; set; }

      public string Notes { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
