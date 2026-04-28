using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController.ToServer
{
    public class Req_InvoiceLine_Sync
    {
        #region Properties

        public Guid? InvoiceLineID { get; set; }

        public Guid? FK_InvoiceTabID { get; set; }

        public int? FK_ProductID { get; set; }

        public string Product { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? LineDiscount { get; set; }

        public decimal? LineTotalExcl { get; set; }

        public decimal? LineTotalVat { get; set; }

        public decimal? LineTotalIncl { get; set; }

        public string Guests { get; set; }

        public bool? SyncedToServer { get; set; }
        #endregion
    }
}
