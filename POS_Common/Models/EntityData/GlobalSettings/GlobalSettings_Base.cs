using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.GlobalSettings
{
  public abstract class GlobalSettings_Base
  {
       #region Properties
       
      public int? GlobalSettingID { get; set; }

      public string Key { get; set; }

      public string Value { get; set; }

      public string Environment { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
