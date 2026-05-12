using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_DocumentSequences
{
  public abstract class DocumentSequence_Base
  {
       #region Properties
       
      public int? DocumentSequenceID { get; set; }

      public string DocumentType { get; set; }

      public string Prefix { get; set; }

      public int? PadLength { get; set; }

      public long? NextNumber { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
