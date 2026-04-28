using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController
{
    public class Res_Debtor_List
    {
        #region Properties

        public int? DebtorID { get; set; }

        public int? FK_CurrencyID { get; set; }

        public string Currency { get; set; }

        public string ShortCode { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }
        #endregion
    }
}
