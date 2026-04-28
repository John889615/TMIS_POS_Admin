using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.POS_CostCenters
{
  public abstract class CostCenter_Base
  {
       #region Properties
       
      public int? CostCenterID { get; set; }

      public int? FK_LocationID { get; set; }

      public string Name { get; set; }

      public string BillingReference { get; set; }

      public int? FK_StatusID { get; set; }

      public int? FK_CostCenterTypeID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public string BC_ID { get; set; }
       #endregion
  }
}
