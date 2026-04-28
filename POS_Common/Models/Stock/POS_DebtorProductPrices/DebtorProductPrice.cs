using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_DebtorProductPrices
{
   public class DebtorProductPrice : DebtorProductPrice_Base
    {
        #region Additional Properties
        
        public int? FK_ProductID { get; set; }
        #endregion
    }
}
