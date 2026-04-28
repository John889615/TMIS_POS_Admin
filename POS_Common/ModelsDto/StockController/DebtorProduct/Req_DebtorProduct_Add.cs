using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.DebtorProduct
{
    public class Req_DebtorProduct_Add
    {
        #region Properties

        public int? FK_ProductID { get; set; }

        public int? FK_DebtorID { get; set; }

        public decimal? CostPrice { get; set; }

        public int? FK_SellUnitID { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public bool? IsAvailable { get; set; }

        public bool? IsActive { get; set; }
        #endregion
    }
}
