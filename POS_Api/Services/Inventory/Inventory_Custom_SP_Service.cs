using Microsoft.Data.SqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;
using POS_Api.Translators;

using POS_Common.Models.Inventory.Custom.SelectProductCombinationsID;
using POS_Common.Models.Inventory.Custom.DeleteProductCombination;

namespace POS_Api.Services.Inventory
{
    public abstract class Inventory_Custom_SP_Service : Inventory_Base_Service
    {
        #region Custom Stored Procedures

        #region DeleteProductCombination

        public static async Task<bool> DeleteProductCombination_Transaction(Req_DeleteProductCombination item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await DeleteProductCombination(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<bool> DeleteProductCombination(Req_DeleteProductCombination item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await DeleteProductCombination(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<bool> DeleteProductCombination(Req_DeleteProductCombination item, SqlConnection sqlConn)
        {
            try
            {
                int rowsAffected = await SqlClient.ExecuteNonQueryStoredProcedureAsync(
                    sqlConn,
                    "ProductCombination_delete",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ProductCombinationID", Value = (object)item.ProductCombinationID ?? DBNull.Value }
                );

                if (rowsAffected > 0)
                {
                    Log.Information("DeleteProductCombination records affected: {Count}", rowsAffected);
                    return true;
                }
                else
                {
                    Log.Warning("No records affected for DeleteProductCombination.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        #endregion

        #region SelectProductCombinationsID

        public static async Task<List<Res_SelectProductCombinationsID>> SelectProductCombinationsID_Transaction(Req_SelectProductCombinationsID item, string connectionString)
        {
            try
            {
                using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await SelectProductCombinationsID(item, sqlConn);
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_SelectProductCombinationsID>> SelectProductCombinationsID(Req_SelectProductCombinationsID item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    return await SelectProductCombinationsID(item, sqlConn);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        public static async Task<List<Res_SelectProductCombinationsID>> SelectProductCombinationsID(Req_SelectProductCombinationsID item, SqlConnection sqlConn)
        {
            try
            {
                List<Res_SelectProductCombinationsID> resultItem = new List<Res_SelectProductCombinationsID>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                    sqlConn,
                    "Combinations_select_ProductID",
                    new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = (object)item.FKProductID ?? DBNull.Value }
                ))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Res_SelectProductCombinationsID>(Inventory_Translator.Translate_SelectProductCombinationsID));
                        Log.Information("SelectProductCombinationsID records found: {Count}", resultItem.Count);
                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No records found for SelectProductCombinationsID.");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated custom stored procedure code");
                return default;
            }
        }

        #endregion

        #endregion
    }
}
