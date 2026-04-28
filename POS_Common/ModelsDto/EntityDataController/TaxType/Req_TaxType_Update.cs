using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.TaxType
{
    public class Req_TaxType_Update
    {
        #region Properties

        public int? POS_TaxTypeID { get; set; }

        public string TaxName { get; set; }

        public int? TaxPercentage { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public bool? IsActive { get; set; }
        #endregion
    }
}
