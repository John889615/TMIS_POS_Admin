using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.StockController.PriceCode
{
    public class Req_PriceCode_Add
    {
        #region Properties

        public string PriceCode { get; set; }

        public string Description { get; set; }

        public bool? IsActive { get; set; }
        #endregion
    }
}
