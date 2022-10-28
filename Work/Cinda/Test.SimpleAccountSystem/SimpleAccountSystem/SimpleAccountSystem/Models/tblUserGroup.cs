namespace SimpleAccountSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblUserGroup")]
    public partial class tblUserGroup
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string cAccount { get; set; }
        [Required]
        public int? cGroupID { get; set; }

        public virtual tblGroup tblGroup { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
