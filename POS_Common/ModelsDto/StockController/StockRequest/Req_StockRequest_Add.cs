using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POS_Common.ModelsDto.StockController.StockRequest
{
    public class Req_StockRequest_Add
    {
        #region Properties

        [Required]
        public int? FK_FromDebtorID { get; set; }

        [Required]
        public int? FK_ToDebtorID { get; set; }

        public string? Notes { get; set; }

        [MinLength(1, ErrorMessage = "At least one line is required.")]
        public List<Req_StockRequest_Add_Line> Lines { get; set; }
        #endregion
    }

    public class Req_StockRequest_Add_Line
    {
        [Required]
        public int? FK_ProductID { get; set; }

        public int? FK_UnitID { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0001", "9999999999", ParseLimitsInInvariantCulture = true)]
        public decimal? Quantity { get; set; }

        public string? Notes { get; set; }
    }
}
