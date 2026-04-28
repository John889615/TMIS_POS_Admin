using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.CostCenterProduct
{
    public class Req_CostCenterProduct_Update
    {
        #region Properties

        public int? POS_CostCenterProductID { get; set; }

        public int? FK_ProductID { get; set; }

        public int? FK_CostCenterID { get; set; }

        public int? FK_TaxTypeID { get; set; }

        public decimal? ItemPrice { get; set; }

        public decimal? Value { get; set; }

        public decimal? Vat { get; set; }

        public int? FK_SellUnitID { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public bool? IsAvailable { get; set; }

        public bool? IsActive { get; set; }
        #endregion
    }
}
