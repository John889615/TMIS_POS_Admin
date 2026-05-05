using System.Collections.Generic;

namespace POS_Common.ModelsDto.StockController.StockRequest
{
    public class Req_StockRequest_Update
    {
        #region Properties

        public int? POS_StockRequestID { get; set; }

        public string RefNumber { get; set; }

        public int? FK_FromDebtorID { get; set; }

        public int? FK_ToDebtorID { get; set; }

        public string Notes { get; set; }

        public List<Req_StockRequest_Add_Line> Lines { get; set; }
        #endregion
    }
}
