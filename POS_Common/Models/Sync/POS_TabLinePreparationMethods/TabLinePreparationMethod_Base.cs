using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_TabLinePreparationMethods
{
  public abstract class TabLinePreparationMethod_Base
  {
       #region Properties
       
      public Guid? TabLinePreparationMethodID { get; set; }

      public Guid? FK_TabLineCombinationID { get; set; }

      public int? FK_PreparationMethodID { get; set; }

      public string PreparationMethodName { get; set; }
       #endregion
  }
}
