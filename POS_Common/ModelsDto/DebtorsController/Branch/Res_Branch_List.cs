using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController.Branch
{
    public class Res_Branch_List
    {
        #region Properties

        public int? BranchID { get; set; }

        public string ShortCode { get; set; }

        public string Name { get; set; }

        public int? FK_StatusID { get; set; }
        #endregion
    }
}
