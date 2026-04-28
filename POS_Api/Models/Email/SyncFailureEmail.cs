namespace POS_Api.Models.Email;

public class SyncFailureEmail
{
    public int SiteId { get; set; }
    public string SiteName { get; set; } = string.Empty;
    public string TypeName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public int ConsecutiveFailures { get; set; }
    public DateTime? LastSuccessAt { get; set; }
    public List<string> To { get; set; } = new();
}
