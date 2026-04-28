using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductPreparation
{
    public class Req_ProductPreparation_Update
    {
        public int? ProductPreparationID { get; set; }
        public int? FK_ProductID { get; set; }
        public int? FK_ProductPreparationMethodID { get; set; }
    }
}
