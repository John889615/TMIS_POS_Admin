using POS_Api.Models.Email;

namespace POS_Api.ServiceInterfaces.Email;

public interface IEmail_Service
{
    Task Send_Sync_Failure_Email(SyncFailureEmail email);
    Task Send_Site_Silent_Email(SiteSilentEmail email);
}
