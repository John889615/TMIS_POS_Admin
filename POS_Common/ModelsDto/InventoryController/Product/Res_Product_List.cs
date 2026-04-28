using POS_Common.Interfaces.Api_Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace POS_Common.ModelsDto.InventoryController.Product
{
    public class Res_Product_List
    {
        #region Properties

        public int? POS_ProductID { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int? FK_ProductTypeID { get; set; }

        public string ProductType { get; set; }

        public bool? IsInventory { get; set; }

        public bool? IsManufactured { get; set; }

        public bool? IsService { get; set; }

        public bool? IsComposite { get; set; }

        public bool? IsStockTracked { get; set; }

        public int? FK_UnitID { get; set; }

        public string Unit { get; set; }

        public string Symbol { get; set; }

        public int? FK_ProductCategoryID { get; set; }

        public string CategoryName { get; set; }

        public int? FK_DefaultUnitID { get; set; }

        public string DefaultUnit { get; set; }

        public string DefaultSymbol { get; set; }

        public string SKU { get; set; }

        public string Barcode { get; set; }

        public string QrCode { get; set; }

        public string ImageUrl { get; set; }
        #endregion
    }
}
