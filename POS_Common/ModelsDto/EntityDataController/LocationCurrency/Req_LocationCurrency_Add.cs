using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.LocationCurrency
{
    public class Req_LocationCurrency_Add
    {
        public int? LocationID { get; set; }

        public int? CurrencyID { get; set; }
    }
}
