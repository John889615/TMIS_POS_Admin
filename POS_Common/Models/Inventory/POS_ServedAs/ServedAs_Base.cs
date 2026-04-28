using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_ServedAs
{
  public abstract class ServedAs_Base
  {
       #region Properties
       
      public int? ServedAsID { get; set; }

      public string ServedAsType { get; set; }

      public string Name { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
