using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.DebtorTypeMappings
{
  public abstract class DebtorTypeMapping_Base
  {
       #region Properties
       
      public int? DebtorTypeMappingID { get; set; }

      public int? FK_DebtorID { get; set; }

      public int? FK_DebtorTypeID { get; set; }

      public int? FK_StatusID { get; set; }

      public int? FK_BranchID { get; set; }

      public int? FK_DepartmentID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
