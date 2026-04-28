using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.TabLine
{
    public class Res_TabLine_List
    {
        #region Properties

        public int? TabLineID { get; set; }

        public int? FK_TabID { get; set; }

        public int? FK_ProductID { get; set; }

        public string Product {  get; set; }

        public int? Quantity { get; set; }

        public bool? IsOrdered { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
