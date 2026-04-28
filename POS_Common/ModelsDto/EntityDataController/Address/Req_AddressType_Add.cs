using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Address
{
    public class Req_AddressType_Add
    {
        #region Properties

        public int? FK_EntityID { get; set; }

        public string Type { get; set; }

        public bool? IsRequired { get; set; }

        public bool? CanEdit { get; set; }
        #endregion
    }
}
