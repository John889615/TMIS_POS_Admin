using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.CountryProvinces
{
  public abstract class CountryProvince_Base
  {
       #region Properties
       
      public int? CountryProvinceID { get; set; }

      public string ProvinceName { get; set; }

      public string ISO2Code { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_CountryID { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
