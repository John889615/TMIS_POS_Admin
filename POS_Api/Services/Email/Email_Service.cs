using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using POS_Api.Models.Email;
using POS_Api.ServiceInterfaces.Email;
using POS_Api.ServiceInterfaces.Logging;
using System.Text;

namespace POS_Api.Services.Email;

public class Email_Service : IEmail_Service
{
    private readonly IConfiguration _config;
    private readonly ILogging_Service _logger;

    public Email_Service(IConfiguration config, ILogging_Service logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task Send_Sync_Failure_Email(SyncFailureEmail email)
    {
        if (email?.To == null || email.To.Count == 0) return;

        var subject = $"[POS Sync] Failure on site {email.SiteName ?? email.SiteId.ToString()}: {email.TypeName}";
        var body = $@"
Site: {email.SiteName} (id {email.SiteId})
Sync type: {email.TypeName}
Consecutive failures: {email.ConsecutiveFailures}
Last successful sync: {(email.LastSuccessAt?.ToString("yyyy-MM-dd HH:mm:ss UTC") ?? "never")}
Error: {Truncate(email.ErrorMessage, 1000)}

Open the BOH Sync Health page on the affected site for details.
";
        await SendAsync(email.To, subject, body);
    }

    public async Task Send_Site_Silent_Email(SiteSilentEmail email)
    {
        if (email?.To == null || email.To.Count == 0) return;

        var lastSeen = email.LastSeenAt?.ToString("yyyy-MM-dd HH:mm:ss UTC") ?? "never";
        var since = email.LastSeenAt.HasValue ? (DateTime.UtcNow - email.LastSeenAt.Value).TotalHours.ToString("F1") : "?";

        var subject = $"[POS Sync] Site silent: {email.SiteName ?? email.SiteId.ToString()}";
        var body = $@"
Site: {email.SiteName} (id {email.SiteId})
Last seen: {lastSeen}
Hours since last sync: {since}

The site has not contacted the central admin in over 2 hours. Investigate connectivity / FE service state.
";
        await SendAsync(email.To, subject, body);
    }

    public async Task Send_Stock_Request_Approval_Email(StockRequestApprovalEmail email)
    {
        if (email?.To == null || email.To.Count == 0) return;

        var subject = $"[Stock Request] {email.RefNumber} awaiting approval";

        var lineLines = new StringBuilder();
        foreach (var line in email.Lines)
        {
            lineLines.Append("  - ").Append(line.ProductName)
                     .Append(" x ").Append(line.Quantity?.ToString("0.####") ?? "?");
            if (!string.IsNullOrWhiteSpace(line.Notes))
                lineLines.Append("  (note: ").Append(line.Notes).Append(')');
            lineLines.AppendLine();
        }

        var body = $@"A stock request needs your approval.

Ref         : {email.RefNumber}
From        : {email.FromDebtorName}
To          : {email.ToDebtorName}
Submitted by: {email.CreatedBy}
Notes       : {(string.IsNullOrWhiteSpace(email.Notes) ? "(none)" : email.Notes)}

Lines requested:
{lineLines}
Open the Stock Request approval page in the admin site to approve, partially approve, or decline.
";
        await SendAsync(email.To, subject, body);
    }

    public async Task Send_Stock_Request_Decided_Email(StockRequestDecidedEmail email)
    {
        if (email?.To == null || email.To.Count == 0) return;

        var subjectTag = email.Outcome switch
        {
            "Approved"          => "Approved",
            "PartiallyApproved" => "Partially Approved",
            "Declined"          => "Declined",
            _                   => email.Outcome
        };

        var subject = $"[Stock Request] {email.RefNumber} - {subjectTag}";

        var lineLines = new StringBuilder();
        foreach (var line in email.Lines)
        {
            var status = line.IsDeclined == true
                ? "DECLINED"
                : (line.ApprovedQuantity ?? 0) < (line.Quantity ?? 0)
                    ? $"PARTIAL: approved {line.ApprovedQuantity?.ToString("0.####") ?? "0"} of {line.Quantity?.ToString("0.####")}"
                    : $"approved {line.ApprovedQuantity?.ToString("0.####") ?? line.Quantity?.ToString("0.####")}";

            lineLines.Append("  - ").Append(line.ProductName).Append("  [").Append(status).Append(']');
            if (!string.IsNullOrWhiteSpace(line.ManagerNotes))
                lineLines.Append("  (manager: ").Append(line.ManagerNotes).Append(')');
            lineLines.AppendLine();
        }

        var nextStep = email.Outcome == "Declined"
            ? "No order required."
            : "Please raise the corresponding Purchase Order in Business Central using the approved quantities below.";

        var body = $@"Stock request {email.RefNumber} has been {subjectTag.ToLower()}.

Ref         : {email.RefNumber}
From        : {email.FromDebtorName}
To          : {email.ToDebtorName}
Decided by  : {email.DecidedBy}
Manager note: {(string.IsNullOrWhiteSpace(email.ManagerNotes) ? "(none)" : email.ManagerNotes)}

Lines:
{lineLines}
{nextStep}
";
        await SendAsync(email.To, subject, body);
    }

    private async Task SendAsync(IList<string> recipients, string subject, string body)
    {
        try
        {
            var fromAddress = _config["Smtp:From"];
            if (string.IsNullOrWhiteSpace(fromAddress))
            {
                _logger.LogService("Email send skipped: Smtp:From not configured");
                return;
            }

            var host = _config["Smtp:Host"];
            if (string.IsNullOrWhiteSpace(host))
            {
                _logger.LogService("Email send skipped: Smtp:Host not configured");
                return;
            }

            if (!int.TryParse(_config["Smtp:Port"], out var port) || port <= 0)
                port = 587;

            var msg = new MimeMessage();
            msg.From.Add(MailboxAddress.Parse(fromAddress));
            foreach (var r in recipients) msg.To.Add(MailboxAddress.Parse(r));
            msg.Subject = subject;
            msg.Body = new TextPart("plain") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
            if (!string.IsNullOrEmpty(_config["Smtp:Username"]))
                await smtp.AuthenticateAsync(_config["Smtp:Username"], _config["Smtp:Password"]);
            await smtp.SendAsync(msg);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            _logger.LogService("Email send failed", ex);
        }
    }

    private static string Truncate(string s, int max) =>
        string.IsNullOrEmpty(s) || s.Length <= max ? s : s.Substring(0, max) + "…";
}
