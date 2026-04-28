using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.DebtorProductPrice
{
    public class Req_DebtorProductPrice_Update
    {
        #region Properties

        public int? POS_DebtorProductPriceID { get; set; }

        public int? FK_DebtorProductID { get; set; }

        public int? FK_PriceCodeID { get; set; }

        public int? FK_TaxID { get; set; }

        public decimal? ItemPrice { get; set; }

        public bool? Inclusive { get; set; }

        public decimal? Vat { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }
        #endregion
    }
}
