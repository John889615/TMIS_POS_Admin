using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_PaymentType_Sync
    {
        public int? PaymentTypeID { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsSecondary { get; set; }

        public bool? SettlePayment { get; set; }

        public bool? RequireAdditionalInfo { get; set; }

        public bool? RequireElevation { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int? FK_PaymentTypeIconID { get; set; }
    }
}
