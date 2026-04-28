using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.POS_LocationCurrencies
{
   public class LocationCurrencies : LocationCurrencies_Base
    {
        #region Additional Properties
        
        public string Currency { get; set; }

        public string Symbol { get; set; }
        #endregion
    }
}
