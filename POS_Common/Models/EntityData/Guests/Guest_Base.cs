using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.Guests
{
  public abstract class Guest_Base
  {
       #region Properties
       
      public int? GuestID { get; set; }

      public string Title { get; set; }

      public string FirstName { get; set; }

      public string MiddleName { get; set; }

      public string LastName { get; set; }

      public DateTime? DateOfBirth { get; set; }

      public string Gender { get; set; }

      public string Nationality { get; set; }

      public string PreferredLanguage { get; set; }

      public string SpecialRequests { get; set; }

      public string LoyaltyNumber { get; set; }

      public string Notes { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
