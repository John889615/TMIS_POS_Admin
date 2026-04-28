using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Debtors.Branches;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.DebtorTypeMappings;
using POS_Common.Models.Debtors.DebtorTypes;
using POS_Common.Models.Debtors.Departments;
using POS_Common.Models.Debtors.POS_CostCenters;

namespace POS_Api.Translators
{
    public class Debtors_Translator : Debtors_Custom_SP_Translator
    {
        #region Translators

        internal static Debtor Translate_Debtor_Debtor(IDataRecord row)
        {
            return new Debtor()
            {
                DebtorID = (int?)row["DebtorID"],
                DebtorTypeMappingID = row["DebtorTypeMappingID"].GetType() != typeof(DBNull) ? (int?)row["DebtorTypeMappingID"] : null,
                ShortCode = (string)row["ShortCode"],
                Name = (string)row["Name"],
                FK_MasterDebtorID = row["MasterDebtorID"].GetType() != typeof(DBNull) ? (int?)row["MasterDebtorID"] : null,
                MasterDebtor = GetNullableString(row, "MasterDebtor"),
                IsMasterDebtor = GetNullableBool(row, "IsMasterDebtor"),
                DebtorTypeID = row["DebtorTypeID"].GetType() != typeof(DBNull) ? (int?)row["DebtorTypeID"] : null,
                DebtorType = GetNullableString(row, "DebtorType"),
                StatusID = row["StatusID"].GetType() != typeof(DBNull) ? (int?)row["StatusID"] : null,
                Status = GetNullableString(row, "Status"),
                BranchID = row["BranchID"].GetType() != typeof(DBNull) ? (int?)row["BranchID"] : null,
                Branch = GetNullableString(row, "Branch"),
                DepartmentID = row["DepartmentID"].GetType() != typeof(DBNull) ? (int?)row["DepartmentID"] : null,
                Department = GetNullableString(row, "Department"),
            };
        }

        internal static Debtor Translate_Debtor_Address(IDataRecord row)
        {
            return new Debtor()
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

        internal static Debtor Translate_Debtor_Contact(IDataRecord row)
        {
            return new Debtor()
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

        internal static CostCenter Translate_CostCenter(IDataRecord row)
        {
            return new CostCenter()
            {
                CostCenterID = (int?)row["CostCenterID"],
                FK_LocationID = (int?)row["FK_LocationID"],
                Name = GetNullableString(row, "Name"),
                BillingReference = GetNullableString(row, "BillingReference"),
                FK_StatusID = (int?)row["FK_StatusID"],
                FK_CostCenterTypeID = (int?)row["FK_CostCenterTypeID"],
                Debtor = GetNullableString(row, "Debtor"),
                Status = GetNullableString(row, "Status"),
                Type = GetNullableString(row, "Type"),
                DateCreated = GetNullableDate(row, "DateCreated"),
                DateUpdated = GetNullableDate(row, "DateUpdated"),
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