using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.AddressTypes
{
  public abstract class AddressType_Base
  {
       #region Properties
       
      public int? AddressTypeID { get; set; }

      public int? FK_EntityID { get; set; }

      public string Type { get; set; }

      public bool? IsRequired { get; set; }

      public bool? CanEdit { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
