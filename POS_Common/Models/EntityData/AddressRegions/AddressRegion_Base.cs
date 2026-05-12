using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.AddressRegions
{
  public abstract class AddressRegion_Base
  {
       #region Properties
       
      public int? AddressRegionID { get; set; }

      public string RegionName { get; set; }

      public string Description { get; set; }

      public int? FK_CountryID { get; set; }

      public int? FK_ProvinceID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
