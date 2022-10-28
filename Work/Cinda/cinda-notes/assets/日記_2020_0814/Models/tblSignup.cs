namespace Member.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblSignup")]
    public partial class tblSignup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSignup()
        {
            tblSignupItem = new HashSet<tblSignupItem>();
        }

        [Key]
        [StringLength(10)]
        public string cMobile { get; set; }

        [StringLength(20)]
        public string cName { get; set; }

        [StringLength(50)]
        public string cEmail { get; set; }

        [Column(TypeName = "date")]
        public DateTime? cCreateDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSignupItem> tblSignupItem { get; set; }
    }
}
