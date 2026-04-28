using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductPreparationMethod
{
    public class Res_ProductPreparationMethod_List
    {
        public int? ProductPreparationMethodID { get; set; }
        public string ShortCode { get; set; }
        public string Method { get; set; }
    }
}
