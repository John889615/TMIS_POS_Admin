using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralItemWithCategory
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string DisplayName { get; set; }

        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }

        public string BaseUnitOfMeasureCode { get; set; }
    }
}
