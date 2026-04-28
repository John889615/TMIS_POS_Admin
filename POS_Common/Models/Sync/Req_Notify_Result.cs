using System;
using System.ComponentModel.DataAnnotations;

namespace POS_Common.Models.Sync
{
    public class Req_Notify_Result
    {
        public int SiteId { get; set; }

        [Required]
        public string TypeName { get; set; }

        /// <summary>
        /// "success" or "failed"
        /// </summary>
        [Required]
        public string Status { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime ObservedAt { get; set; }
    }
}
