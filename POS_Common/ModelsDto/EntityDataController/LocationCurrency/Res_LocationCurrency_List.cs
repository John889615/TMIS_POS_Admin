using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.LocationCurrency
{
    public class Res_LocationCurrency_List
    {
        public int? LocationCurrencyID { get; set; }

        public int? CurrencyID { get; set; }

        public string Currency { get; set; }

        public string Symbol { get; set; }

        public bool? IsActive { get; set; }
    }
}
