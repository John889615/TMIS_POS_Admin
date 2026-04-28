using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.MenuItem
{
    public class Req_MenuItem_Add
    {
        #region Properties

        public int? FK_MenuID { get; set; }

        public string Item { get; set; }

        public string Description { get; set; }

        public int? FK_POS_MenuItemID { get; set; }

        public IFormFile ImageFile { get; set; }
        #endregion
    }
}
