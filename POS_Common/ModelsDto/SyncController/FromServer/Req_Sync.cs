using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController.FromServer
{

    public class Req_Guest_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<Guest_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class Guest_Sync
    {
        public int? GuestID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string PreferredLanguage { get; set; }
        public string SpecialRequests { get; set; }
        public string LoyaltyNumber { get; set; }
        public string Notes { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_BookingGuest_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<BookingGuest_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class BookingGuest_Sync
    {
        public int? BookingGuestID { get; set; }
        public int? FK_BookingHeaderID { get; set; }
        public int? FK_GuestID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_Account_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<Account_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class Account_Sync
    {
        public Guid? AccountID { get; set; }
        public string Name { get; set; }
        public int? FK_BookingHeaderID { get; set; }
        public bool? IsClosed { get; set; }
        public int? FK_ResponsibleID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_AccountGuest_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<AccountGuest_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class AccountGuest_Sync
    {
        public Guid? AccountGuestID { get; set; }
        public Guid? FK_AccountID { get; set; }
        public int? FK_GuestID { get; set; }
        public bool? IsResponsible { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_Arrival_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<Arrival_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class Arrival_Sync
    {
        public Guid? ArrivalID { get; set; }
        public int? FK_GuestID { get; set; }
        public string CheckedInBy { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string CheckedOutBy { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_CashUpHeader_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<CashUpHeader_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class CashUpHeader_Sync
    {
        public Guid? CashUpHeaderID { get; set; }
        public int? FK_CostCenterID { get; set; }
        public int? FK_CurrencyID { get; set; }
        public DateTime? CashUpDate { get; set; }
        public string CashedUpBy { get; set; }
        public decimal? TotalSystemAmount { get; set; }
        public decimal? TotalCountedAmount { get; set; }
        public decimal? TotalVariance { get; set; }
        public string Notes { get; set; }
        public bool? IsFinalised { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_CashUpLine_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<CashUpLine_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class CashUpLine_Sync
    {
        public Guid? CashUpPaymentTypeID { get; set; }
        public Guid? FK_CashUpID { get; set; }
        public int? FK_PaymentTypeID { get; set; }
        public decimal SystemAmount { get; set; }
        public decimal CountedAmount { get; set; }
        public decimal? VarianceAmount { get; set; }
        public string Notes { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_Tab_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<Tab_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class Tab_Sync
    {
        public Guid? TabID { get; set; }
        public int? FK_LocationID { get; set; }
        public string CreatedBy { get; set; }
        public Guid? FK_AccountID { get; set; }
        public int? FK_CostCenterID { get; set; }
        public int? FK_PaymentTypeID { get; set; }
        public string TabName { get; set; }
        public int? TableName { get; set; }
        public int? NoOfGuests { get; set; }
        public decimal? Gratuity { get; set; }
        public int? GratuityPerc { get; set; }
        public decimal? Discount { get; set; }
        public int? DiscountPerc { get; set; }
        public bool? IsVoided { get; set; }
        public string VoidNote { get; set; }
        public bool? IsPaid { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal? AmountDue { get; set; }
        public decimal? VatTotal { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? TableNumber { get; set; }
        public int? FK_CurrencyID { get; set; }
        public decimal? CurrentExchangeRate { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_TabLine_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<TabLine_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class TabLine_Sync
    {
        public Guid? TabLineID { get; set; }
        public Guid? FK_TabID { get; set; }
        public string CreatedBy { get; set; }
        public int? FK_ProductID { get; set; }
        public int? FK_PriceCodeID { get; set; }
        public Guid? FK_PointerID { get; set; }
        public decimal UnitCostExcl { get; set; }
        public decimal Vat { get; set; }
        public decimal UnitCostIncl { get; set; }
        public string Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Discount { get; set; }
        public int? DiscountPerc { get; set; }
        public bool? IsVoided { get; set; }
        public string Notes { get; set; }
        public string AutoNotes { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string ServedAs { get; set; }
        public bool? ServedAsQuantified { get; set; }
        public decimal? ServedAsQuantity { get; set; }
        public int? FK_MenuID { get; set; }
        public string MenuName { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_TabLineCombination_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<TabLineCombination_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class TabLineCombination_Sync
    {
        public Guid? TabLineCombinationID { get; set; }
        public Guid? FK_TabLineID { get; set; }
        public int? FK_ProductCombinationID { get; set; }
        public string Product { get; set; }
        public bool? Hold { get; set; }
        public string Notes { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_TabLineExtra_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<TabLineExtra_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class TabLineExtra_Sync
    {
        public Guid? TabLineExtraID { get; set; }
        public Guid? FK_TabLineID { get; set; }
        public int? FK_ProductID { get; set; }
        public string Product { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_TabLineGuest_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<TabLineGuest_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class TabLineGuest_Sync
    {
        public Guid? TabLineGuestID { get; set; }
        public Guid? FK_TabLineID { get; set; }
        public int? FK_GuestID { get; set; }
        public string Note { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_TabLinePreparationMethod_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<TabLinePreparationMethod_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class TabLinePreparationMethod_Sync
    {
        public Guid? TabLinePreparationMethodID { get; set; }
        public Guid? FK_TabLineCombinationID { get; set; }
        public int? FK_PreparationMethodID { get; set; }
        public string PreparationMethodName { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_TablineSubstitute_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<TablineSubstitute_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class TablineSubstitute_Sync
    {
        public Guid? TablineSubstituteID { get; set; }
        public Guid? FK_ParentTabLineID { get; set; }
        public Guid? FK_SubstituionTabLineID { get; set; }
        public Guid? FK_ParentTabLineCombinationID { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_InvoiceHeader_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<InvoiceHeader_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class InvoiceHeader_Sync
    {
        public Guid? InvoiceHeaderID { get; set; }
        public Guid? FK_AccountID { get; set; }
        public int? FK_LocationID { get; set; }
        public string InvoiceNo { get; set; }
        public string PartyName { get; set; }
        public string BookingReference { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal GratuityTotal { get; set; }
        public decimal ExclTotal { get; set; }
        public decimal VatTotal { get; set; }
        public decimal InclTotal { get; set; }
        public bool? IsDiscarded { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DatePaid { get; set; }
        public bool? SyncedToServer { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_InvoiceTab_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<InvoiceTab_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class InvoiceTab_Sync
    {
        public Guid? InvoiceTabID { get; set; }
        public Guid? FK_InvoiceHeaderID { get; set; }
        public Guid? FK_TabID { get; set; }
        public decimal TabGratuity { get; set; }
        public decimal TabDiscount { get; set; }
        public decimal TabTotalExcl { get; set; }
        public decimal TabTotalVat { get; set; }
        public decimal TabTotalIncl { get; set; }
        public DateTime? TabDateOpened { get; set; }
        public DateTime? TabDateClosed { get; set; }
        public bool? SyncedToServer { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_InvoiceLine_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<InvoiceLine_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class InvoiceLine_Sync
    {
        public Guid? InvoiceLineID { get; set; }
        public Guid? FK_InvoiceTabID { get; set; }
        public string Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal LineDiscount { get; set; }
        public decimal LineTotalExcl { get; set; }
        public decimal LineTotalVat { get; set; }
        public decimal LineTotalIncl { get; set; }
        public string Guests { get; set; }
        public bool? SyncedToServer { get; set; }
        public int? FK_ProductID { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_InvoicePayment_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<InvoicePayment_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class InvoicePayment_Sync
    {
        public Guid? InvoicePaymentID { get; set; }
        public Guid? FK_InvoiceID { get; set; }
        public int? FK_PaymentTypeID { get; set; }
        public int? FK_FromCurrencyID { get; set; }
        public int? FK_ToCurrencyID { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal? FromTotal { get; set; }
        public decimal? ToTotal { get; set; }
        public decimal FromAmountPaid { get; set; }
        public decimal ToAmountPaid { get; set; }
        public decimal? ExchangeRate { get; set; }
        public DateTime? ExchangeDate { get; set; }
        public DateTime? DatePaid { get; set; }
        public string SyncStatus { get; set; }
    }

    public class Req_VoidLog_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<VoidLog_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class VoidLog_Sync
    {
        public Guid? VoidLogID { get; set; }
        public Guid? FK_TabID { get; set; }
        public Guid? FK_TabLineID { get; set; }
        public string VoidedBy { get; set; }
        public string Note { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string SyncStatus { get; set; }
    }
}
