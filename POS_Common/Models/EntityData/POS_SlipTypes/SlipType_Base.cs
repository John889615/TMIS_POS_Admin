using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.POS_SlipTypes
{
  public abstract class SlipType_Base
  {
       #region Properties
       
      public int? SlipTypeID { get; set; }

      public string SlipType { get; set; }

      public string SlipCode { get; set; }

      public string Description { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
