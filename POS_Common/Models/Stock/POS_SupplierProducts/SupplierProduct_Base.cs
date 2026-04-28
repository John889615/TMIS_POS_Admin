using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_SupplierProducts
{
  public abstract class SupplierProduct_Base
  {
       #region Properties
       
      public int? SupplierProductID { get; set; }

      public int? FK_CreditorID { get; set; }

      public int? FK_ProductID { get; set; }

      public int? FK_DebtorID { get; set; }

      public string SupplierItemCode { get; set; }

      public int? FK_BaseUnitID { get; set; }

      public int? FK_PacUnitID { get; set; }

      public decimal? UnitsPerPack { get; set; }

      public decimal? Quantity { get; set; }

      public bool? TrackPackLevel { get; set; }

      public decimal? LastPurchasePrice { get; set; }

      public DateTime? LastPurchaseDate { get; set; }

      public int? FK_TaxTypeID { get; set; }

      public int? LeadTimeDays { get; set; }

      public int? IsPreferred { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateAdded { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
