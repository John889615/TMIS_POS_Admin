using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using POS_Api.ServiceInterfaces.Logging;
using POS_Common.Models.EntityData.GlobalSettings;
using POS_Common.ModelsDto;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Interfaces;

namespace POS_Api.Helpers
{
    public class ImageHelper
    {
        #region Members

        private readonly ILogging_Service _logger;
        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public ImageHelper(IConfiguration configuration, ILogging_Service logger, IUserContext userContext)
        {
            _configuration = configuration;
            _logger = logger;
            //_httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
        }
        #endregion

        #region Methods

        public async Task<ImageSaveResult> SaveImageAsync(IFormFile file, string relativeFolder, List<GlobalSettings> globalSettings)
        {
            try
            {
                var targetWidth = 300;
                var targetHeight = 168;

                if (file == null || file.Length == 0)
                    return null;

                string rootPath = _configuration["ImageStorage:RootFileSystemPath"];
                rootPath = rootPath.Replace("{Path}", globalSettings.FirstOrDefault(x => x.Key == "Image_Admin_Server_Path").Value)
                                        .Replace("{TenantID}", _userContext.TenantID.ToString());

                string folderName = relativeFolder;
                string folderPath = Path.Combine(rootPath, folderName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string extension = Path.GetExtension(file.FileName).ToLower();
                string newFileName = Guid.NewGuid().ToString() + extension;
                string fullPath = Path.Combine(folderPath, newFileName);

                // Load uploaded file into memory
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using var input = SKImage.FromEncodedData(memoryStream.ToArray());
                using var original = SKBitmap.FromImage(input);

                // Resize operation
                var resized = original.Resize(
                    new SKImageInfo(targetWidth, targetHeight),
                    SKFilterQuality.High);

                if (resized == null)
                    throw new Exception("Image resize failed. The image may be corrupted.");

                using var image = SKImage.FromBitmap(resized);
                using var outputData = image.Encode(
                    extension == ".png" ? SKEncodedImageFormat.Png : SKEncodedImageFormat.Jpeg,
                    90);

                // Save resized image
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    outputData.SaveTo(stream);
                }

                string baseUrl = $"{_configuration["ImageStorage:BaseUrl"]}/{relativeFolder}/{newFileName}";
                baseUrl = baseUrl.Replace("{0}", _userContext.TenantID.ToString());
                string localUrl = $"{_configuration["ImageStorage:LocalUrl"]}/{relativeFolder}/{newFileName}";
                localUrl = localUrl.Replace("{0}", _userContext.TenantID.ToString());
                //return $"{baseUrl}/{folderName}/{newFileName}";
                return new ImageSaveResult
                {
                    BaseUrl = baseUrl,
                    LocalUrl = localUrl,
                };
            }
            catch (Exception ex)
            {
                _logger.LogService("Image saving failed", ex);
                return null;
            }
        }

        #endregion

    }
}
