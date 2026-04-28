using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController
{
    public class Res_DebtorType_List
    {
        #region Properties

        public int? DebtorTypeID { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
