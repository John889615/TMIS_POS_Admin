using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POS_Common.ModelsDto.StockController.StockRequest
{
    public class Req_StockRequest_Approve
    {
        [Required]
        public int? POS_StockRequestID { get; set; }

        public string ManagerNotes { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Provide a decision for at least one line.")]
        public List<Req_StockRequest_Approve_Line> LineDecisions { get; set; }
    }

    public class Req_StockRequest_Approve_Line
    {
        [Required]
        public int? POS_StockRequestLineID { get; set; }

        // null/0 + IsDeclined=true => declined
        // < requested qty                    => partial
        // = requested qty                    => fully approved
        public decimal? ApprovedQuantity { get; set; }

        public bool? IsDeclined { get; set; }

        public string ManagerNotes { get; set; }
    }
}
