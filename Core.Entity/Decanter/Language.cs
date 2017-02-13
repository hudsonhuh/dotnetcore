
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity.Decanter
{
    public partial class Language
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Language()
        {
        }

        [Key]
        public int LanguageNo { get; set; }

        public int? ServiceNo { get; set; }

        [Required]
        [StringLength(500)]
        public string Key { get; set; }

        public string en { get; set; }

        [Column("zh-CN")]
        public string zh_CN { get; set; }

        [Column("zh-TW")]
        public string zh_TW { get; set; }

        public string ja { get; set; }

        public string ru { get; set; }

        public DateTime RegDate { get; set; }

        public virtual Service Service { get; set; }
    }
}
