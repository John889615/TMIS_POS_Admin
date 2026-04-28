using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BcODataResponse<T>
    {
        public List<T> Value { get; set; }
    }

}
