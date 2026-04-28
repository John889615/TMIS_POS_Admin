using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using POS_Api.Helpers;
using POS_Api.ServiceInterfaces.Cache;
using POS_Api.ServiceInterfaces.Debtors;
using POS_Api.ServiceInterfaces.Inventory;
using POS_Api.ServiceInterfaces.Logging;
using POS_Api.Services.Debtors;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.Models.Debtors.Branches;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.DebtorTypeMappings;
using POS_Common.Models.Debtors.DebtorTypes;
using POS_Common.Models.Debtors.Departments;
using POS_Common.Models.Debtors.POS_CostCenterPrinters;
using POS_Common.Models.Debtors.POS_CostCenters;
using POS_Common.Models.Debtors.POS_CostCenterTypes;
using POS_Common.Models.Debtors.POS_Locations;
using POS_Common.Models.EntityData.Addresses;
using POS_Common.Models.EntityData.AddressRegions;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.Contacts;
using POS_Common.Models.EntityData.ContactTypes;
using POS_Common.Models.EntityData.Countries;
using POS_Common.Models.EntityData.CountryProvinces;
using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.Entities;
using POS_Common.Models.EntityData.EntityAddresses;
using POS_Common.Models.EntityData.EntityContacts;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.Models.EntityData.Users;
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
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Interfaces;
using TMIS_Common.Sql;

namespace POS_Api.Services
{
    public class Debtors_Service : Debtors_Custom_Service, IDebtors_Service
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

        public Debtors_Service(IConfiguration configuration, ILogging_Service logger
            , IHttpContextAccessor httpContextAccessor, IUserContext userContext, ICache_Service cacheService, ImageHelper imageHelper)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
            _cacheService = cacheService;

            Current_User_Management();
            _imageHelper = imageHelper;
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

        #region Debtors

        public async Task<ApiResponse<List<Res_Debtor_List>>> List_Debtors()
        {
            try
            {
                _logger.LogService("Starting Debtor List");

                var debtorResponse = await POS_Locations_Select_All(new Location()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var currencies = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Currencies;

                var response = new List<Res_Debtor_List>();

                if (debtorResponse != null && debtorResponse.Any())
                {
                    foreach (var debtor in debtorResponse)
                    {

                        response.Add(new Res_Debtor_List()
                        {
                            DebtorID = debtor.LocationID,
                            FK_CurrencyID = debtor.FK_CurrencyID,
                            Currency = currencies.FirstOrDefault(x => x.CurrencyID == debtor.FK_CurrencyID)?.Currency,
                            ShortCode = debtor.ShortCode,
                            Name = debtor.Name,
                            IsActive = debtor.IsActive
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_Debtor_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Debtor(Req_Debtor_Add request)
        {
            try
            {
                _logger.LogService("Starting Location Add", request);

                var debtorInsert = await POS_Locations_Insert(new Location()
                {
                    ShortCode = request.ShortCode,
                    Name = request.Name,
                    FK_CurrencyID = request.FK_CurrencyID,
                    IsActive = request.IsActive ?? true,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Location add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Debtor(Req_Debtor_Update request)
        {
            try
            {
                _logger.LogService("Starting Location Update", request);

                var debtorResponse = await POS_Locations_Select_Single(new Location()
                {
                    LocationID = request.DebtorID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (debtorResponse == null)
                {
                    _logger.LogService("Location not found", request.DebtorID);
                    return ApiResponse.Fail<object>(AppErrorCode.DebtorNotFound, new List<string> { "Location not found." }, 404);
                }

                var debtorUpdate = await POS_Locations_Update(new Location()
                {
                    LocationID = request.DebtorID,
                    ShortCode = string.IsNullOrWhiteSpace(request.ShortCode) ? debtorResponse.ShortCode : request.ShortCode,
                    Name = string.IsNullOrWhiteSpace(request.Name) ? debtorResponse.Name : request.Name,
                    FK_CurrencyID = request.FK_CurrencyID ?? debtorResponse.FK_CurrencyID,
                    IsActive = request.IsActive ?? debtorResponse.IsActive,
                    DateCreated = debtorResponse.DateCreated,
                    DateUpdated = DateTime.Now,
                    FK_CreatedUserID = debtorResponse.FK_CreatedUserID,
                    FK_UpdatedUserID = _userContext.UserID
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

        #region Debtor Addresses

        public async Task<ApiResponse<List<Res_DebtorAddressType_List>>> List_Debtor_Address_Types()
        {
            try
            {
                _logger.LogService("Starting Debtor Address Type List");

                var debtorAddressTypeResponse = await DebtorAddresses_Select_Debtor(new AddressType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_DebtorAddressType_List>();

                if (debtorAddressTypeResponse != null && debtorAddressTypeResponse.Any())
                {
                    foreach (var debtorAddressType in debtorAddressTypeResponse)
                    {

                        response.Add(new Res_DebtorAddressType_List()
                        {
                            AddressTypeID = debtorAddressType.AddressTypeID,
                            Type = debtorAddressType.Type
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address type list", ex);
                return ApiResponse.Fail<List<Res_DebtorAddressType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Debtor_Address(Req_DebtorAddress_Add request)
        {
            try
            {
                _logger.LogService("Starting Location Address Add", request);

                var debtorAddressInsert = await EntityData.EntityData_Custom_Service.Addresses_Insert(new Address()
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

                var entityResponse = await Entity_Select_Debtor(new Entity()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityAddressInsert = await EntityData.EntityData_Custom_Service.EntityAddresses_Insert(new EntityAddress()
                {
                    FK_EntityID = entityResponse.EntityID,
                    EntityRecordID = request.FK_DebtorID,
                    FK_AddressID = debtorAddressInsert.AddressID,
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

        public async Task<ApiResponse<object>> Update_Debtor_Address(Req_DebtorAddress_Update request)
        {
            try
            {
                _logger.LogService("Starting Debtor Update", request);

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
                    EntityRecordID = request.DebtorID ?? entityAddressResponse.EntityRecordID,
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
                _logger.LogService("Exception during Role add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Debtor Contacts

        public async Task<ApiResponse<object>> Add_Debtor_Contact(Req_DebtorContact_Add request)
        {
            try
            {
                _logger.LogService("Starting Debtor Contact Add", request);

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

                var entityResponse = await Entity_Select_Debtor(new Entity()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityContactInsert = await EntityData.EntityData_Custom_Service.EntityContacts_Insert(new EntityContact()
                {
                    FK_EntityID = entityResponse.EntityID,
                    EntityRecordID = request.FK_DebtorID,
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

        public async Task<ApiResponse<object>> Update_Debtor_Contact(Req_DebtorContact_Update request)
        {
            try
            {
                _logger.LogService("Starting Debtor Contact Update", request);

                var debtorContactResponse = await EntityData.EntityData_Custom_Service.Contacts_Select_Single(new Contact()
                {
                    ContactID = request.ContactID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var entityContactResponse = await EntityContacts_Select_Single_ContactID(new EntityContact()
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
                    EntityRecordID = request.FK_DebtorID ?? entityContactResponse.EntityRecordID,
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

        #region Cost Centers

        public async Task<ApiResponse<List<Res_CostCenter_List>>> List_CostCenters()
        {
            try
            {
                _logger.LogService("Starting Cost Center List");

                var costCenterResponse = await CostCenters_Select_All(new CostCenter()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CostCenter_List>();

                if (costCenterResponse != null && costCenterResponse.Any())
                {
                    foreach (var costCenter in costCenterResponse)
                    {

                        response.Add(new Res_CostCenter_List()
                        {
                            CostCenterID = costCenter.CostCenterID,
                            Name = costCenter.Name,
                            DebtorID = costCenter.FK_LocationID,
                            Debtor = costCenter.Debtor,
                            StatusID = costCenter.FK_StatusID,
                            Status = costCenter.Status,
                            CostCenterTypeID = costCenter.FK_CostCenterTypeID,
                            Type = costCenter.Type,
                            BillingReference = costCenter.BillingReference
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_CostCenter_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_CostCenter(Req_CostCenter_Add request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Add", request);

                //var debtorResponse = await Debtors_Select_Single_Name(new Debtor()
                //{
                //    Name = request.Name
                //}, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                //if (debtorResponse != null)
                //{
                //    _logger.LogService("Debtor already exists", request.Name);
                //    return ApiResponse.Fail<object>(AppErrorCode.DebtorExists, new List<string> { "Debtor already exists." }, 400);
                //}

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var costCenterInsert = await POS_CostCenters_Insert(new CostCenter()
                    {
                        FK_LocationID = request.FK_DebtorID,
                        Name = request.Name,
                        BillingReference = request.BillingReference,
                        FK_StatusID = request.FK_StatusID,
                        FK_CostCenterTypeID = request.FK_CostCenterTypeID,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "cost_centers";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }

                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];
                        

                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 4,
                            FK_ItemID = costCenterInsert.CostCenterID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Address add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_CostCenter(Req_CostCenter_Update request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Update", request);

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var costCenterResponse = await POS_CostCenters_Select_Single(new CostCenter()
                    {
                        CostCenterID = request.POS_CostCenterID
                    }, sqlConn);

                    if (costCenterResponse == null)
                    {
                        _logger.LogService("Cost center not found", request.POS_CostCenterID);
                        return ApiResponse.Fail<object>(AppErrorCode.CostCenterNotFound, new List<string> { "Cost center not found." }, 404);
                    }

                    var costCenterUpdate = await POS_CostCenters_Update(new CostCenter()
                    {
                        CostCenterID = request.POS_CostCenterID,
                        FK_LocationID = request.FK_DebtorID ?? costCenterResponse.FK_LocationID,
                        Name = string.IsNullOrWhiteSpace(request.Name) ? costCenterResponse.Name : request.Name,
                        BillingReference = string.IsNullOrWhiteSpace(request.BillingReference) ? costCenterResponse.BillingReference : request.BillingReference,
                        FK_StatusID = request.FK_StatusID ?? costCenterResponse.FK_StatusID,
                        FK_CostCenterTypeID = request.FK_CostCenterTypeID ?? costCenterResponse.FK_CostCenterTypeID,
                        DateCreated = costCenterResponse.DateCreated,
                        DateUpdated = DateTime.Now
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "cost_centers";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }



                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];
                        

                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 4,
                            FK_ItemID = request.POS_CostCenterID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

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

        #region Cost Center Types

        public async Task<ApiResponse<List<Res_CostCenterType_List>>> List_CostCenterTypes()
        {
            try
            {
                _logger.LogService("Starting Cost Center Type List");

                var costCenterTypeResponse = await POS_CostCenterTypes_Select_All(new CostCenterType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CostCenterType_List>();

                if (costCenterTypeResponse != null && costCenterTypeResponse.Any())
                {
                    foreach (var costCenterType in costCenterTypeResponse)
                    {

                        response.Add(new Res_CostCenterType_List()
                        {
                            POS_CostCenterTypeID = costCenterType.CostCenterTypeID,
                            Name = costCenterType.Name
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_CostCenterType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Cost Center Printers

        public async Task<ApiResponse<List<Res_CostCenterPrinter_List>>> List_CostCenter_Printers(Req_CostCenterPrinter_List request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Printer List", request);

                var printerLinks = await POS_CostCenterPrinters_Select_All(new CostCenterPrinter()
                {
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var response = new List<Res_CostCenterPrinter_List>();

                var slipType = _cacheService.GetCacheAsync(_userContext.TenantID).Result.SlipTypes;

                if (printerLinks != null && printerLinks.Any())
                {
                    var filteredLinks = printerLinks;

                    if (request?.FK_CostCenterID != null)
                    {
                        filteredLinks = filteredLinks
                            .Where(x => x.FK_CostCenterID == request.FK_CostCenterID)
                            .ToList();
                    }

                    foreach (var link in filteredLinks)
                    {
                        response.Add(new Res_CostCenterPrinter_List()
                        {
                            CostCenterPrinterID = link.CostCenterPrinterID,
                            FK_CostCenterID = link.FK_CostCenterID,
                            FK_PrinterID = link.FK_PrinterID,
                            FK_InvoiceSlipTypeID = link.FK_InvoiceSlipTypeID,
                            InvoiceSlipType = slipType.FirstOrDefault(x => x.SlipTypeID == link.FK_InvoiceSlipTypeID)?.SlipType,
                            FK_TabSlipTypeID = link.FK_TabSlipTypeID,
                            TabSlipType = slipType.FirstOrDefault(x => x.SlipTypeID == link.FK_TabSlipTypeID)?.SlipType,
                            FK_CreatedUserID = link.FK_CreatedUserID,
                            FK_UpdatedUserID = link.FK_UpdatedUserID,
                            DateCreated = link.DateCreated,
                            DateUpdated = link.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during cost center printer list", ex);
                return ApiResponse.Fail<List<Res_CostCenterPrinter_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }
        }

        public async Task<ApiResponse<object>> Add_CostCenter_Printer(Req_CostCenterPrinter_Add request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Printer Add", request);

                var existingLinks = await POS_CostCenterPrinters_Select_All(new CostCenterPrinter()
                {
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var existingLink = existingLinks?
                    .FirstOrDefault(x =>
                        x.FK_CostCenterID == request.FK_CostCenterID &&
                        x.FK_PrinterID == request.FK_PrinterID);

                if (existingLink != null)
                {
                    return ApiResponse.Fail<object>(
                        AppErrorCode.ValidationError,
                        new List<string> { "Printer is already linked to this cost center." },
                        400
                    );
                }

                await POS_CostCenterPrinters_Insert(new CostCenterPrinter()
                {
                    FK_CostCenterID = request.FK_CostCenterID,
                    FK_PrinterID = request.FK_PrinterID,
                    FK_InvoiceSlipTypeID = request.FK_InvoiceSlipTypeID,
                    FK_TabSlipTypeID = request.FK_TabSlipTypeID,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Cost Center Printer add", ex);
                return ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }
        }

        public async Task<ApiResponse<object>> Update_CostCenter_Printer(Req_CostCenterPrinter_Update request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Printer Update", request);

                var existingLink = await POS_CostCenterPrinters_Select_Single(new CostCenterPrinter()
                {
                    CostCenterPrinterID = request.CostCenterPrinterID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (existingLink == null)
                {
                    return ApiResponse.Fail<object>(
                        AppErrorCode.ValidationError,
                        new List<string> { "Cost center printer link not found." },
                        404
                    );
                }

                await POS_CostCenterPrinters_Update(new CostCenterPrinter()
                {
                    CostCenterPrinterID = request.CostCenterPrinterID,
                    FK_CostCenterID = request.FK_CostCenterID ?? existingLink.FK_CostCenterID,
                    FK_PrinterID = request.FK_PrinterID ?? existingLink.FK_PrinterID,
                    FK_InvoiceSlipTypeID = request.FK_InvoiceSlipTypeID,
                    FK_TabSlipTypeID = request.FK_TabSlipTypeID,
                    FK_CreatedUserID = existingLink.FK_CreatedUserID,
                    FK_UpdatedUserID = _userContext.UserID,
                    DateCreated = existingLink.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Cost Center Printer update", ex);
                return ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }
        }

        public async Task<ApiResponse<CostCenterPrinter>> Switch_CostCenter_Printer_Link(Req_CostCenterPrinter_Switch request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Printer Switch Link", request);

                var switchResponse = await POS_CostCenterPrinters_Switch_Link(new CostCenterPrinter()
                {
                    FK_CostCenterID = request.FK_CostCenterID,
                    FK_PrinterID = request.FK_PrinterID
                },
                _userContext.UserID,
                _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (switchResponse == null)
                {
                    return ApiResponse.Fail<CostCenterPrinter>(
                        AppErrorCode.ServerError,
                        new List<string> { "No response was returned from switch link procedure." },
                        500
                    );
                }

                return ApiResponse.Success(switchResponse);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Cost Center Printer switch link", ex);
                return ApiResponse.Fail<CostCenterPrinter>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion
        #endregion
    }
}

