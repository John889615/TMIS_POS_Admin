using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.EntitySettings
{
  public abstract class EntitySetting_Base
  {
       #region Properties
       
      public int? EntitySettingID { get; set; }

      public int? FK_EntityID { get; set; }

      public bool? IsCreditor { get; set; }

      public bool? IsDebtor { get; set; }

      public bool? IsBranch { get; set; }

      public bool? IsDepartment { get; set; }

      public bool? IsUser { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
