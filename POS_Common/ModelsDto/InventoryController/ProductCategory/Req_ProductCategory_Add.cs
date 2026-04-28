using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductCategory
{
    public class Req_ProductCategory_Add
    {
        #region Properties

        public string CategoryName { get; set; }

        public int? FK_ProductCategoryID { get; set; }

        public bool? IsMaster { get; set; }
        #endregion
    }
}
