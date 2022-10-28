using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _05ViewModel.Models;

namespace _05ViewModel.ViewModel
{
    public class CVM
    {
        public List<tDepartment> department { get; set; }
        public List<tEmployee> employee { get; set; }
    }
}