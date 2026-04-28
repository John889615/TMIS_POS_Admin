using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.DebtorTypes
{
  public abstract class DebtorType_Base
  {
       #region Properties
       
      public int? DebtorTypeID { get; set; }

      public string Type { get; set; }

      public string Description { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
