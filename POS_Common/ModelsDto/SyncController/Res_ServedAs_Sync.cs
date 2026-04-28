using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_ServedAs_Sync
    {
        #region Properties

        public int? ServedAsID { get; set; }

        public string ServedAsType { get; set; }

        public string Name { get; set; }

        public int? FK_CreatedUserID { get; set; }

        public int? FK_UpdatedUserID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
