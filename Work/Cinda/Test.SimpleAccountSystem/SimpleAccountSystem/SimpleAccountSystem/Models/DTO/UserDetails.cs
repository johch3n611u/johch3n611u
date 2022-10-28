using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleAccountSystem.Models.DTO
{
    public class UserDetails
    {
        [Key]
        [StringLength(20)]
        [DisplayName("帳號")]
        [Required]
        public string cAccount { get; set; }
        [StringLength(20)]
        [DisplayName("姓名")]
        [Required]
        public string cName { get; set; }
        [StringLength(50)]
        [DisplayName("郵箱")]
        [Required]
        public string cEmail { get; set; }
        [DisplayName("啟用狀態")]
        [Required]
        public int? cStatus { get; set; }
        [DisplayName("權限")]
        public string cGroupNames { get; set; }

    }
}