using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Creditors.CreditorTypeMappings;
using POS_Common.Models.Creditors.CreditorTypes;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.POS_CostCenters;

namespace POS_Api.Translators
{
   public class Creditors_Translator : Creditors_Custom_SP_Translator
   {
        #region Translators

        internal static Creditor Translate_Creditor_Creditor(IDataRecord row)
        {
            return new Creditor()
            {
                CreditorID = (int?)row["CreditorID"],
                ShortCode = (string)row["ShortCode"],
                Name = (string)row["Name"],
                MasterCreditor = GetNullableString(row, "MasterCreditor"),
                IsMasterCreditor = GetNullableBool(row, "IsMasterCreditor"),
                CreditorType = GetNullableString(row, "CreditorType"),
                Status = GetNullableString(row, "Status"),
            };
        }

        internal static Creditor Translate_Creditor_Address(IDataRecord row)
        {
            return new Creditor()
            {
                EntityAddressID = (int?)row["EntityAddressID"],
                EntityID = (int?)row["EntityID"],
                AddressID = (int?)row["AddressID"],
                AddressTypeID = (int?)row["AddressTypeID"],
                IsPrimary = GetNullableBool(row, "IsPrimary"),
                ValidFrom = GetNullableDate(row, "ValidFrom"),
                ValidTo = GetNullableDate(row, "ValidTo"),
                AddressType = GetNullableString(row, "AddressType"),
                IsRequired = GetNullableBool(row, "IsRequired"),
                CanEdit = GetNullableBool(row, "CanEdit"),
                ProvinceName = GetNullableString(row, "ProvinceName"),
                CountryName = GetNullableString(row, "CountryName"),
                StreetAddress = GetNullableString(row, "StreetAddress"),
                Locality = GetNullableString(row, "Locality"),
                PostalCode = GetNullableString(row, "PostalCode"),
                Landmark = GetNullableString(row, "Landmark"),
                Latitude = GetNullableString(row, "Latitude"),
                Longitude = GetNullableString(row, "Longitude"),
                Notes = GetNullableString(row, "Notes"),
                EntityName = GetNullableString(row, "EntityName"),
            };
        }

        internal static Creditor Translate_Creditor_Contact(IDataRecord row)
        {
            return new Creditor()
            {
                EntityContactID = (int?)row["EntityContactID"],
                EntityID = (int?)row["EntityID"],
                ContactID = (int?)row["ContactID"],
                ContactTypeID = (int?)row["ContactTypeID"],
                IsEmergency = GetNullableBool(row, "IsEmergency"),
                IsMarketing = GetNullableBool(row, "IsMarketing"),
                IsPrimary = GetNullableBool(row, "IsPrimary"),
                PreferredContactTime = GetNullableString(row, "PreferredContactTime"),
                PreferredLanguageCode = GetNullableString(row, "PreferredLanguageCode"),
                ValidFrom = GetNullableDate(row, "ValidFrom"),
                ValidTo = GetNullableDate(row, "ValidTo"),
                DialingCode = GetNullableString(row, "DialingCode"),
                ContactValue = GetNullableString(row, "ContactValue"),
                IsVerified = GetNullableBool(row, "IsVerified"),
                VerificationToken = GetNullableString(row, "VerificationToken"),
                VerifiedAt = GetNullableDate(row, "VerifiedAt"),
                Notes = GetNullableString(row, "Notes"),
                IsEmailType = GetNullableBool(row, "IsEmailType"),
                IsPhoneNumberType = GetNullableBool(row, "IsPhoneNumberType"),
                ContactType = GetNullableString(row, "ContactType"),
                EntityName = GetNullableString(row, "EntityName"),
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



