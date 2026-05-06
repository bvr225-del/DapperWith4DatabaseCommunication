using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Repositories;
using Serilog;

namespace DapperWith4DatabaseCommunication.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILoggingFactory _loggingFactory;

        public DepartmentService(IDepartmentRepository departmentRepository, ILoggingFactory loggingFactory)
        {
            _departmentRepository = departmentRepository;
            _loggingFactory = loggingFactory;
        }
        public async Task<int> AddDepartment(DepartmentDto deptdetail)
        {
            Log.Information("DepartmentService: AddDepartment method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: AddDepartment method Excution Starts");//logg the message in database using custom logging factory

            Department objDept = new Department();
            objDept.deptname = deptdetail.deptname;
            objDept.deptlocation = deptdetail.deptlocation;
            objDept.deptid = deptdetail.deptid;
            var res = await _departmentRepository.AddDepartment(objDept);
            Log.Information("DepartmentService: AddDepartment method Excution Ended");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: AddDepartment method Excution Ended");//logg the message in database using custom logging factory

            return res;

        }

        public async Task<string> DeleteDepartment(int departmentid)
        {
            Log.Information("DepartmentService: DeleteDepartment method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: DeleteDepartment method Excution Starts");//logg the message in database using custom logging factory

            var result = await _departmentRepository.DeleteDepartment(departmentid);
            Log.Information("DepartmentService: DeleteDepartment method Excution Ended");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: DeleteDepartment method Excution Ended");//logg the message in database using custom logging factory
            return result;

        }

        public async Task<DepartmentDto> GetDepartmentById(int deptid)
        {
            Log.Information("DepartmentService: GetDepartmentById method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: GetDepartmentById method Excution Starts");//logg the message in database using custom logging factory

            var res = await _departmentRepository.GetDepartmentById(deptid);
            DepartmentDto deptdto = new DepartmentDto();
            deptdto.deptid = res.deptid;
            deptdto.deptname = res.deptname;
            deptdto.deptlocation = res.deptlocation;
            Log.Information("DepartmentService: GetDepartmentById method Excution Ended");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService:GetDepartmentById method Excution Ended");//logg the message in database using custom logging factory

            return deptdto;

        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            Log.Information("DepartmentService: GetDepartments method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: GetDepartments method Excution Starts");//logg the message in database using custom logging factory

            List<DepartmentDto> lstempdto = new List<DepartmentDto>();
            var res = await _departmentRepository.GetDepartments();
            foreach (Department dept in res)
            {
                DepartmentDto deptdto = new DepartmentDto();
                deptdto.deptid = dept.deptid;
                deptdto.deptname = dept.deptname;
                deptdto.deptlocation = dept.deptlocation;
                Log.Information("DepartmentService: GetDepartments method Excution Ended");
                await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: GetDepartments method Excution Ended");//logg the message in database using custom logging factory

                lstempdto.Add(deptdto);

            }
            return lstempdto;

        }

        public async Task<string> UpdateDepartment(DepartmentDto deptdetail)
        {
            Log.Information("DepartmentService: UpdateDepartment method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: UpdateDepartment method Excution Starts");//logg the message in database using custom logging factory

            Department objDept = new Department();
            objDept.deptid = deptdetail.deptid;
            objDept.deptname = deptdetail.deptname;
            objDept.deptlocation = deptdetail.deptlocation;
            var result = await _departmentRepository.UpdateDepartment(objDept);
            Log.Information("DepartmentService: UpdateDepartment method Excution Ended");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "DepartmentService: UpdateDepartment method Excution Ended");//logg the message in database using custom logging factory

            return result;

        }
    }
}
