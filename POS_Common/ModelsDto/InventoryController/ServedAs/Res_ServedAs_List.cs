using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ServedAs
{
    public class Res_ServedAs_List
    {

        #region Properties

        public int? ServedAsID { get; set; }

        public string ServedAsType { get; set; }

        public string Name { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
