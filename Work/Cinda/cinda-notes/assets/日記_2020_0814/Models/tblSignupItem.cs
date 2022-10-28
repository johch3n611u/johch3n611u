namespace Member.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblSignupItem")]
    public partial class tblSignupItem
    {
        [StringLength(10)]
        public string cMobile { get; set; }

        public int? cItemID { get; set; }

        public int ID { get; set; }

        public virtual tblActiveItem tblActiveItem { get; set; }

        public virtual tblSignup tblSignup { get; set; }
    }
}
