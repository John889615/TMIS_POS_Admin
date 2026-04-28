using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.SubmittedPurchaseOrder
{
    public class Req_PurchaseOrderStatus_Update
    {
        public int? POS_PurchaseOrderID { get; set; }

        public bool? IsDeclined { get; set; }

        public string ManagerNotes { get; set; }
    }
}
