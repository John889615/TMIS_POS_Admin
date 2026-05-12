using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.EntityData.POS_PaymentTypes;
using POS_Common.Models.EntityData.Guests;
using POS_Common.Models.EntityData.BookingGuests;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.Models.EntityData.POS_ImageCategories;
using POS_Common.Models.EntityData.POS_PaymentTypeIcons;
using POS_Common.Models.EntityData.POS_Settings;
using POS_Common.Models.EntityData.POS_ExchangeRates;
using POS_Common.Models.EntityData.CurrencyExchangeRates;
using POS_Common.Models.EntityData.GlobalSettings;
using POS_Common.Models.EntityData.POS_SlipTypes;
using POS_Common.Models.EntityData.BookingHeaders;

namespace POS_Api.Translators
{
   public abstract class EntityData_Base_Translator
   {
       #region Translators
       
      internal static Currency Translate_Currency(IDataRecord row)
      {
         return new Currency()
         {
            CurrencyID = (int?)row["CurrencyID"],
            Currency = (string)row["Currency"],
            Name = (string)row["Name"],
            ISO2Code = row["ISO2Code"].GetType() != typeof(DBNull) ? (string)row["ISO2Code"] : null,
            Symbol = row["Symbol"].GetType() != typeof(DBNull) ? (string)row["Symbol"] : null,
            FK_CreatedUserID = row["FK_CreatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_CreatedUserID"] : null,
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
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
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
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
            FK_GuestID = row["FK_GuestID"].GetType() != typeof(DBNull) ? (int?)row["FK_GuestID"] : null,
            FK_BookingHeaderID = (int?)row["FK_BookingHeaderID"],
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
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static ImageCategory Translate_ImageCategory(IDataRecord row)
      {
         return new ImageCategory()
         {
            ImageCategoryID = (int?)row["ImageCategoryID"],
            Category = (string)row["Category"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
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
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
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
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
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
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
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

       
      internal static BookingHeader Translate_BookingHeader(IDataRecord row)
      {
         return new BookingHeader()
         {
            BookingHeaderID = (int?)row["BookingHeaderID"],
            PartyName = (string)row["PartyName"],
            BookingReference = (string)row["BookingReference"],
            TravelStart = row["TravelStart"].GetType() != typeof(DBNull) ? (DateTime?)row["TravelStart"] : null,
            TravelEnd = row["TravelEnd"].GetType() != typeof(DBNull) ? (DateTime?)row["TravelEnd"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
            IsStaffBooking = (bool?)row["IsStaffBooking"],
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
