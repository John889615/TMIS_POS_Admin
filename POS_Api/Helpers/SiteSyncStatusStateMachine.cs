using System;

namespace POS_Api.Helpers;

public class SiteSyncStatusState
{
    public int ConsecutiveFailures { get; set; }
    public DateTime? LastSuccessAt { get; set; }
    public DateTime? LastFailureAt { get; set; }
    public string LastErrorMessage { get; set; }
    public DateTime? AlertSentAt { get; set; }
}

public static class SiteSyncStatusStateMachine
{
    public static (SiteSyncStatusState next, bool shouldEmail) Apply(
        SiteSyncStatusState current,
        string status,
        string errorMessage,
        DateTime observedAt,
        int threshold)
    {
        var next = new SiteSyncStatusState
        {
            ConsecutiveFailures = current.ConsecutiveFailures,
            LastSuccessAt = current.LastSuccessAt,
            LastFailureAt = current.LastFailureAt,
            LastErrorMessage = current.LastErrorMessage,
            AlertSentAt = current.AlertSentAt,
        };

        if (string.Equals(status, "success", StringComparison.OrdinalIgnoreCase))
        {
            next.ConsecutiveFailures = 0;
            next.LastSuccessAt = observedAt;
            next.LastErrorMessage = null;
            next.AlertSentAt = null;
            return (next, false);
        }

        next.ConsecutiveFailures = current.ConsecutiveFailures + 1;
        next.LastFailureAt = observedAt;
        next.LastErrorMessage = errorMessage;

        var shouldEmail = next.ConsecutiveFailures >= threshold && next.AlertSentAt == null;
        if (shouldEmail) next.AlertSentAt = observedAt;
        return (next, shouldEmail);
    }
}
