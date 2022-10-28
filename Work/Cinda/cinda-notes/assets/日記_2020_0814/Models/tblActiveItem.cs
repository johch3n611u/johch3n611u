namespace Member.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblActiveItem")]
    public partial class tblActiveItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblActiveItem()
        {
            tblSignupItem = new HashSet<tblSignupItem>();
        }

        [Key]
        public int cItemID { get; set; }

        [Column(TypeName = "text")]
        public string cItemName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? cActiveDt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSignupItem> tblSignupItem { get; set; }
    }
}
