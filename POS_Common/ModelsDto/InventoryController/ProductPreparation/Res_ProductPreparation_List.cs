using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductPreparation
{
    public class Res_ProductPreparation_List
    {
        public int? ProductPreparationID { get; set; }
        public int? FK_ProductID { get; set; }
        public string ProductName { get; set; }
        public int? FK_ProductPreparationMethodID { get; set; }
        public string PreparationMethod { get; set; }
        public string MethodShortCode { get; set; }
    }
}
