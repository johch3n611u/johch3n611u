using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleAccountSystem.Models.DTO
{
    public class BoolDropdownList
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public string DisplayText { get; set; }
    }
}