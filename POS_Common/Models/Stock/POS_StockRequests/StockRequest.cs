using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_StockRequests
{
   public class StockRequest : StockRequest_Base
    {
        #region Additional Properties
        
        public string FromDebtorName { get; set; }

        public string ToDebtorName { get; set; }

        public string OrderStatus { get; set; }

        public string CreatedBy { get; set; }
        #endregion
    }
}
