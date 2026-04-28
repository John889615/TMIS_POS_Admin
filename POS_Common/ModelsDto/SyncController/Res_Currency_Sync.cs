using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_Currency_Sync
    {
        #region Properties

        public int? CurrencyID { get; set; }

        public string Currency { get; set; }

        public string Name { get; set; }

        public string ISO2Code { get; set; }

        public string Symbol { get; set; }
        #endregion
    }
}
