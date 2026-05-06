using POS_Common.ModelsDto.SyncController.FromServer;
using System;
using System.Collections.Generic;

namespace POS_Common.ModelsDto.SyncController.PushBatch
{
    /// <summary>
    /// Request envelope for the unified push-sync endpoint
    /// (POST /api/sync/push/batch). FOH ships entities for one of three
    /// groups per call - MasterRefs, Operational, Transactional.
    /// Refer to spec 2026-05-06-push-sync-redesign-design.md.
    /// </summary>
    public class Req_Push_Batch
    {
        /// <summary>FOH location/site identifier.</summary>
        public int? SiteID { get; set; }

        /// <summary>Unique per-call GUID; server-side dedupe via BatchDedupe.</summary>
        public Guid? BatchID { get; set; }

        /// <summary>One of: "MasterRefs" | "Operational" | "Transactional".</summary>
        public string Group { get; set; }

        /// <summary>The typed entity payloads for this batch group.</summary>
        public Push_Batch_Entities Entities { get; set; }
    }

    /// <summary>
    /// Carries every entity supported across all three groups. FOH only
    /// populates the entities relevant to the current Group; the others
    /// are null. Server inspects which lists are non-null per group.
    /// </summary>
    public class Push_Batch_Entities
    {
        // ---- Group: MasterRefs ----
        public List<Guest_Sync> Guests { get; set; }
        public List<BookingHeader_Sync> BookingHeaders { get; set; }
        public List<Account_Sync> Accounts { get; set; }
        public List<BookingGuest_Sync> BookingGuests { get; set; }
        public List<AccountGuest_Sync> AccountGuests { get; set; }
        public List<Arrival_Sync> Arrivals { get; set; }

        // ---- Group: Operational ----
        public List<Tab_Sync> Tabs { get; set; }
        public List<TabLine_Sync> TabLines { get; set; }
        public List<TabLineCombination_Sync> TabLineCombinations { get; set; }
        public List<TabLineExtra_Sync> TabLineExtras { get; set; }
        public List<TabLineGuest_Sync> TabLineGuests { get; set; }
        public List<TabLinePreparationMethod_Sync> TabLinePreparationMethods { get; set; }
        public List<TablineSubstitute_Sync> TablineSubstitutes { get; set; }
        public List<VoidLog_Sync> VoidLogs { get; set; }
        public List<CashUpHeader_Sync> CashUpHeaders { get; set; }
        public List<CashUpLine_Sync> CashUpLines { get; set; }

        // ---- Group: Transactional ----
        public List<InvoiceHeader_Sync> InvoiceHeaders { get; set; }
        public List<InvoiceTab_Sync> InvoiceTabs { get; set; }
        public List<InvoiceLine_Sync> InvoiceLines { get; set; }
        public List<InvoicePayment_Sync> InvoicePayments { get; set; }
    }
}
