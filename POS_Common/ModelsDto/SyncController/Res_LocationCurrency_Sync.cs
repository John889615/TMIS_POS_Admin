using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_LocationCurrency_Sync
    {
        #region Properties

        public int? LocationCurrencyID { get; set; }

        public int? FK_LocationID { get; set; }

        public int? FK_CurrencyID { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
