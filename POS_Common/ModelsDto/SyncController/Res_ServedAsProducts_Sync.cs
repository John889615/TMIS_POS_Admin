using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_ServedAsProducts_Sync
    {
        #region Properties

        public int? ServedAsProductID { get; set; }

        public int? FK_ProductID { get; set; }

        public int? FK_ServedAsID { get; set; }

        public bool? IsQuantified { get; set; }

        public decimal? Quantity { get; set; }

        public int? FK_CreatedUserID { get; set; }

        public int? FK_UpdatedUserID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool? IsDefault { get; set; }
        #endregion
    }
}
