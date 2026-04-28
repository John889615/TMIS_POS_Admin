using System;

namespace POS_Common.Models.Inventory.Custom.SelectProductCombinationsID
{
    public class Req_SelectProductCombinationsID
    {
        #region Properties

        /// <summary>
        /// Maps to @FK_ProductID
        /// </summary>
        public int? FKProductID { get; set; }

        #endregion
    }
}
