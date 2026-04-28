using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_TabLineCombinations
{
  public abstract class TabLineCombination_Base
  {
       #region Properties
       
      public Guid? TabLineCombinationID { get; set; }

      public Guid? FK_TabLineID { get; set; }

      public int? FK_ProductCombinationID { get; set; }

      public string Product { get; set; }

      public bool? Hold { get; set; }

      public string Notes { get; set; }
       #endregion
  }
}
