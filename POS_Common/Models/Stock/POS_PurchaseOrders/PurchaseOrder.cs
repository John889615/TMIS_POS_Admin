using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_PurchaseOrders
{
   public class PurchaseOrder : PurchaseOrder_Base
    {
        #region Additional Properties
        
        public string SupplierName { get; set; }

        public string DebtorName { get; set; }

        public string CostCenterName { get; set; }

        public string OrderStatus { get; set; }

        public string CreatedBy { get; set; }

        public int? SupplierProductID { get; set; }

        public decimal? UnitCost { get; set; }

        public int? TaxTypeID { get; set; }

        public decimal? TaxRate { get; set; }

        public int? FK_ProductID { get; set; }
        #endregion
    }
}
