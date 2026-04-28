using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.Statuses
{
  public abstract class Status_Base
  {
       #region Properties
       
      public int? StatusID { get; set; }

      public int? FK_EntityID { get; set; }

      public int? FK_StatusGroupID { get; set; }

      public string SystemCode { get; set; }

      public string DisplayName { get; set; }

      public bool? IsActive { get; set; }

      public bool? CanEdit { get; set; }

      public bool? ShowInUI { get; set; }

      public int? SortOrder { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
