using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleAccountSystem.Models.DTO
{
    public class CheckBoxListInfo
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public string DisplayText { get; set; }
        [Required]
        public string IsChecked { get; set; }
     
    }
}