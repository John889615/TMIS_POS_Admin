using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Creditors.Creditors
{
  public abstract class Creditor_Base
  {
       #region Properties
       
      public int? CreditorID { get; set; }

      public string ShortCode { get; set; }

      public string Name { get; set; }

      public int? FK_MasterCreditorID { get; set; }

      public bool? IsMasterCreditor { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public string BC_ID { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
