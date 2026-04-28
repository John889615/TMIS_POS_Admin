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
    public class Sync_Custom_Service: Sync_Custom_SP_Service
    {
        #region Invoice

        public static async Task<InvoiceHeader> InvoiceHeaders_Insert_ID(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await InvoiceHeaders_Insert_ID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceHeader> InvoiceHeaders_Insert_ID(InvoiceHeader item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceHeader resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "InvoiceHeaders_insert_id",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceHeaderID", Value = item.InvoiceHeaderID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@InvoiceNo", Value = item.InvoiceNo }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@PartyName", Value = item.PartyName }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BookingReference", Value = item.BookingReference }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@DiscountTotal", Value = item.DiscountTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@GratuityTotal", Value = item.GratuityTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@ExclTotal", Value = item.ExclTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@VatTotal", Value = item.VatTotal }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@InclTotal", Value = item.InclTotal }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@IsDiscarded", Value = item.IsDiscarded }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DateCreated", Value = item.DateCreated }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@DatePaid", Value = item.DatePaid }
                        , new SqlParameter() { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "@SyncedToServer", Value = item.SyncedToServer }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@BC_InvoiceID", Value = item.BC_InvoiceID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceHeader>(Sync_Translator.Translate_InvoiceHeader);
                        Log.Information("InvoiceHeader found: InvoiceHeaderID={InvoiceHeaderID}, FK_LocationID={FK_LocationID}, InvoiceNo={InvoiceNo}, PartyName={PartyName}, BookingReference={BookingReference}, DiscountTotal={DiscountTotal}, GratuityTotal={GratuityTotal}, ExclTotal={ExclTotal}, VatTotal={VatTotal}, InclTotal={InclTotal}, IsDiscarded={IsDiscarded}, DateCreated={DateCreated}, DatePaid={DatePaid}", resultItem.InvoiceHeaderID, resultItem.FK_LocationID, resultItem.InvoiceNo, resultItem.PartyName, resultItem.BookingReference, resultItem.DiscountTotal, resultItem.GratuityTotal, resultItem.ExclTotal, resultItem.VatTotal, resultItem.InclTotal, resultItem.IsDiscarded, resultItem.DateCreated, resultItem.DatePaid);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceHeader failed to create.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceLine> InvoiceLines_Insert_ID(InvoiceLine item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await InvoiceLines_Insert_ID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceLine> InvoiceLines_Insert_ID(InvoiceLine item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceLine resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "InvoiceLines_insert_id",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceLineID", Value = item.InvoiceLineID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceTabID", Value = item.FK_InvoiceTabID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Product", Value = item.Product }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Quantity", Value = item.Quantity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineDiscount", Value = item.LineDiscount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalExcl", Value = item.LineTotalExcl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalVat", Value = item.LineTotalVat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@LineTotalIncl", Value = item.LineTotalIncl }
                        , new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Guests", Value = item.Guests }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceLine>(Sync_Translator.Translate_InvoiceLine);
                        //Log.Information("InvoiceLine found: InvoiceLineID={InvoiceLineID}, FK_InvoiceHeaderID={FK_InvoiceHeaderID}, FK_ProductID={FK_ProductID}, Product={Product}, Quantity={Quantity}, LineDiscount={LineDiscount}, LineTotalExcl={LineTotalExcl}, LineTotalVat={LineTotalVat}, LineTotalIncl={LineTotalIncl}, Guests={Guests}", resultItem.InvoiceLineID, resultItem.FK_InvoiceHeaderID, resultItem.FK_ProductID, resultItem.Product, resultItem.Quantity, resultItem.LineDiscount, resultItem.LineTotalExcl, resultItem.LineTotalVat, resultItem.LineTotalIncl, resultItem.Guests);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceLine failed to create.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceTab> InvoiceTabs_Insert_ID(InvoiceTab item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await InvoiceTabs_Insert_ID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<InvoiceTab> InvoiceTabs_Insert_ID(InvoiceTab item, SqlConnection sqlConn)
        {
            try
            {
                InvoiceTab resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "InvoiceTabs_insert_id",
                        new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@InvoiceTabID", Value = item.InvoiceTabID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_InvoiceHeaderID", Value = item.FK_InvoiceHeaderID }
                        , new SqlParameter() { DbType = DbType.Guid, Direction = ParameterDirection.Input, ParameterName = "@FK_TabID", Value = item.FK_TabID }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabGratuity", Value = item.TabGratuity }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabDiscount", Value = item.TabDiscount }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalExcl", Value = item.TabTotalExcl }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalVat", Value = item.TabTotalVat }
                        , new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@TabTotalIncl", Value = item.TabTotalIncl }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@TabDateOpened", Value = item.TabDateOpened }
                        , new SqlParameter() { DbType = DbType.DateTime, Direction = ParameterDirection.Input, ParameterName = "@TabDateClosed", Value = item.TabDateClosed }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<InvoiceTab>(Sync_Translator.Translate_InvoiceTab);
                        Log.Information("InvoiceTab found: InvoiceTabID={InvoiceTabID}, FK_InvoiceHeaderID={FK_InvoiceHeaderID}, FK_TabID={FK_TabID}, TabGratuity={TabGratuity}, TabDiscount={TabDiscount}, TabTotalExcl={TabTotalExcl}, TabTotalVat={TabTotalVat}, TabTotalIncl={TabTotalIncl}, TabDateOpened={TabDateOpened}, TabDateClosed={TabDateClosed}, SyncedToServer={SyncedToServer}", resultItem.InvoiceTabID, resultItem.FK_InvoiceHeaderID, resultItem.FK_TabID, resultItem.TabGratuity, resultItem.TabDiscount, resultItem.TabTotalExcl, resultItem.TabTotalVat, resultItem.TabTotalIncl, resultItem.TabDateOpened, resultItem.TabDateClosed);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("InvoiceTab failed to create.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }
        #endregion
    }
}
