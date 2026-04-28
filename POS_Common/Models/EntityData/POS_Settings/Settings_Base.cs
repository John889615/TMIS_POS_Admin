using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.POS_Settings
{
  public abstract class Settings_Base
  {
       #region Properties
       
      public int? SettingID { get; set; }

      public string CompanyName { get; set; }

      public string Email { get; set; }

      public string HeadOfficeNo { get; set; }

      public int? FK_CurrencyID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
