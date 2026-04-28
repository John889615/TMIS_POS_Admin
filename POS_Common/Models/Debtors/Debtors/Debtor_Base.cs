using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.Debtors
{
  public abstract class Debtor_Base
  {
       #region Properties
       
      public int? DebtorID { get; set; }

      public string ShortCode { get; set; }

      public string Name { get; set; }

      public int? FK_MasterDebtorID { get; set; }

      public bool? IsMasterDebtor { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public string BC_ID { get; set; }
       #endregion
  }
}
