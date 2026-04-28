using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_Image_Sync
    {
        #region Properties

        public int? ImageID { get; set; }

        public int? FK_ImageCategoryID { get; set; }

        public int? FK_ItemID { get; set; }

        public string FileSystemPath { get; set; }

        public string RelativePath { get; set; }

        public string ImageName { get; set; }

        public string FileExtension { get; set; }

        public string ImageUrl { get; set; }

        public string LocalUrl { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
