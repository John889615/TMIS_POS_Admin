using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_TabLineExtras
{
  public abstract class TabLineExtra_Base
  {
       #region Properties
       
      public Guid? TabLineExtraID { get; set; }

      public Guid? FK_TabLineID { get; set; }

      public int? FK_ProductID { get; set; }

      public string Product { get; set; }
       #endregion
  }
}
