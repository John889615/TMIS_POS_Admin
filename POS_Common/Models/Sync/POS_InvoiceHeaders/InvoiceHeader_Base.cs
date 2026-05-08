using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_InvoiceHeaders
{
  public abstract class InvoiceHeader_Base
  {
       #region Properties
       
      public Guid? InvoiceHeaderID { get; set; }

      public int? FK_LocationID { get; set; }

      public Guid? FK_AccountID { get; set; }

      public string InvoiceNo { get; set; }

      public string PartyName { get; set; }

      public string BookingReference { get; set; }

      public decimal? DiscountTotal { get; set; }

      public decimal? GratuityTotal { get; set; }

      public decimal? ExclTotal { get; set; }

      public decimal? VatTotal { get; set; }

      public decimal? InclTotal { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DatePaid { get; set; }

      public int? FK_CurrencyID { get; set; }

      public bool? IsPaid { get; set; }

      public decimal? AmountPaid { get; set; }

      public decimal? AmountDue { get; set; }

      public bool? IsVoided { get; set; }

      public string VoidReason { get; set; }

      public DateTime? VoidedDate { get; set; }

      public string VoidedBy { get; set; }
       #endregion
  }
}
