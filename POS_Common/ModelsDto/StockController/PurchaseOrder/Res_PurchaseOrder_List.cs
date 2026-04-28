using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.PurchaseOrder
{
    public class Res_PurchaseOrder_List
    {
        #region Properties

        public int? POS_PurchaseOrderID { get; set; }

        public string OrderNumber { get; set; }

        public int? SupplierID { get; set; }

        public string SupplierName { get; set; }

        public int? DebtorID { get; set; }

        public string DebtorName { get; set; }

        public int? CostCenterID { get; set; }

        public string CostCenterName { get; set; }

        public int? OrderStatusID { get; set; }

        public string OrderStatus { get; set; }

        public string CreatedBy { get; set; }

        public string Notes { get; set; }

        public string ManagerNotes { get; set; }
        #endregion
    }
}
