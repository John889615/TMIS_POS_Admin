using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_PaymentTypeIcon_Sync
    {
        #region Properties

        public int? PaymentTypeIconID { get; set; }

        public string IconPath { get; set; }

        public string Category { get; set; }

        public DateTime? DateCreated { get; set; }
        #endregion
    }
}
