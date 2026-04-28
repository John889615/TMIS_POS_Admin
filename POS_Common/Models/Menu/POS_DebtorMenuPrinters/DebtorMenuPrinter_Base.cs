using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_DebtorMenuPrinters
{
  public abstract class DebtorMenuPrinter_Base
  {
       #region Properties
       
      public int? DebtorMenuPrinterID { get; set; }

      public int? FK_DebtorMenuID { get; set; }

      public int? FK_PrinterID { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_OrderSlipTypeID { get; set; }
       #endregion
  }
}
