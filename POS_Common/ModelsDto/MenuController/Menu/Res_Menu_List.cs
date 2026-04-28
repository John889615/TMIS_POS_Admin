using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.Menu
{
    public class Res_Menu_List
    {
        #region Properties

        public int? MenuID { get; set; }

        public string MenuName { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public string SourceType { get; set; }

        public string Location { get; set; }

        public bool? IsActive { get; set; }

        public string ImageUrl { get; set; }
        #endregion
    }
}
