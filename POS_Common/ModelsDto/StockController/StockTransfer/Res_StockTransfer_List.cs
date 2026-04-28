using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.StockTransfer
{
    public class Res_StockTransfer_List
    {
        #region Properties

        public int? POS_StockTransferID { get; set; }

        public string RefNumber { get; set; }

        public int? FK_FromDebtorID { get; set; }

        public string FromDebtorName { get; set; }

        public int? FK_ToDebtorID { get; set; }

        public string ToDebtorName { get; set; }

        public int? FK_OrderStatusID { get; set; }

        public string OrderStatus { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DateTransfered { get; set; }

        public string Notes { get; set; }
        #endregion
    }
}
