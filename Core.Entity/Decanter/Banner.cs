
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity.Decanter
{
    public partial class Banner
    {
        [Key]
        public int BannerNo { get; set; }

        public int? ServiceNo { get; set; }

        [StringLength(100)]
        public string BannerType { get; set; }

        [StringLength(500)]
        public string TitleKey { get; set; }

        [StringLength(500)]
        public string ContentKey { get; set; }

        public string ImageUrl { get; set; }

        public string LinkUrl { get; set; }

        public byte? SortOrder { get; set; }

        public bool? IsPublic { get; set; }

        public bool? IsOpenNewTab { get; set; }

        public bool? IsEnable { get; set; }

        public bool? IsNew { get; set; }

        public bool? IsQRCode { get; set; }

        public bool? IsDownload { get; set; }

        public bool? HasToken { get; set; }

        public DateTime? RegDate { get; set; }

        public virtual Service Service { get; set; }
    }
}
