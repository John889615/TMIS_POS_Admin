using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Currency
{
    public class Req_Currency_Update
    {
        #region Properties

        public int? CurrencyID { get; set; }

        public string Currency { get; set; }

        public string Name { get; set; }

        public string ISO2Code { get; set; }
        #endregion
    }
}
