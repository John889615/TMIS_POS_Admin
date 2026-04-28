using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_ProductPreparation_Sync
    {
        #region Properties

        public int? ProductPreparationID { get; set; }

        public int? FK_ProductID { get; set; }

        public int? FK_ProductPreparationMethodID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
