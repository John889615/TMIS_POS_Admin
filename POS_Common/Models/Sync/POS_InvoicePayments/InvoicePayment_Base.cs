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

      public int? FK_FromCurrencyID { get; set; }

      public int? FK_ToCurrencyID { get; set; }

      public string FromCurrency { get; set; }

      public string ToCurrency { get; set; }

      public decimal? FromTotal { get; set; }

      public decimal? ToTotal { get; set; }

      public decimal? FromAmountPaid { get; set; }

      public decimal? ToAmountPaid { get; set; }

      public decimal? ExchangeRate { get; set; }

      public DateTime? ExchangeDate { get; set; }

      public DateTime? DatePaid { get; set; }
       #endregion
  }
}
