using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_InvoiceTabs
{
  public abstract class InvoiceTab_Base
  {
       #region Properties
       
      public Guid? InvoiceTabID { get; set; }

      public Guid? FK_InvoiceHeaderID { get; set; }

      public Guid? FK_TabID { get; set; }

      public decimal? TabGratuity { get; set; }

      public decimal? TabDiscount { get; set; }

      public decimal? TabTotalExcl { get; set; }

      public decimal? TabTotalVat { get; set; }

      public decimal? TabTotalIncl { get; set; }

      public DateTime? TabDateOpened { get; set; }

      public DateTime? TabDateClosed { get; set; }
       #endregion
  }
}
