using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_ProductExtraCategory_Sync
    {
        #region Properties

        public int? ProductExtraCategoryID { get; set; }

        public string Category { get; set; }

        public int? DisplayOrder { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
