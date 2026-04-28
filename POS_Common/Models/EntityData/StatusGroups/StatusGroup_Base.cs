using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.StatusGroups
{
  public abstract class StatusGroup_Base
  {
       #region Properties
       
      public int? StatusGroupID { get; set; }

      public string GroupName { get; set; }

      public string Description { get; set; }
       #endregion
  }
}
