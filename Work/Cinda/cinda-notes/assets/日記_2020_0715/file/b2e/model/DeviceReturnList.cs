using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Model.Entity
{
    public class DeviceReturnList
    {
        public decimal Id { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string DeviceReturn { get; set; }

        public string DeviceReturnDesc { get; set; }

        public string XfortReturn { get; set; }

        public string XfortDesc { get; set; }

        public string NbBringout { get; set; }

        public string NbBringoutDesc { get; set; }

        public decimal? SignFormId { get; set; }

        public decimal? AccountId { get; set; }

        public string XfortAssetNo { get; set; }

        public string NbAssetNo { get; set; }

        // FK JOIN

        public string ResignName { get; set; }

        public string ResignEmpNo { get; set; }

        public string ResignDepartment { get; set; }

        public string XfortControl { get; set; }

        public string NbCustody { get; set; }

        public string SignFormNo { get; set; }
    }
}
