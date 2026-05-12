using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.Currencies
{
  public abstract class Currency_Base
  {
       #region Properties
       
      public int? CurrencyID { get; set; }

      public string Currency { get; set; }

      public string Name { get; set; }

      public string ISO2Code { get; set; }

      public string Symbol { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
