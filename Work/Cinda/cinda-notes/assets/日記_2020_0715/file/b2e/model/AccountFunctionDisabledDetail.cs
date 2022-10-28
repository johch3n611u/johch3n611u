using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Model.Entity
{
    public class AccountFunctionDisabledDetail
    {
        public string Name { get; set; }
        public DateTime? CloseDate { get; set; }
        public string FormType { get; set; }
        //
        public int? AFdisabledId { get; set; }
        public decimal? Id { get; set; }
        public decimal? SignFormId { get; set; }
        public string Status { get; set; }
        public string ServiceName { get; set; }
        public string Disabled { get; set; }
        public DateTime? PrecloseDate { get; set; }
        public DateTime? DisabledDate { get; set; }
    }
}
