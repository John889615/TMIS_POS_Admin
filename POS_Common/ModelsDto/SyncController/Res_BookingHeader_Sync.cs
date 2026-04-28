using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_BookingHeader_Sync
    {
        public int? BookingHeaderID { get; set; }

        public string PartyName { get; set; }

        public string BookingReference { get; set; }

        public DateTime? TravelStart { get; set; }

        public DateTime? TravelEnd { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
