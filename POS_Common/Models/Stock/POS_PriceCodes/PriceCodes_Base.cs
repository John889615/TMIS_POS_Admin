using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_PriceCodes
{
  public abstract class PriceCodes_Base
  {
       #region Properties
       
      public int? PriceCodeID { get; set; }

      public string PriceCode { get; set; }

      public string Description { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
