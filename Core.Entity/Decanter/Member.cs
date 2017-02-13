
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity.Decanter
{
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Audit = new HashSet<Audit>();
            MemberRole = new HashSet<MemberRole>();
        }

        [Key]
        public int MemberNo { get; set; }

        [Required]
        [StringLength(100)]
        public string MemberId { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string MemberName { get; set; }

        public int DepartmentNo { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public DateTime? LoginDate { get; set; }

        public DateTime? ApproveDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        [Required]
        [StringLength(100)]
        public string UserIP { get; set; }

        public bool IsDelete { get; set; }

        public DateTime RegDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audit> Audit { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberRole> MemberRole { get; set; }
    }
}
