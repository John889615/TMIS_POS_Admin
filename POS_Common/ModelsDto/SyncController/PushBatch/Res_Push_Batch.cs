using System;
using System.Collections.Generic;

namespace POS_Common.ModelsDto.SyncController.PushBatch
{
    /// <summary>
    /// Result envelope returned by POST /api/sync/push/batch.
    /// One Entity_Result per processed entity inside the requested group.
    /// FOH consumes Results to mark accepted rows synced and log rejects.
    /// </summary>
    public class Res_Push_Batch
    {
        public Guid? BatchID { get; set; }

        public string Group { get; set; }

        /// <summary>Keyed by entity name (e.g. "Tabs", "InvoiceHeaders").</summary>
        public Dictionary<string, Entity_Result> Results { get; set; } = new();
    }

    public class Entity_Result
    {
        public int Accepted { get; set; }

        public int Rejected { get; set; }

        public List<Entity_Error> Errors { get; set; } = new();
    }

    public class Entity_Error
    {
        /// <summary>Entity primary-key identifier (GUID or INT, stringified).</summary>
        public string Id { get; set; }

        public string Reason { get; set; }
    }
}
