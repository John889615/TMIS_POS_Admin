using System;

namespace POS_Common.Models.Sync
{
    public class ImageBytesResult
    {
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public DateTime LastModified { get; set; }
    }
}
