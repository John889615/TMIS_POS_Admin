using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.Menu
{
    public class Req_Menu_Add
    {
        #region Properties

        public string MenuName { get; set; }

        public bool? IsActive { get; set; }

        public IFormFile ImageFile { get; set; }
        #endregion
    }
}
