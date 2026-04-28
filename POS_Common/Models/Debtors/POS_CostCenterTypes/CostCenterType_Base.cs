using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.POS_CostCenterTypes
{
  public abstract class CostCenterType_Base
  {
       #region Properties
       
      public int? CostCenterTypeID { get; set; }

      public string Name { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
