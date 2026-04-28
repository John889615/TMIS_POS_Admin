namespace POS_Api.Models.Email;

public class SiteSilentEmail
{
    public int? SiteId { get; set; }
    public string SiteName { get; set; } = string.Empty;
    public DateTime? LastSeenAt { get; set; }
    public List<string> To { get; set; } = new();
}
