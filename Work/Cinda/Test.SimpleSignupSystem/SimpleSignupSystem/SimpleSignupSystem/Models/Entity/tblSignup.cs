using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSignupSystem.Models.Entity
{
    [Table("tblSignup")]
    public class tblSignup
    {
        public tblSignup()
        {
            tblSignupItem = new HashSet<tblSignupItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string cMobile { get; set; }

        [Column(TypeName = "Nvarchar")]
        [MaxLength(20)]
        public string cName { get; set; }

        [Column(TypeName = "Nvarchar")]
        [MaxLength(50)]
        public string cEmail { get; set; }

        [Column(TypeName = "date")]
        public DateTime cCreateDT { get; set; } = new DateTime();

        public ICollection<tblSignupItem> tblSignupItem { get; set; }

        // database first 系統產生

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public tblSignup()
        //{
        //    tblSignupItem = new HashSet<tblSignupItem>();
        //}

        //[Key]
        //[StringLength(10)]
        //public string cMobile { get; set; }

        //[StringLength(20)]
        //public string cName { get; set; }

        //[StringLength(50)]
        //public string cEmail { get; set; }

        //[Column(TypeName = "date")]
        //public DateTime? cCreateDT { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblSignupItem> tblSignupItem { get; set; }
    }
}