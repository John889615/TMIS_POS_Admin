using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Creditors.Creditors
{
   public class Creditor : Creditor_Base
    {
        #region Additional Properties
        public int? CreditorTypeID { get; set; }

        public int? CreditorTypeMappingID { get; set; }

        public string CreditorType { get; set; }

        public string Status { get; set; }

        public string MasterCreditor { get; set; }

        public int? EntityID { get; set; }
        #endregion

        #region Creditor Address

        public int? EntityAddressID { get; set; }

        public int? AddressTypeID { get; set; }

        public int? AddressID { get; set; }

        public bool? IsPrimary { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public string AddressType { get; set; }

        public bool? IsRequired { get; set; }

        public bool? CanEdit { get; set; }

        public string ProvinceName { get; set; }

        public string CountryName { get; set; }

        public string StreetAddress { get; set; }

        public string Locality { get; set; }

        public string PostalCode { get; set; }

        public string Landmark { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Notes { get; set; }

        public string EntityName { get; set; }
        #endregion

        #region Creditor Contact

        public int? EntityContactID { get; set; }
        public int? ContactID { get; set; }
        public int? ContactTypeID { get; set; }
        public bool? IsEmergency { get; set; }
        public bool? IsMarketing { get; set; }
        public string PreferredContactTime { get; set; }
        public string PreferredLanguageCode { get; set; }
        public string DialingCode { get; set; }
        public string ContactValue { get; set; }
        public bool? IsVerified { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public bool? IsEmailType { get; set; }
        public bool? IsPhoneNumberType { get; set; }
        public string ContactType { get; set; }
        #endregion
    }
}
