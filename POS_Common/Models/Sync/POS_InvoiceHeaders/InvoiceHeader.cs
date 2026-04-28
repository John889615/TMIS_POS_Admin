using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_InvoiceHeaders
{
   public class InvoiceHeader : InvoiceHeader_Base
    {
        #region Additional Properties
        
        public string LocationBC_ID { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? UnitPriceExcl { get; set; }

        public string ItemNo { get; set; }
        #endregion
    }
}
