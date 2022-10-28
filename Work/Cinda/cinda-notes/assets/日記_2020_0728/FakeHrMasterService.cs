using System;
using System.Collections.Generic;
using AutoMapper;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.HumanResource;
using Mxic.ITC.PAM.Repository.FakeRepository;

using Mxic.ITC.PAM.Interface;
using System.Linq;

namespace Mxic.ITC.PAM.Srv
{
    public class FakeHrMasterService : IHrMasterService
    {
        private readonly IMapper _mapper;

        public FakeHrMasterService()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<GLEmployee, FakeHrMaster>()
                        .ForMember(s => s.AdName, opt => opt.MapFrom(s => s.adName))
                        .ForMember(s => s.EmployeeNo, opt => opt.MapFrom(s => s.empNo))
                        .ForMember(s => s.DeptNo, opt => opt.MapFrom(s => s.deptNo))
                        .ForMember(s => s.NickName, opt => opt.MapFrom(s => s.empName))
                        .ForMember(s => s.DeptGroup, opt => opt.MapFrom(s => s.deptGroup))
                        .ForMember(s => s.Manage, opt => opt.MapFrom(s => s.managerEmpNo));

                    cfg.CreateMap<FakeHrMaster, GLEmployee>()
                        .ForMember(s => s.adName, opt => opt.MapFrom(s => s.AdName))
                        .ForMember(s => s.empNo, opt => opt.MapFrom(s => s.EmployeeNo))
                        .ForMember(s => s.deptNo, opt => opt.MapFrom(s => s.DeptNo))
                        .ForMember(s => s.empName, opt => opt.MapFrom(s => s.NickName))
                        .ForMember(s => s.deptGroup, opt => opt.MapFrom(s => s.DeptGroup))
                        .ForMember(s => s.managerEmpNo, opt => opt.MapFrom(s => s.Manage))
                        .ForMember(s => s.email, opt => opt.MapFrom(s => string.Format("{0}@yopmail.com", s.AdName)));

                    cfg.CreateMap<FakeHrMaster, MxEmployee>()
                        .ForMember(s => s.adName, opt => opt.MapFrom(s => s.AdName))
                        .ForMember(s => s.empNo, opt => opt.MapFrom(s => s.EmployeeNo))
                        .ForMember(s => s.deptNo, opt => opt.MapFrom(s => s.DeptNo))
                        .ForMember(s => s.empName, opt => opt.MapFrom(s => s.NickName))
                        .ForMember(s => s.email, opt => opt.MapFrom(s => string.Format("{0}@yopmail.com", s.AdName)));

                    cfg.CreateMap<Department, GLDept>()
                        .ForMember(s => s.deptNo, opt => opt.MapFrom(s => s.Name));
                });

            _mapper = config.CreateMapper();
        }

        public string UseJwt { get; set; }

        public GLEmployee GetEmployee(string empNo)
        {
            var repository = new HrMasterRepository();
            return _mapper.Map<GLEmployee>(repository.GetEmployee(empNo));
        }
        public List<GLEmployee> GetEmployees(List<string> empNos)
        {
            var repository = new HrMasterRepository();
            return _mapper.Map<List<GLEmployee>>(repository.GetEmployees(empNos));
        }
        public List<GLEmployee> GetEmployeesByDeptNo(string deptNo)
        {
            var repository = new HrMasterRepository();
            return _mapper.Map<List<GLEmployee>>(repository.GetEmployeesByDeptNo(deptNo));
        }


        public List<GLEmployee> GetEmployeePABQueryResult(GLEmployee empPara)
        {
            var repository = new HrMasterRepository();
            return _mapper.Map<List<GLEmployee>>(repository.GetEmployeePABQueryResult(empPara));
        }

        public MxEmployee GetDeptMgrInfo(string deptNo)
        {
            var repository = new HrMasterRepository();
            return _mapper.Map<MxEmployee>(repository.GetManagerByDeptNo(deptNo));
        }

        public List<GLDept> GetAllDept()
        {
            using (var repository = new Repository.DepartmentRepository())
            {
                var departments = repository.GetDepartment();
                return _mapper.Map<List<GLDept>>(departments);
            }
        }

        public List<MxEmployee> GetAllManagerByEmpNo(string empNo, int level)
        {
            var mxEmployees = new List<MxEmployee>();
            var repository = new HrMasterRepository();
            var employee = repository.GetEmployee(empNo);
            if (employee != null)
            {
                mxEmployees.Add(_mapper.Map<MxEmployee>(repository.GetManagerByDeptNo(employee.DeptNo)));
            }

            return mxEmployees;
        }


        public GLEmployee Authroized(string adName, string password)
        {
            var repository = new HrMasterRepository();
            var master = repository.Login(adName, password);
            return _mapper.Map<GLEmployee>(master);
        }

        public IQueryable<MxDeptMember> GetDeptMembers(string deptNo)
        {
            throw new NotImplementedException();
        }
        public List<GLEmployee> GetAllManagedEmployeeByMgrNo(string mgrNo)
        {
            var repository = new HrMasterRepository();
            return _mapper.Map<List<GLEmployee>>(repository.GetAllManagedEmployeeByMgrNo(mgrNo));
        }
        public List<GLDept> GetAllManagedDeptByMgrNo(string mgrNo)
        {
            var repository = new HrMasterRepository();
            return _mapper.Map<List<GLDept>>(repository.GetAllManagedDeptByMgrNo(mgrNo));
        }

        public GLEmployee GetEmployeeIncludeQuit(string empNo)
        {
            var repository = new HrMasterRepository();
            return _mapper.Map<GLEmployee>(repository.GetEmployeeIncludeQuit(empNo));
        }

        /// <summary>
        /// 依照給予日期判斷是否為工作天
        /// </summary>
        /// <remarks>e.g 離職日 = 帳號權限預計關閉日 +2 個工作天</remarks>
        /// <returns>String Y/N</returns>
        public string IsHoliday(DateTime Date)
        {
            return "Y";
        }
    }
}
