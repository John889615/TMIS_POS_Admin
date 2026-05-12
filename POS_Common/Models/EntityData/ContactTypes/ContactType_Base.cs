using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.ContactTypes
{
  public abstract class ContactType_Base
  {
       #region Properties
       
      public int? ContactTypeID { get; set; }

      public string Type { get; set; }

      public bool? IsPhoneNumberType { get; set; }

      public bool? IsEmailType { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
