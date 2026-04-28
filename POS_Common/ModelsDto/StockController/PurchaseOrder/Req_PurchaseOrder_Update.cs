using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.PurchaseOrder
{
    public class Req_PurchaseOrder_Update
    {
        #region Properties

        public int? POS_PurchaseOrderID { get; set; }

        public string OrderNumber { get; set; }

        public int? FK_SupplierID { get; set; }

        public int? FK_DebtorID { get; set; }

        public int? FK_CostCenterID { get; set; }

        public bool? IsSubmitted { get; set; }

        public string Notes { get; set; }
        #endregion
    }
}
