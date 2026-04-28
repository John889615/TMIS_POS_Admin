using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController.Department
{
    public class Res_Department_List
    {
        #region Properties

        public int? DepartmentID { get; set; }

        public string ShortCode { get; set; }

        public string Name { get; set; }

        public int? FK_StatusID { get; set; }
        #endregion
    }
}
