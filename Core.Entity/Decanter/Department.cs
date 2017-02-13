
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity.Decanter
{
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            Department1 = new HashSet<Department>();
            Member = new HashSet<Member>();
        }

        [Key]
        public int DepartmentNo { get; set; }

        public int? ParentDepartmentNo { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        public byte SortOrder { get; set; }

        public bool? IsPublic { get; set; }

        public bool? IsDelete { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? RegDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department1 { get; set; }

        public virtual Department Department2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Member { get; set; }
    }
}
