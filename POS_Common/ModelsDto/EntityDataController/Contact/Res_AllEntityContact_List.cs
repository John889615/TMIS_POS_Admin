using POS_Common.Models.EntityData.EntityContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Contact
{
    public class Res_AllEntityContact_List
    {
        public int? EntityContactID { get; set; }

        public int? FK_ContactTypeID { get; set; }

        public string ContactType { get; set; }

        public int? FK_ContactID { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsMarketing { get; set; }

        public bool? IsEmergency { get; set; }

        public string PreferredContactTime { get; set; }

        public string PreferredLanguageCode { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public string ContactValue { get; set; }

        public int? FK_DialingCodeID { get; set; }

        public string DialingCode { get; set; }

        public bool? IsVerified { get; set; }

        public string VerificationToken { get; set; }

        public DateTime? VerifiedAt { get; set; }

        public string Notes { get; set; }
    }
}
