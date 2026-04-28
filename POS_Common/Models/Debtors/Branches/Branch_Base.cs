using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.Branches
{
  public abstract class Branch_Base
  {
       #region Properties
       
      public int? BranchID { get; set; }

      public string ShortCode { get; set; }

      public string Name { get; set; }

      public int? FK_StatusID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
