using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.PurchaseOrderLine
{
    public class Res_PurchaseOrderLine_List
    {
        #region Properties

        public int? POS_PurchaseOrderLineID { get; set; }

        public int? PurchaseOrderID { get; set; }

        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? UnitCostExcl { get; set; }

        public decimal? UnitCostIncl { get; set; }

        public int? TaxTypeID { get; set; }

        public decimal? TaxRate { get; set; }

        public decimal? TotalCostExcl { get; set; }

        public decimal? TotalCostIncl { get; set; }

        public string Notes { get; set; }

        public bool? IsDeclined { get; set; }

        public string MangerNotes { get; set; }
        #endregion
    }
}
