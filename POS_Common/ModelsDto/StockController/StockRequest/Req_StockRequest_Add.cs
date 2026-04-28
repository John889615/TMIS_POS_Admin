using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.StockRequest
{
    public class Req_StockRequest_Add
    {
        #region Properties

        public string RefNumber { get; set; }

        public int? FK_FromDebtorID { get; set; }

        public int? FK_ToDebtorID { get; set; }

        public string Notes { get; set; }

        public bool? IsSubmitted { get; set; }
        #endregion
    }
}
