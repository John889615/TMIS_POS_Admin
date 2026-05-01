namespace POS_Common.ModelsDto.StockController.StockRequestLine
{
    public class Res_StockRequestLine_List
    {
        #region Properties

        public int? POS_StockRequestLineID { get; set; }

        public int? FK_StockRequestID { get; set; }

        public int? FK_ProductID { get; set; }

        public string ProductName { get; set; }

        public int? FK_UnitID { get; set; }

        public string Unit { get; set; }

        public string Symbol { get; set; }

        public decimal? Quantity { get; set; }

        public string Notes { get; set; }

        public string ManagerNotes { get; set; }

        public bool? IsDeclined { get; set; }

        public decimal? ApprovedQuantity { get; set; }
        #endregion
    }
}
