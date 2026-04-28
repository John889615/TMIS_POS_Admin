using POS_Common.ModelsDto.SyncController.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController.ToServer
{

    public class Req_InvoiceHeader_Sync
    {
        #region Properties

        public Guid? InvoiceHeaderID { get; set; }

        public int? FK_LocationID { get; set; }

        public string InvoiceNo { get; set; }

        public string PartyName { get; set; }

        public string BookingReference { get; set; }

        public decimal? DiscountTotal { get; set; }

        public decimal? GratuityTotal { get; set; }

        public decimal? ExclTotal { get; set; }

        public decimal? VatTotal { get; set; }

        public decimal? InclTotal { get; set; }

        public bool? IsDiscarded { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DatePaid { get; set; }

        public List<Req_InvoiceTab_Sync> InvoiceTabs { get; set; }

        public List<Req_InvoiceLine_Sync> InvoiceLines { get; set; }
        #endregion
    }
}
