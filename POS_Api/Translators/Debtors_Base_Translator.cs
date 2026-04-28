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
using POS_Common.Models.Debtors.POS_CostCenterTypes;
using POS_Common.Models.Debtors.POS_Locations;
using POS_Common.Models.Debtors.POS_LocationCurrencies;
using POS_Common.Models.Debtors.POS_CostCenterPrinters;

namespace POS_Api.Translators
{
   public abstract class Debtors_Base_Translator
   {
       #region Translators
       
      internal static Branch Translate_Branch(IDataRecord row)
      {
         return new Branch()
         {
            BranchID = (int?)row["BranchID"],
            ShortCode = (string)row["ShortCode"],
            Name = (string)row["Name"],
            FK_StatusID = (int?)row["FK_StatusID"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static Debtor Translate_Debtor(IDataRecord row)
      {
         return new Debtor()
         {
            DebtorID = (int?)row["DebtorID"],
            ShortCode = (string)row["ShortCode"],
            Name = (string)row["Name"],
            FK_MasterDebtorID = row["FK_MasterDebtorID"].GetType() != typeof(DBNull) ? (int?)row["FK_MasterDebtorID"] : null,
            IsMasterDebtor = (bool?)row["IsMasterDebtor"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            BC_ID = row["BC_ID"].GetType() != typeof(DBNull) ? (string)row["BC_ID"] : null,
         };
      }

       
      internal static DebtorTypeMapping Translate_DebtorTypeMapping(IDataRecord row)
      {
         return new DebtorTypeMapping()
         {
            DebtorTypeMappingID = (int?)row["DebtorTypeMappingID"],
            FK_DebtorID = (int?)row["FK_DebtorID"],
            FK_DebtorTypeID = (int?)row["FK_DebtorTypeID"],
            FK_StatusID = (int?)row["FK_StatusID"],
            FK_BranchID = row["FK_BranchID"].GetType() != typeof(DBNull) ? (int?)row["FK_BranchID"] : null,
            FK_DepartmentID = row["FK_DepartmentID"].GetType() != typeof(DBNull) ? (int?)row["FK_DepartmentID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static DebtorType Translate_DebtorType(IDataRecord row)
      {
         return new DebtorType()
         {
            DebtorTypeID = (int?)row["DebtorTypeID"],
            Type = (string)row["Type"],
            Description = (string)row["Description"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static Department Translate_Department(IDataRecord row)
      {
         return new Department()
         {
            DepartmentID = (int?)row["DepartmentID"],
            ShortCode = (string)row["ShortCode"],
            Name = (string)row["Name"],
            FK_StatusID = (int?)row["FK_StatusID"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static CostCenter Translate_CostCenter(IDataRecord row)
      {
         return new CostCenter()
         {
            CostCenterID = (int?)row["CostCenterID"],
            FK_LocationID = (int?)row["FK_LocationID"],
            Name = (string)row["Name"],
            BillingReference = (string)row["BillingReference"],
            FK_StatusID = (int?)row["FK_StatusID"],
            FK_CostCenterTypeID = (int?)row["FK_CostCenterTypeID"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            BC_ID = row["BC_ID"].GetType() != typeof(DBNull) ? (string)row["BC_ID"] : null,
         };
      }

       
      internal static CostCenterType Translate_CostCenterType(IDataRecord row)
      {
         return new CostCenterType()
         {
            CostCenterTypeID = (int?)row["CostCenterTypeID"],
            Name = (string)row["Name"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static Location Translate_Location(IDataRecord row)
      {
         return new Location()
         {
            LocationID = (int?)row["LocationID"],
            FK_CurrencyID = (int?)row["FK_CurrencyID"],
            BC_ID = row["BC_ID"].GetType() != typeof(DBNull) ? (string)row["BC_ID"] : null,
            ShortCode = row["ShortCode"].GetType() != typeof(DBNull) ? (string)row["ShortCode"] : null,
            Name = (string)row["Name"],
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            FK_UpdatedUserID = (int?)row["FK_UpdatedUserID"],
            ContactEmail = row["ContactEmail"].GetType() != typeof(DBNull) ? (string)row["ContactEmail"] : null,
            SupportEmail = row["SupportEmail"].GetType() != typeof(DBNull) ? (string)row["SupportEmail"] : null,
            LastSyncSeenAt = row["LastSyncSeenAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastSyncSeenAt"] : null,
            SilentAlertSentAt = row["SilentAlertSentAt"].GetType() != typeof(DBNull) ? (DateTime?)row["SilentAlertSentAt"] : null,
         };
      }

       
      internal static LocationCurrencies Translate_LocationCurrencies(IDataRecord row)
      {
         return new LocationCurrencies()
         {
            LocationCurrencyID = (int?)row["LocationCurrencyID"],
            FK_CurrencyID = (int?)row["FK_CurrencyID"],
            FK_LocationID = (int?)row["FK_LocationID"],
            IsActive = (bool?)row["IsActive"],
            DateCreated = (DateTime?)row["DateCreated"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            FK_UpdatedUserID = (int?)row["FK_UpdatedUserID"],
         };
      }

       
      internal static CostCenterPrinter Translate_CostCenterPrinter(IDataRecord row)
      {
         return new CostCenterPrinter()
         {
            CostCenterPrinterID = (int?)row["CostCenterPrinterID"],
            FK_CostCenterID = row["FK_CostCenterID"].GetType() != typeof(DBNull) ? (int?)row["FK_CostCenterID"] : null,
            FK_PrinterID = (int?)row["FK_PrinterID"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = row["FK_UpdatedUserID"].GetType() != typeof(DBNull) ? (int?)row["FK_UpdatedUserID"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            FK_InvoiceSlipTypeID = row["FK_InvoiceSlipTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_InvoiceSlipTypeID"] : null,
            FK_TabSlipTypeID = row["FK_TabSlipTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_TabSlipTypeID"] : null,
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
