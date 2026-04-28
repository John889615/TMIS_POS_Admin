using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.CreditorsController.CreditorContact
{
    public class Req_CreditorContact_Add
    {
        #region Contact

        public string ContactValue { get; set; }

        public int? FK_ContactTypeID { get; set; }

        public int? FK_DialingCodeID { get; set; }

        public bool? IsVerified { get; set; }

        public string VerificationToken { get; set; }

        public DateTime? VerifiedAt { get; set; }

        public string Notes { get; set; }
        #endregion

        #region Entity Contact

        public int? FK_CreditorID { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsMarketing { get; set; }

        public bool? IsEmergency { get; set; }

        public string PreferredContactTime { get; set; }

        public string PreferredLanguageCode { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
        #endregion
    }
}
