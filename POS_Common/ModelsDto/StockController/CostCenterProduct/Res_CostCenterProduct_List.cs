using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.CostCenterProduct
{
    public class Res_CostCenterProduct_List
    {
        #region Properties

        public int? POS_CostCenterProductID { get; set; }

        public int? FK_ProductID { get; set; }

        public string ProductName { get; set; }

        public int? FK_CostCenterID { get; set; }

        public string CostCenter { get; set; }

        public int? FK_TaxTypeID { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Value { get; set; }

        public decimal? Vat { get; set; }

        public decimal? ItemPrice { get; set; }

        public int? FK_SellUnitID { get; set; }

        public string Symbol { get; set; }

        public string Unit { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public bool? IsAvailable { get; set; }

        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
        #endregion
    }
}
