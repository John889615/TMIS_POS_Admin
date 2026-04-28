using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_DebtorProducts
{
   public class DebtorProduct : DebtorProduct_Base
    {
        #region Additional Properties
        
        public string ProductName { get; set; }

        public string Debtor { get; set; }

        public string Symbol { get; set; }

        public string Unit { get; set; }
        #endregion
    }
}
