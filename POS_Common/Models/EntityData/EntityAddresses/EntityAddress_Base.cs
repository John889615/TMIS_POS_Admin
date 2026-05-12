using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.EntityAddresses
{
  public abstract class EntityAddress_Base
  {
       #region Properties
       
      public int? EntityAddressID { get; set; }

      public int? FK_EntityID { get; set; }

      public int? EntityRecordID { get; set; }

      public int? FK_AddressID { get; set; }

      public int? FK_AddressTypeID { get; set; }

      public bool? IsPrimary { get; set; }

      public DateTime? ValidFrom { get; set; }

      public DateTime? ValidTo { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
