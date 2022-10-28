using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Model.Entity
{
    public class PermissionDisablelistSub
    {
        public int SignFormId { get; set; }
        public int ApplyNo { get; set; }
        public string Item { get; set; }
        public string FunctionType { get; set; }
        public string UseType { get; set; }
        public int Assetid { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public string Module { get; set; }
    }
}
