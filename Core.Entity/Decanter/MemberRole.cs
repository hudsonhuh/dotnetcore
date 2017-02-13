
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity.Decanter
{
    public partial class MemberRole
    {
        [Key]
        public int RoleNo { get; set; }

        public int MemberNo { get; set; }

        public int MenuNo { get; set; }

        public DateTime RegDate { get; set; }

        public virtual Member Member { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
