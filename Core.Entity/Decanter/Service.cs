
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity.Decanter
{
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            //Banner = new HashSet<Banner>();
            //Language = new HashSet<Language>();
            //Menu = new HashSet<Menu>();
        }

        [Key]
        public int ServiceNo { get; set; }

        [StringLength(100)]
        public string ServiceName { get; set; }

        [StringLength(500)]
        public string ServiceUrl { get; set; }

        public bool IsPublic { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime RegDate { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banner> Banner { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Language> Language { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menu { get; set; }
    }
}
