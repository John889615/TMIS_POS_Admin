using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_SlipPrinters
{
  public abstract class SlipPrinter_Base
  {
       #region Properties
       
      public int? SlipPrinterID { get; set; }

      public int? FK_LocationID { get; set; }

      public int? CostCenterID { get; set; }

      public string Name { get; set; }

      public string Model { get; set; }

      public string IpAddress { get; set; }

      public int? Port { get; set; }

      public bool? IsDefault { get; set; }

      public bool? IsActive { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public bool? AutoCut { get; set; }
       #endregion
  }
}
