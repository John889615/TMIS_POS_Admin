using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductCategory
{
    public class Res_ProductCategory_List
    {
        #region Properties

        #region Properties

        public int? POS_ProductCategoryID { get; set; }

        public string CategoryName { get; set; }

        public int? FK_ProductCategoryID { get; set; }

        public string CategoryMaster { get; set; }

        public bool? IsMaster { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
        #endregion
    }
}
