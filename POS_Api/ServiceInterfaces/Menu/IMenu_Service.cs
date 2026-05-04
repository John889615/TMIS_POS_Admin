using POS_Common.Models;
using POS_Common.ModelsDto.MenuController.DebtorMenu;
using POS_Common.ModelsDto.MenuController.DebtorMenuItemProduct;
using POS_Common.ModelsDto.MenuController.MenuItemProduct;
using POS_Common.ModelsDto.MenuController.DebtorMenuPrinter;
using POS_Common.ModelsDto.MenuController.Menu;
using POS_Common.ModelsDto.MenuController.MenuItem;
using POS_Common.ModelsDto.MenuController.MenuItemProduct;
using POS_Common.ModelsDto.MenuController.TabLine;
using POS_Common.ModelsDto.MenuController.Tabs;
using POS_Common.ModelsDto.StockController.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.Menu
{
    public interface IMenu_Service
    {
        Task<ApiResponse<List<Res_Menu_List>>> List_Menus(Req_Menu_List request);
        Task<ApiResponse<MenuTreeResponse>> List_Menu_Tree(Req_MenuTree_List request);
        Task<ApiResponse<List<MenuTreeResponse>>> List_All_Menu_Tree();
        Task<ApiResponse<object>> Add_Menu(Req_Menu_Add request);
        Task<ApiResponse<object>> Copy_Menu(Req_Menu_Copy request);
        Task<ApiResponse<object>> Update_Menu(Req_Menu_Update request);

        Task<ApiResponse<List<Res_MenuItem_List>>> List_Menu_Items(Req_MenuItem_List request);
        Task<ApiResponse<object>> Add_Menu_Item(Req_MenuItem_Add request);
        Task<ApiResponse<object>> Update_Menu_Item(Req_MenuItem_Update request);
        Task<ApiResponse<object>> Remove_Menu_Item(Req_MenuItem_Remove request);

        Task<ApiResponse<List<Res_MenuItemProduct_List>>> List_Menu_Item_Products(Req_MenuItemProduct_List request);
        Task<ApiResponse<object>> Add_Menu_Item_Product(Req_MenuItemProduct_Add request);
        Task<ApiResponse<object>> Remove_Menu_Item_Product(Req_MenuItemProduct_Remove request);
        Task<ApiResponse<object>> Reorder_Menu_Item_Products(Req_MenuItemProduct_Reorder request);

        
        //Task<ApiResponse<object>> Add_Debtor_Menu(Req_DebtorMenu_Add request);
        Task<ApiResponse<object>> Update_Debtor_Menu(Req_DebtorMenu_Update request);
        Task<ApiResponse<DebtorMenuTreeResponse>> List_Debtor_Menu_Tree(Req_DebtorMenuTree_List request);

        Task<ApiResponse<object>> Add_Debtor_Menu_Item_Product_Printer(Req_DebtorMenuItemProductPrinter_Add request);

        Task<ApiResponse<object>> Add_Debtor_Menu_Item(Req_MenuItem_Add request);
        Task<ApiResponse<object>> Update_Debtor_Menu_Item(Req_MenuItem_Update request);
        Task<ApiResponse<object>> Remove_Debtor_Menu_Item(Req_MenuItem_Remove request);

        Task<ApiResponse<object>> Add_Debtor_Menu_Item_Product(Req_MenuItemProduct_Add request);
        Task<ApiResponse<object>> Remove_Debtor_Menu_Item_Product(Req_MenuItemProduct_Remove request);
        Task<ApiResponse<object>> Reorder_Debtor_Menu_Item_Products(Req_DebtorMenuItemProduct_Reorder request);

        Task<ApiResponse<object>> Add_Debtor_Menu_Printer(Req_DebtorMenuPrinter_Add request);
        Task<ApiResponse<object>> Update_Debtor_Menu_Printer(Req_DebtorMenuPrinter_Update request);
        Task<ApiResponse<object>> Delete_Debtor_Menu_Printer(Req_DebtorMenuPrinter_Delete request);

        Task<ApiResponse<List<Res_DebtorMenuPrinter_List>>> List_Debtor_Menu_Printers(Req_DebtorMenuPrinter_List request);

    }
}
