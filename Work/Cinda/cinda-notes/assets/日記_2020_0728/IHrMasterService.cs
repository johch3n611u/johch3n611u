using Mxic.ITC.PAM.Model.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mxic.ITC.PAM.Interface
{
    public interface IHrMasterService
    {
        string UseJwt { get; set; }
        GLEmployee GetEmployee(string empNo);
        List<GLEmployee> GetEmployees(List<string> empNo);
        List<GLEmployee> GetEmployeesByDeptNo(string deptNo);
        MxEmployee GetDeptMgrInfo(string deptNo);
        List<GLDept> GetAllDept();
        List<MxEmployee> GetAllManagerByEmpNo(string empNo, int level);
        IQueryable<MxDeptMember> GetDeptMembers(string deptNo);
        List<GLEmployee> GetEmployeePABQueryResult(GLEmployee empPara);
        List<GLEmployee> GetAllManagedEmployeeByMgrNo(string mgrNo);
        List<GLDept> GetAllManagedDeptByMgrNo(string mgrNo);
        GLEmployee GetEmployeeIncludeQuit(string empNo);
        /// <summary>
        /// 依照給予日期判斷是否為工作天
        /// </summary>
        /// <remarks>e.g 離職日 = 帳號權限預計關閉日 +2 個工作天，FakeHrMaster一律回復 "N"</remarks>
        /// <returns>String Y/N</returns>
        string IsHoliday(DateTime Date);
    }
}
