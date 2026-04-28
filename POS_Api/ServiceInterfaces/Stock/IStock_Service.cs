using POS_Common.Models;
using POS_Common.ModelsDto.StockController.CostCenterProduct;
using POS_Common.ModelsDto.StockController.DebtorProduct;
using POS_Common.ModelsDto.StockController.DebtorProductPrice;
using POS_Common.ModelsDto.StockController.PriceCode;
using POS_Common.ModelsDto.StockController.PurchaseOrder;
using POS_Common.ModelsDto.StockController.PurchaseOrderLine;
using POS_Common.ModelsDto.StockController.StockRequest;
using POS_Common.ModelsDto.StockController.StockRequestLine;
using POS_Common.ModelsDto.StockController.StockTransfer;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrder;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrderLines;
using POS_Common.ModelsDto.StockController.SupplierProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.Stock
{
    public interface IStock_Service
    {
        Task<ApiResponse<List<Res_DebtorProduct_List>>> List_Debtor_Products(Req_DebtorProduct_List request);
        Task<ApiResponse<object>> Add_Debtor_Product(Req_DebtorProduct_Add request);
        Task<ApiResponse<object>> Update_Debtor_Product(Req_DebtorProduct_Update request);

        Task<ApiResponse<List<Res_DebtorProductPrice_List>>> List_Debtor_Product_Prices(Req_DebtorProductPrice_List request);
        Task<ApiResponse<object>> Add_Debtor_Product_Price(Req_DebtorProductPrice_Add request);
        Task<ApiResponse<object>> Update_Debtor_Product_Price(Req_DebtorProductPrice_Update request);

        Task<ApiResponse<List<Res_PriceCode_List>>> List_Price_Codes();
        Task<ApiResponse<object>> Add_Price_Code(Req_PriceCode_Add request);
        Task<ApiResponse<object>> Update_Price_Code(Req_PriceCode_Update request);

        Task<ApiResponse<List<Res_CostCenterProduct_List>>> List_Cost_Center_Products(Req_CostCenterProduct_List request);
        Task<ApiResponse<object>> Add_Cost_Center_Product(Req_CostCenterProduct_Add request);
        Task<ApiResponse<object>> Update_Cost_Center_Product(Req_CostCenterProduct_Update request);

        Task<ApiResponse<List<Res_SupplierProduct_List>>> List_Supplier_Products(Req_SupplierProduct_List request);
        Task<ApiResponse<object>> Add_Supplier_Product(Req_SupplierProduct_Add request);
        Task<ApiResponse<object>> Update_Supplier_Product(Req_SupplierProduct_Update request);

        //Task<ApiResponse<List<Res_PurchaseOrder_List>>> List_Purchase_Orders(Req_PurchaseOrder_List request);
        //Task<ApiResponse<object>> Add_Purchase_Order(Req_PurchaseOrder_Add request);
        //Task<ApiResponse<object>> Update_Purchase_Order(Req_PurchaseOrder_Update request);

        //Task<ApiResponse<List<Res_PurchaseOrderLine_List>>> List_Purchase_Order_Lines(Req_PurchaseOrderLine_List request);
        //Task<ApiResponse<object>> Add_Purchase_Order_Line(Req_PurchaseOrderLine_Add request);

        //Task<ApiResponse<List<Res_SubmittedPurchaseOrder_List>>> List_Submitted_Purchase_Orders();
        //Task<ApiResponse<object>> Update_Purchase_Order_Status(Req_PurchaseOrderStatus_Update request);

        //Task<ApiResponse<List<Res_SubmittedPurchaseOrderLines_List>>> List_Submitted_Purchase_Order_Lines(Req_SubmittedPurchaseOrderLines_List request);
        //Task<ApiResponse<object>> Update_Purchase_Order_Line_Status(Req_PurchaseOrderLineStatus_Update request);

        //Task<ApiResponse<List<Res_StockRequest_List>>> List_Stock_Requests(Req_StockRequest_List request);
        //Task<ApiResponse<object>> Add_Stock_Request(Req_StockRequest_Add request);
        //Task<ApiResponse<object>> Update_Stock_Request(Req_StockRequest_Update request);

        //Task<ApiResponse<List<Res_StockRequestLine_List>>> List_Stock_Request_Lines(Req_StockRequestLine_List request);
        //Task<ApiResponse<object>> Add_Stock_Request_Line(Req_StockRequestLine_Add request);

        //Task<ApiResponse<List<Res_StockTransfer_List>>> List_Stock_Transfers(Req_StockTransfer_List request);
        //Task<ApiResponse<object>> Add_Stock_Transfer(Req_StockTransfer_Add request);
        //Task<ApiResponse<object>> Update_Stock_Transfer(Req_StockTransfer_Update request);
    }
}
