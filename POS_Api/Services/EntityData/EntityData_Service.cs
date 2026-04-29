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

using POS_Common.Models.EntityData.Addresses;
using POS_Common.Models.EntityData.AddressRegions;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.Contacts;
using POS_Common.Models.EntityData.ContactTypes;
using POS_Common.Models.EntityData.Continents;
using POS_Common.Models.EntityData.Countries;
using POS_Common.Models.EntityData.CountryProvinces;
using POS_Common.Models.EntityData.CountrySubregions;
using POS_Common.Models.EntityData.CountryRegions;
using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.DialingCodes;
using POS_Common.Models.EntityData.Entities;
using POS_Common.Models.EntityData.EntityAddresses;
using POS_Common.Models.EntityData.EntityContacts;
using POS_Common.Models.EntityData.Statuses;
using POS_Common.Models.EntityData.StatusGroups;
using POS_Common.Models.EntityData.TimeZones;
using POS_Api.ServiceInterfaces.EntityData;
using POS_Api.Services.EntityData;
using POS_Common.Interfaces;
using POS_Common.ModelsDto.EntityDataController;
using POS_Common.ModelsDto.EntityDataController.Address;
using POS_Common.ModelsDto.EntityDataController.Contact;
using POS_Common.ModelsDto.EntityDataController.Country;
using POS_Common.ModelsDto.EntityDataController.Currency;
using POS_Common.ModelsDto.EntityDataController.DialingCode;
using POS_Common.ModelsDto.EntityDataController.Timezone;
using POS_Common.ModelsDto.EntityDataController.Entity;
using TMIS_Common.Interfaces;
using POS_Common.Models.EntityData.Users;
using POS_Api.ServiceInterfaces.Cache;
using Azure.Core;
using Azure;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.ModelsDto.DebtorsController.DebtorAddress;
using POS_Common.ModelsDto.EntityDataController.SlipPrinter;
using POS_Common.Models.Sync.POS_SlipPrinters;
using POS_Common.ModelsDto.EntityDataController.PaymentType;
using POS_Common.Models.EntityData.POS_PaymentTypes;
using POS_Common.ModelsDto.EntityDataController.TaxType;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.EntityData.POS_PaymentTypeIcons;
using POS_Common.ModelsDto.EntityDataController.LocationCurrency;
using POS_Common.Models.Debtors.POS_LocationCurrencies;
using POS_Common.ModelsDto.EntityDataController.Settings;
using POS_Common.ModelsDto.EntityDataController.ExchangeRates;
using POS_Common.Models.EntityData.POS_Settings;
using POS_Common.Models.Debtors.POS_CostCenters;
using POS_Common.Models.EntityData.POS_ExchangeRates;
using POS_Api.Helpers;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.ModelsDto.EntityDataController.SlipType;
using POS_Common.Models.EntityData.POS_SlipTypes;
using POS_Api.Services.Sync;

namespace POS_Api.Services.EntityData
{
    public class EntityData_Service : EntityData_Custom_Service, IEntityData_Service
    {
        #region Members

        private readonly ILogging_Service _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        private readonly ICache_Service _cacheService;
        private readonly ImageHelper _imageHelper;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public EntityData_Service(IConfiguration configuration, ILogging_Service logger
            , IHttpContextAccessor httpContextAccessor, IUserContext userContext, ICache_Service cacheService, ImageHelper imageHelper)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
            _cacheService = cacheService;
            _imageHelper = imageHelper;

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

        #region Addresses
        public async Task<ApiResponse<List<Res_AllEntityAddress_List>>> List_All_Entity_Addresses(Req_AllEntityAddress_List request)
        {
            try
            {

                _logger.LogService("Starting All Entity Address List", request);

                var allEntityAddressResponse = await EntityAddresses_Select_All_Entity(new EntityAddress()
                {
                    FK_EntityID = request.EntityID,
                    EntityRecordID = request.EntityRecordID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var countries = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Countries;
                var provinces = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Provinces;
                var addressRegions = _cacheService.GetCacheAsync(_userContext.TenantID).Result.AddressRegions;
                var addressTypes = _cacheService.GetCacheAsync(_userContext.TenantID).Result.AddressTypes; 

                var response = new List<Res_AllEntityAddress_List>();

                if (allEntityAddressResponse != null && allEntityAddressResponse.Any())
                {
                    foreach (var allEntityAddress in allEntityAddressResponse)
                    {

                        response.Add(new Res_AllEntityAddress_List()
                        {
                            EntityAddressID = allEntityAddress.EntityAddressID,
                            AddressID = allEntityAddress.FK_AddressID,
                            FK_AddressTypeID = allEntityAddress.FK_AddressTypeID,

                            AddressType = allEntityAddress.FK_AddressTypeID != null
                               ? addressTypes.FirstOrDefault(x => x.AddressTypeID == allEntityAddress.FK_AddressTypeID).Type
                               : null,

                            IsPrimary = allEntityAddress.IsPrimary,
                            ValidFrom = allEntityAddress.ValidFrom,
                            ValidTo = allEntityAddress.ValidTo,
                            FK_CountryID = allEntityAddress.FK_CountryID,

                            Country = allEntityAddress.FK_CountryID != null
                                ? countries.FirstOrDefault(x => x.CountryID == allEntityAddress.FK_CountryID).CountryName
                                : null,

                            FK_ProvinceID = allEntityAddress.FK_ProvinceID,

                            Province = allEntityAddress.FK_ProvinceID != null
                                ? provinces.FirstOrDefault(x => x.CountryProvinceID == allEntityAddress.FK_ProvinceID).ProvinceName
                                : null,

                            FK_AddressRegionID = allEntityAddress.FK_AddressRegionID,

                            AddressRegion = allEntityAddress.FK_AddressRegionID != null
                                ? addressRegions.FirstOrDefault(x => x.AddressRegionID == allEntityAddress.FK_AddressRegionID).RegionName
                                : null,

                            StreetAddress = allEntityAddress.StreetAddress,
                            Locality = allEntityAddress.Locality,
                            PostalCode = allEntityAddress.PostalCode,
                            Landmark = allEntityAddress.Landmark,
                            Latitude = allEntityAddress.Latitude,
                            Longitude = allEntityAddress.Longitude,
                            Notes = allEntityAddress.Notes,
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_AllEntityAddress_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Address_List>>> List_Addresses()
        {
            try
            {
                _logger.LogService("Starting Address List");

                var addressResponse = await Addresses_Select_All(new Address()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Address_List>();

                if (addressResponse != null && addressResponse.Any())
                {
                    foreach (var address in addressResponse)
                    {

                        response.Add(new Res_Address_List()
                        {
                            AddressID = address.AddressID,
                            FK_CountryID = address.FK_CountryID,
                            FK_ProvinceID = address.FK_ProvinceID,
                            FK_AddressRegionID = address.FK_AddressRegionID,
                            StreetAddress = address.StreetAddress,
                            Locality = address.Locality,
                            PostalCode = address.PostalCode,
                            Landmark = address.Landmark,
                            Latitude = address.Latitude,
                            Longitude = address.Longitude,
                            Notes = address.Notes
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_Address_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Address(Req_Address_Add request)
        {
            try
            {
                _logger.LogService("Starting Address Add", request);

                var addressInsert = await Addresses_Insert(new Address()
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
                    DateUpdated = DateTime.Now,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Address add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Address(Req_Address_Update request)
        {
            try
            {
                _logger.LogService("Starting Address Update", request);

                var addressResponse = await Addresses_Select_Single(new Address()
                {
                    AddressID = request.AddressID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (addressResponse == null)
                {
                    _logger.LogService("Address not found", request.AddressID);
                    return ApiResponse.Fail<object>(AppErrorCode.AddressNotFound, new List<string> { "Address not found." }, 404);
                }

                var addressUpdate = await Addresses_Update(new Address()
                {
                    AddressID = request.AddressID,
                    FK_CountryID = request.FK_CountryID ?? addressResponse.FK_CountryID,
                    FK_ProvinceID = request.FK_ProvinceID ?? addressResponse.FK_ProvinceID,
                    FK_AddressRegionID = request.FK_AddressRegionID ?? addressResponse.FK_AddressRegionID,
                    StreetAddress = string.IsNullOrWhiteSpace(request.StreetAddress) ? addressResponse.StreetAddress : request.StreetAddress,
                    Locality = string.IsNullOrWhiteSpace(request.Locality) ? addressResponse.Locality : request.Locality,
                    PostalCode = string.IsNullOrWhiteSpace(request.PostalCode) ? addressResponse.PostalCode : request.PostalCode,
                    Landmark = string.IsNullOrWhiteSpace(request.Landmark) ? addressResponse.Landmark : request.Landmark,
                    Latitude = request.Latitude ?? addressResponse.Latitude,
                    Longitude = request.Longitude ?? addressResponse.Longitude,
                    Notes = string.IsNullOrWhiteSpace(request.Notes) ? addressResponse.Notes : request.Notes,
                    DateCreated = addressResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Role add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Address Regions

        public async Task<ApiResponse<List<Res_AddressRegion_List>>> List_Address_Regions()
        {
            try
            {
                _logger.LogService("Starting Address Region List");

                var addressRegionResponse = await AddressRegions_Select_All(new AddressRegion()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_AddressRegion_List>();

                if (addressRegionResponse != null && addressRegionResponse.Any())
                {
                    foreach (var addressRegion in addressRegionResponse)
                    {

                        response.Add(new Res_AddressRegion_List()
                        {
                            AddressRegionID = addressRegion.AddressRegionID,
                            RegionName = addressRegion.RegionName,
                            Description = addressRegion.Description,
                            FK_CountryID = addressRegion.FK_CountryID,
                            FK_ProvinceID = addressRegion.FK_ProvinceID
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during time zone list", ex);
                return ApiResponse.Fail<List<Res_AddressRegion_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Address_Region(Req_AddressRegion_Add request)
        {
            try
            {
                _logger.LogService("Starting Address Region Add", request);

                var addressRegionInsert = await AddressRegions_Insert(new AddressRegion()
                {
                    RegionName = request.RegionName,
                    Description = request.Description,
                    FK_CountryID = request.FK_CountryID,
                    FK_ProvinceID = request.FK_ProvinceID,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Address Region add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Address_Region(Req_AddressRegion_Update request)
        {
            try
            {
                _logger.LogService("Starting Address Region Update", request);

                var addressRegionResponse = await AddressRegions_Select_Single(new AddressRegion()
                {
                    AddressRegionID = request.AddressRegionID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (addressRegionResponse == null)
                {
                    _logger.LogService("Address region not found", request.AddressRegionID);
                    return ApiResponse.Fail<object>(AppErrorCode.AddressRegionNotFound, new List<string> { "Address region not found." }, 404);
                }

                var addressUpdate = await AddressRegions_Update(new AddressRegion()
                {
                    AddressRegionID = request.AddressRegionID,
                    RegionName = string.IsNullOrWhiteSpace(request.RegionName) ? addressRegionResponse.RegionName : request.RegionName,
                    Description = string.IsNullOrWhiteSpace(request.Description) ? addressRegionResponse.Description : request.Description,
                    FK_CountryID = request.FK_CountryID ?? addressRegionResponse.FK_CountryID,
                    FK_ProvinceID = request.FK_ProvinceID ?? addressRegionResponse.FK_ProvinceID,
                    DateCreated = addressRegionResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Role add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion

        #region Address Types

        public async Task<ApiResponse<List<Res_AddressType_List>>> List_Address_Types()
        {
            try
            {
                _logger.LogService("Starting Address Type List");

                var addressTypeResponse = await AddressTypes_Select_All(new AddressType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_AddressType_List>();

                if (addressTypeResponse != null && addressTypeResponse.Any())
                {
                    foreach (var addressType in addressTypeResponse)
                    {

                        response.Add(new Res_AddressType_List()
                        {
                            AddressTypeID = addressType.AddressTypeID,
                            FK_EntityID = addressType.FK_EntityID,
                            Type = addressType.Type,
                            IsRequired = addressType.IsRequired,
                            CanEdit = addressType.CanEdit
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address type list", ex);
                return ApiResponse.Fail<List<Res_AddressType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Address_Type(Req_AddressType_Add request)
        {
            try
            {
                _logger.LogService("Starting Address Type Add", request);

                var addressTypeInsert = await AddressTypes_Insert(new AddressType()
                {
                    FK_EntityID = request.FK_EntityID,
                    Type = request.Type,
                    IsRequired = request.IsRequired ?? false,
                    CanEdit = request.CanEdit ?? false,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Address Type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Address_Type(Req_AddressType_Update request)
        {
            try
            {
                _logger.LogService("Starting Address Type Update", request);

                var addressTypeResponse = await AddressTypes_Select_Single(new AddressType()
                {
                    AddressTypeID = request.AddressTypeID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (addressTypeResponse == null)
                {
                    _logger.LogService("Address Type not found", request.AddressTypeID);
                    return ApiResponse.Fail<object>(AppErrorCode.AddressTypeNotFound, new List<string> { "Address Type not found." }, 404);
                }

                var addressTypeUpdate = await AddressTypes_Update(new AddressType()
                {
                    AddressTypeID = request.AddressTypeID,
                    FK_EntityID = request.FK_EntityID ?? addressTypeResponse.FK_EntityID,
                    Type = string.IsNullOrWhiteSpace(request.Type) ? addressTypeResponse.Type : request.Type,
                    IsRequired = request.IsRequired ?? addressTypeResponse.IsRequired,
                    CanEdit = request.CanEdit ?? addressTypeResponse.CanEdit,
                    DateCreated = addressTypeResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Address Type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Contacts
        public async Task<ApiResponse<List<Res_AllEntityContact_List>>> List_All_Entity_Contacts(Req_AllEntityContact_List request)
        {
            try
            {

                _logger.LogService("Starting All Entity Contact List", request);

                var allEntityContactResponse = await EntityContacts_Select_All_Entity(new EntityContact()
                {
                    FK_EntityID = request.EntityID,
                    EntityRecordID = request.EntityRecordID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var countries = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Countries;
                var provinces = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Provinces;
                var addressRegions = _cacheService.GetCacheAsync(_userContext.TenantID).Result.AddressRegions;
                var dialingCode = _cacheService.GetCacheAsync(_userContext.TenantID).Result.DialingCodes;
                var addressTypes = _cacheService.GetCacheAsync(_userContext.TenantID).Result.AddressTypes;
                var contactTypes = _cacheService.GetCacheAsync(_userContext.TenantID).Result.ContactTypes;

                var response = new List<Res_AllEntityContact_List>();

                if (allEntityContactResponse != null && allEntityContactResponse.Any())
                {
                    foreach (var allEntityContact in allEntityContactResponse)
                    {

                        response.Add(new Res_AllEntityContact_List()
                        {
                            EntityContactID = allEntityContact.EntityContactID,
                            FK_ContactID = allEntityContact.FK_ContactID,
                            FK_ContactTypeID = allEntityContact.FK_ContactTypeID,

                            ContactType = allEntityContact.FK_ContactTypeID != null
                               ? addressTypes.FirstOrDefault(x => x.AddressTypeID == allEntityContact.FK_ContactTypeID).Type
                               : null,

                            IsPrimary = allEntityContact.IsPrimary,
                            IsMarketing = allEntityContact.IsMarketing,
                            IsEmergency = allEntityContact.IsEmergency,
                            PreferredContactTime = allEntityContact.PreferredContactTime,
                            PreferredLanguageCode = allEntityContact.PreferredLanguageCode,
                            ValidFrom = allEntityContact.ValidFrom,
                            ValidTo = allEntityContact.ValidTo,
                            ContactValue = allEntityContact.ContactValue,

                            FK_DialingCodeID = allEntityContact.FK_DialingCodeID,

                            DialingCode = allEntityContact.FK_DialingCodeID != null
                                ? dialingCode.FirstOrDefault(x => x.DialingCodeID == allEntityContact.FK_DialingCodeID).DialingCode
                                : null,

                            IsVerified = allEntityContact.IsVerified,
                            VerificationToken = allEntityContact.VerificationToken,
                            VerifiedAt = allEntityContact.VerifiedAt,
                            Notes = allEntityContact.Notes,
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_AllEntityContact_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Contact_List>>> List_Contacts()
        {
            try
            {
                _logger.LogService("Starting Contact List");

                var contactResponse = await Contacts_Select_All(new Contact()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Contact_List>();

                if (contactResponse != null && contactResponse.Any())
                {
                    foreach (var contact in contactResponse)
                    {

                        response.Add(new Res_Contact_List()
                        {
                            ContactID = contact.ContactID,
                            ContactValue = contact.ContactValue,
                            FK_ContactTypeID = contact.FK_ContactTypeID,
                            FK_DialingCodeID = contact.FK_DialingCodeID,
                            IsVerified = contact.IsVerified,
                            VerificationToken = contact.VerificationToken,
                            VerifiedAt = contact.VerifiedAt,
                            Notes = contact.Notes
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during contact list", ex);
                return ApiResponse.Fail<List<Res_Contact_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Contact(Req_Contact_Add request)
        {
            try
            {
                _logger.LogService("Starting Contact Add", request);

                var contactInsert = await Contacts_Insert(new Contact()
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

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Contact add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Contact(Req_Contact_Update request)
        {
            try
            {
                _logger.LogService("Starting Contact Update", request);

                var contactResponse = await Contacts_Select_Single(new Contact()
                {
                    ContactID = request.ContactID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (contactResponse == null)
                {
                    _logger.LogService("Contact not found", request.ContactID);
                    return ApiResponse.Fail<object>(AppErrorCode.ContactNotFound, new List<string> { "Contact not found." }, 404);
                }

                var contactUpdate = await Contacts_Update(new Contact()
                {
                    ContactID = request.ContactID,
                    ContactValue = string.IsNullOrWhiteSpace(request.ContactValue) ? contactResponse.ContactValue : request.ContactValue,
                    FK_ContactTypeID = request.FK_ContactTypeID ?? contactResponse.FK_ContactTypeID,
                    FK_DialingCodeID = request.FK_DialingCodeID ?? contactResponse.FK_DialingCodeID,
                    IsVerified = request.IsVerified ?? contactResponse.IsVerified,
                    VerificationToken = string.IsNullOrWhiteSpace(request.VerificationToken) ? contactResponse.VerificationToken : request.VerificationToken,
                    VerifiedAt = request.VerifiedAt ?? contactResponse.VerifiedAt,
                    Notes = string.IsNullOrWhiteSpace(request.Notes) ? contactResponse.Notes : request.Notes,
                    DateCreated = contactResponse.DateCreated,
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

        #region Contact Types

        public async Task<ApiResponse<List<Res_ContactType_List>>> List_Contact_Types()
        {
            try
            {
                _logger.LogService("Starting Contact Type List");

                var contactTypeResponse = await ContactTypes_Select_All(new ContactType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ContactType_List>();

                if (contactTypeResponse != null && contactTypeResponse.Any())
                {
                    foreach (var contactType in contactTypeResponse)
                    {

                        response.Add(new Res_ContactType_List()
                        {
                            ContactTypeID = contactType.ContactTypeID,
                            Type = contactType.Type,
                            IsPhoneNumberType = contactType.IsPhoneNumberType,
                            IsEmailType = contactType.IsEmailType
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during contact type list", ex);
                return ApiResponse.Fail<List<Res_ContactType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Contact_Type(Req_ContactType_Add request)
        {
            try
            {
                _logger.LogService("Starting Contact Type Add", request);

                var contactTypeInsert = await ContactTypes_Insert(new ContactType()
                {
                    Type = request.Type,
                    IsPhoneNumberType = request.IsPhoneNumberType ?? false,
                    IsEmailType = request.IsEmailType ?? false,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Contact type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Contact_Type(Req_ContactType_Update request)
        {
            try
            {
                _logger.LogService("Starting Contact Type Update", request);

                var contactTypeResponse = await ContactTypes_Select_Single(new ContactType()
                {
                    ContactTypeID = request.ContactTypeID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (contactTypeResponse == null)
                {
                    _logger.LogService("Contact type not found", request.ContactTypeID);
                    return ApiResponse.Fail<object>(AppErrorCode.ContactTypeNotFound, new List<string> { "Contact type not found." }, 404);
                }

                var contactTypeUpdate = await ContactTypes_Update(new ContactType()
                {
                    ContactTypeID = request.ContactTypeID,
                    Type = string.IsNullOrWhiteSpace(request.Type) ? contactTypeResponse.Type : request.Type,
                    IsPhoneNumberType = request.IsPhoneNumberType ?? contactTypeResponse.IsPhoneNumberType,
                    IsEmailType = request.IsEmailType ?? contactTypeResponse.IsEmailType
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Contact type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Continents

        public async Task<ApiResponse<List<Res_Continent_List>>> List_Continents()
        {
            try
            {
                _logger.LogService("Starting Continent List");

                var continentResponse = await Continents_Select_All(new Continent()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Continent_List>();

                if (continentResponse != null && continentResponse.Any())
                {
                    foreach (var continent in continentResponse)
                    {

                        response.Add(new Res_Continent_List()
                        {
                            ContinentID = continent.ContinentID,
                            Name = continent.Name,
                            ShortCode = continent.ShortCode
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during continent list", ex);
                return ApiResponse.Fail<List<Res_Continent_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Countries

        public async Task<ApiResponse<List<Res_Country_List>>> List_Countries()
        {
            try
            {
                _logger.LogService("Starting Country List");

                var countryResponse = await Countries_Select_All(new Country()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Country_List>();

                if (countryResponse != null && countryResponse.Any())
                {
                    foreach (var country in countryResponse)
                    {

                        response.Add(new Res_Country_List()
                        {
                            CountryID = country.CountryID,
                            CountryName = country.CountryName,
                            NativeName = country.NativeName,
                            OfficialName = country.OfficialName,
                            ISO2Code = country.ISO2Code,
                            ISO3Code = country.ISO3Code,
                            PrimaryLanguageCode = country.PrimaryLanguageCode,
                            NumericCode = country.NumericCode,
                            FK_DialingCodeID = country.FK_DialingCodeID,
                            FK_CurrencyID = country.FK_CurrencyID,
                            FK_CountryRegionID = country.FK_CountryRegionID,
                            FK_CountrySubregionID = country.FK_CountrySubregionID,
                            FK_TimeZoneID = country.FK_TimeZoneID
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during country list", ex);
                return ApiResponse.Fail<List<Res_Country_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Country(Req_Country_Add request)
        {
            try
            {
                _logger.LogService("Starting Country Add", request);

                var countryInsert = await Countries_Insert(new Country()
                {
                    CountryName = request.CountryName,
                    NativeName = request.NativeName,
                    OfficialName = request.OfficialName,
                    ISO2Code = request.ISO2Code,
                    ISO3Code = request.ISO3Code,
                    PrimaryLanguageCode = request.PrimaryLanguageCode,
                    NumericCode = request.NumericCode,
                    FK_DialingCodeID = request.FK_DialingCodeID,
                    FK_CurrencyID = request.FK_CurrencyID,
                    FK_CountryRegionID = request.FK_CountryRegionID,
                    FK_CountrySubregionID = request.FK_CountrySubregionID,
                    FK_TimeZoneID = request.FK_TimeZoneID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Country add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Country(Req_Country_Update request)
        {
            try
            {
                _logger.LogService("Starting Country Update", request);

                var countryResponse = await Countries_Select_Single(new Country()
                {
                    CountryID = request.CountryID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (countryResponse == null)
                {
                    _logger.LogService("Country not found", request.CountryID);
                    return ApiResponse.Fail<object>(AppErrorCode.CountryNotFound, new List<string> { "Country not found." }, 404);
                }

                var countryUpdate = await Countries_Update(new Country()
                {
                    CountryID = request.CountryID,
                    CountryName = string.IsNullOrWhiteSpace(request.CountryName) ? countryResponse.CountryName : request.CountryName,
                    NativeName = string.IsNullOrWhiteSpace(request.NativeName) ? countryResponse.NativeName : request.NativeName,
                    OfficialName = string.IsNullOrWhiteSpace(request.OfficialName) ? countryResponse.OfficialName : request.OfficialName,
                    ISO2Code = string.IsNullOrWhiteSpace(request.ISO2Code) ? countryResponse.ISO2Code : request.ISO2Code,
                    ISO3Code = string.IsNullOrWhiteSpace(request.ISO3Code) ? countryResponse.ISO3Code : request.ISO3Code,
                    PrimaryLanguageCode = string.IsNullOrWhiteSpace(request.PrimaryLanguageCode) ? countryResponse.PrimaryLanguageCode : request.PrimaryLanguageCode,
                    NumericCode = request.NumericCode ?? countryResponse.NumericCode,
                    FK_DialingCodeID = request.FK_DialingCodeID ?? countryResponse.FK_DialingCodeID,
                    FK_CurrencyID = request.FK_CurrencyID ?? countryResponse.FK_CurrencyID,
                    FK_CountryRegionID = request.FK_CountryRegionID ?? countryResponse.FK_CountryRegionID,
                    FK_CountrySubregionID = request.FK_CountrySubregionID ?? countryResponse.FK_CountrySubregionID,
                    FK_TimeZoneID = request.FK_TimeZoneID ?? countryResponse.FK_TimeZoneID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during country add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Country Provinces

        public async Task<ApiResponse<List<Res_CountryProvince_List>>> List_Country_Provinces()
        {
            try
            {
                _logger.LogService("Starting Country Province List");

                var countryProvinceResponse = await CountryProvinces_Select_All(new CountryProvince()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CountryProvince_List>();

                if (countryProvinceResponse != null && countryProvinceResponse.Any())
                {
                    foreach (var countryProvince in countryProvinceResponse)
                    {

                        response.Add(new Res_CountryProvince_List()
                        {
                            CountryProvinceID = countryProvince.CountryProvinceID,
                            ProvinceName = countryProvince.ProvinceName,
                            ISO2Code = countryProvince.ISO2Code,
                            FK_CountryID = countryProvince.FK_CountryID
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during country province list", ex);
                return ApiResponse.Fail<List<Res_CountryProvince_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Country_Province(Req_CountryProvince_Add request)
        {
            try
            {
                _logger.LogService("Starting Country Province Add", request);

                var countryProvinceInsert = await CountryProvinces_Insert(new CountryProvince()
                {
                    ProvinceName = request.ProvinceName,
                    ISO2Code = request.ISO2Code,
                    FK_CountryID = request.FK_CountryID,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Country Province add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Country_Province(Req_CountryProvince_Update request)
        {
            try
            {
                _logger.LogService("Starting Country Province Update", request);

                var countryProvinceResponse = await CountryProvinces_Select_Single(new CountryProvince()
                {
                    CountryProvinceID = request.CountryProvinceID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (countryProvinceResponse == null)
                {
                    _logger.LogService("Country Province not found", request.CountryProvinceID);
                    return ApiResponse.Fail<object>(AppErrorCode.CountryProvinceNotFound, new List<string> { "Country Province not found." }, 404);
                }

                var countryProvinceUpdate = await CountryProvinces_Update(new CountryProvince()
                {
                    CountryProvinceID = request.CountryProvinceID,
                    ProvinceName = string.IsNullOrWhiteSpace(request.ProvinceName) ? countryProvinceResponse.ProvinceName : request.ProvinceName,
                    ISO2Code = string.IsNullOrWhiteSpace(request.ISO2Code) ? countryProvinceResponse.ISO2Code : request.ISO2Code,
                    FK_CountryID = request.FK_CountryID ?? countryProvinceResponse.FK_CountryID,
                    DateCreated = countryProvinceResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during country Province add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Country Regions

        public async Task<ApiResponse<List<Res_CountryRegion_List>>> List_Country_Regions()
        {
            try
            {
                _logger.LogService("Starting Country Region List");

                var countryRegionResponse = await CountryRegions_Select_All(new CountryRegion()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CountryRegion_List>();

                if (countryRegionResponse != null && countryRegionResponse.Any())
                {
                    foreach (var countryRegion in countryRegionResponse)
                    {

                        response.Add(new Res_CountryRegion_List()
                        {
                            CountryRegionID = countryRegion.CountryRegionID,
                            Region = countryRegion.Region,
                            FK_ContinentID = countryRegion.FK_ContinentID
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during country Region list", ex);
                return ApiResponse.Fail<List<Res_CountryRegion_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Country_Region(Req_CountryRegion_Add request)
        {
            try
            {
                _logger.LogService("Starting Country Region Add", request);

                var countryRegionInsert = await CountryRegions_Insert(new CountryRegion()
                {
                    Region = request.Region,
                    FK_ContinentID = request.FK_ContinentID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Country Region add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Country_Region(Req_CountryRegion_Update request)
        {
            try
            {
                _logger.LogService("Starting Country Region Update", request);

                var countryRegionResponse = await CountryRegions_Select_Single(new CountryRegion()
                {
                    CountryRegionID = request.CountryRegionID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (countryRegionResponse == null)
                {
                    _logger.LogService("Country Region not found", request.CountryRegionID);
                    return ApiResponse.Fail<object>(AppErrorCode.CountryRegionNotFound, new List<string> { "Country Region not found." }, 404);
                }

                var countryRegionUpdate = await CountryRegions_Update(new CountryRegion()
                {
                    CountryRegionID = request.CountryRegionID,
                    Region = string.IsNullOrWhiteSpace(request.Region) ? countryRegionResponse.Region : request.Region,
                    FK_ContinentID = request.FK_ContinentID ?? countryRegionResponse.FK_ContinentID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during country region add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Country Subregions

        public async Task<ApiResponse<List<Res_CountrySubRegion_List>>> List_Country_Subregions()
        {
            try
            {
                _logger.LogService("Starting Country Region List");

                var countrySubRegionResponse = await CountrySubregions_Select_All(new CountrySubregion()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CountrySubRegion_List>();

                if (countrySubRegionResponse != null && countrySubRegionResponse.Any())
                {
                    foreach (var countrySubRegion in countrySubRegionResponse)
                    {

                        response.Add(new Res_CountrySubRegion_List()
                        {
                            CountrySubregionID = countrySubRegion.CountrySubregionID,
                            Subregion = countrySubRegion.Subregion,
                            FK_CountryRegionID = countrySubRegion.FK_CountryRegionID
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during country subregion list", ex);
                return ApiResponse.Fail<List<Res_CountrySubRegion_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Country_Subregion(Req_CountrySubRegion_Add request)
        {
            try
            {
                _logger.LogService("Starting Country Subregion Add", request);

                var countrySubregionInsert = await CountrySubregions_Insert(new CountrySubregion()
                {
                    Subregion = request.Subregion,
                    FK_CountryRegionID = request.FK_CountryRegionID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Country Subregion add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Country_Subregion(Req_CountrySubRegion_Update request)
        {
            try
            {
                _logger.LogService("Starting Country Subregion Update", request);

                var countrySubregionResponse = await CountrySubregions_Select_Single(new CountrySubregion()
                {
                    CountrySubregionID = request.CountrySubregionID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (countrySubregionResponse == null)
                {
                    _logger.LogService("Country Subregion not found", request.CountrySubregionID);
                    return ApiResponse.Fail<object>(AppErrorCode.CountrySubregionNotFound, new List<string> { "Country Subregion not found." }, 404);
                }

                var countrySubregionUpdate = await CountrySubregions_Update(new CountrySubregion()
                {
                    CountrySubregionID = request.CountrySubregionID,
                    Subregion = string.IsNullOrWhiteSpace(request.Subregion) ? countrySubregionResponse.Subregion : request.Subregion,
                    FK_CountryRegionID = request.FK_CountryRegionID ?? countrySubregionResponse.FK_CountryRegionID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during country subregion add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Currencies

        public async Task<ApiResponse<List<Res_Currency_List>>> List_Currencies()
        {
            try
            {
                _logger.LogService("Starting Currency List");

                var currencyResponse = await Currencies_Select_All(new Currency()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Currency_List>();

                if (currencyResponse != null && currencyResponse.Any())
                {
                    foreach (var currency in currencyResponse)
                    {

                        response.Add(new Res_Currency_List()
                        {
                            CurrencyID = currency.CurrencyID,
                            Currency = currency.Currency,
                            Name = currency.Name,
                            ISO2Code = currency.ISO2Code
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during currency list", ex);
                return ApiResponse.Fail<List<Res_Currency_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Currency(Req_Currency_Add request)
        {
            try
            {
                _logger.LogService("Starting Currency Add", request);

                var CurrencyInsert = await Currencies_Insert(new Currency()
                {
                    Currency = request.Currency,
                    Name = request.Name,
                    ISO2Code = request.ISO2Code
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Currency add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Currency(Req_Currency_Update request)
        {
            try
            {
                _logger.LogService("Starting Currency Update", request);

                var currencyResponse = await Currencies_Select_Single(new Currency()
                {
                    CurrencyID = request.CurrencyID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (currencyResponse == null)
                {
                    _logger.LogService("Currency not found", request.CurrencyID);
                    return ApiResponse.Fail<object>(AppErrorCode.CurrencyNotFound, new List<string> { "Currency not found." }, 404);
                }

                var CurrencyUpdate = await Currencies_Update(new Currency()
                {
                    CurrencyID = request.CurrencyID,
                    Currency = string.IsNullOrWhiteSpace(request.Currency) ? currencyResponse.Currency : request.Currency,
                    Name = string.IsNullOrWhiteSpace(request.Name) ? currencyResponse.Name : request.Name,
                    ISO2Code = string.IsNullOrWhiteSpace(request.ISO2Code) ? currencyResponse.ISO2Code : request.ISO2Code
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Currency add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Location Currency

        public async Task<ApiResponse<List<Res_LocationCurrency_List>>> List_Location_Currencies(Req_LocationCurrency_List request)
        {
            try
            {
                _logger.LogService("Starting Location Currency List");

                var locationCurrencyResponse = await Location_Currencies_Select_Active(new LocationCurrencies()
                {
                    FK_LocationID = request.LocationID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_LocationCurrency_List>();

                if (locationCurrencyResponse != null && locationCurrencyResponse.Any())
                {
                    foreach (var currency in locationCurrencyResponse)
                    {

                        response.Add(new Res_LocationCurrency_List()
                        {
                            LocationCurrencyID = currency.LocationCurrencyID,
                            CurrencyID = currency.FK_CurrencyID,
                            Currency = currency.Currency,
                            Symbol = currency.Symbol,
                            IsActive = currency.IsActive
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during currency list", ex);
                return ApiResponse.Fail<List<Res_LocationCurrency_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Location_Currency(Req_LocationCurrency_Add request)
        {
            try
            {
                _logger.LogService("Starting Currency Add", request);

                var locationCurrencyInsert = await Debtors.Debtors_Custom_Service.POS_LocationCurrencies_Insert(new LocationCurrencies()
                {
                    FK_CurrencyID = request.CurrencyID,
                    FK_LocationID = request.LocationID,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Currency add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Location_Currency(Req_LocationCurrency_Remove request)
        {
            try
            {
                _logger.LogService("Starting Currency Update", request);

                var locationCurrencyRemove = await Location_Currency_Remove(new LocationCurrencies()
                {
                    LocationCurrencyID = request.LocationCurrencyID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Currency add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Dialing Codes

        public async Task<ApiResponse<List<Res_DialingCode_List>>> List_Dialing_Codes()
        {
            try
            {
                _logger.LogService("Starting Dialing Code List");

                var dialingCodeResponse = await DialingCodes_Select_All(new DialingCode()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_DialingCode_List>();

                if (dialingCodeResponse != null && dialingCodeResponse.Any())
                {
                    foreach (var dialingCode in dialingCodeResponse)
                    {
                        response.Add(new Res_DialingCode_List()
                        {
                            DialingCodeID = dialingCode.DialingCodeID,
                            DialingCode = dialingCode.DialingCode,
                            ISO2Code = dialingCode.ISO2Code
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during dialing code list", ex);
                return ApiResponse.Fail<List<Res_DialingCode_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Dialing_Code(Req_DialingCode_Add request)
        {
            try
            {
                _logger.LogService("Starting Dialing Code Add", request);

                var dialingCodeInsert = await DialingCodes_Insert(new DialingCode()
                {
                    DialingCode = request.DialingCode,
                    ISO2Code = request.ISO2Code
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during dialing code add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Dialing_Code(Req_DialingCode_Update request)
        {
            try
            {
                _logger.LogService("Starting Dialing Code Update", request);

                var dialingCodeResponse = await DialingCodes_Select_Single(new DialingCode()
                {
                    DialingCodeID = request.DialingCodeID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (dialingCodeResponse == null)
                {
                    _logger.LogService("Dialing Code not found", request.DialingCodeID);
                    return ApiResponse.Fail<object>(AppErrorCode.DialingCodeNotFound, new List<string> { "Dialing Code not found." }, 404);
                }

                var dialingCodeUpdate = await DialingCodes_Update(new DialingCode()
                {
                    DialingCodeID = request.DialingCodeID,
                    DialingCode = string.IsNullOrWhiteSpace(request.DialingCode) ? dialingCodeResponse.DialingCode : request.DialingCode,
                    ISO2Code = string.IsNullOrWhiteSpace(request.ISO2Code) ? dialingCodeResponse.ISO2Code : request.ISO2Code
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during dialing code add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Entities

        public async Task<ApiResponse<List<Res_Entity_List>>> List_Entities()
        {
            try
            {
                _logger.LogService("Starting Entity List");

                var entityResponse = await Entities_Select_All(new Entity()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Entity_List>();

                if (entityResponse != null && entityResponse.Any())
                {
                    foreach (var entity in entityResponse)
                    {
                        response.Add(new Res_Entity_List()
                        {
                            EntityID = entity.EntityID,
                            Name = entity.Name
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during entity list", ex);
                return ApiResponse.Fail<List<Res_Entity_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Entity Addressses

        public async Task<ApiResponse<List<Res_EntityAddress_List>>> List_Entity_Addresses()
        {
            try
            {
                _logger.LogService("Starting Entity Address List");

                var entityAddressResponse = await EntityAddresses_Select_All(new EntityAddress()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_EntityAddress_List>();

                if (entityAddressResponse != null && entityAddressResponse.Any())
                {
                    foreach (var entityAddress in entityAddressResponse)
                    {
                        response.Add(new Res_EntityAddress_List()
                        {
                            EntityAddressID = entityAddress.EntityAddressID,
                            FK_EntityID = entityAddress.FK_EntityID,
                            EntityRecordID = entityAddress.EntityRecordID,
                            FK_AddressID = entityAddress.FK_AddressID,
                            FK_AddressTypeID = entityAddress.FK_AddressTypeID,
                            IsPrimary = entityAddress.IsPrimary,
                            ValidFrom = entityAddress.ValidFrom,
                            ValidTo = entityAddress.ValidTo
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during entity address list", ex);
                return ApiResponse.Fail<List<Res_EntityAddress_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Entity_Address(Req_EntityAddress_Add request)
        {
            try
            {
                _logger.LogService("Starting Entity Address Add", request);

                var entityAddressInsert = await EntityAddresses_Insert(new EntityAddress()
                {
                    FK_EntityID = request.FK_EntityID,
                    EntityRecordID = request.EntityRecordID,
                    FK_AddressID = request.FK_AddressID,
                    FK_AddressTypeID = request.FK_AddressTypeID,
                    IsPrimary = request.IsPrimary ?? false,
                    ValidFrom = request.ValidFrom ?? DateTime.Now,
                    ValidTo = request.ValidTo ?? DateTime.Now,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during entity address add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Entity_Address(Req_EntityAddress_Update request)
        {
            try
            {
                _logger.LogService("Starting Entity Address Update", request);

                var entityAddressResponse = await EntityAddresses_Select_Single(new EntityAddress()
                {
                    EntityAddressID = request.EntityAddressID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (entityAddressResponse == null)
                {
                    _logger.LogService("Entity address not found", request.EntityAddressID);
                    return ApiResponse.Fail<object>(AppErrorCode.EntityAddressNotFound, new List<string> { "Entity address not found." }, 404);
                }

                var entityAddressUpdate = await EntityAddresses_Update(new EntityAddress()
                {
                    EntityAddressID = request.EntityAddressID,
                    FK_EntityID = request.FK_EntityID ?? entityAddressResponse.FK_EntityID,
                    EntityRecordID = request.EntityRecordID ?? entityAddressResponse.EntityRecordID,
                    FK_AddressID = request.FK_AddressID ?? entityAddressResponse.FK_AddressID,
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
                _logger.LogService("Exception during entity address add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Entity Contacts

        public async Task<ApiResponse<List<Res_EntityContact_List>>> List_Entity_Contacts()
        {
            try
            {
                _logger.LogService("Starting Entity Contact List");

                var entityContactResponse = await EntityContacts_Select_All(new EntityContact()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_EntityContact_List>();

                if (entityContactResponse != null && entityContactResponse.Any())
                {
                    foreach (var entityContact in entityContactResponse)
                    {
                        response.Add(new Res_EntityContact_List()
                        {
                            EntityContactID = entityContact.EntityContactID,
                            FK_EntityID = entityContact.FK_EntityID,
                            EntityRecordID = entityContact.EntityRecordID,
                            FK_ContactID = entityContact.FK_ContactID,
                            IsPrimary = entityContact.IsPrimary,
                            IsMarketing = entityContact.IsMarketing,
                            IsEmergency = entityContact.IsEmergency,
                            PreferredContactTime = entityContact.PreferredContactTime,
                            PreferredLanguageCode = entityContact.PreferredLanguageCode,
                            ValidFrom = entityContact.ValidFrom,
                            ValidTo = entityContact.ValidTo
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during entity contact list", ex);
                return ApiResponse.Fail<List<Res_EntityContact_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Entity_Contact(Req_EntityContact_Add request)
        {
            try
            {
                _logger.LogService("Starting Entity Contact Add", request);

                var entityContactInsert = await EntityContacts_Insert(new EntityContact()
                {
                    FK_EntityID = request.FK_EntityID,
                    EntityRecordID = request.EntityRecordID,
                    FK_ContactID = request.FK_ContactID,
                    IsPrimary = request.IsPrimary,
                    IsMarketing = request.IsMarketing,
                    IsEmergency = request.IsEmergency,
                    PreferredContactTime = request.PreferredContactTime,
                    PreferredLanguageCode = request.PreferredLanguageCode,
                    ValidFrom = request.ValidFrom,
                    ValidTo = request.ValidTo,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during entity contact add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Entity_Contact(Req_EntityContact_Update request)
        {
            try
            {
                _logger.LogService("Starting Entity Contact Update", request);

                var entityContactResponse = await EntityContacts_Select_Single(new EntityContact()
                {
                    EntityContactID = request.EntityContactID,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (entityContactResponse == null)
                {
                    _logger.LogService("Entity contact not found", request.EntityContactID);
                    return ApiResponse.Fail<object>(AppErrorCode.EntityContactNotFound, new List<string> { "Entity contact not found." }, 404);
                }

                var entityContactUpdate = await EntityContacts_Update(new EntityContact()
                {
                    EntityContactID = request.EntityContactID,
                    FK_EntityID = request.FK_EntityID ?? entityContactResponse.FK_EntityID,
                    EntityRecordID = request.EntityRecordID ?? entityContactResponse.EntityRecordID,
                    FK_ContactID = request.FK_ContactID ?? entityContactResponse.FK_ContactID,
                    IsPrimary = request.IsPrimary ?? entityContactResponse.IsPrimary,
                    IsMarketing = request.IsMarketing ?? entityContactResponse.IsMarketing,
                    IsEmergency = request.IsEmergency ?? entityContactResponse.IsEmergency,
                    PreferredContactTime = request.PreferredContactTime ?? entityContactResponse.PreferredContactTime,
                    PreferredLanguageCode = request.PreferredLanguageCode ?? entityContactResponse.PreferredLanguageCode,
                    ValidFrom = request.ValidFrom ?? entityContactResponse.ValidFrom,
                    ValidTo = request.ValidTo ?? entityContactResponse.ValidTo,
                    DateCreated = entityContactResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during entity contact add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Statuses/Status Groups

        public async Task<ApiResponse<List<Res_Status_List>>> List_Statuses()
        {
            try
            {
                _logger.LogService("Starting Status List");

                var statusResponse = await Statuses_Select_All(new Status()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Status_List>();

                if (statusResponse != null && statusResponse.Any())
                {
                    foreach (var status in statusResponse)
                    {
                        response.Add(new Res_Status_List()
                        {
                            StatusID = status.StatusID,
                            FK_EntityID = status.FK_EntityID,
                            FK_StatusGroupID = status.FK_StatusGroupID,
                            SystemCode = status.SystemCode,
                            DisplayName = status.DisplayName,
                            IsActive = status.IsActive,
                            CanEdit = status.CanEdit,
                            ShowInUI = status.ShowInUI,
                            SortOrder = status.SortOrder
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during status list", ex);
                return ApiResponse.Fail<List<Res_Status_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_StatusGroup_List>>> List_Status_Groups()
        {
            try
            {
                _logger.LogService("Starting Status Group List");

                var stausGroupResponse = await StatusGroups_Select_All(new StatusGroup()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_StatusGroup_List>();

                if (stausGroupResponse != null && stausGroupResponse.Any())
                {
                    foreach (var statusGroup in stausGroupResponse)
                    {
                        response.Add(new Res_StatusGroup_List()
                        {
                            StatusGroupID = statusGroup.StatusGroupID,
                            GroupName = statusGroup.GroupName,
                            Description = statusGroup.Description
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during status group list", ex);
                return ApiResponse.Fail<List<Res_StatusGroup_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Timezones

        public async Task<ApiResponse<List<Res_Timezone_List>>> List_Timezones()
        {
            try
            {
                _logger.LogService("Starting Timezone List");

                // 1. Authenticate user (you should add the logic to retrieve user from DB here)
                var timeZoneResponse = await TimeZones_Select_All(new _TimeZone()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Timezone_List>();

                foreach (var timeZone in timeZoneResponse)
                {

                    response.Add(new Res_Timezone_List()
                    {
                        TimeZoneID = (int)timeZone.TimeZoneID,
                        TimeZone = timeZone.TimeZone,
                        UTCOffset = timeZone.UTCOffset,
                        ObservesDST = (bool)timeZone.ObservesDST
                    });
                }




                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during time zone list", ex);
                return ApiResponse.Fail<List<Res_Timezone_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Timezone(Req_Timezone_Add request)
        {
            try
            {
                _logger.LogService("Starting Timezone Add", request);

                var timezoneInsert = await TimeZones_Insert(new _TimeZone()
                {
                    TimeZone = request.TimeZone,
                    UTCOffset = request.UTCOffset,
                    ObservesDST = request.ObservesDST
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Timezone add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Timezone(Req_Timezone_Update request)
        {
            try
            {
                _logger.LogService("Starting Timezone Update", request);

                var timezoneResponse = await TimeZones_Select_Single(new _TimeZone()
                {
                    TimeZoneID = request.TimeZoneID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (timezoneResponse == null)
                {
                    _logger.LogService("Time Zone not found", request.TimeZoneID);
                    return ApiResponse.Fail<object>(AppErrorCode.TimeZoneNotFound, new List<string> { "Time Zone not found." }, 404);
                }

                var timezoneUpdate = await TimeZones_Update(new _TimeZone()
                {
                    TimeZoneID = request.TimeZoneID,
                    TimeZone = string.IsNullOrWhiteSpace(request.TimeZone) ? timezoneResponse.TimeZone : request.TimeZone,
                    UTCOffset = request.UTCOffset ?? timezoneResponse.UTCOffset,
                    ObservesDST = request.ObservesDST ?? timezoneResponse.ObservesDST
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during time zone update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Slip Printers

        public async Task<ApiResponse<List<Res_SlipPrinter_List>>> List_Slip_Printers()
        {
            try
            {
                _logger.LogService("Starting Slip Printer List");

                // 1. Authenticate user (you should add the logic to retrieve user from DB here)
                var slipPrinterResponse = await Sync_Base_Service.POS_SlipPrinters_Select_All(new SlipPrinter()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var location = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Location;
                var costCenter = _cacheService.GetCacheAsync(_userContext.TenantID).Result.CostCenter;
                var user = _cacheService.GetCacheAsync(_userContext.TenantID).Result.User;

                var response = new List<Res_SlipPrinter_List>();
                if (slipPrinterResponse != null && slipPrinterResponse.Any())
                {
                    foreach (var slipPrinters in slipPrinterResponse)
                    {
                        response.Add(new Res_SlipPrinter_List()
                        {
                            SlipPrinterID = (int)slipPrinters.SlipPrinterID,
                            DebtorID = (int)slipPrinters.FK_LocationID,

                            Debtor = slipPrinters.FK_LocationID != null
                                   ? location?.FirstOrDefault(x => x.LocationID == slipPrinters.FK_LocationID)?.Name
                                   : null,

                            CostCenterID = slipPrinters.CostCenterID,

                            CostCenter = slipPrinters.CostCenterID != null
                                   ? costCenter?.FirstOrDefault(x => x.CostCenterID == slipPrinters.CostCenterID)?.Name
                                   : null,

                            Name = slipPrinters.Name,
                            Model = slipPrinters.Model,
                            IpAddress = slipPrinters.IpAddress,
                            Port = slipPrinters.Port,
                            IsActive = slipPrinters.IsActive,
                            IsDefault = slipPrinters.IsDefault,

                            CreatedBy = slipPrinters.FK_CreatedUserID != null
                                   ? user?.FirstOrDefault(x => x.UserID == slipPrinters.FK_CreatedUserID) != null
                                   ? user.FirstOrDefault(x => x.UserID == slipPrinters.FK_CreatedUserID).Firstname + " " +
                                     user.FirstOrDefault(x => x.UserID == slipPrinters.FK_CreatedUserID).Lastname
                                   : null
                                   : null,

                            UpdatedBy = slipPrinters.FK_UpdatedUserID != null
                                   ? user?.FirstOrDefault(x => x.UserID == slipPrinters.FK_UpdatedUserID) != null
                                   ? user.FirstOrDefault(x => x.UserID == slipPrinters.FK_UpdatedUserID).Firstname + " " +
                                     user.FirstOrDefault(x => x.UserID == slipPrinters.FK_UpdatedUserID).Lastname
                                   : null
                                   : null,
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during time zone list", ex);
                return ApiResponse.Fail<List<Res_SlipPrinter_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Slip_Printer(Req_SlipPrinter_Add request)
        {
            try
            {
                _logger.LogService("Starting Slip Printer Add", request);

                var slipPrinterInsert = await Sync_Base_Service.POS_SlipPrinters_Insert(new SlipPrinter()
                {
                    FK_LocationID = request.DebtorID,
                    CostCenterID = request.CostCenterID,
                    Name = request.Name,
                    Model = request.Model,
                    IpAddress = request.IpAddress,
                    Port = request.Port,
                    IsActive = request.IsActive,
                    IsDefault = request.IsDefault,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Timezone add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Slip_Printer(Req_SlipPrinter_Update request)
        {
            try
            {
                _logger.LogService("Starting Slip Printer Update", request);

                var slipPrinterResponse = await Sync_Base_Service.POS_SlipPrinters_Select_Single(new SlipPrinter()
                {
                    SlipPrinterID = request.SlipPrinterID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (slipPrinterResponse == null)
                {
                    _logger.LogService("Slip Printer not found", request.SlipPrinterID);
                    return ApiResponse.Fail<object>(AppErrorCode.TimeZoneNotFound, new List<string> { "Slip Printer not found." }, 404);
                }   

                var slipPrinterUpdate = await Sync_Base_Service.POS_SlipPrinters_Update(new SlipPrinter()
                {
                    SlipPrinterID = request.SlipPrinterID,
                    FK_LocationID = request.DebtorID ?? slipPrinterResponse.FK_LocationID,
                    CostCenterID = request.CostCenterID ?? slipPrinterResponse.CostCenterID,
                    Name = string.IsNullOrWhiteSpace(request.Name) ? slipPrinterResponse.Name : request.Name,
                    Model = string.IsNullOrWhiteSpace(request.Model) ? slipPrinterResponse.Model : request.Model,
                    IpAddress = string.IsNullOrWhiteSpace(request.IpAddress) ? slipPrinterResponse.IpAddress : request.IpAddress,
                    Port = request.Port ?? slipPrinterResponse.Port,
                    IsActive = request.IsActive ?? slipPrinterResponse.IsActive,
                    IsDefault = request.IsDefault ?? slipPrinterResponse.IsDefault,
                    FK_CreatedUserID = slipPrinterResponse.FK_CreatedUserID,
                    FK_UpdatedUserID = _userContext.UserID,
                    DateCreated = slipPrinterResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during time zone update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Payment Types

        public async Task<ApiResponse<List<Res_PaymentTypeIcon_List>>> List_Payment_Type_Icons()
        {
            try
            {
                _logger.LogService("Starting Payment Type Icon List");

                var paymentTypeIconResponse = await POS_PaymentTypeIcons_Select_All(new PaymentTypeIcon()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var response = new List<Res_PaymentTypeIcon_List>();
                if (paymentTypeIconResponse != null && paymentTypeIconResponse.Any())
                {
                    foreach (var paymentTypeIcon in paymentTypeIconResponse)
                    {
                        response.Add(new Res_PaymentTypeIcon_List()
                        {
                            PaymentTypeIconID = paymentTypeIcon.PaymentTypeIconID,
                            IconPath = paymentTypeIcon.IconPath,
                            Category = paymentTypeIcon.Category
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during payment type icon list", ex);
                return ApiResponse.Fail<List<Res_PaymentTypeIcon_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_PaymentType_List>>> List_Payment_Types()
        {
            try
            {
                _logger.LogService("Starting Payment Type List");

                var paymentTypeResponse = await POS_PaymentTypes_Select_All(new PaymentType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var paymenttypeIcons = _cacheService.GetCacheAsync(_userContext.TenantID).Result.PaymentTypeIcon;

                var response = new List<Res_PaymentType_List>();
                if (paymentTypeResponse != null && paymentTypeResponse.Any())
                {
                    foreach (var paymentType in paymentTypeResponse)
                    {
                        response.Add(new Res_PaymentType_List()
                        {
                            PaymentTypeID = paymentType.PaymentTypeID,
                            FK_PaymentTypeIconID = paymentType.FK_PaymentTypeIcon,

                            IconPath = paymentType.FK_PaymentTypeIcon != null
                                   ? paymenttypeIcons?.FirstOrDefault(x => x.PaymentTypeIconID == paymentType.FK_PaymentTypeIcon)?.IconPath
                                   : null,

                            Name = paymentType.Name,
                            IsActive = paymentType.IsActive,
                            IsPrimary = paymentType.IsPrimary,
                            IsSecondary = paymentType.IsSecondary,
                            SettlePayment = paymentType.SettlePayment,
                            RequireElevation = paymentType.RequireElevation,
                            RequireAdditionalInfo = paymentType.RequireAdditionalInfo
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during payment type list", ex);
                return ApiResponse.Fail<List<Res_PaymentType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Payment_Type(Req_PaymentType_Add request)
        {
            try
            {
                _logger.LogService("Starting Payment Type Add", request);

                var paymentTypeInsert = await POS_PaymentTypes_Insert(new PaymentType()
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    IsPrimary = request.IsPrimary,
                    IsSecondary = request.IsSecondary,
                    SettlePayment = request.SettlePayment,
                    RequireElevation = request.RequireElevation,
                    RequireAdditionalInfo = request.RequireAdditionalInfo,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FK_PaymentTypeIcon = request.FK_PaymentTypeIconID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Timezone add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Payment_Type(Req_PaymentType_Update request)
        {
            try
            {
                _logger.LogService("Starting Payment Type Update", request);

                var paymentTypeResponse = await POS_PaymentTypes_Select_Single(new PaymentType()
                {
                    PaymentTypeID = request.PaymentTypeID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (paymentTypeResponse == null)
                {
                    _logger.LogService("Payment Type not found", request.PaymentTypeID);
                    return ApiResponse.Fail<object>(AppErrorCode.PaymentTypeNotFound, new List<string> { "Payment Type not found." }, 404);
                }

                var paymentTypeUpdate = await POS_PaymentTypes_Update(new PaymentType()
                {
                    PaymentTypeID = request.PaymentTypeID,
                    Name = string.IsNullOrWhiteSpace(request.Name) ? paymentTypeResponse.Name : request.Name,
                    IsActive = request.IsActive ?? paymentTypeResponse.IsActive,
                    IsPrimary = request.IsPrimary ?? paymentTypeResponse.IsPrimary,
                    IsSecondary = request.IsSecondary ?? paymentTypeResponse.IsSecondary,
                    DateCreated = paymentTypeResponse.DateCreated,
                    DateUpdated = DateTime.Now,
                    FK_PaymentTypeIcon = request.FK_PaymentTypeIconID ?? paymentTypeResponse.FK_PaymentTypeIcon,
                    SettlePayment = request.SettlePayment ?? paymentTypeResponse.SettlePayment,
                    RequireElevation = request.RequireElevation ?? paymentTypeResponse.RequireElevation,
                    RequireAdditionalInfo = request.RequireAdditionalInfo ?? paymentTypeResponse.RequireAdditionalInfo
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during payment type update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion

        #region Tax Types

        public async Task<ApiResponse<List<Res_TaxType_List>>> List_Tax_Types()
        {
            try
            {
                _logger.LogService("Starting Tax Type List");

                // 1. Authenticate user (you should add the logic to retrieve user from DB here)
                var taxTypeResponse = await POS_TaxTypes_Select_All(new TaxType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var response = new List<Res_TaxType_List>();
                if (taxTypeResponse != null && taxTypeResponse.Any())
                {
                    foreach (var taxTypes in taxTypeResponse)
                    {
                        response.Add(new Res_TaxType_List()
                        {
                            POS_TaxTypeID = (int)taxTypes.TaxTypeID,
                            TaxName = taxTypes.TaxName,
                            TaxPercentage = taxTypes.TaxPercentage,
                            ValidFrom = taxTypes.ValidFrom,
                            ValidTo = taxTypes.ValidTo,
                            IsActive = taxTypes.IsActive
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax types list", ex);
                return ApiResponse.Fail<List<Res_TaxType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Tax_Type(Req_TaxType_Add request)
        {
            try
            {
                _logger.LogService("Starting Tax Type Add", request);

                var taxTypeInsert = await POS_TaxTypes_Insert(new TaxType()
                {
                    TaxName = request.TaxName,
                    TaxPercentage = request.TaxPercentage,
                    ValidFrom = request.ValidFrom,
                    ValidTo = request.ValidTo,
                    IsActive = request.IsActive,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Tax_Type(Req_TaxType_Update request)
        {
            try
            {
                _logger.LogService("Starting Tax Type Update", request);

                var taxTypeResponse = await POS_TaxTypes_Select_Single(new TaxType()
                {
                    TaxTypeID = request.POS_TaxTypeID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (taxTypeResponse == null)
                {
                    _logger.LogService("Tax Type not found", request.POS_TaxTypeID);
                    return ApiResponse.Fail<object>(AppErrorCode.TimeZoneNotFound, new List<string> { "Tax Type not found." }, 404);
                }

                var taxTypeUpdate = await POS_TaxTypes_Update(new TaxType()
                {
                    TaxTypeID = request.POS_TaxTypeID,
                    TaxName = string.IsNullOrWhiteSpace(request.TaxName) ? taxTypeResponse.TaxName : request.TaxName,
                    TaxPercentage = request.TaxPercentage ?? taxTypeResponse.TaxPercentage,
                    ValidFrom = request.ValidFrom ?? taxTypeResponse.ValidFrom,
                    ValidTo = request.ValidTo ?? taxTypeResponse.ValidTo,
                    IsActive = request.IsActive ?? taxTypeResponse.IsActive,
                    DateCreated = taxTypeResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax type update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion

        #region Settings

        public async Task<ApiResponse<List<Res_Settings_List>>> List_Settings()
        {
            try
            {
                _logger.LogService("Starting Settings List");

                // 1. Authenticate user (you should add the logic to retrieve user from DB here)
                var settingsResponse = await POS_Settings_Select_All(new Settings()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var currency = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Currencies;

                var response = new List<Res_Settings_List>();
                if (settingsResponse != null && settingsResponse.Any())
                {
                    foreach (var setting in settingsResponse)
                    {
                        response.Add(new Res_Settings_List()
                        {
                            SettingID = (int)setting.SettingID,
                            Company = setting.CompanyName,
                            Email = setting.Email,
                            HeadOfficeNo = setting.HeadOfficeNo,
                            FK_CurrencyID = setting.FK_CurrencyID,
                            Currency = setting.FK_CurrencyID != null
                                   ? currency?.FirstOrDefault(x => x.CurrencyID == setting.FK_CurrencyID)?.Currency
                                   : null,
                        });
                    }
                }

                else
                {
                    _logger.LogService("No settings found for tenant", _userContext.TenantID);
                    return ApiResponse.Fail<List<Res_Settings_List>>(AppErrorCode.SettingsNotFound, new List<string> { "Please add settings." }, 404);
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax types list", ex);
                return ApiResponse.Fail<List<Res_Settings_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Setting(Req_Settings_Add request)
        {
            try
            {
                _logger.LogService("Starting Tax Type Add", request);

                var settingsInsert = await POS_Settings_Insert(new Settings()
                {
                    CompanyName = request.Company,
                    Email = request.Email,
                    HeadOfficeNo = request.HeadOfficeNo,
                    FK_CurrencyID = request.FK_CurrencyID,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                    .Where(x => x.Environment == _configuration["Environment"]).ToList();

                if (request.ImageFile != null)
                {
                    var relativePath = "slip_logo";

                    var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                    if (imageUrl == null)
                    {
                        return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                    }

                    string rootPath = _configuration["ImageStorage:RootFileSystemPath"];

                    await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                    {
                        FK_ImageCategoryID = 3,  // e.g. 1 = Menu
                        FK_ItemID = settingsInsert.SettingID,
                        FileSystemPath = rootPath,
                        RelativePath = relativePath,
                        ImageName = Path.GetFileName(imageUrl.BaseUrl),
                        FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                        ImageUrl = imageUrl.BaseUrl,
                        LocalUrl = imageUrl.LocalUrl,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
                }

                // Update location Settings TODO!!!!

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Setting(Req_Settings_Update request)
        {
            try
            {
                _logger.LogService("Starting Settings Update", request);

                var settingResponse = await POS_Settings_Select_Single(new Settings()
                {
                    SettingID = request.SettingID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var settingUpdate = await POS_Settings_Update(new Settings()
                {
                    SettingID = request.SettingID,
                    CompanyName = string.IsNullOrWhiteSpace(request.Company) ? settingResponse.CompanyName : request.Company,
                    Email = string.IsNullOrWhiteSpace(request.Email) ? settingResponse.Email : request.Email,
                    HeadOfficeNo = string.IsNullOrWhiteSpace(request.HeadOfficeNo) ? settingResponse.HeadOfficeNo : settingResponse.HeadOfficeNo,
                    FK_CurrencyID = request.FK_CurrencyID ?? settingResponse.FK_CurrencyID,
                    DateCreated = settingResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                    .Where(x => x.Environment == _configuration["Environment"]).ToList();

                if (request.ImageFile != null)
                {
                    var relativePath = "slip_logo";

                    var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                    if (imageUrl == null)
                    {
                        return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                    }

                    string rootPath = _configuration["ImageStorage:RootFileSystemPath"];

                    await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                    {
                        FK_ImageCategoryID = 3,  // e.g. 1 = Menu
                        FK_ItemID = request.SettingID,
                        FileSystemPath = rootPath,
                        RelativePath = relativePath,
                        ImageName = Path.GetFileName(imageUrl.BaseUrl),
                        FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                        ImageUrl = imageUrl.BaseUrl,
                        LocalUrl = imageUrl.LocalUrl,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
                }

                // Update location Settings TODO!!!!

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax type update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion

        #region Exchange Rate

        public async Task<ApiResponse<List<Res_ExchangeRate_List>>> List_Exchange_Rates()
        {
            try
            {
                _logger.LogService("Starting Exchange Rate List");

                // 1. Authenticate user (you should add the logic to retrieve user from DB here)
                var exchangeRateResponse = await POS_ExchangeRates_Select_All(new ExchangeRate()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var currency = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Currencies;

                var response = new List<Res_ExchangeRate_List>();
                if (exchangeRateResponse != null && exchangeRateResponse.Any())
                {
                    foreach (var rate in exchangeRateResponse)
                    {
                        response.Add(new Res_ExchangeRate_List()
                        {
                            ExchangeRateID = (int)rate.ExchangeRateID,
                            FK_CurrencyID = rate.FK_CurrencyID,
                            Currency = rate.FK_CurrencyID != null
                                   ? currency?.FirstOrDefault(x => x.CurrencyID == rate.FK_CurrencyID)?.Currency
                                   : null,
                            ExchangeRate = rate.ExchangeRate,
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax types list", ex);
                return ApiResponse.Fail<List<Res_ExchangeRate_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Exchange_Rate(Req_ExchangeRate_Add request)
        {
            try
            {
                _logger.LogService("Starting Tax Type Add", request);

                var rateInsert = await POS_ExchangeRates_Insert(new ExchangeRate()
                {
                    FK_CurrencyID = request.FK_CurrencyID,
                    ExchangeRate = request.ExchangeRate,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Exchange_Rate(Req_ExchangeRate_Update request)
        {
            try
            {
                _logger.LogService("Starting Exchange Rate Update", request);

                var rateResponse = await POS_ExchangeRates_Select_Single(new ExchangeRate()
                {
                    ExchangeRateID = request.ExchangeRateID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var rateUpdate = await POS_ExchangeRates_Update(new ExchangeRate()
                {
                    ExchangeRateID = request.ExchangeRateID,
                    FK_CurrencyID = request.FK_CurrencyID ?? rateResponse.FK_CurrencyID,
                    ExchangeRate = request.ExchangeRate ?? rateResponse.ExchangeRate,
                    DateCreated = rateResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during tax type update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion

        #region Slip Types

        public async Task<ApiResponse<List<Res_SlipType_List>>> List_Slip_Types()
        {
            try
            {
                _logger.LogService("Starting Slip Type List");

                // 1. Authenticate user (you should add the logic to retrieve user from DB here)
                var taxTypeResponse = await POS_SlipTypes_Select_All(new SlipType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var response = new List<Res_SlipType_List>();
                if (taxTypeResponse != null && taxTypeResponse.Any())
                {
                    foreach (var taxTypes in taxTypeResponse)
                    {
                        response.Add(new Res_SlipType_List()
                        {
                            SlipTypeID = (int)taxTypes.SlipTypeID,
                            SlipType = taxTypes.SlipType,
                            Description = taxTypes.Description,
                            SlipCode = taxTypes.SlipCode
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during slip types list", ex);
                return ApiResponse.Fail<List<Res_SlipType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #endregion
    }
}


















