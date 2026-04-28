using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.ExchangeRates
{
    public class Res_ExchangeRate_List
    {
        #region Properties

        public int? ExchangeRateID { get; set; }

        public int? FK_CurrencyID { get; set; }

        public string Currency { get; set; }

        public decimal? ExchangeRate { get; set; }
        #endregion
    }
}
