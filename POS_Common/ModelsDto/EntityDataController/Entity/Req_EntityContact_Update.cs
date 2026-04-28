using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Entity
{
    public class Req_EntityContact_Update
    {
        #region Properties

        public int? EntityContactID { get; set; }

        public int? FK_EntityID { get; set; }

        public int? EntityRecordID { get; set; }

        public int? FK_ContactID { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsMarketing { get; set; }

        public bool? IsEmergency { get; set; }

        public string PreferredContactTime { get; set; }

        public string PreferredLanguageCode { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
        #endregion
    }
}
