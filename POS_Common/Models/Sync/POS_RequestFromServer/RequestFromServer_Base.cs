using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_RequestFromServer
{
  public abstract class RequestFromServer_Base
  {
       #region Properties
       
      public int? RequestFromServerID { get; set; }

      public string Type { get; set; }

      public DateTime? LastRequestDate { get; set; }

      public int? CallSequence { get; set; }

      public int? SyncFrequency { get; set; }

      public bool? IsActive { get; set; }

      public string ApiUrl { get; set; }
       #endregion
  }
}
