using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController
{
    public class Req_Debtor_Add
    {
        #region Properties

        public string ShortCode { get; set; }

        public string Name { get; set; }

        public int? FK_CurrencyID { get; set; }

        public bool? IsActive { get; set; }
        #endregion
    }
}
