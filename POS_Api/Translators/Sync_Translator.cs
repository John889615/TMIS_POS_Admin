using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS_Common.Models.Sync.POS_SlipPrinters;
using POS_Common.Models.Sync.POS_InvoiceHeaders;

namespace POS_Api.Translators
{
   public class Sync_Translator : Sync_Custom_SP_Translator
   {
        #region Translators

        internal static InvoiceHeader Translate_InvoiceHeader_BC(IDataRecord row)
        {
            return new InvoiceHeader()
            {
                InvoiceNo = (string)row["InvoiceNo"],
                LocationBC_ID = row["LocationBC_ID"].GetType() != typeof(DBNull) ? (string)row["LocationBC_ID"] : null,
                Quantity = (decimal?)row["Quantity"],
                UnitPriceExcl = (decimal?)row["UnitPriceExcl"],
                ItemNo = (string?)row["ItemNo"],
            };
        }
        #endregion
    }
}

