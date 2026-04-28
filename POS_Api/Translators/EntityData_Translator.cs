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
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.POS_LocationCurrencies;

namespace POS_Api.Translators
{
   public class EntityData_Translator : EntityData_Custom_SP_Translator
   {
        #region Translators

        //internal static EntityAddress Translate_EntityAddress_Entity(IDataRecord row)
        //{
        //    return new EntityAddress()
        //    {
        //        EntityAddressID = (int?)row["EntityAddressID"],
        //        FK_EntityID = (int?)row["FK_EntityID"],
        //        EntityRecordID = (int?)row["EntityRecordID"],
        //        FK_AddressID = (int?)row["FK_AddressID"],
        //        FK_AddressTypeID = (int?)row["FK_AddressTypeID"],
        //        IsPrimary = (bool?)row["IsPrimary"],
        //        ValidFrom = row["ValidFrom"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidFrom"] : null,
        //        ValidTo = row["ValidTo"].GetType() != typeof(DBNull) ? (DateTime?)row["ValidTo"] : null,
        //    };
        //}

        internal static EntityAddress Translate_EntityAddress_Entity(IDataRecord row)
        {
            return new EntityAddress()
            {
                EntityAddressID = (int?)row["EntityAddressID"],
                FK_AddressID = row["FK_AddressID"].GetType() != typeof(DBNull) ? (int?)row["FK_AddressID"] : null,
                FK_AddressTypeID = row["FK_AddressTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_AddressTypeID"] : null,
                IsPrimary = GetNullableBool(row, "IsPrimary"),
                ValidFrom = GetNullableDate(row, "ValidFrom"),
                ValidTo = GetNullableDate(row, "ValidTo"),
                FK_CountryID = row["FK_CountryID"].GetType() != typeof(DBNull) ? (int?)row["FK_CountryID"] : null,
                FK_ProvinceID = row["FK_ProvinceID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProvinceID"] : null,
                FK_AddressRegionID = row["FK_AddressRegionID"].GetType() != typeof(DBNull) ? (int?)row["FK_AddressRegionID"] : null,
                StreetAddress = GetNullableString(row, "StreetAddress"),
                Locality = GetNullableString(row, "Locality"),
                PostalCode = GetNullableString(row, "PostalCode"),
                Landmark = GetNullableString(row, "Landmark"),
                Latitude = row["Latitude"].GetType() != typeof(DBNull) ? (decimal?)row["Latitude"] : null,
                Longitude = row["Longitude"].GetType() != typeof(DBNull) ? (decimal?)row["Longitude"] : null,
                Notes = GetNullableString(row, "Notes"),
            };
        }

        internal static EntityContact Translate_EntityContact_Entity(IDataRecord row)
        {
            return new EntityContact()
            {
                EntityContactID = (int?)row["EntityContactID"],
                FK_ContactID = row["FK_ContactID"].GetType() != typeof(DBNull) ? (int?)row["FK_ContactID"] : null,
                FK_ContactTypeID = row["FK_ContactTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_ContactTypeID"] : null,
                IsPrimary = GetNullableBool(row, "IsPrimary"),
                IsMarketing = GetNullableBool(row, "IsMarketing"),
                IsEmergency = GetNullableBool(row, "IsEmergency"),
                PreferredContactTime = GetNullableString(row, "PreferredContactTime"),
                PreferredLanguageCode = GetNullableString(row, "PreferredLanguageCode"),
                ValidFrom = GetNullableDate(row, "ValidFrom"),
                ValidTo = GetNullableDate(row, "ValidTo"),
                ContactValue = GetNullableString(row, "ContactValue"),
                FK_DialingCodeID = row["FK_DialingCodeID"].GetType() != typeof(DBNull) ? (int?)row["FK_DialingCodeID"] : null,
                IsVerified = GetNullableBool(row, "IsVerified"),
                VerificationToken = GetNullableString(row, "VerificationToken"),
                VerifiedAt = GetNullableDate(row, "VerifiedAt"),
                Notes = GetNullableString(row, "Notes"),
            };
        }

        internal static LocationCurrencies Translate_LocationCurrency(IDataRecord row)
        {
            return new LocationCurrencies()
            {
                LocationCurrencyID = row["LocationCurrencyID"].GetType() != typeof(DBNull) ? (int?)row["LocationCurrencyID"] : null,
                FK_CurrencyID = (int?)row["FK_CurrencyID"],
                IsActive = (bool?)row["IsActive"],
                Currency = GetNullableString(row, "Currency"),
                Symbol = GetNullableString(row, "Symbol"),
            };
        }
        #endregion

        private static string? GetNullableString(IDataRecord record, string columnName)
        {
            return HasColumn(record, columnName) && record[columnName] != DBNull.Value
                ? (string)record[columnName]
                : null;
        }

        private static bool? GetNullableBool(IDataRecord record, string columnName)
        {
            return HasColumn(record, columnName) && record[columnName] != DBNull.Value
                ? (bool?)record[columnName]
                : null;
        }

        private static DateTime? GetNullableDate(IDataRecord record, string columnName)
        {
            return HasColumn(record, columnName) && record[columnName] != DBNull.Value
                ? (DateTime?)record[columnName]
                : null;
        }

        private static bool HasColumn(IDataRecord record, string columnName)
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


















