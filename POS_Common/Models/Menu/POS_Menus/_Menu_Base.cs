using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_Menus
{
  public abstract class _Menu_Base
  {
       #region Properties
       
      public int? MenuID { get; set; }

      public string MenuName { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
