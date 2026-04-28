using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.SlipPrinter
{
    public class Req_SlipPrinter_Update
    {
        #region Properties

        public int? SlipPrinterID { get; set; }

        public int? DebtorID { get; set; }

        public int? CostCenterID { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public string IpAddress { get; set; }

        public int? Port { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }
        #endregion
    }
}
