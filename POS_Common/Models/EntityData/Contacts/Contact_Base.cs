using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.Contacts
{
  public abstract class Contact_Base
  {
       #region Properties
       
      public int? ContactID { get; set; }

      public string ContactValue { get; set; }

      public int? FK_ContactTypeID { get; set; }

      public int? FK_DialingCodeID { get; set; }

      public bool? IsVerified { get; set; }

      public string VerificationToken { get; set; }

      public DateTime? VerifiedAt { get; set; }

      public string Notes { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
