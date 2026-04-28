using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralItemCategoryList
    {
        public List<BusinessCentralItemCategory> Value { get; set; } = new List<BusinessCentralItemCategory>();
    }

    public class BusinessCentralItemCategory
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
    }
}
