using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.POS_ImageCategories
{
  public abstract class ImageCategory_Base
  {
       #region Properties
       
      public int? ImageCategoryID { get; set; }

      public string Category { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
