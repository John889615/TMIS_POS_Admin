using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POS_Common.ModelsDto.MenuController.MenuItemProduct
{
    public class Req_MenuItemProduct_Reorder
    {
        #region Properties

        [Required]
        public int? FK_MenuItemID { get; set; }

        // Ordered list of POS_MenuItemProductID values. Position in this
        // list becomes DisplayOrder (0-based) on each row. Pass the FULL
        // list for the menu item; rows not mentioned are left untouched.
        [Required]
        [MinLength(1, ErrorMessage = "Provide at least one ID.")]
        public List<int> OrderedIDs { get; set; }
        #endregion
    }
}
