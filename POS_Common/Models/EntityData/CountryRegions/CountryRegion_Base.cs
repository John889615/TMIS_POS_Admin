using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.CountryRegions
{
  public abstract class CountryRegion_Base
  {
       #region Properties
       
      public int? CountryRegionID { get; set; }

      public string Region { get; set; }

      public int? FK_ContinentID { get; set; }
       #endregion
  }
}
