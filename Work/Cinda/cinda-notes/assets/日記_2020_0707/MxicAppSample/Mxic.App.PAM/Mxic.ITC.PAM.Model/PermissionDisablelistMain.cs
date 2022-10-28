using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Model.Entity
{
    public class PermissionDisablelistMain
    {
        public int SignFormId { get; set; }
        public int EmpNo { get; set; }
        public DateTime CloseDate { get; set; }
        public string Signer { get; set; }
        public string DisableDesc { get; set; }
    }
}
