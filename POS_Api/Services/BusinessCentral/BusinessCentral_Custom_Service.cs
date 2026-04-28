using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.Debtors.POS_Locations;
using POS_Common.Models.Inventory.POS_Units;
using POS_Common.Models.Menu.POS_DebtorMenus;
using POS_Common.Models.Stock.POS_DebtorProducts;
using POS_Common.Models.Stock.POS_PriceCodes;
using POS_Common.Models.Sync.POS_InvoiceHeaders;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;
using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Inventory.POS_ProductCategories;

namespace POS_Api.Services.BusinessCentral
{
    public class BusinessCentral_Custom_Service
    {
        public static async Task<PriceCodes> PriceCodes_Insert_Update(PriceCodes item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await PriceCodes_Insert_Update(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<PriceCodes> PriceCodes_Insert_Update(PriceCodes item, SqlConnection sqlConn)
        {
            try
            {
                PriceCodes resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "PriceCodes_insert_update",
                        new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@PriceCode", Value = item.PriceCode }
                        , new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@Description", Value = item.Description }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<PriceCodes>(Stock_Translator.Translate_PriceCodes);
                        //Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
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

        public static async Task<ProductCategory> ProductCategories_Insert_Update(ProductCategory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ProductCategories_Insert_Update(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ProductCategory> ProductCategories_Insert_Update(ProductCategory item, SqlConnection sqlConn)
        {
            try
            {
                ProductCategory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "ProductCategories_insert_update",
                        new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@CategoryName", Value = item.CategoryName }
                        , new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ProductCategory>(Inventory_Translator.Translate_ProductCategory);
                        //Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
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

        public static async Task<Location> Location_Insert_Update(Location item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Location_Insert_Update(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Location> Location_Insert_Update(Location item, SqlConnection sqlConn)
        {
            try
            {
                Location resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Location_insert_update",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_CurrencyID", Value = item.FK_CurrencyID }
                        , new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@ShortCode", Value = item.ShortCode }
                        , new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@Name", Value = item.Name }
                        , new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Location>(Debtors_Translator.Translate_Location);
                        //Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
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

        public static async Task<Unit> Unit_Insert_Update(Unit item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Unit_Insert_Update(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Unit> Unit_Insert_Update(Unit item, SqlConnection sqlConn)
        {
            try
            {
                Unit resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Unit_insert_update",
                        new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@Unit", Value = item.Unit }
                        , new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Unit>(Inventory_Translator.Translate_Unit);
                        //Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
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

        public static async Task<Product> Product_Insert_Update(Product item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Product_Insert_Update(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Product> Product_Insert_Update(Product item, SqlConnection sqlConn)
        {
            try
            {
                Product resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Product_insert_update",
                        new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@ProductName", Value = item.ProductName }
                        , new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_ProductTypeID", Value = item.FK_ProductTypeID }
                        , new SqlParameter() { DbType = System.Data.DbType.Boolean, Direction = System.Data.ParameterDirection.Input, ParameterName = "@IsStockTracked", Value = item.IsStockTracked }
                        , new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_UnitID", Value = item.FK_UnitID }
                        , new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_ProductCategoryID", Value = item.FK_ProductCategoryID }
                        , new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_DefaultUnitID", Value = item.FK_DefaultUnitID }
                        , new SqlParameter() { DbType = System.Data.DbType.Boolean, Direction = System.Data.ParameterDirection.Input, ParameterName = "@IsActive", Value = item.IsActive }
                        , new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@BC_ID", Value = item.BC_ID }
                        , new SqlParameter() { DbType = System.Data.DbType.String, Direction = System.Data.ParameterDirection.Input, ParameterName = "@ItemNo", Value = item.ItemNo }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Product>(Inventory_Translator.Translate_Product);
                        //Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
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

        public static async Task<DebtorProduct> Product_Location_Insert_Update(DebtorProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Product_Location_Insert_Update(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<DebtorProduct> Product_Location_Insert_Update(DebtorProduct item, SqlConnection sqlConn)
        {
            try
            {
                DebtorProduct resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "ProductLocation_insert_update",
                        new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_LocationID", Value = item.FK_LocationID }
                        , new SqlParameter() { DbType = System.Data.DbType.Decimal, Direction = System.Data.ParameterDirection.Input, ParameterName = "@CostPrice", Value = item.CostPrice }
                        , new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, ParameterName = "@FK_SellUnitID", Value = item.FK_SellUnitID }
                        , new SqlParameter() { DbType = System.Data.DbType.Decimal, Direction = System.Data.ParameterDirection.Input, ParameterName = "@QuantityOnHand", Value = item.QuantityOnHand }
                        , new SqlParameter() { DbType = System.Data.DbType.Boolean, Direction = System.Data.ParameterDirection.Input, ParameterName = "@IsAvailable", Value = item.IsAvailable }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<DebtorProduct>(Stock_Translator.Translate_DebtorProduct);
                        //Log.Information("Purchase Order records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }

                    else
                    {
                        Log.Warning("No Purchase Order records found.");
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

        #region Invoice

        public static async Task<List<InvoiceHeader>> List_Invoice(InvoiceHeader item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await List_Invoice(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<InvoiceHeader>> List_Invoice(InvoiceHeader item, SqlConnection sqlConn)
        {
            try
            {
                List<InvoiceHeader> resultItem = new List<InvoiceHeader>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "invoiceHeader_select_all_BC"))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<InvoiceHeader>(Sync_Translator.Translate_InvoiceHeader_BC));
                        Log.Information("Product records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Product records found.");
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
