
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity.Decanter
{
    public partial class Audit
    {
        [Key]
        public int AuditNo { get; set; }

        public int? MemberNo { get; set; }

        public int? MenuNo { get; set; }

        public byte? Action { get; set; }

        [StringLength(500)]
        public string Url { get; set; }

        [StringLength(100)]
        public string TableName { get; set; }

        public string Message { get; set; }

        [StringLength(100)]
        public string UserIP { get; set; }

        public DateTime RegDate { get; set; }

        public virtual Member Member { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
