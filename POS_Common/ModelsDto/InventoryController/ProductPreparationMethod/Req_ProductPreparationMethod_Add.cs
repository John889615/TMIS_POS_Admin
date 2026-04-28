using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductPreparationMethod
{
    public class Req_ProductPreparationMethod_Add
    {
        public string ShortCode { get; set; }
        public string Method { get; set; }
    }
}
