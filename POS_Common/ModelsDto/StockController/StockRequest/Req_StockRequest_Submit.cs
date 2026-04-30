using System.ComponentModel.DataAnnotations;

namespace POS_Common.ModelsDto.StockController.StockRequest
{
    public class Req_StockRequest_Submit
    {
        [Required]
        public int? POS_StockRequestID { get; set; }
    }
}
