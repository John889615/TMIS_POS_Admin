using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.Sync.POS_InvoiceHeaders;
using POS_Common.Models.Sync.POS_InvoiceLines;
using POS_Common.Models.Sync.POS_InvoiceTabs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.Sync
{
    public class Sync_Custom_Service : Sync_Custom_SP_Service
    {
        // Note (2026-05-06, Spec 2):
        // The legacy single-row Insert_ID wrappers for InvoiceHeaders, InvoiceLines,
        // and InvoiceTabs were removed. They were dead code (no callers anywhere
        // in the solution) and they referenced columns dropped by Spec 1
        // (InvoiceHeader.IsDiscarded -> IsVoided). The push path now goes through
        // Sync_Service.Process_Push_Batch -> List_Invoice_* (bulk upsert via TVP).
    }
}
