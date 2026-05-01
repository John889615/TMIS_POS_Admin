namespace POS_Api.Models.Email;

public class StockRequestDecidedEmail
{
    public int StockRequestID { get; set; }
    public string RefNumber { get; set; } = string.Empty;
    public string FromDebtorName { get; set; } = string.Empty;
    public string ToDebtorName { get; set; } = string.Empty;
    public string DecidedBy { get; set; } = string.Empty;
    public string ManagerNotes { get; set; } = string.Empty;
    public string Outcome { get; set; } = string.Empty; // Approved | PartiallyApproved | Declined
    public List<StockRequestEmailLine> Lines { get; set; } = new();
    public List<string> To { get; set; } = new();
}
