using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_TablineSubstitutes
{
  public abstract class TabLineSubstitute_Base
  {
       #region Properties
       
      public Guid? TablineSubstituteID { get; set; }

      public Guid? FK_ParentTabLineID { get; set; }

      public Guid? FK_SubstituionTabLineID { get; set; }

      public Guid? FK_ParentTabLineCombinationID { get; set; }
       #endregion
  }
}
