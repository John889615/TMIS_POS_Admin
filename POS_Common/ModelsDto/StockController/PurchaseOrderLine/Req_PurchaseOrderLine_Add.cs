using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.PurchaseOrderLine
{
    public class Req_PurchaseOrderLine_Add
    {
        #region Properties

        public List<PurchaseOrderLines> PurchaseOrderLines { get; set; }
        #endregion
    }

    public class PurchaseOrderLines
    {
        public int? POS_PurchaseOrderLineID { get; set; }

        public int? FK_PurchaseOrderID { get; set; }

        public int? FK_ProductID { get; set; }

        public decimal? Quantity { get; set; }

        public string Notes { get; set; }
    }
}
