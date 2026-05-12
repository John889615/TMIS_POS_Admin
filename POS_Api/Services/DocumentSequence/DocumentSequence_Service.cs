using Microsoft.Data.SqlClient;
using Serilog;
using System;
using System.Data;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.DocumentSequence
{
    public static class DocumentSequence_Service
    {
        // Document-type keys used in POS_DocumentSequences.DocumentType.
        // Add a constant per document type that needs a generated reference.
        public static class DocumentTypes
        {
            public const string StockRequest  = "StockRequest";
            public const string PurchaseOrder = "PurchaseOrder";
        }

        // Calls POS_DocumentSequences_Next on the supplied connection (usually
        // inside an outer TransactionScope) and returns the freshly-minted
        // reference, e.g. "SR00001". Returns null on any failure - callers
        // should treat null as a hard error and abort the parent operation.
        public static async Task<string> Get_Next_Reference(string documentType, SqlConnection sqlConn)
        {
            try
            {
                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "POS_DocumentSequences_Next",
                    new SqlParameter { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@DocumentType", Value = documentType }))
                {
                    if (await reader.ReadAsync() && !reader.IsDBNull(reader.GetOrdinal("RefNumber")))
                    {
                        return reader.GetString(reader.GetOrdinal("RefNumber"));
                    }

                    Log.Warning("POS_DocumentSequences_Next returned no row for DocumentType={DocumentType}", documentType);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error minting next reference for DocumentType={DocumentType}", documentType);
                return null;
            }
        }
    }
}
