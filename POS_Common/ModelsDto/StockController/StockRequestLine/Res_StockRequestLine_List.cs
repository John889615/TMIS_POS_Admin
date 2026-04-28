using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.StockRequestLine
{
    public class Res_StockRequestLine_List
    {
        #region Properties

        public int? POS_StockRequestLineID { get; set; }

        public int? FK_StockRequestID { get; set; }

        public int? FK_ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal? Quantity { get; set; }

        public string Notes { get; set; }

        public string ManagerNotes { get; set; }

        public bool? IsDeclined { get; set; }
        #endregion
    }
}
