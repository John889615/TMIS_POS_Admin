using POS_Common.ModelsDto.DebtorsController.CostCenter;
using POS_Common.ModelsDto.DebtorsController.CostCenterType;
using POS_Common.ModelsDto.DebtorsController.DebtorAddress;
using POS_Common.ModelsDto.DebtorsController.DebtorContact;
using POS_Common.ModelsDto.DebtorsController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_Common.Models;
using POS_Common.ModelsDto.CreditorsController.Creditor;
using POS_Common.ModelsDto.CreditorsController.CreditorType;
using POS_Common.ModelsDto.CreditorsController.CreditorAddress;
using POS_Common.ModelsDto.CreditorsController.CreditorContact;

namespace POS_Api.ServiceInterfaces.Creditors
{
    public interface ICreditors_Service
    {
        Task<ApiResponse<List<Res_Creditor_List>>> List_Creditors();
        Task<ApiResponse<object>> Add_Creditor(Req_Creditor_Add request);
        Task<ApiResponse<object>> Update_Creditor(Req_Creditor_Update request);

        Task<ApiResponse<List<Res_CreditorType_List>>> List_CreditorTypes();

        Task<ApiResponse<object>> Add_Creditor_Address(Req_CreditorAddress_Add request);
        Task<ApiResponse<object>> Update_Creditor_Address(Req_CreditorAddress_Update request);
        Task<ApiResponse<List<Res_CreditorAddressType_List>>> List_Creditor_Address_Types();

        Task<ApiResponse<object>> Add_Creditor_Contact(Req_CreditorContact_Add request);
        Task<ApiResponse<object>> Update_Creditor_Contact(Req_CreditorContact_Update request);
    }
}
