using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Logs.POS_Logs;

namespace POS_Api.Translators
{
   public abstract class Logs_Base_Translator
   {
       #region Translators
       
      internal static POS_Log Translate_POS_Log(IDataRecord row)
      {
         return new POS_Log()
         {
            AuditLogID = (int?)row["AuditLogID"],
            Action = (string)row["Action"],
            ItemID = row["ItemID"].GetType() != typeof(DBNull) ? (int?)row["ItemID"] : null,
            Item = row["Item"].GetType() != typeof(DBNull) ? (string)row["Item"] : null,
            FK_UserID = (int?)row["FK_UserID"],
            ActionDate = (DateTime?)row["ActionDate"],
         };
      }

       #endregion

       protected static string GetNullableString(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (string)record[columnName]
               : null;
       }

       protected static bool? GetNullableBool(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (bool?)record[columnName]
               : null;
       }

       protected static DateTime? GetNullableDate(IDataRecord record, string columnName)
       {
           return HasColumn(record, columnName) && record[columnName] != DBNull.Value
               ? (DateTime?)record[columnName]
               : null;
       }

       protected static bool HasColumn(IDataRecord record, string columnName)
       {
           for (int i = 0; i < record.FieldCount; i++)
           {
               if (record.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                   return true;
           }

           return false;
       }
   }
}
