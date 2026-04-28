using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_MenuItems
{
   public class MenuItem : MenuItem_Base
    {
        #region Additional Properties
        
        public string MenuName { get; set; }

        public string ParentItem { get; set; }
        #endregion
    }
}
