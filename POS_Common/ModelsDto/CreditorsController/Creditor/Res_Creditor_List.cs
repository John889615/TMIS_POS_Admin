using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.CreditorsController.Creditor
{
    public class Res_Creditor_List
    {
        #region Properties

        public int? CreditorID { get; set; }

        public int? CreditorTypeMappingID { get; set; }

        public int? CreditorTypeID { get; set; }

        public string ShortCode { get; set; }

        public string Name { get; set; }

        public string MasterCreditor{ get; set; }

        public bool? IsMasterCreditor { get; set; }

        public string CreditorType { get; set; }

        public string Status { get; set; }
        #endregion
    }
}
