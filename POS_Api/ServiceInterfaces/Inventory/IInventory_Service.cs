using POS_Common.Models;
using POS_Common.ModelsDto.InventoryController.Product;
using POS_Common.ModelsDto.InventoryController.ProductCategory;
using POS_Common.ModelsDto.InventoryController.ProductCombination;
using POS_Common.ModelsDto.InventoryController.ProductExtra;
using POS_Common.ModelsDto.InventoryController.ProductExtraCategories;
using POS_Common.ModelsDto.InventoryController.ProductPreparation;
using POS_Common.ModelsDto.InventoryController.ProductPreparationMethod;
using POS_Common.ModelsDto.InventoryController.ProductSubstitution;
using POS_Common.ModelsDto.InventoryController.ProductType;
using POS_Common.ModelsDto.InventoryController.ServedAs;
using POS_Common.ModelsDto.InventoryController.ServedAsProducts;
using POS_Common.ModelsDto.InventoryController.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.Inventory
{
    public interface IInventory_Service
    {
        Task<ApiResponse<List<Res_Product_List>>> List_Products();
        Task<ApiResponse<object>> Add_Product(Req_Product_Add request);
        Task<ApiResponse<object>> Update_Product(Req_Product_Update request);

        Task<ApiResponse<List<Res_ProductCombination_List>>> List_Product_Combinations(Req_ProductCombination_List request);
        Task<ApiResponse<object>> Add_Product_Combination(Req_ProductCombination_Add request);
        Task<ApiResponse<object>> Update_Product_Combination(Req_ProductCombination_Update request);
        Task<ApiResponse<object>> Remove_Product_Combination(Req_ProductCombination_Delete request);

        Task<ApiResponse<List<Res_ProductExtraCategory_List>>> List_Product_Extra_Categories();
        Task<ApiResponse<object>> Add_Product_Extra_Category(Req_ProductExtraCategory_Add request);
        Task<ApiResponse<object>> Update_Product_Extra_Category(Req_ProductExtraCategory_Update request);

        Task<ApiResponse<List<Res_ProductExtra_List>>> List_Product_Extras(Req_ProductExtra_List request);
        Task<ApiResponse<object>> Add_Product_Extra(Req_ProductExtra_Add request);
        Task<ApiResponse<object>> Update_Product_Extra(Req_ProductExtra_Update request);
        Task<ApiResponse<object>> Remove_Product_Extra(Req_ProductExtra_Delete request);

        Task<ApiResponse<List<Res_ProductPreparation_List>>> List_Product_Preparation(Req_ProductPreparation_List request);
        Task<ApiResponse<object>> Add_Product_Preparation(Req_ProductPreparation_Add request);
        Task<ApiResponse<object>> Update_Product_Preparation(Req_ProductPreparation_Update request);
        Task<ApiResponse<object>> Remove_Product_Preparation(Req_ProductPreparation_Delete request);

        Task<ApiResponse<List<Res_ProductPreparationMethod_List>>> List_Product_Preparation_Methods();
        Task<ApiResponse<object>> Add_Product_Preparation_Method(Req_ProductPreparationMethod_Add request);
        Task<ApiResponse<object>> Update_Product_Preparation_Method(Req_ProductPreparationMethod_Update request);

        Task<ApiResponse<List<Res_ProductSubstitution_List>>> List_Product_Substitutions(Req_Substitution_List request);
        Task<ApiResponse<object>> Add_Product_Substitution(Req_ProductSubstitution_Add request);
        Task<ApiResponse<object>> Update_Product_Substitution(Req_ProductSubstitution_Update request);
        Task<ApiResponse<object>> Remove_Product_Substitution(Req_ProductSubstitution_Delete request);

        Task<ApiResponse<List<Res_ProductType_List>>> List_Product_Types();
        Task<ApiResponse<object>> Add_Product_Type(Req_ProductType_Add request);
        Task<ApiResponse<object>> Update_Product_Type(Req_ProductType_Update request);

        Task<ApiResponse<List<Res_ProductCategory_List>>> List_Product_Categories();
        Task<ApiResponse<object>> Add_Product_Category(Req_ProductCategory_Add request);
        Task<ApiResponse<object>> Update_Product_Category(Req_ProductCategory_Update request);

        Task<ApiResponse<List<Res_Unit_List>>> List_Units();
        Task<ApiResponse<object>> Add_Unit(Req_Unit_Add request);
        Task<ApiResponse<object>> Update_Unit(Req_Unit_Update request);

        #region Served As

        Task<ApiResponse<List<Res_ServedAs_List>>> List_Served_As();

        Task<ApiResponse<object>> Add_Served_As(Req_ServedAs_Add request);

        Task<ApiResponse<object>> Update_Served_As(Req_ServedAs_Update request);
        #endregion

        #region Served As Products

        Task<ApiResponse<List<Res_Served_As_Products_List>>> List_Served_As_Products(Req_Served_As_Products_List request);

        Task<ApiResponse<object>> Add_Served_As_Product(Req_Served_As_Products_Add request);

        Task<ApiResponse<object>> Update_Served_As_Product(Req_Served_As_Product_Update request);

        Task<ApiResponse<object>> Remove_Served_As_Product(Req_Served_As_Products_Remove request);
        #endregion
    }
}
