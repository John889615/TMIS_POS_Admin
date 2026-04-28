using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Inventory.POS_ProductCategories;
using POS_Common.Models.Inventory.POS_ProductCombinations;
using POS_Common.Models.Inventory.POS_ProductExtras;
using POS_Common.Models.Inventory.POS_ProductPreparation;
using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Inventory.POS_ProductSubstitutions;
using POS_Common.Models.Inventory.POS_ServedAsProducts;
using POS_Common.Models.Inventory.POS_Units;
using POS_Common.Models.Menu.POS_MenuItems;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.Inventory
{
    public class Inventory_Custom_Service : Inventory_Custom_SP_Service
    {
        #region Methods

        #region Products

        public static async Task<List<Product>> Products_Select_All_Products(Product item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Products_Select_All_Products(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<Product>> Products_Select_All_Products(Product item, SqlConnection sqlConn)
        {
            try
            {
                List<Product> resultItem = new List<Product>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "products_select_all_products"))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<Product>(Inventory_Translator.Translate_Product_Product));
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

        public static async Task<Product> Products_Select_Single_Name(Product item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Products_Select_Single_Name(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Product> Products_Select_Single_Name(Product item, SqlConnection sqlConn)
        {
            try
            {
                Product resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Products_select_single_name",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ProductName", Value = item.ProductName }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Product>(Inventory_Translator.Translate_Product);
                        Log.Information("Product found: ProductID={ProductID}, ProductName={ProductName}, Description={Description}, FK_ProductTypeID={FK_ProductTypeID}, IsStockTracked={IsStockTracked}, FK_UnitID={FK_UnitID}, FK_ProductCategoryID={FK_ProductCategoryID}, FK_DefaultUnitID={FK_DefaultUnitID}, SKU={SKU}, Barcode={Barcode}, QrCode={QrCode}, IsActive={IsActive}, DateAdded={DateAdded}, DateUpdated={DateUpdated}", resultItem.ProductID, resultItem.ProductName, resultItem.Description, resultItem.FK_ProductTypeID, resultItem.IsStockTracked, resultItem.FK_UnitID, resultItem.FK_ProductCategoryID, resultItem.FK_DefaultUnitID, resultItem.SKU, resultItem.Barcode, resultItem.QrCode, resultItem.IsActive, resultItem.DateAdded, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Product found with the given Product Name.");
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

        #region Product Combinations

        public static async Task<List<ProductCombination>> ProductCombinations_Select_All_ProductID(ProductCombination item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ProductCombinations_Select_All_ProductID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<ProductCombination>> ProductCombinations_Select_All_ProductID(ProductCombination item, SqlConnection sqlConn)
        {
            try
            {
                List<ProductCombination> resultItem = new List<ProductCombination>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Combinations_select_ProductID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<ProductCombination>(Inventory_Translator.Translate_ProductCombination));
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

        public static async Task<ProductCombination> POS_ProductCombinations_Delete(ProductCombination item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ProductCombinations_Delete(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ProductCombination> POS_ProductCombinations_Delete(ProductCombination item, SqlConnection sqlConn)
        {
            try
            {
                ProductCombination resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "ProductCombination_delete",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ProductCombinationID", Value = item.ProductCombinationID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ProductCombination>(Inventory_Translator.Translate_ProductCombination);
                        Log.Information("POS_MenuItemProduct Removed");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Menu Item Product Removed");
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

        public static async Task<ProductCombination> Product_Combinations_Select_Single_ID(ProductCombination item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Product_Combinations_Select_Single_ID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ProductCombination> Product_Combinations_Select_Single_ID(ProductCombination item, SqlConnection sqlConn)
        {
            try
            {
                ProductCombination resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "ProductCombination_select_all_id",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }
                        , new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductItemID", Value = item.FK_ProductItemID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ProductCombination>(Inventory_Translator.Translate_ProductCombination);
                        Log.Information("POS_MenuItemProduct Removed");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Menu Item Product Removed");
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

        #region Product Extras

        public static async Task<List<ProductExtra>> Product_Extras_Select_All_ProductID(ProductExtra item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Product_Extras_Select_All_ProductID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<ProductExtra>> Product_Extras_Select_All_ProductID(ProductExtra item, SqlConnection sqlConn)
        {
            try
            {
                List<ProductExtra> resultItem = new List<ProductExtra>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Extras_select_ProductID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<ProductExtra>(Inventory_Translator.Translate_ProductExtra));
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

        public static async Task<ProductExtra> POS_ProductExtras_Delete(ProductExtra item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ProductExtras_Delete(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ProductExtra> POS_ProductExtras_Delete(ProductExtra item, SqlConnection sqlConn)
        {
            try
            {
                ProductExtra resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "ProductExtra_delete",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ProductExtraID", Value = item.ProductExtraID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ProductExtra>(Inventory_Translator.Translate_ProductExtra);
                        Log.Information("POS_MenuItemProduct Removed");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Menu Item Product Removed");
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

        #region Product Preparation

        public static async Task<List<ProductPreparation>> ProductPreparation_Select_All_ProductID(ProductPreparation item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ProductPreparation_Select_All_ProductID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<ProductPreparation>> ProductPreparation_Select_All_ProductID(ProductPreparation item, SqlConnection sqlConn)
        {
            try
            {
                List<ProductPreparation> resultItem = new List<ProductPreparation>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Preparation_select_ProductID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<ProductPreparation>(Inventory_Translator.Translate_ProductPreparation));
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

        public static async Task<ProductPreparation> POS_ProductPreparations_Delete(ProductPreparation item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ProductPreparations_Delete(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ProductPreparation> POS_ProductPreparations_Delete(ProductPreparation item, SqlConnection sqlConn)
        {
            try
            {
                ProductPreparation resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "ProductPreparation_delete",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ProductPreparationID", Value = item.ProductPreparationID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ProductPreparation>(Inventory_Translator.Translate_ProductPreparation);
                        Log.Information("POS_MenuItemProduct Removed");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Menu Item Product Removed");
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

        #region Product Substitution

        public static async Task<ProductSubstitution> POS_ProductSubstitutions_Delete(ProductSubstitution item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ProductSubstitutions_Delete(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ProductSubstitution> POS_ProductSubstitutions_Delete(ProductSubstitution item, SqlConnection sqlConn)
        {
            try
            {
                ProductSubstitution resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "ProductSubstitution_delete",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ProductSubstitutionID", Value = item.ProductSubstitutionID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ProductSubstitution>(Inventory_Translator.Translate_ProductSubstitution);
                        Log.Information("POS_MenuItemProduct Removed");

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("Menu Item Product Removed");
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

        #region Product Categories

        public static async Task<ProductCategory> Category_Select_Single_Name(ProductCategory item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Category_Select_Single_Name(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<ProductCategory> Category_Select_Single_Name(ProductCategory item, SqlConnection sqlConn)
        {
            try
            {
                ProductCategory resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Category_select_single_name",
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@CategoryName", Value = item.CategoryName }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<ProductCategory>(Inventory_Translator.Translate_ProductCategory);
                        Log.Information("ProductCategory found: ProductCategoryID={ProductCategoryID}, CategoryName={CategoryName}, FK_ProductCategoryID={FK_ProductCategoryID}, IsMaster={IsMaster}, IsActive={IsActive}, DateAdded={DateAdded}, DateUpdated={DateUpdated}", resultItem.ProductCategoryID, resultItem.CategoryName, resultItem.FK_ProductCategoryID, resultItem.IsMaster, resultItem.IsActive, resultItem.DateAdded, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Product Category found with the given Product Name.");
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

        #region Product Preparation

        public static async Task<List<ProductSubstitution>> ProductSubstitutions_Select_All_ProductID(ProductSubstitution item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await ProductSubstitutions_Select_All_ProductID(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<ProductSubstitution>> ProductSubstitutions_Select_All_ProductID(ProductSubstitution item, SqlConnection sqlConn)
        {
            try
            {
                List<ProductSubstitution> resultItem = new List<ProductSubstitution>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Substituition_select_ProductID",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<ProductSubstitution>(Inventory_Translator.Translate_ProductSubstitution));
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

        #region Units

        public static async Task<Unit> Unit_Select_Single_Name(Unit item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Unit_Select_Single_Name(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<Unit> Unit_Select_Single_Name(Unit item, SqlConnection sqlConn)
        {
            try
            {
                Unit resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Unit_select_single_name",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@Unit", Value = item.Unit }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<Unit>(Inventory_Translator.Translate_Unit);
                        Log.Information("POS_Unit found: POS_UnitID={POS_UnitID}, Unit={Unit}, Symbol={Symbol}, IsActive={IsActive}, DateCreated={DateCreated}, DateUpdated={DateUpdated}", resultItem.UnitID, resultItem.Unit, resultItem.Symbol, resultItem.IsActive, resultItem.DateCreated, resultItem.DateUpdated);

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No Unit found with the given Product Name.");
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

        #region Served As Products

        public static async Task<List<ServedAsProduct>> POS_ServedAsProducts_Select_All_Product(ServedAsProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ServedAsProducts_Select_All_Product(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<List<ServedAsProduct>> POS_ServedAsProducts_Select_All_Product(ServedAsProduct item, SqlConnection sqlConn)
        {
            try
            {
                List<ServedAsProduct> resultItem = new List<ServedAsProduct>();

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "POS_ServedAsProducts_select_all_product",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID }))
                {
                    if (reader.HasRows)
                    {
                        resultItem.AddRange(await reader.TranslateAsync<ServedAsProduct>(Inventory_Translator.Translate_ServedAsProduct_Product));
                        Log.Information("ServedAsProduct records found: ", resultItem.Count.ToString());

                        return resultItem;
                    }
                    else
                    {
                        Log.Warning("No ServedAsProduct records found.");
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


        public static async Task<bool> POS_ServedAsProducts_Set_Default(ServedAsProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ServedAsProducts_Set_Default(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<bool> POS_ServedAsProducts_Set_Default(ServedAsProduct item, SqlConnection sqlConn)
        {
            try
            {

                int rowsAffected = await SqlClient.ExecuteNonQueryStoredProcedureAsync(
                        sqlConn,
                        "POS_ServedAsProducts_set_default",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ProductID", Value = item.FK_ProductID },
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@FK_ServedAsID", Value = item.FK_ServedAsID });


                if (rowsAffected == 0)
                {
                    Log.Warning("No ServedAsProduct set default");
                    return false;
                }
                else
                {
                    Log.Information("ServedAsProduct record set default.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<bool> POS_ServedAsProducts_Remove_Product(ServedAsProduct item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await POS_ServedAsProducts_Remove_Product(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<bool> POS_ServedAsProducts_Remove_Product(ServedAsProduct item, SqlConnection sqlConn)
        {
            try
            {
                int rowsAffected = await SqlClient.ExecuteNonQueryStoredProcedureAsync(
                        sqlConn,
                        "POS_ServedAsProducts_remove_product",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@ServedAsProductID", Value = item.ServedAsProductID });


                if (rowsAffected == 0)
                {
                    Log.Warning("No ServedAsProduct record removed with the given ServedAsProductID.");
                    return false;
                }
                else
                {
                    Log.Information("ServedAsProduct record removed successfully with the given ServedAsProductID.");
                    return true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to remove served as product");
                return default;
            }
        }
        #endregion

        #endregion
    }
}
