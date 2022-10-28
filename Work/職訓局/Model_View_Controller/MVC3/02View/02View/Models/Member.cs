using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _02View.Models
{
    public class Member
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}