using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.Countries
{
  public abstract class Country_Base
  {
       #region Properties
       
      public int? CountryID { get; set; }

      public string CountryName { get; set; }

      public string NativeName { get; set; }

      public string OfficialName { get; set; }

      public string ISO2Code { get; set; }

      public string ISO3Code { get; set; }

      public string PrimaryLanguageCode { get; set; }

      public short? NumericCode { get; set; }

      public int? FK_DialingCodeID { get; set; }

      public int? FK_CurrencyID { get; set; }

      public int? FK_ContinentID { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
