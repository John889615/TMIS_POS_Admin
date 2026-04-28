using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.CreditorsController.Creditor
{
    public class Req_Creditor_Update
    {
        public int? CreditorID { get; set; }

        public int? CreditorTypeMappingID { get; set; }

        public string ShortCode { get; set; }

        public string Name { get; set; }

        public int? FK_MasterCreditorID { get; set; }

        public bool? IsMasterCreditor { get; set; }

        public int? FK_CreditorTypeID { get; set; }

        public int? FK_StatusID { get; set; }
    }
}
