using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.DebtorMenu
{
    public class Req_DebtorMenu_Update
    {
        #region Properties

        public int? POS_MenuID { get; set; }

        public int? FK_DebtorID { get; set; }

        public int? FK_CostCenterID { get; set; }

        //public int? FK_MenuID { get; set; }

        public string MenuName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public IFormFile ImageFile { get; set; }
        #endregion
    }
}
