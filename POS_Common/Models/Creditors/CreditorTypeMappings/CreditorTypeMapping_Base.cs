using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Creditors.CreditorTypeMappings
{
  public abstract class CreditorTypeMapping_Base
  {
       #region Properties
       
      public int? CreditorTypeMappingID { get; set; }

      public int? FK_CreditorID { get; set; }

      public int? FK_CreditorTypeID { get; set; }

      public int? FK_StatusID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
