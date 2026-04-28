using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController.FromServer
{

    public class Req_BookingHeader_Sync
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public List<BookingHeader_Sync> Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }
    }

    public class BookingHeader_Sync
    {
        public int? BookingHeaderID { get; set; }
        public string PartyName { get; set; }
        public string BookingReference { get; set; }
        public DateTime? TravelStart { get; set; }
        public DateTime? TravelEnd { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool? IsStaffBooking { get; set; }
        public string SyncStatus { get; set; }
    }
}
