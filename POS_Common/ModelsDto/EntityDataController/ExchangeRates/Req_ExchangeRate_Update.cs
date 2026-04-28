using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.ExchangeRates
{
    public class Req_ExchangeRate_Update
    {
        #region Properties

        public int? ExchangeRateID { get; set; }

        public int? FK_CurrencyID { get; set; }

        public decimal? ExchangeRate { get; set; }
        #endregion
    }
}
