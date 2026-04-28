using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_MenuItemProducts
{
   public class MenuItemProduct : MenuItemProduct_Base
    {
        #region Additional Properties
        
        public string Item { get; set; }

        public string ProductName { get; set; }
        #endregion
    }
}
