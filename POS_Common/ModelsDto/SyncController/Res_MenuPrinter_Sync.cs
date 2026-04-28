using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_MenuPrinter_Sync
    {
        #region Properties

        public int? POS_DebtorMenuPrinterID { get; set; }

        public int? FK_DebtorMenuID { get; set; }

        public int? FK_PrinterID { get; set; }

        public int? FK_OrderSlipTypeID { get; set; }

        public int? FK_CreatedUserID { get; set; }

        public int? FK_UpdatedUserID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
