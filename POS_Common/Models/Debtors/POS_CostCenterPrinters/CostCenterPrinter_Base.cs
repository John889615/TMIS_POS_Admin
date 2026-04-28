using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.POS_CostCenterPrinters
{
  public abstract class CostCenterPrinter_Base
  {
       #region Properties
       
      public int? CostCenterPrinterID { get; set; }

      public int? FK_CostCenterID { get; set; }

      public int? FK_PrinterID { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_InvoiceSlipTypeID { get; set; }

      public int? FK_TabSlipTypeID { get; set; }
       #endregion
  }
}
