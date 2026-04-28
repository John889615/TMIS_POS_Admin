using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_LocationProduct_Sync
    {
        public int? LocationProductID { get; set; }

        public int? FK_ProductID { get; set; }

        public int? FK_LocationID { get; set; }

        public int? FK_SellUnitID { get; set; }

        public decimal? CostPrice { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public bool? IsAvailable { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
