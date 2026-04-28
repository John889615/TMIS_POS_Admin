using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController.ToServer
{
    public class Req_InvoiceTab_Sync
    {
        #region Properties

        public Guid? InvoiceTabID { get; set; }

        public Guid? FK_InvoiceHeaderID { get; set; }

        public Guid? FK_TabID { get; set; }

        public decimal? TabGratuity { get; set; }

        public decimal? TabDiscount { get; set; }

        public decimal? TabTotalExcl { get; set; }

        public decimal? TabTotalVat { get; set; }

        public decimal? TabTotalIncl { get; set; }

        public DateTime? TabDateOpened { get; set; }

        public DateTime? TabDateClosed { get; set; }

        public bool? SyncedToServer { get; set; }
        #endregion
    }
}
