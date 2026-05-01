using System;

namespace POS_Common.Models.Stock.POS_StockRequestReviewers
{
    public class StockRequestReviewer
    {
        public int? POS_StockRequestReviewerID { get; set; }

        public int? FK_ToDebtorID { get; set; }

        public int? FK_UserID { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Role { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
