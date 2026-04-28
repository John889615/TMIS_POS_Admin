using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_ProductPreparationMethod_Sync
    {
        #region Properties

        public int? ProductPreparationMethodID { get; set; }

        public string ShortCode { get; set; }

        public string Method { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
