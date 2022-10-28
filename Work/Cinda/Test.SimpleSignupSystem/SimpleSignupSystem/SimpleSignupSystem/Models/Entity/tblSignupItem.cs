using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSignupSystem.Models.Entity
{
    [Table("tblSignupItem")]
    public class tblSignupItem
    {
        // 手動編輯

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Key]
        [Column(Order = 2, TypeName = "varchar")]
        [ForeignKey("tblSignup")]
        [MaxLength(10)]
        public string cMobile { get; set; }
        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("tblActiveItem")]
        public int cItemID { get; set; }

        public virtual tblActiveItem tblActiveItem { get; set; }
        public virtual tblSignup tblSignup { get; set; }

        // database first 系統產生

        //[StringLength(10)]
        //public string cMobile { get; set; }
        //public int? cItemID { get; set; }
        //public int ID { get; set; }

        //public virtual tblActiveItem tblActiveItem { get; set; }
        //public virtual tblSignup tblSignup { get; set; }
    }

}
