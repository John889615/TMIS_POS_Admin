using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_DebtorMenuItemProductPrinters
{
  public abstract class DebtorMenuItemProductPrinter_Base
  {
       #region Properties
       
      public int? DebtorMenuItemProductPrinterID { get; set; }

      public int? FK_MenuItemProductID { get; set; }

      public int? FK_PrinterID { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
