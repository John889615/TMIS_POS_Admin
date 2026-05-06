using POS_Common.Models;
using POS_Common.Models.Sync;
using POS_Common.ModelsDto.StockController.PurchaseOrder;
using POS_Common.ModelsDto.SyncController;
using POS_Common.ModelsDto.SyncController.FromServer;
using POS_Common.ModelsDto.SyncController.PushBatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.Sync
{
    public interface ISync_Service
    {
        #region From Server

        Task<ApiResponse<List<Res_Location_Sync>>> List_Locations();

        Task<ApiResponse<List<Res_LocationCurrency_Sync>>> List_Location_Currencies();

        Task<ApiResponse<List<Res_CostCenter_Sync>>> List_Cost_Centers();

        Task<ApiResponse<List<Res_SlipPrinter_Sync>>> List_Slip_Printers();

        Task<ApiResponse<List<Res_Unit_Sync>>> List_Units();

        Task<ApiResponse<List<Res_TaxType_Sync>>> List_Tax_Types();

        Task<ApiResponse<List<Res_Product_Sync>>> List_Products();

        Task<ApiResponse<List<Res_LocationProduct_Sync>>> List_Location_Products();

        Task<ApiResponse<List<Res_DebtorProductPrices_Sync>>> List_Debtor_Product_Prices();

        Task<ApiResponse<List<Res_PriceCode_Sync>>> List_Price_Codes();

        Task<ApiResponse<List<Res_CostCenterProduct_Sync>>> List_Cost_Center_Products();

        //Task<ApiResponse<List<Res_BookingHeader_Sync>>> List_Booking_Headers();

        //Task<ApiResponse<List<Res_Guest_Sync>>> List_Guests();

        // Pull-from-server List_Booking_Guests is currently disabled (impl commented out
        // in Sync_Service.cs). Re-enable here once the impl is wired up.
        //Task<ApiResponse<List<Res_BookingGuest_Sync>>> List_Booking_Guests();

        Task<ApiResponse<List<Res_PaymentType_Sync>>> List_Payment_Types();

        Task<ApiResponse<List<Res_Menu_Sync>>> List_Menus();

        Task<ApiResponse<List<Res_MenuPrinter_Sync>>> List_Menu_Printers();

        Task<ApiResponse<List<Res_MenuItem_Sync>>> List_Menu_Items();

        Task<ApiResponse<List<Res_MenuItemProduct_Sync>>> List_Menu_Item_Products();

        Task<ApiResponse<List<Res_MenuItemProductPrinter_Sync>>> List_Menu_Item_Product_Printers();

        Task<ApiResponse<List<Res_ProductCombination_Sync>>> List_Product_Combinations();

        Task<ApiResponse<List<Res_ProductExtra_Sync>>> List_Product_Extras();

        Task<ApiResponse<List<Res_ProductExtraCategory_Sync>>> List_Product_Extra_Categories();

        Task<ApiResponse<List<Res_ProductPreparation_Sync>>> List_Product_Preparation();

        Task<ApiResponse<List<Res_ProductPreparationMethod_Sync>>> List_Product_Preparation_Methods();

        Task<ApiResponse<List<Res_ProductSubstitution_Sync>>> List_Product_Substitutions();

        Task<ApiResponse<List<Res_Image_Sync>>> List_Images();

        Task<ApiResponse<List<Res_ImageCategory_Sync>>> List_Image_Categories();

        Task<ApiResponse<List<Res_PaymentTypeIcon_Sync>>> List_Payment_Type_Icons();

        Task<ApiResponse<List<Res_Settings_Sync>>> List_Settings();

        Task<ApiResponse<List<Res_GlobalSettings_Sync>>> List_Global_Settings();

        Task<ApiResponse<List<Res_ServedAs_Sync>>> List_Served_As();

        Task<ApiResponse<List<Res_ServedAsProducts_Sync>>> List_Served_As_Products();

        Task<ApiResponse<List<Res_Currency_Sync>>> List_Currencies();

        Task<ApiResponse<List<Res_CurrencyExchangeRate_Sync>>> List_Currency_Exchange_Rates();

        Task<ApiResponse<List<Res_CostCenterPrinter_Sync>>> List_Cost_Center_Printers();

        Task<ApiResponse<List<Res_SlipTypes_Sync>>> List_Slip_Types();

        Task<ApiResponse<ImageBytesResult>> Get_Image_Bytes(int imageId, CancellationToken cancellationToken = default);
        #endregion

        #region To Server

        Task<ApiResponse<bool>> List_Booking_Headers(List<BookingHeader_Sync> request);
        Task<ApiResponse<bool>> List_Guests(List<Guest_Sync> request);
        Task<ApiResponse<bool>> List_Booking_Guests(List<BookingGuest_Sync> request);
        Task<ApiResponse<bool>> List_Accounts(List<Account_Sync> request);
        Task<ApiResponse<bool>> List_Account_Guests(List<AccountGuest_Sync> request);
        Task<ApiResponse<bool>> List_Arrivals(List<Arrival_Sync> request);
        Task<ApiResponse<bool>> List_CashUp_Headers(List<CashUpHeader_Sync> request);
        Task<ApiResponse<bool>> List_CashUp_Lines(List<CashUpLine_Sync> request);
        Task<ApiResponse<bool>> List_Tabs(List<Tab_Sync> request);
        Task<ApiResponse<bool>> List_Tab_Lines(List<TabLine_Sync> request);
        Task<ApiResponse<bool>> List_TabLine_Combinations(List<TabLineCombination_Sync> request);
        Task<ApiResponse<bool>> List_TabLine_Extras(List<TabLineExtra_Sync> request);
        Task<ApiResponse<bool>> List_TabLine_Guests(List<TabLineGuest_Sync> request);
        Task<ApiResponse<bool>> List_TabLine_Preparation_Methods(List<TabLinePreparationMethod_Sync> request);
        Task<ApiResponse<bool>> List_Tabline_Substitutes(List<TablineSubstitute_Sync> request);
        Task<ApiResponse<bool>> List_Invoice_Headers(List<InvoiceHeader_Sync> request);
        Task<ApiResponse<bool>> List_Invoice_Tabs(List<InvoiceTab_Sync> request);
        Task<ApiResponse<bool>> List_Invoice_Lines(List<InvoiceLine_Sync> request);
        Task<ApiResponse<bool>> List_Invoice_Payments(List<InvoicePayment_Sync> request);
        Task<ApiResponse<bool>> List_Void_Logs(List<VoidLog_Sync> request);
        #endregion

        #region Admin

        Task<ApiResponse<bool>> Notify_Result(Req_Notify_Result request);
        #endregion

        #region Push Batch (Spec 2)

        Task<ApiResponse<Res_Push_Batch>> Process_Push_Batch(Req_Push_Batch request);
        #endregion
    }
}
