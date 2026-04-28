using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.StockTransfer
{
    public class Req_StockTransfer_Update
    {
        #region Properties

        public int? POS_StockTransferID { get; set; }

        public string RefNumber { get; set; }

        public int? FK_FromDebtorID { get; set; }

        public int? FK_ToDebtorID { get; set; }

        public DateTime? DateTransfered { get; set; }

        public string Notes { get; set; }
        #endregion
    }
}
