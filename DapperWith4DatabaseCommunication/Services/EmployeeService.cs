using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using Serilog;

namespace DapperWith4DatabaseCommunication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggingFactory _loggingFactory;

        public EmployeeService(IEmployeeRepository employeeRepository, ILoggingFactory loggingFactory)
        {
            _employeeRepository= employeeRepository;
            _loggingFactory = loggingFactory;
        }
        public async Task<int> AddEmployees(EmployeeDto empdetail)
        {
            Log.Information("EmployeeServices: AddEmployes method Excution Starts");
            await _loggingFactory.AddLoggingMessages("chandu", "Information", "EmployeeServices: AddEmployes method Excution Starts");//logg the message in database using custom logging factory

            Employee emp = new Employee();
            emp.empid = empdetail.empid;
            emp.empsalary = empdetail.empsalary;
            emp.empname = empdetail.empname;
            var res = await _employeeRepository.AddEmployees(emp);

            Log.Information("EmployeeServices: AddEmployes method Excution Ended");
            await _loggingFactory.AddLoggingMessages("chandu", "Information", "EmployeeServices: AddEmployes method Excution Ended");//logg the message in database using custom logging factory

            return res;

        }

        public async Task<bool> DeleteEmployeeById(int empid)
        {
            await _employeeRepository.DeleteEmployeeById(empid);
            return true;


        }

        public async Task<EmployeeDto> GetEmployeeById(int empid)
        {
            var res = await _employeeRepository.GetEmployeeById(empid);
            EmployeeDto empdto = new EmployeeDto();
            empdto.empid = res.empid;
            empdto.empname = res.empname;
            empdto.empsalary = res.empsalary;
            return empdto;

        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            List<EmployeeDto> lstempdto = new List<EmployeeDto>();
            var res = await _employeeRepository.GetEmployees();
            foreach (Employee emp in res)
            {
                EmployeeDto empdto = new EmployeeDto();
                empdto.empid = emp.empid;
                empdto.empsalary = emp.empsalary;
                empdto.empname = emp.empname;
                lstempdto.Add(empdto);

            }
            return lstempdto;

        }

        public async Task<bool> UpdateEmployee(EmployeeDto empdetail)
        {
            Employee emp = new Employee();
            emp.empid = empdetail.empid;
            emp.empsalary = empdetail.empsalary;
            emp.empname = empdetail.empname;
            await _employeeRepository.UpdateEmployee(emp);
            return true;

        }
    }
}
