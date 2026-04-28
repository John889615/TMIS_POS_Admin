using POS_Common.Models;
using POS_Common.Models.Debtors.POS_CostCenterPrinters;
using POS_Common.ModelsDto.DebtorsController;
using POS_Common.ModelsDto.DebtorsController.Branch;
using POS_Common.ModelsDto.DebtorsController.CostCenter;
using POS_Common.ModelsDto.DebtorsController.CostCenterPrinter;
using POS_Common.ModelsDto.DebtorsController.CostCenterType;
using POS_Common.ModelsDto.DebtorsController.DebtorAddress;
using POS_Common.ModelsDto.DebtorsController.DebtorContact;
using POS_Common.ModelsDto.DebtorsController.Department;
using POS_Common.ModelsDto.EntityDataController;
using POS_Common.ModelsDto.EntityDataController.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.Debtors
{
    public interface IDebtors_Service
    {
        Task<ApiResponse<List<Res_Debtor_List>>> List_Debtors();
        Task<ApiResponse<object>> Add_Debtor(Req_Debtor_Add request);
        Task<ApiResponse<object>> Update_Debtor(Req_Debtor_Update request);

        Task<ApiResponse<List<Res_CostCenter_List>>> List_CostCenters();
        Task<ApiResponse<object>> Add_CostCenter(Req_CostCenter_Add request);
        Task<ApiResponse<object>> Update_CostCenter(Req_CostCenter_Update request);

        Task<ApiResponse<List<Res_CostCenterType_List>>> List_CostCenterTypes();

        
        Task<ApiResponse<object>> Add_Debtor_Address(Req_DebtorAddress_Add request);
        Task<ApiResponse<object>> Update_Debtor_Address(Req_DebtorAddress_Update request);
        Task<ApiResponse<List<Res_DebtorAddressType_List>>> List_Debtor_Address_Types();

        Task<ApiResponse<object>> Add_Debtor_Contact(Req_DebtorContact_Add request);
        Task<ApiResponse<object>> Update_Debtor_Contact(Req_DebtorContact_Update request);

        Task<ApiResponse<List<Res_CostCenterPrinter_List>>> List_CostCenter_Printers(Req_CostCenterPrinter_List request);
        Task<ApiResponse<object>> Add_CostCenter_Printer(Req_CostCenterPrinter_Add request);
        Task<ApiResponse<object>> Update_CostCenter_Printer(Req_CostCenterPrinter_Update request);

        Task<ApiResponse<CostCenterPrinter>> Switch_CostCenter_Printer_Link(Req_CostCenterPrinter_Switch request);
    }
}
