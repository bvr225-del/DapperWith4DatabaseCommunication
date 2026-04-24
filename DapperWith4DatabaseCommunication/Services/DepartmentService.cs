using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Repositories;

namespace DapperWith4DatabaseCommunication.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository= departmentRepository;
        }
        public async Task<int> AddDepartment(DepartmentDto deptdetail)
        {
            Department objDept = new Department();
            objDept.deptname = deptdetail.deptname;
            objDept.deptlocation = deptdetail.deptlocation;
            objDept.deptid = deptdetail.deptid;
            var res = await _departmentRepository.AddDepartment(objDept);
            return res;

        }

        public async Task<string> DeleteDepartment(int departmentid)
        {
            var result = await _departmentRepository.DeleteDepartment(departmentid);
            return result;

        }

        public async Task<DepartmentDto> GetDepartmentById(int deptid)
        {
            var res = await _departmentRepository.GetDepartmentById(deptid);
            DepartmentDto deptdto = new DepartmentDto();
            deptdto.deptid = res.deptid;
            deptdto.deptname = res.deptname;
            deptdto.deptlocation = res.deptlocation;
            return deptdto;

        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            List<DepartmentDto> lstempdto = new List<DepartmentDto>();
            var res = await _departmentRepository.GetDepartments();
            foreach (Department dept in res)
            {
                DepartmentDto deptdto = new DepartmentDto();
                deptdto.deptid = dept.deptid;
                deptdto.deptname = dept.deptname;
                deptdto.deptlocation = dept.deptlocation;
                lstempdto.Add(deptdto);

            }
            return lstempdto;

        }

        public async Task<string> UpdateDepartment(DepartmentDto deptdetail)
        {
            Department objDept = new Department();
            objDept.deptid = deptdetail.deptid;
            objDept.deptname = deptdetail.deptname;
            objDept.deptlocation = deptdetail.deptlocation;
            var result = await _departmentRepository.UpdateDepartment(objDept);
            return result;

        }
    }
}
