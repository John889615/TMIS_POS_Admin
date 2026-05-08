using System;

namespace POS_Common.Models.BusinessCentral
{
    /// <summary>
    /// Outcome of one invoice push attempt. The hosted-service sweep
    /// captures one of these per invoice it processes; the manual
    /// controller endpoint returns one for the requested invoice.
    /// See spec: 2026-05-08-bc-invoice-push-design.md.
    /// </summary>
    public class Bc_Push_Result
    {
        public Guid InvoiceHeaderID { get; set; }

        /// <summary>True only if BC posted the invoice and we stamped the BC ID.</summary>
        public bool Pushed { get; set; }

        /// <summary>True if the invoice was already in BC (idempotent no-op).</summary>
        public bool AlreadyPushed { get; set; }

        /// <summary>BC posted-invoice GUID (or "ORDER:&lt;orderId&gt;" placeholder when AutoPost=false).</summary>
        public string BC_InvoiceID { get; set; }

        /// <summary>BC posted-invoice document number (e.g. PS-INV103021). Null until shipAndInvoice succeeds.</summary>
        public string BC_InvoiceNo { get; set; }

        /// <summary>BC sales-order GUID. Stamped immediately after the order header is created so a retry can resume.</summary>
        public string BC_SalesOrderID { get; set; }

        /// <summary>BC sales-order document number (e.g. SO103021).</summary>
        public string BC_SalesOrderNo { get; set; }

        /// <summary>Stamped on failure; otherwise null. Truncated to 4000 chars.</summary>
        public string ErrorMessage { get; set; }
    }

    public class Bc_VoidedInvoice_Row
    {
        public Guid InvoiceHeaderID { get; set; }
        public string InvoiceNo { get; set; }
        public string PartyName { get; set; }
        public string BookingReference { get; set; }
        public decimal? InclTotal { get; set; }
        public DateTime? VoidedDate { get; set; }
        public string VoidedBy { get; set; }
        public string VoidReason { get; set; }
        public string BC_InvoiceID { get; set; }
        public string BC_InvoiceNo { get; set; }
        public string BC_SalesOrderID { get; set; }
        public string BC_SalesOrderNo { get; set; }
        public DateTime? BC_PushedAt { get; set; }
    }

    public class Bc_VoidedInvoices_Response
    {
        public System.Collections.Generic.List<Bc_VoidedInvoice_Row> VoidedAndPushed { get; set; } = new();
        public System.Collections.Generic.List<Bc_VoidedInvoice_Row> VoidedAndNotPushed { get; set; } = new();
    }
}
