using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.Continents
{
  public abstract class Continent_Base
  {
       #region Properties
       
      public int? ContinentID { get; set; }

      public string Name { get; set; }

      public string ShortCode { get; set; }
       #endregion
  }
}
