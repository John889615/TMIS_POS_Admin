using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.EntityContacts
{
  public abstract class EntityContact_Base
  {
       #region Properties
       
      public int? EntityContactID { get; set; }

      public int? FK_EntityID { get; set; }

      public int? EntityRecordID { get; set; }

      public int? FK_ContactID { get; set; }

      public bool? IsPrimary { get; set; }

      public bool? IsMarketing { get; set; }

      public bool? IsEmergency { get; set; }

      public string PreferredContactTime { get; set; }

      public string PreferredLanguageCode { get; set; }

      public DateTime? ValidFrom { get; set; }

      public DateTime? ValidTo { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
