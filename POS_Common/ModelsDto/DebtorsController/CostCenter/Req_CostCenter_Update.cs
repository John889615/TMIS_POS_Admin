using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController.CostCenter
{
    public class Req_CostCenter_Update
    {
        #region Properties

        public int? POS_CostCenterID { get; set; }

        public int? FK_DebtorID { get; set; }

        public string Name { get; set; }

        public string BillingReference { get; set; }

        public int? FK_StatusID { get; set; }

        public int? FK_CostCenterTypeID { get; set; }

        public IFormFile ImageFile { get; set; }
        #endregion
    }
}
