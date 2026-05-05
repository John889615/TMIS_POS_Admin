namespace POS_Api.Models.Email;

public class StockRequestApprovalEmail
{
    public int StockRequestID { get; set; }
    public string RefNumber { get; set; } = string.Empty;
    public List<string> To { get; set; } = new();
}

public class StockRequestEmailLine
{
    public string ProductName { get; set; } = string.Empty;
    public decimal? Quantity { get; set; }
    public decimal? ApprovedQuantity { get; set; }
    public bool? IsDeclined { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string ManagerNotes { get; set; } = string.Empty;
}
