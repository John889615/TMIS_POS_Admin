using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.TabLine
{
    public class Req_TabLine_Add
    {
        #region Properties

        public int? FK_TabID { get; set; }

        public int? FK_ProductID { get; set; }

        public int? Quantity { get; set; }
        #endregion
    }
}
