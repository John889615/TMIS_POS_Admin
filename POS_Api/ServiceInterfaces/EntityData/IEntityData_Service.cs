using POS_Common.Models;
using POS_Common.ModelsDto.EntityDataController;
using POS_Common.ModelsDto.EntityDataController.Address;
using POS_Common.ModelsDto.EntityDataController.Contact;
using POS_Common.ModelsDto.EntityDataController.Country;
using POS_Common.ModelsDto.EntityDataController.Currency;
using POS_Common.ModelsDto.EntityDataController.DialingCode;
using POS_Common.ModelsDto.EntityDataController.Entity;
using POS_Common.ModelsDto.EntityDataController.ExchangeRates;
using POS_Common.ModelsDto.EntityDataController.LocationCurrency;
using POS_Common.ModelsDto.EntityDataController.PaymentType;
using POS_Common.ModelsDto.EntityDataController.Settings;
using POS_Common.ModelsDto.EntityDataController.SlipPrinter;
using POS_Common.ModelsDto.EntityDataController.SlipType;
using POS_Common.ModelsDto.EntityDataController.TaxType;
using POS_Common.ModelsDto.EntityDataController.Timezone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.ModelsDto.AuthController.AddUsers;
using TMIS_Common.ModelsDto.AuthController.Users;

namespace POS_Api.ServiceInterfaces.EntityData
{
    public interface IEntityData_Service
    {
        Task<ApiResponse<List<Res_AllEntityAddress_List>>> List_All_Entity_Addresses(Req_AllEntityAddress_List request);

        Task<ApiResponse<List<Res_AllEntityContact_List>>> List_All_Entity_Contacts(Req_AllEntityContact_List request);

        Task<ApiResponse<List<Res_Address_List>>> List_Addresses();
        Task<ApiResponse<object>> Add_Address(Req_Address_Add request);
        Task<ApiResponse<object>> Update_Address(Req_Address_Update request);

        Task<ApiResponse<List<Res_AddressRegion_List>>> List_Address_Regions();
        Task<ApiResponse<object>> Add_Address_Region(Req_AddressRegion_Add request);
        Task<ApiResponse<object>> Update_Address_Region(Req_AddressRegion_Update request);

        Task<ApiResponse<List<Res_AddressType_List>>> List_Address_Types();
        Task<ApiResponse<object>> Add_Address_Type(Req_AddressType_Add request);
        Task<ApiResponse<object>> Update_Address_Type(Req_AddressType_Update request);

        Task<ApiResponse<List<Res_Contact_List>>> List_Contacts();
        Task<ApiResponse<object>> Add_Contact(Req_Contact_Add request);
        Task<ApiResponse<object>> Update_Contact(Req_Contact_Update request);

        Task<ApiResponse<List<Res_ContactType_List>>> List_Contact_Types();
        Task<ApiResponse<object>> Add_Contact_Type(Req_ContactType_Add request);
        Task<ApiResponse<object>> Update_Contact_Type(Req_ContactType_Update request);

        Task<ApiResponse<List<Res_Continent_List>>> List_Continents();

        Task<ApiResponse<List<Res_Country_List>>> List_Countries();
        Task<ApiResponse<object>> Add_Country(Req_Country_Add request);
        Task<ApiResponse<object>> Update_Country(Req_Country_Update request);

        Task<ApiResponse<List<Res_CountryProvince_List>>> List_Country_Provinces();
        Task<ApiResponse<object>> Add_Country_Province(Req_CountryProvince_Add request);
        Task<ApiResponse<object>> Update_Country_Province(Req_CountryProvince_Update request);

        Task<ApiResponse<List<Res_CountryRegion_List>>> List_Country_Regions();
        Task<ApiResponse<object>> Add_Country_Region(Req_CountryRegion_Add request);
        Task<ApiResponse<object>> Update_Country_Region(Req_CountryRegion_Update request);

        Task<ApiResponse<List<Res_CountrySubRegion_List>>> List_Country_Subregions();
        Task<ApiResponse<object>> Add_Country_Subregion(Req_CountrySubRegion_Add request);
        Task<ApiResponse<object>> Update_Country_Subregion(Req_CountrySubRegion_Update request);

        Task<ApiResponse<List<Res_Currency_List>>> List_Currencies();
        Task<ApiResponse<object>> Add_Currency(Req_Currency_Add request);
        Task<ApiResponse<object>> Update_Currency(Req_Currency_Update request);

        Task<ApiResponse<List<Res_DialingCode_List>>> List_Dialing_Codes();
        Task<ApiResponse<object>> Add_Dialing_Code(Req_DialingCode_Add request);
        Task<ApiResponse<object>> Update_Dialing_Code(Req_DialingCode_Update request);

        Task<ApiResponse<List<Res_Entity_List>>> List_Entities();

        Task<ApiResponse<List<Res_EntityAddress_List>>> List_Entity_Addresses();
        Task<ApiResponse<object>> Add_Entity_Address(Req_EntityAddress_Add request);
        Task<ApiResponse<object>> Update_Entity_Address(Req_EntityAddress_Update request);

        Task<ApiResponse<List<Res_EntityContact_List>>> List_Entity_Contacts();
        Task<ApiResponse<object>> Add_Entity_Contact(Req_EntityContact_Add request);
        Task<ApiResponse<object>> Update_Entity_Contact(Req_EntityContact_Update request);

        Task<ApiResponse<List<Res_Status_List>>> List_Statuses();

        Task<ApiResponse<List<Res_StatusGroup_List>>> List_Status_Groups();

        Task<ApiResponse<List<Res_Timezone_List>>> List_Timezones();
        Task<ApiResponse<object>> Add_Timezone(Req_Timezone_Add request);
        Task<ApiResponse<object>> Update_Timezone(Req_Timezone_Update request);

        Task<ApiResponse<List<Res_SlipPrinter_List>>> List_Slip_Printers();
        Task<ApiResponse<object>> Add_Slip_Printer(Req_SlipPrinter_Add request);
        Task<ApiResponse<object>> Update_Slip_Printer(Req_SlipPrinter_Update request);

        Task<ApiResponse<List<Res_PaymentTypeIcon_List>>> List_Payment_Type_Icons();
        Task<ApiResponse<List<Res_PaymentType_List>>> List_Payment_Types();
        Task<ApiResponse<object>> Add_Payment_Type(Req_PaymentType_Add request);
        Task<ApiResponse<object>> Update_Payment_Type(Req_PaymentType_Update request);

        Task<ApiResponse<List<Res_TaxType_List>>> List_Tax_Types();
        Task<ApiResponse<object>> Add_Tax_Type(Req_TaxType_Add request);
        Task<ApiResponse<object>> Update_Tax_Type(Req_TaxType_Update request);

        Task<ApiResponse<List<Res_LocationCurrency_List>>> List_Location_Currencies(Req_LocationCurrency_List request);
        Task<ApiResponse<object>> Add_Location_Currency(Req_LocationCurrency_Add request);
        Task<ApiResponse<object>> Remove_Location_Currency(Req_LocationCurrency_Remove request);

        Task<ApiResponse<List<Res_Settings_List>>> List_Settings();
        Task<ApiResponse<object>> Add_Setting(Req_Settings_Add request);
        Task<ApiResponse<object>> Update_Setting(Req_Settings_Update request);

        Task<ApiResponse<List<Res_ExchangeRate_List>>> List_Exchange_Rates();
        Task<ApiResponse<object>> Add_Exchange_Rate(Req_ExchangeRate_Add request);
        Task<ApiResponse<object>> Update_Exchange_Rate(Req_ExchangeRate_Update request);

        Task<ApiResponse<List<Res_SlipType_List>>> List_Slip_Types();

    }
}
