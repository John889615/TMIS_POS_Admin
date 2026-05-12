using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.DialingCodes
{
  public abstract class DialingCode_Base
  {
       #region Properties
       
      public int? DialingCodeID { get; set; }

      public string DialingCode { get; set; }

      public string ISO2Code { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
