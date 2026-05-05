using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POS_Common.ModelsDto.MenuController.DebtorMenuItemProduct
{
    public class Req_DebtorMenuItemProduct_Reorder
    {
        #region Properties

        [Required]
        public int? FK_DebtorMenuItemID { get; set; }

        // Ordered list of POS_DebtorMenuItemProductID (a.k.a. MenuItemProductID
        // on POS_DebtorMenuItemProducts) values. Position in this list becomes
        // DisplayOrder (0-based) on each row. Pass the FULL list for the
        // debtor menu item; rows not mentioned are left untouched.
        [Required]
        [MinLength(1, ErrorMessage = "Provide at least one ID.")]
        public List<int> OrderedIDs { get; set; }
        #endregion
    }
}
