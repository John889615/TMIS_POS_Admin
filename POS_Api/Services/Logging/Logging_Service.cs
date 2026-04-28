using Microsoft.AspNetCore.Http;
using POS_Api.ServiceInterfaces.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TMIS_Common.Models;

namespace POS_Api.Services.Logging
{
    public class Logging_Service : ILogging_Service
    {
        private readonly LoggingConfig _config;
        private readonly IHttpContextAccessor _http;

        public Logging_Service(LoggingConfig config, IHttpContextAccessor http)
        {
            _config = config;
            _http = http;
        }

        private string BuildContext(object payload)
        {
            var context = _http.HttpContext;

            object safePayload = payload;

            // Handle Exception payloads safely
            if (payload is Exception ex)
            {
                safePayload = new
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    HResult = ex.HResult,
                    InnerMessage = ex.InnerException?.Message // renamed to avoid duplicate property names
                };
            }

            return SafeSerialize(new
            {
                Timestamp = DateTime.UtcNow,
                IP = context?.Connection?.RemoteIpAddress?.ToString(),
                Host = context?.Request?.Host.Value,
                Path = context?.Request?.Path.Value,
                Query = context?.Request?.QueryString.Value,
                Method = context?.Request?.Method,
                Headers = context?.Request?.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                Payload = safePayload
            });
        }



        private string SafeSerialize(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch
            {
                return obj?.ToString() ?? "null";
            }
        }

        public void LogController(string message, object payload = null)
        {
            if (_config.LogController)
                Log.Information($"[CONTROLLER] {message} | {BuildContext(payload)}");
        }

        public void LogService(string message, object payload = null)
        {
            if (_config.LogService)
                Log.Information($"[SERVICE] {message} | {BuildContext(payload)}");
        }

        public void LogDatabase(string message, object payload = null)
        {
            if (_config.LogDatabase)
                Log.Information($"[DATABASE] {message} | {BuildContext(payload)}");
        }

        public void LogValidation(string message, object payload = null)
        {
            if (_config.LogValidation)
                Log.Warning($"[VALIDATION] {message} | {BuildContext(payload)}");
        }
    }

}
