using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.Debtors
{
   public class Debtor : Debtor_Base
   {
        #region Additional Properties

        public int? DebtorTypeMappingID { get; set; }

        public int? DebtorTypeID { get; set; }

        public string DebtorType { get; set; }

        public int? BranchID { get; set; }

        public string Branch { get; set; }

        public int? DepartmentID { get; set; }

        public string Department { get; set; }

        public int? StatusID { get; set; }

        public string Status { get; set; }

        public string MasterDebtor { get; set; }

        public int? EntityID { get; set; }

        #region Debtor Address

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

        #region Debtor Contact

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

        #endregion
    }
}
