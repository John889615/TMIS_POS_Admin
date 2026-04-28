using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.EntityData.Addresses;
using POS_Common.Models.EntityData.AddressRegions;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.Contacts;
using POS_Common.Models.EntityData.ContactTypes;
using POS_Common.Models.EntityData.Continents;
using POS_Common.Models.EntityData.Countries;
using POS_Common.Models.EntityData.CountryProvinces;
using POS_Common.Models.EntityData.CountrySubregions;
using POS_Common.Models.EntityData.CountryRegions;
using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.DialingCodes;
using POS_Common.Models.EntityData.Entities;
using POS_Common.Models.EntityData.EntityAddresses;
using POS_Common.Models.EntityData.EntityContacts;
using POS_Common.Models.EntityData.Statuses;
using POS_Common.Models.EntityData.StatusGroups;
using POS_Common.Models.EntityData.TimeZones;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.EntityData.POS_PaymentTypes;
using POS_Common.Models.EntityData.TH_BookingHeaders;
using POS_Common.Models.EntityData.Guests;
using POS_Common.Models.EntityData.TH_BookingGuests;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.Models.EntityData.POS_ImageCategories;
using POS_Common.Models.EntityData.POS_PaymentTypeIcons;
using POS_Common.Models.EntityData.POS_Settings;
using POS_Common.Models.EntityData.POS_ExchangeRates;
using POS_Common.Models.EntityData.CurrencyExchangeRates;
using POS_Common.Models.EntityData.GlobalSettings;
using POS_Common.Models.EntityData.POS_SlipTypes;
using POS_Common.Models.EntityData.EntitySettings;

namespace POS_Api.Translators
{
   public abstract class EntityData_Base_Translator
   {
       #region Translators
       
      internal static Address Translate_Address(IDataRecord row)
      {
         return new Address()
         {
            AddressID = (int?)row["AddressID"],
            FK_CountryID = (int?)row["FK_CountryID"],
            FK_ProvinceID = row["FK_ProvinceID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProvinceID"] : null,
            FK_AddressRegionID = row["FK_AddressRegionID"].GetType() != typeof(DBNull) ? (int?)row["FK_AddressRegionID"] : null,
            StreetAddress = row["StreetAddress"].GetType() != typeof(DBNull) ? (string)row["StreetAddress"] : null,
            Locality = row["Locality"].GetType() != typeof(DBNull) ? (string)row["Locality"] : null,
            PostalCode = row["PostalCode"].GetType() != typeof(DBNull) ? (string)row["PostalCode"] : null,
            Landmark = row["Landmark"].GetType() != typeof(DBNull) ? (string)row["Landmark"] : null,
            Latitude = row["Latitude"].GetType() != typeof(DBNull) ? (decimal?)row["Latitude"] : null,
            Longitude = row["Longitude"].GetType() != typeof(DBNull) ? (decimal?)row["Longitude"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static AddressRegion Translate_AddressRegion(IDataRecord row)
      {
         return new AddressRegion()
         {
            AddressRegionID = (int?)row["AddressRegionID"],
            RegionName = (string)row["RegionName"],
            Description = row["Description"].GetType() != typeof(DBNull) ? (string)row["Description"] : null,
            FK_CountryID = row["FK_CountryID"].GetType() != typeof(DBNull) ? (int?)row["FK_CountryID"] : null,
            FK_ProvinceID = row["FK_ProvinceID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProvinceID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static AddressType Translate_AddressType(IDataRecord row)
      {
         return new AddressType()
         {
            AddressTypeID = (int?)row["AddressTypeID"],
            FK_EntityID = (int?)row["FK_EntityID"],
            Type = (string)row["Type"],
            IsRequired = (bool?)row["IsRequired"],
            CanEdit = (bool?)row["CanEdit"],
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static Contact Translate_Contact(IDataRecord row)
      {
         return new Contact()
         {
            ContactID = (int?)row["ContactID"],
            ContactValue = (string)row["ContactValue"],
            FK_ContactTypeID = (int?)row["FK_ContactTypeID"],
            FK_DialingCodeID = row["FK_DialingCodeID"].GetType() != typeof(DBNull) ? (int?)row["FK_DialingCodeID"] : null,
            IsVerified = (bool?)row["IsVerified"],
            VerificationToken = row["VerificationToken"].GetType() != typeof(DBNull) ? (string)row["VerificationToken"] : null,
            VerifiedAt = row["VerifiedAt"].GetType() != typeof(DBNull) ? (DateTime?)row["VerifiedAt"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ContactType Translate_ContactType(IDataRecord row)
      {
         return new ContactType()
         {
            ContactTypeID = (int?)row["ContactTypeID"],
            Type = (string)row["Type"],
            IsPhoneNumberType = (bool?)row["IsPhoneNumberType"],
            IsEmailType = (bool?)row["IsEmailType"],
         };
      }

       
      internal static Continent Translate_Continent(IDataRecord row)
      {
         return new Continent()
         {
            ContinentID = (int?)row["ContinentID"],
            Name = (string)row["Name"],
            ShortCode = row["ShortCode"].GetType() != typeof(DBNull) ? (string)row["ShortCode"] : null,
         };
      }

       
      internal static Country Translate_Country(IDataRecord row)
      {
         return new Country()
         {
            CountryID = (int?)row["CountryID"],
            CountryName = (string)row["CountryName"],
            NativeName = row["NativeName"].GetType() != typeof(DBNull) ? (string)row["NativeName"] : null,
            OfficialName = row["OfficialName"].GetType() != typeof(DBNull) ? (string)row["OfficialName"] : null,
            ISO2Code = (string)row["ISO2Code"],
            ISO3Code = (string)row["ISO3Code"],
            PrimaryLanguageCode = (string)row["PrimaryLanguageCode"],
            NumericCode = row["NumericCode"].GetType() != typeof(DBNull) ? (short?)row["NumericCode"] : null,
            FK_DialingCodeID = row["FK_DialingCodeID"].GetType() != typeof(DBNull) ? (int?)row["FK_DialingCodeID"] : null,
            FK_CurrencyID = row["FK_CurrencyID"].GetType() != typeof(DBNull) ? (int?)row["FK_CurrencyID"] : null,
            FK_CountryRegionID = row["FK_CountryRegionID"].GetType() != typeof(DBNull) ? (int?)row["FK_CountryRegionID"] : null,
            FK_CountrySubregionID = row["FK_CountrySubregionID"].GetType() != typeof(DBNull) ? (int?)row["FK_CountrySubregionID"] : null,
            FK_TimeZoneID = row["FK_TimeZoneID"].GetType() != typeof(DBNull) ? (int?)row["FK_TimeZoneID"] : null,
         };
      }

       
      internal static CountryProvince Translate_CountryProvince(IDataRecord row)
      {
         return new CountryProvince()
         {
            CountryProvinceID = (int?)row["CountryProvinceID"],
            ProvinceName = (string)row["ProvinceName"],
            ISO2Code = (string)row["ISO2Code"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            FK_CountryID = row["FK_CountryID"].GetType() != typeof(DBNull) ? (int?)row["FK_CountryID"] : null,
         };
      }

       
      internal static CountrySubregion Translate_CountrySubregion(IDataRecord row)
      {
         return new CountrySubregion()
         {
            CountrySubregionID = (int?)row["CountrySubregionID"],
            Subregion = (string)row["Subregion"],
            FK_CountryRegionID = (int?)row["FK_CountryRegionID"],
         };
      }

       
      internal static CountryRegion Translate_CountryRegion(IDataRecord row)
      {
         return new CountryRegion()
         {
            CountryRegionID = (int?)row["CountryRegionID"],
            Region = (string)row["Region"],
            FK_ContinentID = (int?)row["FK_ContinentID"],
         };
      }

       
      internal static Currency Translate_Currency(IDataRecord row)
      {
         return new Currency()
         {
            CurrencyID = (int?)row["CurrencyID"],
            Currency = (string)row["Currency"],
            Name = (string)row["Name"],
            ISO2Code = row["ISO2Code"].GetType() != typeof(DBNull) ? (string)row["ISO2Code"] : null,
            Symbol = row["Symbol"].GetType() != typeof(DBNull) ? (string)row["Symbol"] : null,
         };
      }

       
      internal static DialingCode Translate_DialingCode(IDataRecord row)
      {
         return new DialingCode()
         {
            DialingCodeID = (int?)row["DialingCodeID"],
            DialingCode = (string)row["DialingCode"],
            ISO2Code = row["ISO2Code"].GetType() != typeof(DBNull) ? (string)row["ISO2Code"] : null,
         };
      }

       
      internal static Entity Translate_Entity(IDataRecord row)
      {
         return new Entity()
         {
            EntityID = (int?)row["EntityID"],
            Name = (string)row["Name"],
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static EntityAddress Translate_EntityAddress(IDataRecord row)
      {
         return new EntityAddress()
         {
            EntityAddressID = (int?)row["EntityAddressID"],
            FK_EntityID = (int?)row["FK_EntityID"],
            EntityRecordID = (int?)row["EntityRecordID"],
            FK_AddressID = (int?)row["FK_AddressID"],
            FK_AddressTypeID = (int?)row["FK_AddressTypeID"],
            IsPrimary = (bool?)row["IsPrimary"],
            ValidFrom = row["ValidFrom"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidFrom"] : null,
            ValidTo = row["ValidTo"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidTo"] : null,
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static EntityContact Translate_EntityContact(IDataRecord row)
      {
         return new EntityContact()
         {
            EntityContactID = (int?)row["EntityContactID"],
            FK_EntityID = (int?)row["FK_EntityID"],
            EntityRecordID = (int?)row["EntityRecordID"],
            FK_ContactID = (int?)row["FK_ContactID"],
            IsPrimary = (bool?)row["IsPrimary"],
            IsMarketing = (bool?)row["IsMarketing"],
            IsEmergency = (bool?)row["IsEmergency"],
            PreferredContactTime = row["PreferredContactTime"].GetType() != typeof(DBNull) ? (string)row["PreferredContactTime"] : null,
            PreferredLanguageCode = row["PreferredLanguageCode"].GetType() != typeof(DBNull) ? (string)row["PreferredLanguageCode"] : null,
            ValidFrom = row["ValidFrom"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidFrom"] : null,
            ValidTo = row["ValidTo"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidTo"] : null,
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static Status Translate_Status(IDataRecord row)
      {
         return new Status()
         {
            StatusID = (int?)row["StatusID"],
            FK_EntityID = (int?)row["FK_EntityID"],
            FK_StatusGroupID = (int?)row["FK_StatusGroupID"],
            SystemCode = (string)row["SystemCode"],
            DisplayName = (string)row["DisplayName"],
            IsActive = (bool?)row["IsActive"],
            CanEdit = (bool?)row["CanEdit"],
            ShowInUI = (bool?)row["ShowInUI"],
            SortOrder = row["SortOrder"].GetType() != typeof(DBNull) ? (int?)row["SortOrder"] : null,
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static StatusGroup Translate_StatusGroup(IDataRecord row)
      {
         return new StatusGroup()
         {
            StatusGroupID = (int?)row["StatusGroupID"],
            GroupName = (string)row["GroupName"],
            Description = row["Description"].GetType() != typeof(DBNull) ? (string)row["Description"] : null,
         };
      }

       
      internal static _TimeZone Translate__TimeZone(IDataRecord row)
      {
         return new _TimeZone()
         {
            TimeZoneID = (int?)row["TimeZoneID"],
            TimeZone = (string)row["TimeZone"],
            UTCOffset = (string)row["UTCOffset"],
            ObservesDST = (bool?)row["ObservesDST"],
         };
      }

       
      internal static TaxType Translate_TaxType(IDataRecord row)
      {
         return new TaxType()
         {
            TaxTypeID = (int?)row["TaxTypeID"],
            TaxName = (string)row["TaxName"],
            TaxPercentage = (int?)row["TaxPercentage"],
            ValidFrom = row["ValidFrom"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidFrom"] : null,
            ValidTo = row["ValidTo"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidTo"] : null,
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static User Translate_User(IDataRecord row)
      {
         return new User()
         {
            UserID = (int?)row["UserID"],
            Firstname = (string)row["Firstname"],
            Lastname = (string)row["Lastname"],
            Username = (string)row["Username"],
         };
      }

       
      internal static PaymentType Translate_PaymentType(IDataRecord row)
      {
         return new PaymentType()
         {
            PaymentTypeID = (int?)row["PaymentTypeID"],
            FK_PaymentTypeIcon = (int?)row["FK_PaymentTypeIcon"],
            Name = (string)row["Name"],
            IsActive = (bool?)row["IsActive"],
            IsPrimary = (bool?)row["IsPrimary"],
            IsSecondary = (bool?)row["IsSecondary"],
            SettlePayment = (bool?)row["SettlePayment"],
            RequireAdditionalInfo = (bool?)row["RequireAdditionalInfo"],
            RequireElevation = (bool?)row["RequireElevation"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static BookingHeader Translate_BookingHeader(IDataRecord row)
      {
         return new BookingHeader()
         {
            BookingHeaderID = (int?)row["BookingHeaderID"],
            PartyName = (string)row["PartyName"],
            BookingReference = (string)row["BookingReference"],
            FK_AgentDebtorID = row["FK_AgentDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_AgentDebtorID"] : null,
            FK_BranchID = (int?)row["FK_BranchID"],
            FK_DepartmentID = (int?)row["FK_DepartmentID"],
            FK_CurrencyID = (int?)row["FK_CurrencyID"],
            QuoteTotal = (decimal?)row["QuoteTotal"],
            BookingTotal = (decimal?)row["BookingTotal"],
            FK_BookingStatusID = (int?)row["FK_BookingStatusID"],
            TravelStart = row["TravelStart"].GetType() != typeof(DBNull) ? (DateTime?)row["TravelStart"] : null,
            TravelEnd = row["TravelEnd"].GetType() != typeof(DBNull) ? (DateTime?)row["TravelEnd"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static Guest Translate_Guest(IDataRecord row)
      {
         return new Guest()
         {
            GuestID = (int?)row["GuestID"],
            Title = row["Title"].GetType() != typeof(DBNull) ? (string)row["Title"] : null,
            FirstName = (string)row["FirstName"],
            MiddleName = row["MiddleName"].GetType() != typeof(DBNull) ? (string)row["MiddleName"] : null,
            LastName = (string)row["LastName"],
            DateOfBirth = row["DateOfBirth"].GetType() != typeof(DBNull) ? (DateTime?)row["DateOfBirth"] : null,
            Gender = row["Gender"].GetType() != typeof(DBNull) ? (string)row["Gender"] : null,
            Nationality = row["Nationality"].GetType() != typeof(DBNull) ? (string)row["Nationality"] : null,
            PreferredLanguage = row["PreferredLanguage"].GetType() != typeof(DBNull) ? (string)row["PreferredLanguage"] : null,
            SpecialRequests = row["SpecialRequests"].GetType() != typeof(DBNull) ? (string)row["SpecialRequests"] : null,
            LoyaltyNumber = row["LoyaltyNumber"].GetType() != typeof(DBNull) ? (string)row["LoyaltyNumber"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateCreated = row["DateCreated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateCreated"] : null,
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static BookingGuest Translate_BookingGuest(IDataRecord row)
      {
         return new BookingGuest()
         {
            BookingGuestID = (int?)row["BookingGuestID"],
            FK_BookingHeaderID = (int?)row["FK_BookingHeaderID"],
            FK_GuestID = row["FK_GuestID"].GetType() != typeof(DBNull) ? (int?)row["FK_GuestID"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static Image Translate_Image(IDataRecord row)
      {
         return new Image()
         {
            ImageID = (int?)row["ImageID"],
            FK_ImageCategoryID = (int?)row["FK_ImageCategoryID"],
            FK_ItemID = (int?)row["FK_ItemID"],
            FileSystemPath = (string)row["FileSystemPath"],
            RelativePath = (string)row["RelativePath"],
            ImageName = (string)row["ImageName"],
            FileExtension = (string)row["FileExtension"],
            ImageUrl = (string)row["ImageUrl"],
            LocalUrl = (string)row["LocalUrl"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static ImageCategory Translate_ImageCategory(IDataRecord row)
      {
         return new ImageCategory()
         {
            ImageCategoryID = (int?)row["ImageCategoryID"],
            Category = (string)row["Category"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static PaymentTypeIcon Translate_PaymentTypeIcon(IDataRecord row)
      {
         return new PaymentTypeIcon()
         {
            PaymentTypeIconID = (int?)row["PaymentTypeIconID"],
            IconPath = (string)row["IconPath"],
            Category = (string)row["Category"],
            DateCreated = (DateTime?)row["DateCreated"],
         };
      }

       
      internal static Settings Translate_Settings(IDataRecord row)
      {
         return new Settings()
         {
            SettingID = (int?)row["SettingID"],
            CompanyName = (string)row["CompanyName"],
            Email = (string)row["Email"],
            HeadOfficeNo = (string)row["HeadOfficeNo"],
            FK_CurrencyID = (int?)row["FK_CurrencyID"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static ExchangeRate Translate_ExchangeRate(IDataRecord row)
      {
         return new ExchangeRate()
         {
            ExchangeRateID = (int?)row["ExchangeRateID"],
            FK_CurrencyID = (int?)row["FK_CurrencyID"],
            ExchangeRate = (decimal?)row["ExchangeRate"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static CurrencyExchangeRate Translate_CurrencyExchangeRate(IDataRecord row)
      {
         return new CurrencyExchangeRate()
         {
            CurrencyExchangeRateID = (int?)row["CurrencyExchangeRateID"],
            FK_FromCurrencyID = (int?)row["FK_FromCurrencyID"],
            FK_ToCurrencyID = (int?)row["FK_ToCurrencyID"],
            ExchangeRate = (decimal?)row["ExchangeRate"],
            ConversionMethod = (string)row["ConversionMethod"],
            EffectiveDate = (DateTime?)row["EffectiveDate"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static GlobalSettings Translate_GlobalSettings(IDataRecord row)
      {
         return new GlobalSettings()
         {
            GlobalSettingID = (int?)row["GlobalSettingID"],
            Key = (string)row["Key"],
            Value = (string)row["Value"],
            Environment = (string)row["Environment"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = (int?)row["FK_UpdatedUserID"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static SlipType Translate_SlipType(IDataRecord row)
      {
         return new SlipType()
         {
            SlipTypeID = (int?)row["SlipTypeID"],
            SlipType = (string)row["SlipType"],
            SlipCode = (string)row["SlipCode"],
            Description = row["Description"].GetType() != typeof(DBNull) ? (string)row["Description"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static EntitySetting Translate_EntitySetting(IDataRecord row)
      {
         return new EntitySetting()
         {
            EntitySettingID = (int?)row["EntitySettingID"],
            FK_EntityID = (int?)row["FK_EntityID"],
            IsCreditor = row["IsCreditor"].GetType() != typeof(DBNull) ? (bool?)row["IsCreditor"] : null,
            IsDebtor = row["IsDebtor"].GetType() != typeof(DBNull) ? (bool?)row["IsDebtor"] : null,
            IsBranch = row["IsBranch"].GetType() != typeof(DBNull) ? (bool?)row["IsBranch"] : null,
            IsDepartment = row["IsDepartment"].GetType() != typeof(DBNull) ? (bool?)row["IsDepartment"] : null,
            IsUser = row["IsUser"].GetType() != typeof(DBNull) ? (bool?)row["IsUser"] : null,
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       #endregion

       protected static string GetNullableString(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (string)record[columnName]
               : null;
       }

       protected static bool? GetNullableBool(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (bool?)record[columnName]
               : null;
       }

       protected static DateTime? GetNullableDate(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (DateTime?)record[columnName]
               : null;
       }

       protected static bool HasColumn(IDataRecord record, string columnName)
       {
           for (int i = 0; i < record.FieldCount; i++)
           {
               if (record.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                   return true;
           }

           return false;
       }
   }
}
