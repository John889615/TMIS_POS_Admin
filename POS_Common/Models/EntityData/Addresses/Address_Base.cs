using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.Addresses
{
  public abstract class Address_Base
  {
       #region Properties
       
      public int? AddressID { get; set; }

      public int? FK_CountryID { get; set; }

      public int? FK_ProvinceID { get; set; }

      public int? FK_AddressRegionID { get; set; }

      public string StreetAddress { get; set; }

      public string Locality { get; set; }

      public string PostalCode { get; set; }

      public string Landmark { get; set; }

      public decimal? Latitude { get; set; }

      public decimal? Longitude { get; set; }

      public string Notes { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
