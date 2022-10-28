namespace SimpleAccountSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblUser")]
    public partial class tblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUser()
        {
            tblUserGroup = new HashSet<tblUserGroup>();
        }

        [Key]
        [StringLength(20)]
        [Required]
        public string cAccount { get; set; }

        [StringLength(20)]
        [Required]
        public string cName { get; set; }

        [StringLength(50)]
        [Required]
        public string cEmail { get; set; }
        public DateTime? cCreateDT { get; set; }
        [Required]
        public int? cStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserGroup> tblUserGroup { get; set; }
    }
}
