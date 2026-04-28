using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_CostCenterProducts
{
   public class CostCenterProduct : CostCenterProduct_Base
    {
        #region Additional Properties

        public string ProductName { get; set; }

        public string CostCenter { get; set; }

        public int? Rate { get; set; }

        public string Symbol { get; set; }

        public string Unit { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
        #endregion
    }
}
