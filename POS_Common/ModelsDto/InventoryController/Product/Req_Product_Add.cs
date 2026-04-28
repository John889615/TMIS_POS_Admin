using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.Product
{
    public class Req_Product_Add
    {
        #region Properties

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int? FK_ProductTypeID { get; set; }

        public bool? IsStockTracked { get; set; }

        public int? FK_UnitID { get; set; }

        public int? FK_ProductCategoryID { get; set; }

        public int? FK_DefaultUnitID { get; set; }

        public string SKU { get; set; }

        public string Barcode { get; set; }

        public string QrCode { get; set; }

        public IFormFile ImageFile { get; set; }
        #endregion
    }
}
