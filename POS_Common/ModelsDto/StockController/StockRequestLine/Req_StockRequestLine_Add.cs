using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.StockRequestLine
{
    public class Req_StockRequestLine_Add
    {
        #region Properties

        public List<StockRequestLine> StockRequestLines { get; set; }
        #endregion
    }

    public class StockRequestLine
    {
        public int? POS_StockRequestLineID { get; set; }

        public int? FK_StockRequestID { get; set; }

        public int? FK_ProductID { get; set; }

        public decimal? Quantity { get; set; }

        public string Notes { get; set; }
    }
}
