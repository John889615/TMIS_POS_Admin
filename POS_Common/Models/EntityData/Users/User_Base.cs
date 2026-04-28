using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.Users
{
  public abstract class User_Base
  {
       #region Properties
       
      public int? UserID { get; set; }

      public string Firstname { get; set; }

      public string Lastname { get; set; }

      public string Username { get; set; }
       #endregion
  }
}
