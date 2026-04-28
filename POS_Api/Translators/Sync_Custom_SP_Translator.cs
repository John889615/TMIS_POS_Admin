using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Sync.Custom.SelectSiteSyncStatus;
using POS_Common.Models.Sync.Custom.SelectSiteSyncStatusForSite;
using POS_Common.Models.Sync.Custom.UpsertSiteSyncStatus;
using POS_Common.Models.Sync.Custom.UpdateLocationsLastSeen;
using POS_Common.Models.Sync.Custom.SelectLocationsSilentSites;
using POS_Common.Models.Sync.Custom.SetLocationsSilentAlert;
using POS_Common.Models.Sync.Custom.SelectLocationRecipients;

namespace POS_Api.Translators
{
   public abstract class Sync_Custom_SP_Translator : Sync_Base_Translator
   {
       #region Custom Stored Procedure Translators

       
      internal static Res_SelectLocationRecipients Translate_SelectLocationRecipients(IDataRecord row)
      {
         return new Res_SelectLocationRecipients()
         {
            SiteId = row["SiteId"].GetType() != typeof(DBNull) ? (int?)row["SiteId"] : null,
            SiteName = row["SiteName"].GetType() != typeof(DBNull) ? (string)row["SiteName"] : null,
            ContactEmail = row["ContactEmail"].GetType() != typeof(DBNull) ? (string)row["ContactEmail"] : null,
            SupportEmail = row["SupportEmail"].GetType() != typeof(DBNull) ? (string)row["SupportEmail"] : null,
         };
      }


       
      internal static Res_SelectLocationsSilentSites Translate_SelectLocationsSilentSites(IDataRecord row)
      {
         return new Res_SelectLocationsSilentSites()
         {
            LocationID = row["LocationID"].GetType() != typeof(DBNull) ? (int?)row["LocationID"] : null,
            LocationName = row["LocationName"].GetType() != typeof(DBNull) ? (string)row["LocationName"] : null,
            LastSyncSeenAt = row["LastSyncSeenAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastSyncSeenAt"] : null,
            ContactEmail = row["ContactEmail"].GetType() != typeof(DBNull) ? (string)row["ContactEmail"] : null,
            SupportEmail = row["SupportEmail"].GetType() != typeof(DBNull) ? (string)row["SupportEmail"] : null,
         };
      }


       
      internal static Res_SelectSiteSyncStatus Translate_SelectSiteSyncStatus(IDataRecord row)
      {
         return new Res_SelectSiteSyncStatus()
         {
            SiteId = row["SiteId"].GetType() != typeof(DBNull) ? (int?)row["SiteId"] : null,
            TypeName = row["TypeName"].GetType() != typeof(DBNull) ? (string)row["TypeName"] : null,
            LastSuccessAt = row["LastSuccessAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastSuccessAt"] : null,
            LastFailureAt = row["LastFailureAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastFailureAt"] : null,
            ConsecutiveFailures = row["ConsecutiveFailures"].GetType() != typeof(DBNull) ? (int?)row["ConsecutiveFailures"] : null,
            LastErrorMessage = row["LastErrorMessage"].GetType() != typeof(DBNull) ? (string)row["LastErrorMessage"] : null,
            LastReportedAt = row["LastReportedAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastReportedAt"] : null,
            AlertSentAt = row["AlertSentAt"].GetType() != typeof(DBNull) ? (DateTime?)row["AlertSentAt"] : null,
         };
      }


       
      internal static Res_SelectSiteSyncStatusForSite Translate_SelectSiteSyncStatusForSite(IDataRecord row)
      {
         return new Res_SelectSiteSyncStatusForSite()
         {
            SiteId = row["SiteId"].GetType() != typeof(DBNull) ? (int?)row["SiteId"] : null,
            TypeName = row["TypeName"].GetType() != typeof(DBNull) ? (string)row["TypeName"] : null,
            LastSuccessAt = row["LastSuccessAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastSuccessAt"] : null,
            LastFailureAt = row["LastFailureAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastFailureAt"] : null,
            ConsecutiveFailures = row["ConsecutiveFailures"].GetType() != typeof(DBNull) ? (int?)row["ConsecutiveFailures"] : null,
            LastErrorMessage = row["LastErrorMessage"].GetType() != typeof(DBNull) ? (string)row["LastErrorMessage"] : null,
            LastReportedAt = row["LastReportedAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastReportedAt"] : null,
            AlertSentAt = row["AlertSentAt"].GetType() != typeof(DBNull) ? (DateTime?)row["AlertSentAt"] : null,
         };
      }


       
      internal static Res_UpdateLocationsLastSeen Translate_UpdateLocationsLastSeen(IDataRecord row)
      {
         return new Res_UpdateLocationsLastSeen()
         {
            SilentAlertCleared = row["SilentAlertCleared"].GetType() != typeof(DBNull) ? (bool?)row["SilentAlertCleared"] : null,
         };
      }


       
      internal static Res_UpsertSiteSyncStatus Translate_UpsertSiteSyncStatus(IDataRecord row)
      {
         return new Res_UpsertSiteSyncStatus()
         {
            SiteId = row["SiteId"].GetType() != typeof(DBNull) ? (int?)row["SiteId"] : null,
            TypeName = row["TypeName"].GetType() != typeof(DBNull) ? (string)row["TypeName"] : null,
            LastSuccessAt = row["LastSuccessAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastSuccessAt"] : null,
            LastFailureAt = row["LastFailureAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastFailureAt"] : null,
            ConsecutiveFailures = row["ConsecutiveFailures"].GetType() != typeof(DBNull) ? (int?)row["ConsecutiveFailures"] : null,
            LastErrorMessage = row["LastErrorMessage"].GetType() != typeof(DBNull) ? (string)row["LastErrorMessage"] : null,
            LastReportedAt = row["LastReportedAt"].GetType() != typeof(DBNull) ? (DateTime?)row["LastReportedAt"] : null,
            AlertSentAt = row["AlertSentAt"].GetType() != typeof(DBNull) ? (DateTime?)row["AlertSentAt"] : null,
         };
      }


       #endregion
   }
}
