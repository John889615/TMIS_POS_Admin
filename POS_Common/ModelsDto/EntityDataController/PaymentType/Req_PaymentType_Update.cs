using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.PaymentType
{
    public class Req_PaymentType_Update
    {
        #region Properties

        public int? PaymentTypeID { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsSecondary { get; set; }

        public int? FK_PaymentTypeIconID { get; set; }

        public bool? SettlePayment { get; set; }

        public bool? RequireElevation { get; set; }

        public bool? RequireAdditionalInfo { get; set; }
        #endregion
    }
}
