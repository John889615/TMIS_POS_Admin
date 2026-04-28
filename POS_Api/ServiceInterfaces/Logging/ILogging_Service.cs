using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.Logging
{
    public interface ILogging_Service
    {
        void LogController(string message, object payload = null);

        void LogService(string message, object payload = null);

        void LogDatabase(string message, object payload = null);

        void LogValidation(string message, object payload = null);
    }
}
