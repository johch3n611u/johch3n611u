using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Model.Entity
{
    public class DeviceReturnMain
    {
        public int SignFormId { get; set; }
        public int EmpNo { get; set; }
        public DateTime ReturnDate { get; set; }
        public string DeviceReturn { get; set; }
        public string DeviceReturnDesc { get; set; }
        public string XfortReturn { get; set; }
        public string XfortDesc { get; set; }
        public string NbBringout { get; set; }
        public string NbBringoutDesc { get; set; }
    }
}
