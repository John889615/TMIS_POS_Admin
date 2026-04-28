using POS_Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models
{
    public class ApiResponse_DoNotUse<T>
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string ErrorCode { get; set; }
        public int? StatusCode { get; set; }
        public object Meta { get; set; }
    }


    public static class ApiResponse_DoNotUse
    {
        public static ApiResponse_DoNotUse<T> Success<T>(T data, string message = null)
        {
            return new ApiResponse_DoNotUse<T>
            {
                Success = true,
                Messages = message != null ? new List<string> { message } : new List<string>(),
                Data = data,
                StatusCode = 200
            };
        }

        public static ApiResponse_DoNotUse<T> Success<T>(T data, List<string> messages)
        {
            return new ApiResponse_DoNotUse<T>
            {
                Success = true,
                Messages = messages ?? new List<string>(),
                Data = data,
                StatusCode = 200
            };
        }

        public static ApiResponse_DoNotUse<T> Fail<T>(string message, string errorCode = null, List<string> errors = null, int? statusCode = 400)
        {
            return new ApiResponse_DoNotUse<T>
            {
                Success = false,
                Messages = message != null ? new List<string> { message } : new List<string>(),
                ErrorCode = errorCode,
                Errors = errors ?? new List<string>(),
                StatusCode = statusCode
            };
        }

        public static ApiResponse_DoNotUse<T> Fail<T>(List<string> messages, string errorCode = null, List<string> errors = null, int? statusCode = 400)
        {
            return new ApiResponse_DoNotUse<T>
            {
                Success = false,
                Messages = messages ?? new List<string>(),
                ErrorCode = errorCode,
                Errors = errors ?? new List<string>(),
                StatusCode = statusCode
            };
        }

        // ✅ NEW: Enum-based overload
        public static ApiResponse_DoNotUse<T> Fail<T>(AppErrorCode errorCode, List<string> errors = null, int? statusCode = 400)
        {
            return new ApiResponse_DoNotUse<T>
            {
                Success = false,
                Messages = new List<string> { errorCode.GetMessage() },
                ErrorCode = errorCode.ToString(),
                Errors = errors ?? new List<string>(),
                StatusCode = statusCode
            };
        }
    }

}
