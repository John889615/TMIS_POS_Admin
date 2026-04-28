using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;
using POS_Api.ServiceInterfaces.Inventory;
using Microsoft.AspNetCore.Http;
using POS_Api.ServiceInterfaces.Logging;
using POS_Common.Enums;
using POS_Common.Models;
using System.Data;
using System.Security.Claims;

using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Creditors.CreditorTypeMappings;
using POS_Common.Models.Creditors.CreditorTypes;
using POS_Api.ServiceInterfaces.Creditors;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.DebtorTypeMappings;
using POS_Common.Models.Debtors.DebtorTypes;
using POS_Common.ModelsDto.DebtorsController.DebtorAddress;
using POS_Common.ModelsDto.DebtorsController.DebtorContact;
using POS_Common.ModelsDto.DebtorsController;
using POS_Common.ModelsDto.CreditorsController.Creditor;
using POS_Common.ModelsDto.CreditorsController.CreditorAddress;
using POS_Common.ModelsDto.CreditorsController.CreditorContact;
using POS_Common.ModelsDto.CreditorsController.CreditorType;
using POS_Api.Services.Creditors;
using TMIS_Common.Interfaces;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.EntityData.Addresses;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.Contacts;
using POS_Common.Models.EntityData.Entities;
using POS_Common.Models.EntityData.EntityAddresses;
using POS_Common.Models.EntityData.EntityContacts;
using System.Globalization;

namespace POS_Api.Services
{
    public class Creditors_Service : Creditors_Custom_Service, ICreditors_Service
    {
        #region Members

        private readonly ILogging_Service _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Creditors_Service(IConfiguration configuration, ILogging_Service logger
            , IHttpContextAccessor httpContextAccessor, IUserContext userContext)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;

            Current_User_Management();
        }
        #endregion

        #region Helper Methods

        // Use IHttpContextAccessor to access HttpContext
        private string GetIpAddressFromRequest()
        {
            return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }

        // Use IHttpContextAccessor to access HttpContext
        private string GetUserAgentFromRequest()
        {
            return _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
        }

        public async void Current_User_Management()
        {
            try
            {
                var creditorResponse = await Base_Service.Current_User_Management(new User()
                {
                    UserID = _userContext.UserID,
                    Firstname = _userContext.Firstname,
                    Lastname = _userContext.Lastname,
                    Username = _userContext.Username
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Methods

        #region Creditors

        public async Task<ApiResponse<List<Res_Creditor_List>>> List_Creditors()
        {
            try
            {
                _logger.LogService("Starting Creditor List");

                var creditorResponse = await Creditors_Select_All_Creditors(new Creditor()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Creditor_List>();

                if (creditorResponse != null && creditorResponse.Any())
                {
                    foreach (var creditor in creditorResponse)
                    {

                        response.Add(new Res_Creditor_List()
                        {
                            CreditorID = creditor.CreditorID,
                            ShortCode = creditor.ShortCode,
                            Name = creditor.Name,
                            MasterCreditor = creditor.MasterCreditor,
                            IsMasterCreditor = creditor.IsMasterCreditor,
                            CreditorType = creditor.CreditorType,
                            CreditorTypeMappingID = creditor.CreditorTypeMappingID,
                            CreditorTypeID = creditor.CreditorTypeID,
                            Status = creditor.Status
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor list", ex);
                return ApiResponse.Fail<List<Res_Creditor_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Creditor(Req_Creditor_Add request)
        {
            try
            {
                _logger.LogService("Starting Creditor Add", request);

                var creditorResponse = await Creditors_Select_Single_Name(new Creditor()
                {
                    Name = request.Name
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (creditorResponse != null)
                {
                    _logger.LogService("Creditor already exists", request.Name);
                    return ApiResponse.Fail<object>(AppErrorCode.CreditorExists, new List<string> { "Creditor already exists." }, 400);
                }

                var creditorInsert = await Creditors_Insert(new Creditor()
                {
                    ShortCode = request.ShortCode,
                    Name = request.Name,
                    FK_MasterCreditorID = request.FK_MasterCreditorID,
                    IsMasterCreditor = request.IsMasterCreditor,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var creditorTypeMappingInsert = await CreditorTypeMappings_Insert(new CreditorTypeMapping()
                {
                    FK_CreditorID = creditorInsert.CreditorID,
                    FK_CreditorTypeID = request.FK_CreditorTypeID,
                    FK_StatusID = request.FK_StatusID,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Creditor(Req_Creditor_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Update", request);

                var creditorResponse = await Creditors_Select_Single(new Creditor()
                {
                    CreditorID = request.CreditorID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var creditorTypeMappingResponse = await CreditorTypeMappings_Select_Single(new CreditorTypeMapping()
                {
                    CreditorTypeMappingID = request.CreditorTypeMappingID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (creditorResponse == null)
                {
                    _logger.LogService("Creditor not found", request.CreditorID);
                    return ApiResponse.Fail<object>(AppErrorCode.CreditorNotFound, new List<string> { "Creditor not found." }, 404);
                }

                var creditorUpdate = await Creditors_Update(new Creditor()
                {
                    CreditorID = request.CreditorID,
                    ShortCode = request.ShortCode ?? creditorResponse.ShortCode,
                    Name = request.Name ?? creditorResponse.Name,
                    FK_MasterCreditorID = request.FK_MasterCreditorID ?? creditorResponse.FK_MasterCreditorID,
                    IsMasterCreditor = request.IsMasterCreditor ?? creditorResponse.IsMasterCreditor,
                    DateCreated = creditorResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (creditorTypeMappingResponse == null)
                {
                    var debtorTypeMappingInsert = await CreditorTypeMappings_Insert(new CreditorTypeMapping()
                    {
                        FK_CreditorID = creditorUpdate.CreditorID,
                        FK_CreditorTypeID = request.FK_CreditorTypeID ?? creditorResponse.CreditorTypeID,
                        FK_StatusID = request.FK_StatusID ?? creditorTypeMappingResponse.FK_StatusID,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
                }

                else
                {
                    var creditorTypeMappingUpdate = await CreditorTypeMappings_Update(new CreditorTypeMapping()
                    {
                        CreditorTypeMappingID = request.CreditorTypeMappingID,
                        FK_CreditorID = creditorUpdate.CreditorID,
                        FK_CreditorTypeID = request.FK_CreditorTypeID ?? creditorTypeMappingResponse.FK_CreditorTypeID,
                        FK_StatusID = request.FK_StatusID ?? creditorTypeMappingResponse.FK_StatusID,
                        DateCreated = creditorTypeMappingResponse.DateCreated,
                        DateUpdated = DateTime.Now
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Creditor Addresses

        public async Task<ApiResponse<List<Res_CreditorAddressType_List>>> List_Creditor_Address_Types()
        {
            try
            {
                _logger.LogService("Starting Creditor Address Type List");

                var creditorAddressTypeResponse = await CreditorAddresses_Select_Creditor(new AddressType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CreditorAddressType_List>();

                if (creditorAddressTypeResponse != null && creditorAddressTypeResponse.Any())
                {
                    foreach (var creditorAddressType in creditorAddressTypeResponse)
                    {

                        response.Add(new Res_CreditorAddressType_List()
                        {
                            AddressTypeID = creditorAddressType.AddressTypeID,
                            Type = creditorAddressType.Type
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address type list", ex);
                return ApiResponse.Fail<List<Res_CreditorAddressType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Creditor_Address(Req_CreditorAddress_Add request)
        {
            try
            {
                _logger.LogService("Starting Creditor Address Add", request);

                var creditorAddressInsert = await EntityData.EntityData_Custom_Service.Addresses_Insert(new Address()
                {
                    FK_CountryID = request.FK_CountryID,
                    FK_ProvinceID = request.FK_ProvinceID,
                    FK_AddressRegionID = request.FK_AddressRegionID,
                    StreetAddress = request.StreetAddress,
                    Locality = request.Locality,
                    PostalCode = request.PostalCode,
                    Landmark = request.Landmark,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Notes = request.Notes,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityResponse = await Entity_Select_Creditor(new Entity()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityAddressInsert = await EntityData.EntityData_Custom_Service.EntityAddresses_Insert(new EntityAddress()
                {
                    FK_EntityID = entityResponse.EntityID,
                    EntityRecordID = request.FK_CreditorID,
                    FK_AddressID = creditorAddressInsert.AddressID,
                    FK_AddressTypeID = request.FK_AddressTypeID,
                    IsPrimary = request.IsPrimary ?? false,
                    ValidFrom = request.ValidFrom ?? DateTime.Now,
                    ValidTo = request.ValidTo,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Address add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Creditor_Address(Req_CreditorAddress_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Address Update", request);

                var debtorAddressResponse = await EntityData.EntityData_Custom_Service.Addresses_Select_Single(new Address()
                {
                    AddressID = request.AddressID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityAddressResponse = await EntityAddresses_Select_Single_AddressID(new EntityAddress()
                {
                    FK_AddressID = request.AddressID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var debtorAddressUpdate = await EntityData.EntityData_Custom_Service.Addresses_Update(new Address()
                {
                    AddressID = request.AddressID,
                    FK_CountryID = request.FK_CountryID ?? debtorAddressResponse.FK_CountryID,
                    FK_ProvinceID = request.FK_ProvinceID ?? debtorAddressResponse.FK_ProvinceID,
                    FK_AddressRegionID = request.FK_AddressRegionID ?? debtorAddressResponse.FK_AddressRegionID,
                    StreetAddress = string.IsNullOrWhiteSpace(request.StreetAddress) ? debtorAddressResponse.StreetAddress : request.StreetAddress,
                    Locality = string.IsNullOrWhiteSpace(request.Locality) ? debtorAddressResponse.Locality : request.Locality,
                    PostalCode = string.IsNullOrWhiteSpace(request.PostalCode) ? debtorAddressResponse.PostalCode : request.PostalCode,
                    Landmark = string.IsNullOrWhiteSpace(request.Landmark) ? debtorAddressResponse.Landmark : request.Landmark,
                    Longitude = !string.IsNullOrWhiteSpace(request.Longitude)
                         ? Convert.ToDecimal(request.Longitude, CultureInfo.InvariantCulture)
                         : debtorAddressResponse.Longitude,

                    Latitude = !string.IsNullOrWhiteSpace(request.Latitude)
                         ? Convert.ToDecimal(request.Latitude, CultureInfo.InvariantCulture)
                         : debtorAddressResponse.Latitude,
                    Notes = string.IsNullOrWhiteSpace(request.Notes) ? debtorAddressResponse.Notes : request.Notes,
                    DateCreated = debtorAddressResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityAddressUpdate = await EntityData.EntityData_Custom_Service.EntityAddresses_Update(new EntityAddress()
                {
                    EntityAddressID = entityAddressResponse.EntityAddressID,
                    FK_EntityID = entityAddressResponse.FK_EntityID,
                    EntityRecordID = request.CreditorID ?? entityAddressResponse.EntityRecordID,
                    FK_AddressID = request.AddressID,
                    FK_AddressTypeID = request.FK_AddressTypeID ?? entityAddressResponse.FK_AddressTypeID,
                    IsPrimary = request.IsPrimary ?? entityAddressResponse.IsPrimary,
                    ValidFrom = request.ValidFrom ?? entityAddressResponse.ValidFrom,
                    ValidTo = request.ValidTo ?? entityAddressResponse.ValidTo,
                    DateCreated = entityAddressResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Address Update add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Debtor Contacts

        public async Task<ApiResponse<object>> Add_Creditor_Contact(Req_CreditorContact_Add request)
        {
            try
            {
                _logger.LogService("Starting Creditor Contact Add", request);

                var debtorContactInsert = await EntityData.EntityData_Custom_Service.Contacts_Insert(new Contact()
                {
                    ContactValue = request.ContactValue,
                    FK_ContactTypeID = request.FK_ContactTypeID,
                    FK_DialingCodeID = request.FK_DialingCodeID,
                    IsVerified = request.IsVerified ?? false,
                    VerificationToken = request.VerificationToken,
                    VerifiedAt = request.VerifiedAt,
                    Notes = request.Notes,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityResponse = await Entity_Select_Creditor(new Entity()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityContactInsert = await EntityData.EntityData_Custom_Service.EntityContacts_Insert(new EntityContact()
                {
                    FK_EntityID = entityResponse.EntityID,
                    EntityRecordID = request.FK_CreditorID,
                    FK_ContactID = debtorContactInsert.ContactID,
                    IsPrimary = request.IsPrimary ?? false,
                    IsMarketing = request.IsMarketing ?? false,
                    IsEmergency = request.IsEmergency ?? false,
                    PreferredContactTime = request.PreferredContactTime,
                    PreferredLanguageCode = request.PreferredLanguageCode,
                    ValidFrom = request.ValidFrom ?? DateTime.Now,
                    ValidTo = request.ValidTo,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Address add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Creditor_Contact(Req_CreditorContact_Update request)
        {
            try
            {
                _logger.LogService("Starting Debtor Contact Update", request);

                var debtorContactResponse = await EntityData.EntityData_Custom_Service.Contacts_Select_Single(new Contact()
                {
                    ContactID = request.ContactID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityContactResponse = await Debtors.Debtors_Custom_Service.EntityContacts_Select_Single_ContactID(new EntityContact()
                {
                    FK_ContactID = request.ContactID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var debtorContactUpdate = await EntityData.EntityData_Custom_Service.Contacts_Update(new Contact()
                {
                    ContactID = request.ContactID,
                    ContactValue = string.IsNullOrWhiteSpace(request.ContactValue) ? debtorContactResponse.ContactValue : request.ContactValue,
                    FK_ContactTypeID = request.FK_ContactTypeID ?? debtorContactResponse.FK_ContactTypeID,
                    FK_DialingCodeID = request.FK_DialingCodeID ?? debtorContactResponse.FK_DialingCodeID,
                    IsVerified = request.IsVerified ?? debtorContactResponse.IsVerified,
                    VerificationToken = string.IsNullOrWhiteSpace(request.VerificationToken) ? debtorContactResponse.VerificationToken : request.VerificationToken,
                    VerifiedAt = request.VerifiedAt ?? debtorContactResponse.VerifiedAt,
                    Notes = string.IsNullOrWhiteSpace(request.Notes) ? debtorContactResponse.Notes : request.Notes,
                    DateCreated = debtorContactResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityContactUpdate = await EntityData.EntityData_Custom_Service.EntityContacts_Update(new EntityContact()
                {
                    EntityContactID = entityContactResponse.EntityContactID,
                    FK_EntityID = entityContactResponse.FK_EntityID,
                    EntityRecordID = request.FK_CreditorID ?? entityContactResponse.EntityRecordID,
                    FK_ContactID = request.ContactID,
                    IsPrimary = request.IsPrimary ?? entityContactResponse.IsPrimary,
                    IsMarketing = request.IsMarketing ?? entityContactResponse.IsMarketing,
                    IsEmergency = request.IsEmergency ?? entityContactResponse.IsEmergency,
                    PreferredContactTime = string.IsNullOrWhiteSpace(request.PreferredContactTime) ? entityContactResponse.PreferredContactTime : request.PreferredContactTime,
                    PreferredLanguageCode = string.IsNullOrWhiteSpace(request.PreferredLanguageCode) ? entityContactResponse.PreferredLanguageCode : request.PreferredLanguageCode,
                    ValidFrom = request.ValidFrom ?? entityContactResponse.ValidFrom,
                    ValidTo = request.ValidTo ?? entityContactResponse.ValidTo,
                    DateCreated = entityContactResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Contact add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Debtor Types

        public async Task<ApiResponse<List<Res_CreditorType_List>>> List_CreditorTypes()
        {
            try
            {
                _logger.LogService("Starting Creditor Type List");

                var creditorTypeResponse = await CreditorTypes_Select_All(new CreditorType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CreditorType_List>();

                if (creditorTypeResponse != null && creditorTypeResponse.Any())
                {
                    foreach (var creditorType in creditorTypeResponse)
                    {

                        response.Add(new Res_CreditorType_List()
                        {
                            CreditorTypeID = creditorType.CreditorTypeID,
                            Type = creditorType.Type,
                            Description = creditorType.Description
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor type list", ex);
                return ApiResponse.Fail<List<Res_CreditorType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #endregion
    }
}



