using POS_Common.Models;
using POS_Common.Models.BusinessCentral;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.BusinessCentral
{
    /// <summary>
    /// Pushes paid POS invoices to Business Central as auto-posted
    /// Sales Orders (Microsoft.NAV.shipAndInvoice). Stock decrements
    /// via the Posted Sales Shipment that BC creates as a side-effect.
    ///
    /// Single entry point shared by the periodic sweep
    /// (BcPushHostedService) and the manual controller endpoint
    /// (POST /api/bc/push/invoice/{id}).
    ///
    /// See spec: 2026-05-08-bc-invoice-push-design.md.
    /// </summary>
    public interface IBc_Push_Service
    {
        /// <summary>
        /// Pushes a single paid POS invoice to BC. Idempotent:
        /// if the invoice already has a BC_InvoiceID stamped, the
        /// method short-circuits and returns AlreadyPushed=true.
        /// On failure, stamps BC_LastError and re-throws so the
        /// caller can surface the error.
        /// </summary>
        Task<ApiResponse<Bc_Push_Result>> PushInvoiceAsync(Guid invoiceHeaderId, CancellationToken token = default);

        /// <summary>
        /// Returns voided invoices for the Admin UI grid, partitioned
        /// by whether they were already pushed to BC.
        /// </summary>
        Task<ApiResponse<Bc_VoidedInvoices_Response>> GetVoidedInvoicesAsync(CancellationToken token = default);
    }
}
