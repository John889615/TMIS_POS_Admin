using System;

namespace POS_Common.Models.Inventory.Custom.SelectProductCombinationsID
{
    public class Res_SelectProductCombinationsID
    {
        #region Properties

        /// <summary>
        /// Maps to ProductCombinationID
        /// </summary>
        public int? ProductCombinationID { get; set; }

        /// <summary>
        /// Maps to FK_ProductID
        /// </summary>
        public int? FKProductID { get; set; }

        /// <summary>
        /// Maps to FK_ProductItemID
        /// </summary>
        public int? FKProductItemID { get; set; }

        /// <summary>
        /// Maps to IsQuantified
        /// </summary>
        public bool? IsQuantified { get; set; }

        /// <summary>
        /// Maps to Quantity
        /// </summary>
        public decimal? Quantity { get; set; }

        /// <summary>
        /// Maps to IsOptional
        /// </summary>
        public bool? IsOptional { get; set; }

        /// <summary>
        /// Maps to IsExtraCharge
        /// </summary>
        public bool? IsExtraCharge { get; set; }

        /// <summary>
        /// Maps to DisplayOrder
        /// </summary>
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// Maps to FK_CreatedUserID
        /// </summary>
        public int? FKCreatedUserID { get; set; }

        /// <summary>
        /// Maps to FK_UpdatedUserID
        /// </summary>
        public int? FKUpdatedUserID { get; set; }

        /// <summary>
        /// Maps to DateCreated
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Maps to DateUpdated
        /// </summary>
        public DateTime? DateUpdated { get; set; }

        #endregion
    }
}
