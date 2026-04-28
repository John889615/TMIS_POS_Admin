using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController
{
    public class Res_StatusGroup_List
    {
        #region Properties

        public int? StatusGroupID { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
