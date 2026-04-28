using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Creditors.CreditorTypeMappings;
using POS_Common.Models.Creditors.CreditorTypes;

namespace POS_Api.Translators
{
   public abstract class Creditors_Base_Translator
   {
       #region Translators
       
      internal static Creditor Translate_Creditor(IDataRecord row)
      {
         return new Creditor()
         {
            CreditorID = (int?)row["CreditorID"],
            ShortCode = (string)row["ShortCode"],
            Name = (string)row["Name"],
            FK_MasterCreditorID = row["FK_MasterCreditorID"].GetType() != typeof(DBNull) ? (int?)row["FK_MasterCreditorID"] : null,
            IsMasterCreditor = (bool?)row["IsMasterCreditor"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
            BC_ID = row["BC_ID"].GetType() != typeof(DBNull) ? (string)row["BC_ID"] : null,
         };
      }

       
      internal static CreditorTypeMapping Translate_CreditorTypeMapping(IDataRecord row)
      {
         return new CreditorTypeMapping()
         {
            CreditorTypeMappingID = (int?)row["CreditorTypeMappingID"],
            FK_CreditorID = (int?)row["FK_CreditorID"],
            FK_CreditorTypeID = (int?)row["FK_CreditorTypeID"],
            FK_StatusID = (int?)row["FK_StatusID"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
         };
      }

       
      internal static CreditorType Translate_CreditorType(IDataRecord row)
      {
         return new CreditorType()
         {
            CreditorTypeID = (int?)row["CreditorTypeID"],
            Type = (string)row["Type"],
            Description = (string)row["Description"],
            DateCreated = (DateTime?)row["DateCreated"],
            DateUpdated = (DateTime?)row["DateUpdated"],
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
