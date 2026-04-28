using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.CountrySubregions
{
  public abstract class CountrySubregion_Base
  {
       #region Properties
       
      public int? CountrySubregionID { get; set; }

      public string Subregion { get; set; }

      public int? FK_CountryRegionID { get; set; }
       #endregion
  }
}
