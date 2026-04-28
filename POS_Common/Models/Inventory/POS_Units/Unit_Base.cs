using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_Units
{
  public abstract class Unit_Base
  {
       #region Properties
       
      public int? UnitID { get; set; }

      public string Unit { get; set; }

      public string Symbol { get; set; }

      public string BC_ID { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
