using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_Tabs
{
  public abstract class Tab_Base
  {
       #region Properties
       
      public Guid? TabID { get; set; }

      public int? FK_LocationID { get; set; }

      public Guid? FK_AccountID { get; set; }

      public int? FK_CostCenterID { get; set; }

      public int? FK_PaymentTypeID { get; set; }

      public int? FK_CurrencyID { get; set; }

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

      public decimal? AmountPaid { get; set; }

      public decimal? AmountDue { get; set; }

      public decimal? VatTotal { get; set; }

      public decimal? CurrentExchangeRate { get; set; }

      public DateTime? PaymentDate { get; set; }

      public DateTime? ClosedDate { get; set; }

      public string AdditionalInfo { get; set; }

      public string CreatedBy { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
