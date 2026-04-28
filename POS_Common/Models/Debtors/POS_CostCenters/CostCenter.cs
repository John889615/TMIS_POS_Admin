using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.POS_CostCenters
{
   public class CostCenter : CostCenter_Base
    {
        #region Additional Properties
        
        public string Debtor {  get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
        #endregion
    }
}
