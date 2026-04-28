using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Contact
{
    public class Req_Contact_Update
    {
        #region Properties

        public int? ContactID { get; set; }

        public string ContactValue { get; set; }

        public int? FK_ContactTypeID { get; set; }

        public int? FK_DialingCodeID { get; set; }

        public bool? IsVerified { get; set; }

        public string VerificationToken { get; set; }

        public DateTime? VerifiedAt { get; set; }

        public string Notes { get; set; }
        #endregion
    }
}
