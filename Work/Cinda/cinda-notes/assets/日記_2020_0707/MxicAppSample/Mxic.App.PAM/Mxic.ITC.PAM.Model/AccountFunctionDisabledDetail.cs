using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Model.Entity
{
    public class AccountFunctionDisabledDetail
    {
        public int AFdisabledId { get; set; }
        public int Id { get; set; }
        public int SignFormId { get; set; }
        public string Status { get; set; }
        public string ServiceName { get; set; }
        public string Disabled { get; set; }
        public DateTime PrecloseDate { get; set; }
        public DateTime DisabledDate { get; set; }
    }
}
