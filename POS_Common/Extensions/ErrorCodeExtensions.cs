using POS_Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ErrorCodeExtensions
{
    public static string GetMessage(this AppErrorCode errorCode)
    {
        var memberInfo = typeof(AppErrorCode).GetMember(errorCode.ToString()).FirstOrDefault();
        var attribute = memberInfo?.GetCustomAttributes(typeof(ErrorMessageAttribute), false).FirstOrDefault() as ErrorMessageAttribute;
        return attribute?.Message ?? errorCode.ToString();
    }
}