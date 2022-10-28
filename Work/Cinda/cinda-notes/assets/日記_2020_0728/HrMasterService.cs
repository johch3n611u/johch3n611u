using System;
using System.Collections.Generic;
using Mxic.Framework.BackendServices;
using Mxic.ITC.PAM.Model.HumanResource;
using Mxic.ITC.PAM.Interface;
using NLog;
using System.Linq;
using System.Runtime.Caching;
using System.Configuration;

namespace Mxic.ITC.PAM.Srv
{
    public class HrMasterService : IHrMasterService
    {
        private static MemoryCache _cache = new MemoryCache("HRCache");
        readonly string hrMasterUrl;
        // Debug Log
        private Logger _Logger = LogManager.GetCurrentClassLogger();
        public string UseJwt { get; set; }

        public HrMasterService()
        {
            hrMasterUrl = ConfigurationManager.AppSettings["HRMasterHost"];
        }

        public GLEmployee GetEmployee(string empNo)
        {
            try
            {
                //var postUrl = "http://dmxhr.macronix.com/master/api/Mxic.HR.Master.BL/Mxic.HR.Master.BL/MxEmployeeService/getEmployee";
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                if (_cache.Contains(empNo))
                    return _cache.Get(empNo) as GLEmployee;

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<GLEmployee>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getEmployee",     // methodName
                    empNo,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                _cache.Add(empNo, result, DateTimeOffset.Now.AddMinutes(10));
                return result;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetEmployee: {ex}，empNo: {empNo}");
                return null;
            }
        }


        public List<GLEmployee> GetEmployees(List<string> empNos)
        {
            try
            {
                List<GLEmployee> ListEmp = new List<GLEmployee>();
                List<string> remove = new List<string>();
                foreach (string item in empNos)
                {
                    if (item != null && _cache.Contains(item))
                    {
                        ListEmp.Add(_cache.Get(item) as GLEmployee);
                        remove.Add(item);
                    }
                }
                empNos.RemoveAll(x => remove.Contains(x));
                if (empNos.Count == 0)
                    return ListEmp;
                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<List<GLEmployee>>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getEmployees",     // methodName
                    empNos,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });
                ListEmp.AddRange(result);
                foreach (string empNo in empNos)
                {
                    GLEmployee emp = result.Find(x => x.empNo == empNo);
                    if (emp != null)
                    {
                        _cache.Add(empNo, emp, DateTimeOffset.Now.AddMinutes(10));
                    }
                }

                return ListEmp;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetEmployees: {ex}，empNo: {empNos}");
                return new List<GLEmployee>();
            }
        }

        public List<GLEmployee> GetEmployeesByDeptNo(string deptNo)
        {
            try
            {
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<List<GLEmployee>>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getEmployeesByDeptNo",     // methodName
                    deptNo,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetEmployeesByDeptNo: {ex}，deptNo: {deptNo}");
                return null;
            }
        }


        public List<GLEmployee> GetEmployeePABQueryResult(GLEmployee empPara)
        {
            try
            {
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                empPara.company = empPara.company.ToUpper() == "MXIC" ? "ALL" : empPara.company;
                var result = backendClient.ExecuteCommand<List<GLEmployee>>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getEmployeePABQueryResult",     // methodName
                    empPara,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetEmployeePABQueryResult: {ex}，empPara: {empPara}");
                return null;
            }
        }

        public MxEmployee GetDeptMgrInfo(string deptNo)
        {
            try
            {
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<MxEmployee>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getDeptMgrInfo",     // methodName
                    deptNo,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetDeptMgrInfo: {ex}，deptNo: {deptNo}");
                return null;
            }
        }

        public List<GLDept> GetAllDept()
        {
            try
            {
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<List<GLDept>>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getAllDept",     // methodName
                    "ALL",  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetAllDept: {ex}");
                return null;
            }
        }

        public List<MxEmployee> GetAllManagerByEmpNo(string empNo, int level)
        {
            try
            {
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<List<MxEmployee>>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getAllManagerByEmpNo",     // methodName
                    new { empNo, level },  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetAllManagerByEmpNo: {ex}，empNo: {empNo}");
                return null;
            }
        }


        public IQueryable<MxDeptMember> GetDeptMembers(string deptNo)
        {
            try
            {
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<IEnumerable<MxDeptMember>>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getDeptMembers",     // methodName
                    deptNo,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result.AsQueryable();
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetDeptMembers: {ex}，deptNo: {deptNo}");
                return null;
            }
        }
        public List<GLEmployee> GetAllManagedEmployeeByMgrNo(string mgrNo)
        {
            try
            {
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<List<GLEmployee>>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getAllManagedEmployeeByMgrNo",     // methodName
                    mgrNo,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetAllManagedEmployeeByMgrNo: {ex}，mgrNo: {mgrNo}");
                return null;
            }
        }
        public List<GLDept> GetAllManagedDeptByMgrNo(string mgrNo)
        {
            try
            {
                //var employee = JsonWebRequest(postUrl, "06519");
                //var result = JsonConvert.DeserializeObject<GLEmployee>(employee);

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<List<GLDept>>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getAllManagedDeptByMgrNo",     // methodName
                    mgrNo,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result;
                //return repository.GetEmployee(empNo);
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetAllManagedDeptByMgrNo: {ex}，mgrNo: {mgrNo}");
                return null;
            }
        }


        public GLEmployee GetEmployeeIncludeQuit(string empNo)
        {
            try
            {
                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<GLEmployee>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "MxEmployeeService",   // className
                    "getEmployeeIncludeQuit",     // methodName
                    empNo,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result;
            }
            catch (Exception ex)
            {
                _Logger.Debug($"GetEmployeeIncludeQuit: {ex}，empNo: {empNo}");
                return null;
            }
        }

        /// <summary>
        /// 依照給予日期判斷是否為工作天
        /// </summary>
        /// <remarks>e.g 離職日 = 帳號權限預計關閉日 +2 個工作天</remarks>
        /// <returns>String Y/N</returns>
        public string IsHoliday(DateTime Date)
        {
            try
            {
                // 專案IT確認事項_20191101_V1.xlsx 跨系統整合API
                var chkHoliday = new ChkHoliday {
                    Date = Date,
                    Company = "MXIC",
                    xLocation = "",
                    Class = "A",
                };

                var backendClient = new BackendServicesClient(new Uri(hrMasterUrl));
                var result = backendClient.ExecuteCommand<GLEmployee>(
                    "Mxic.HR.Master.BL",   // filename
                    "Mxic.HR.Master.BL",   // namespaceName
                    "HolidayService",   // className
                    "isHoliday",     // methodName
                    chkHoliday,  // parameter  (object)
                    UseJwt,
                    (t) =>
                    {
                        // error handling
                        Console.WriteLine(t.ExceptionLocation);
                    });

                return result.ToString();
            }
            catch (Exception ex)
            {
                //_Logger.Debug($"GetEmployeeIncludeQuit: {ex}，empNo: {empNo}");
                return null;
            }

        }
    }
}
