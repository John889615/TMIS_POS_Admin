using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.SubmittedPurchaseOrderLines
{
    public class Req_PurchaseOrderLineStatus_Update
    {
        public int? POS_PurchaseOrderLineID { get; set; }

        public int? FK_PurchaseOrderID { get; set; }

        public bool? IsDeclined { get; set; }

        public string ManagerNotes { get; set; }
    }
}
