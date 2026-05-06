using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_InvoicePayments
{
  public abstract class InvoicePayment_Base
  {
       #region Properties
       
      public Guid? InvoicePaymentID { get; set; }

      public Guid? FK_InvoiceID { get; set; }

      public int? FK_PaymentTypeID { get; set; }

      public int? FK_BaseCurrencyID { get; set; }

      public int? FK_PaymentCurrencyID { get; set; }

      public string BaseCurrencyCode { get; set; }

      public string PaymentCurrencyCode { get; set; }

      public decimal? BaseAmountPaid { get; set; }

      public decimal? PaymentAmountPaid { get; set; }

      public decimal? ExchangeRate { get; set; }

      public DateTime? ExchangeDate { get; set; }

      public DateTime? DatePaid { get; set; }

      public string StaffName { get; set; }

      public Guid? IdempotencyKey { get; set; }

      public string Reference { get; set; }

      public string Notes { get; set; }

      public bool? IsVoided { get; set; }

      public string VoidReason { get; set; }

      public DateTime? VoidedDate { get; set; }

      public string VoidedBy { get; set; }

      public string SignatureBase64 { get; set; }
       #endregion
  }
}
