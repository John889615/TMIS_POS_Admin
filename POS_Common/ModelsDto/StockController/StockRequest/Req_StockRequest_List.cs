namespace POS_Common.ModelsDto.StockController.StockRequest
{
    public class Req_StockRequest_List
    {
        public int? ToDebtorID { get; set; }

        public int? FromDebtorID { get; set; }

        public int? OrderStatusID { get; set; }
    }
}
