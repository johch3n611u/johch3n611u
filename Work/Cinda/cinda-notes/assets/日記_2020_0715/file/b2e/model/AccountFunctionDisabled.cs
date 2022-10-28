using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Model.Entity
{
    public class AccountFunctionDisabled
    {   
        public decimal Id { get; set; }
        public decimal? SignFormId { get; set; }
        public string FormStatus { get; set; }
        public string FormType { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Department { get; set; }
        public string EmpNo { get; set; }
        public string Name { get; set; }


    }
}
