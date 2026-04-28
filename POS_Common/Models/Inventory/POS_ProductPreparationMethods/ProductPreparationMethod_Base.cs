using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_ProductPreparationMethods
{
  public abstract class ProductPreparationMethod_Base
  {
       #region Properties
       
      public int? ProductPreparationMethodID { get; set; }

      public string ShortCode { get; set; }

      public string Method { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
