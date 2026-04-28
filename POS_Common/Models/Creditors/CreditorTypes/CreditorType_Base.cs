using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Creditors.CreditorTypes
{
  public abstract class CreditorType_Base
  {
       #region Properties
       
      public int? CreditorTypeID { get; set; }

      public string Type { get; set; }

      public string Description { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
