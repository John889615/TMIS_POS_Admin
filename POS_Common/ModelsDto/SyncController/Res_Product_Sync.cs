using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_Product_Sync
    {
        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public bool? IsStockTracked { get; set; }

        public int? FK_UnitID { get; set; }

        public int? FK_DefaultUnitID { get; set; }

        public int? FK_DefaultTaxTypeID { get; set; }

        public string SKU { get; set; }

        public string Barcode { get; set; }

        public string QrCode { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
