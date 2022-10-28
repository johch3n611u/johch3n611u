using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSignupSystem.Models.Entity
{
    [Table("tblActiveItem")]
    public class tblActiveItem
    {
        // 手動編輯

        public tblActiveItem()
        {
            tblSignupItem = new HashSet<tblSignupItem>();
        }

        [Key]
        [Column(Order = 1)]
        public int cItemID { get; set; }
        [Column(TypeName = "Nvarchar")]
        public string cItemName { get; set; }
        [Column(TypeName = "text")]
        public string cActiveDt { get; set; }
        public ICollection<tblSignupItem> tblSignupItem { get; set; }

        // database first 系統產生

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        //public tblActiveItem()
        //{
        //    tblSignupItem = new HashSet<tblSignupItem>();
        //}

        //[Key]
        //public int cItemID { get; set; }
        //[Column(TypeName = "text")]
        //public string cItemName { get; set; }
        //[Column(TypeName = "text")]
        //public string cActiveDt { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        //public virtual ICollection<tblSignupItem> tblSignupItem { get; set; }
    }
}