
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity.Decanter
{
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            Audit = new HashSet<Audit>();
            MemberRole = new HashSet<MemberRole>();
            Menu1 = new HashSet<Menu>();
        }

        [Key]
        public int MenuNo { get; set; }

        public int ServiceNo { get; set; }

        public int? ParentMenuNo { get; set; }

        [StringLength(100)]
        public string MenuName { get; set; }

        [StringLength(500)]
        public string MenuUrl { get; set; }

        public byte? SortOrder { get; set; }

        public bool? IsPublic { get; set; }

        public bool? IsDelete { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? RegDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audit> Audit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberRole> MemberRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menu1 { get; set; }

        public virtual Menu Menu2 { get; set; }

        public virtual Service Service { get; set; }
    }
}
