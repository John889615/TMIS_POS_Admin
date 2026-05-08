using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Sync.POS_SlipPrinters;
using POS_Common.Models.Sync.POS_InvoiceHeaders;
using POS_Common.Models.Sync.POS_InvoiceLines;
using POS_Common.Models.Sync.POS_RequestFromServer;
using POS_Common.Models.Sync.POS_InvoiceTabs;
using POS_Common.Models.Sync.POS_AccountGuests;
using POS_Common.Models.Sync.POS_Accounts;
using POS_Common.Models.Sync.POS_Arrivals;
using POS_Common.Models.Sync.POS_CashUpHeaders;
using POS_Common.Models.Sync.POS_CashUpLines;
using POS_Common.Models.Sync.POS_InvoicePayments;
using POS_Common.Models.Sync.POS_TabLineCombinations;
using POS_Common.Models.Sync.POS_TabLineExtras;
using POS_Common.Models.Sync.POS_TabLineGuests;
using POS_Common.Models.Sync.POS_TabLinePreparationMethods;
using POS_Common.Models.Sync.POS_TabLines;
using POS_Common.Models.Sync.POS_TablineSubstitutes;
using POS_Common.Models.Sync.POS_Tabs;
using POS_Common.Models.Sync.POS_VoidLogs;
using POS_Common.Models.Sync.SiteSyncStatus;
using POS_Common.Models.Sync.Custom.SelectSiteSyncStatus;
using POS_Common.Models.Sync.Custom.SelectSiteSyncStatusForSite;
using POS_Common.Models.Sync.Custom.UpsertSiteSyncStatus;
using POS_Common.Models.Sync.Custom.UpdateLocationsLastSeen;
using POS_Common.Models.Sync.Custom.SelectLocationsSilentSites;
using POS_Common.Models.Sync.Custom.SetLocationsSilentAlert;
using POS_Common.Models.Sync.Custom.SelectLocationRecipients;

namespace POS_Api.Translators
{
   public abstract class Sync_Base_Translator
   {
       #region Translators
       
      internal static SlipPrinter Translate_SlipPrinter(IDataRecord row)
      {
         return new SlipPrinter()
         {
            SlipPrinterID = (int?)row["SlipPrinterID"],
            FK_LocationID = (int?)row["FK_LocationID"],
            CostCenterID = row["CostCenterID"].GetType() != typeof(DBNull) ? (int?)row["CostCenterID"] : null,
            Name = (string)row["Name"],
            Model = (string)row["Model"],
            IpAddress = (string)row["IpAddress"],
            Port = (int?)row["Port"],
            IsDefault = (bool?)row["IsDefault"],
            IsActive = (bool?)row["IsActive"],
            FK_CreatedUserID = (int?)row["FK_CreatedUserID"],
            FK_UpdatedUserID = (int?)row["FK_UpdatedUserID"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            AutoCut = row["AutoCut"].GetType() != typeof(DBNull) ? (bool?)row["AutoCut"] : null,
         };
      }

       
      internal static InvoiceHeader Translate_InvoiceHeader(IDataRecord row)
      {
         return new InvoiceHeader()
         {
            InvoiceHeaderID = (Guid?)row["InvoiceHeaderID"],
            FK_LocationID = (int?)row["FK_LocationID"],
            FK_AccountID = row["FK_AccountID"].GetType() != typeof(DBNull) ? (Guid?)row["FK_AccountID"] : null,
            InvoiceNo = (string)row["InvoiceNo"],
            PartyName = row["PartyName"].GetType() != typeof(DBNull) ? (string)row["PartyName"] : null,
            BookingReference = row["BookingReference"].GetType() != typeof(DBNull) ? (string)row["BookingReference"] : null,
            DiscountTotal = (decimal?)row["DiscountTotal"],
            GratuityTotal = (decimal?)row["GratuityTotal"],
            ExclTotal = (decimal?)row["ExclTotal"],
            VatTotal = (decimal?)row["VatTotal"],
            InclTotal = (decimal?)row["InclTotal"],
            DateCreated = (DateTime?)row["DateCreated"],
            DatePaid = row["DatePaid"].GetType() != typeof(DBNull) ? (DateTime?)row["DatePaid"] : null,
            FK_CurrencyID = (int?)row["FK_CurrencyID"],
            IsPaid = (bool?)row["IsPaid"],
            AmountPaid = (decimal?)row["AmountPaid"],
            AmountDue = (decimal?)row["AmountDue"],
            IsVoided = (bool?)row["IsVoided"],
            VoidReason = row["VoidReason"].GetType() != typeof(DBNull) ? (string)row["VoidReason"] : null,
            VoidedDate = row["VoidedDate"].GetType() != typeof(DBNull) ? (DateTime?)row["VoidedDate"] : null,
            VoidedBy = row["VoidedBy"].GetType() != typeof(DBNull) ? (string)row["VoidedBy"] : null,
         };
      }

       
      internal static InvoiceLine Translate_InvoiceLine(IDataRecord row)
      {
         return new InvoiceLine()
         {
            InvoiceLineID = (Guid?)row["InvoiceLineID"],
            FK_InvoiceTabID = (Guid?)row["FK_InvoiceTabID"],
            FK_ProductID = row["FK_ProductID"].GetType() != typeof(DBNull) ? (int?)row["FK_ProductID"] : null,
            Product = (string)row["Product"],
            Quantity = (decimal?)row["Quantity"],
            LineDiscount = (decimal?)row["LineDiscount"],
            LineTotalExcl = (decimal?)row["LineTotalExcl"],
            LineTotalVat = (decimal?)row["LineTotalVat"],
            LineTotalIncl = (decimal?)row["LineTotalIncl"],
            Guests = row["Guests"].GetType() != typeof(DBNull) ? (string)row["Guests"] : null,
         };
      }

       
      internal static RequestFromServer Translate_RequestFromServer(IDataRecord row)
      {
         return new RequestFromServer()
         {
            RequestFromServerID = (int?)row["RequestFromServerID"],
            Type = (string)row["Type"],
            LastRequestDate = row["LastRequestDate"].GetType() != typeof(DBNull) ? (DateTime?)row["LastRequestDate"] : null,
            CallSequence = (int?)row["CallSequence"],
            SyncFrequency = (int?)row["SyncFrequency"],
            IsActive = (bool?)row["IsActive"],
            ApiUrl = (string)row["ApiUrl"],
         };
      }

       
      internal static InvoiceTab Translate_InvoiceTab(IDataRecord row)
      {
         return new InvoiceTab()
         {
            InvoiceTabID = (Guid?)row["InvoiceTabID"],
            FK_InvoiceHeaderID = (Guid?)row["FK_InvoiceHeaderID"],
            FK_TabID = (Guid?)row["FK_TabID"],
            TabGratuity = (decimal?)row["TabGratuity"],
            TabDiscount = (decimal?)row["TabDiscount"],
            TabTotalExcl = (decimal?)row["TabTotalExcl"],
            TabTotalVat = (decimal?)row["TabTotalVat"],
            TabTotalIncl = (decimal?)row["TabTotalIncl"],
            TabDateOpened = (DateTime?)row["TabDateOpened"],
            TabDateClosed = (DateTime?)row["TabDateClosed"],
         };
      }

       
      internal static AccountGuest Translate_AccountGuest(IDataRecord row)
      {
         return new AccountGuest()
         {
            AccountGuestID = (Guid?)row["AccountGuestID"],
            FK_AccountID = (Guid?)row["FK_AccountID"],
            FK_GuestID = (int?)row["FK_GuestID"],
            IsResponsible = (bool?)row["IsResponsible"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static Account Translate_Account(IDataRecord row)
      {
         return new Account()
         {
            AccountID = (Guid?)row["AccountID"],
            Name = (string)row["Name"],
            FK_BookingHeaderID = (int?)row["FK_BookingHeaderID"],
            IsClosed = (bool?)row["IsClosed"],
            FK_ResponsibleID = (int?)row["FK_ResponsibleID"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = row["DateUpdated"].GetType() != typeof(DBNull) ? (DateTime?)row["DateUpdated"] : null,
         };
      }

       
      internal static Arrival Translate_Arrival(IDataRecord row)
      {
         return new Arrival()
         {
            ArrivalID = (Guid?)row["ArrivalID"],
            FK_GuestID = (int?)row["FK_GuestID"],
            CheckedInBy = row["CheckedInBy"].GetType() != typeof(DBNull) ? (string)row["CheckedInBy"] : null,
            CheckInDate = (DateTime?)row["CheckInDate"],
            CheckedOutBy = row["CheckedOutBy"].GetType() != typeof(DBNull) ? (string)row["CheckedOutBy"] : null,
            CheckOutDate = row["CheckOutDate"].GetType() != typeof(DBNull) ? (DateTime?)row["CheckOutDate"] : null,
         };
      }

       
      internal static CashUpHeader Translate_CashUpHeader(IDataRecord row)
      {
         return new CashUpHeader()
         {
            CashUpHeaderID = (Guid?)row["CashUpHeaderID"],
            FK_CostCenterID = (int?)row["FK_CostCenterID"],
            FK_CurrencyID = (int?)row["FK_CurrencyID"],
            CashUpDate = (DateTime?)row["CashUpDate"],
            CashUpBy = row["CashUpBy"].GetType() != typeof(DBNull) ? (string)row["CashUpBy"] : null,
            TotalSystemAmount = row["TotalSystemAmount"].GetType() != typeof(DBNull) ? (decimal?)row["TotalSystemAmount"] : null,
            TotalCountedAmount = row["TotalCountedAmount"].GetType() != typeof(DBNull) ? (decimal?)row["TotalCountedAmount"] : null,
            TotalVariance = row["TotalVariance"].GetType() != typeof(DBNull) ? (decimal?)row["TotalVariance"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            IsFinalised = (bool?)row["IsFinalised"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static CashUpLine Translate_CashUpLine(IDataRecord row)
      {
         return new CashUpLine()
         {
            CashUpPaymentTypeID = (Guid?)row["CashUpPaymentTypeID"],
            FK_CashUpID = (Guid?)row["FK_CashUpID"],
            FK_PaymentTypeID = (int?)row["FK_PaymentTypeID"],
            SystemAmount = (decimal?)row["SystemAmount"],
            CountedAmount = (decimal?)row["CountedAmount"],
            VarianceAmount = row["VarianceAmount"].GetType() != typeof(DBNull) ? (decimal?)row["VarianceAmount"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static InvoicePayment Translate_InvoicePayment(IDataRecord row)
      {
         return new InvoicePayment()
         {
            InvoicePaymentID = (Guid?)row["InvoicePaymentID"],
            FK_InvoiceID = (Guid?)row["FK_InvoiceID"],
            FK_PaymentTypeID = (int?)row["FK_PaymentTypeID"],
            FK_BaseCurrencyID = (int?)row["FK_BaseCurrencyID"],
            FK_PaymentCurrencyID = (int?)row["FK_PaymentCurrencyID"],
            BaseCurrencyCode = (string)row["BaseCurrencyCode"],
            PaymentCurrencyCode = (string)row["PaymentCurrencyCode"],
            BaseAmountPaid = (decimal?)row["BaseAmountPaid"],
            PaymentAmountPaid = (decimal?)row["PaymentAmountPaid"],
            ExchangeRate = (decimal?)row["ExchangeRate"],
            ExchangeDate = (DateTime?)row["ExchangeDate"],
            DatePaid = (DateTime?)row["DatePaid"],
            StaffName = (string)row["StaffName"],
            IdempotencyKey = (Guid?)row["IdempotencyKey"],
            Reference = row["Reference"].GetType() != typeof(DBNull) ? (string)row["Reference"] : null,
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            IsVoided = (bool?)row["IsVoided"],
            VoidReason = row["VoidReason"].GetType() != typeof(DBNull) ? (string)row["VoidReason"] : null,
            VoidedDate = row["VoidedDate"].GetType() != typeof(DBNull) ? (DateTime?)row["VoidedDate"] : null,
            VoidedBy = row["VoidedBy"].GetType() != typeof(DBNull) ? (string)row["VoidedBy"] : null,
            SignatureBase64 = row["SignatureBase64"].GetType() != typeof(DBNull) ? (string)row["SignatureBase64"] : null,
         };
      }

       
      internal static TabLineCombination Translate_TabLineCombination(IDataRecord row)
      {
         return new TabLineCombination()
         {
            TabLineCombinationID = (Guid?)row["TabLineCombinationID"],
            FK_TabLineID = (Guid?)row["FK_TabLineID"],
            FK_ProductCombinationID = (int?)row["FK_ProductCombinationID"],
            Product = (string)row["Product"],
            Hold = (bool?)row["Hold"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
         };
      }

       
      internal static TabLineExtra Translate_TabLineExtra(IDataRecord row)
      {
         return new TabLineExtra()
         {
            TabLineExtraID = (Guid?)row["TabLineExtraID"],
            FK_TabLineID = (Guid?)row["FK_TabLineID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            Product = (string)row["Product"],
         };
      }

       
      internal static TabLineGuest Translate_TabLineGuest(IDataRecord row)
      {
         return new TabLineGuest()
         {
            TabLineGuestID = (Guid?)row["TabLineGuestID"],
            FK_TabLineID = (Guid?)row["FK_TabLineID"],
            FK_GuestID = (int?)row["FK_GuestID"],
            Note = row["Note"].GetType() != typeof(DBNull) ? (string)row["Note"] : null,
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static TabLinePreparationMethod Translate_TabLinePreparationMethod(IDataRecord row)
      {
         return new TabLinePreparationMethod()
         {
            TabLinePreparationMethodID = (Guid?)row["TabLinePreparationMethodID"],
            FK_TabLineCombinationID = (Guid?)row["FK_TabLineCombinationID"],
            FK_PreparationMethodID = (int?)row["FK_PreparationMethodID"],
            PreparationMethodName = (string)row["PreparationMethodName"],
         };
      }

       
      internal static TabLine Translate_TabLine(IDataRecord row)
      {
         return new TabLine()
         {
            TabLineID = (Guid?)row["TabLineID"],
            FK_TabID = (Guid?)row["FK_TabID"],
            FK_ProductID = (int?)row["FK_ProductID"],
            FK_PriceCodeID = (int?)row["FK_PriceCodeID"],
            FK_PointerID = row["FK_PointerID"].GetType() != typeof(DBNull) ? (Guid?)row["FK_PointerID"] : null,
            UnitCostExcl = (decimal?)row["UnitCostExcl"],
            Vat = (decimal?)row["Vat"],
            UnitCostIncl = (decimal?)row["UnitCostIncl"],
            Product = (string)row["Product"],
            Quantity = (decimal?)row["Quantity"],
            Discount = row["Discount"].GetType() != typeof(DBNull) ? (decimal?)row["Discount"] : null,
            DiscountPerc = row["DiscountPerc"].GetType() != typeof(DBNull) ? (decimal?)row["DiscountPerc"] : null,
            IsVoided = (bool?)row["IsVoided"],
            Notes = row["Notes"].GetType() != typeof(DBNull) ? (string)row["Notes"] : null,
            AutoNotes = row["AutoNotes"].GetType() != typeof(DBNull) ? (string)row["AutoNotes"] : null,
            CreatedBy = (string)row["CreatedBy"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            ServedAs = row["ServedAs"].GetType() != typeof(DBNull) ? (string)row["ServedAs"] : null,
            ServedAsQuantified = row["ServedAsQuantified"].GetType() != typeof(DBNull) ? (bool?)row["ServedAsQuantified"] : null,
            ServedAsQuantity = row["ServedAsQuantity"].GetType() != typeof(DBNull) ? (decimal?)row["ServedAsQuantity"] : null,
            FK_MenuID = row["FK_MenuID"].GetType() != typeof(DBNull) ? (int?)row["FK_MenuID"] : null,
            MenuName = row["MenuName"].GetType() != typeof(DBNull) ? (string)row["MenuName"] : null,
            Gratuity = row["Gratuity"].GetType() != typeof(DBNull) ? (decimal?)row["Gratuity"] : null,
            GratuityPerc = row["GratuityPerc"].GetType() != typeof(DBNull) ? (decimal?)row["GratuityPerc"] : null,
         };
      }

       
      internal static TabLineSubstitute Translate_TabLineSubstitute(IDataRecord row)
      {
         return new TabLineSubstitute()
         {
            TablineSubstituteID = (Guid?)row["TablineSubstituteID"],
            FK_ParentTabLineID = (Guid?)row["FK_ParentTabLineID"],
            FK_SubstituionTabLineID = (Guid?)row["FK_SubstituionTabLineID"],
            FK_ParentTabLineCombinationID = row["FK_ParentTabLineCombinationID"].GetType() != typeof(DBNull) ? (Guid?)row["FK_ParentTabLineCombinationID"] : null,
         };
      }

       
      internal static Tab Translate_Tab(IDataRecord row)
      {
         return new Tab()
         {
            TabID = (Guid?)row["TabID"],
            FK_LocationID = (int?)row["FK_LocationID"],
            FK_AccountID = row["FK_AccountID"].GetType() != typeof(DBNull) ? (Guid?)row["FK_AccountID"] : null,
            FK_CostCenterID = row["FK_CostCenterID"].GetType() != typeof(DBNull) ? (int?)row["FK_CostCenterID"] : null,
            FK_PaymentTypeID = row["FK_PaymentTypeID"].GetType() != typeof(DBNull) ? (int?)row["FK_PaymentTypeID"] : null,
            FK_CurrencyID = row["FK_CurrencyID"].GetType() != typeof(DBNull) ? (int?)row["FK_CurrencyID"] : null,
            TabName = row["TabName"].GetType() != typeof(DBNull) ? (string)row["TabName"] : null,
            TableName = row["TableName"].GetType() != typeof(DBNull) ? (int?)row["TableName"] : null,
            NoOfGuests = row["NoOfGuests"].GetType() != typeof(DBNull) ? (int?)row["NoOfGuests"] : null,
            Gratuity = row["Gratuity"].GetType() != typeof(DBNull) ? (decimal?)row["Gratuity"] : null,
            GratuityPerc = row["GratuityPerc"].GetType() != typeof(DBNull) ? (decimal?)row["GratuityPerc"] : null,
            Discount = row["Discount"].GetType() != typeof(DBNull) ? (decimal?)row["Discount"] : null,
            DiscountPerc = row["DiscountPerc"].GetType() != typeof(DBNull) ? (decimal?)row["DiscountPerc"] : null,
            IsVoided = (bool?)row["IsVoided"],
            VoidNote = row["VoidNote"].GetType() != typeof(DBNull) ? (string)row["VoidNote"] : null,
            IsPaid = (bool?)row["IsPaid"],
            AmountPaid = (decimal?)row["AmountPaid"],
            AmountDue = row["AmountDue"].GetType() != typeof(DBNull) ? (decimal?)row["AmountDue"] : null,
            VatTotal = row["VatTotal"].GetType() != typeof(DBNull) ? (decimal?)row["VatTotal"] : null,
            CurrentExchangeRate = row["CurrentExchangeRate"].GetType() != typeof(DBNull) ? (decimal?)row["CurrentExchangeRate"] : null,
            PaymentDate = row["PaymentDate"].GetType() != typeof(DBNull) ? (DateTime?)row["PaymentDate"] : null,
            ClosedDate = row["ClosedDate"].GetType() != typeof(DBNull) ? (DateTime?)row["ClosedDate"] : null,
            AdditionalInfo = row["AdditionalInfo"].GetType() != typeof(DBNull) ? (string)row["AdditionalInfo"] : null,
            CreatedBy = (string)row["CreatedBy"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            TableNumber = row["TableNumber"].GetType() != typeof(DBNull) ? (int?)row["TableNumber"] : null,
         };
      }

       
      internal static VoidLog Translate_VoidLog(IDataRecord row)
      {
         return new VoidLog()
         {
            VoidLogID = (Guid?)row["VoidLogID"],
            FK_TabID = row["FK_TabID"].GetType() != typeof(DBNull) ? (Guid?)row["FK_TabID"] : null,
            FK_TabLineID = row["FK_TabLineID"].GetType() != typeof(DBNull) ? (Guid?)row["FK_TabLineID"] : null,
            VoidedBy = (string)row["VoidedBy"],
            Note = row["Note"].GetType() != typeof(DBNull) ? (string)row["Note"] : null,
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static SiteSyncStatus Translate_SiteSyncStatus(IDataRecord row)
      {
         return new SiteSyncStatus()
         {
            SiteId = (int?)row["SiteId"],
            TypeName = (string)row["TypeName"],
            LastSuccessAt = row["LastSuccessAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastSuccessAt"] : null,
            LastFailureAt = row["LastFailureAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastFailureAt"] : null,
            ConsecutiveFailures = (int?)row["ConsecutiveFailures"],
            LastErrorMessage = row["LastErrorMessage"].GetType() != typeof(DBNull) ? (string)row["LastErrorMessage"] : null,
            LastReportedAt = (DateTime?)row["LastReportedAt"],
            AlertSentAt = row["AlertSentAt"].GetType() != typeof(DBNull) ? (DateTime?)row["AlertSentAt"] : null,
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
