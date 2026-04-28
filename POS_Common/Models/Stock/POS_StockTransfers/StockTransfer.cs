using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_StockTransfers
{
   public class StockTransfer : StockTransfer_Base
    {
        #region Additional Properties

        public string FromDebtor { get; set; }

        public string ToDebtor { get; set; }

        public string OrderStatus { get; set; }

        public string CreatedBy { get; set; }
        #endregion
    }
}
