using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.DebtorProduct
{
    public class Res_DebtorProduct_List
    {
        #region Properties

        public int? POS_DebtorProductID { get; set; }

        public int? FK_ProductID { get; set; }

        public string ProductName { get; set; }

        public int? FK_DebtorID { get; set; }

        public string Debtor { get; set; }

        public decimal? CostPrice { get; set; }

        public int? FK_SellUnitID { get; set; }

        public string Symbol { get; set; }

        public string Unit { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public bool? IsAvailable { get; set; }

        public bool? IsActive { get; set; }

        #endregion
    }
}
