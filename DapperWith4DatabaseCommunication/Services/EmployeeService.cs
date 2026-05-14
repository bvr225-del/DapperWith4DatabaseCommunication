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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(IEmployeeRepository employeeRepository, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository= employeeRepository;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddEmployees(EmployeeDto empdetail)
        {
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            #region Serilog Logging the mesages 
            Log.Information($"EmployeeServices: AddEmployes method Excution Starts and Current Loggedin username:{userName}");
            Log.Information($"EmployeeServices: AddEmployes method Inputparamter EmployeeName:{empdetail.empname}");
            Log.Information($"EmployeeServices: AddEmployes method Inputparamter EmployeeSalary:{empdetail.empsalary}");
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: AddEmployees method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeServices:AddEmployees method  Inputparamter EmployeeName:{empdetail.empname}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeServices:AddEmployes method Inputparamter EmployeeSalary:{empdetail.empsalary}");//logg the message in database using custom logging factory
            #endregion

            Employee emp = new Employee();
            emp.empid = empdetail.empid;
            emp.empsalary = empdetail.empsalary;
            emp.empname = empdetail.empname;
            var res = await _employeeRepository.AddEmployees(emp);
            Log.Information($"EmployeeServices: AddEmployees method Excution Ended and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: AddEmployees method Excution Ended");//logg the message in database using custom logging factory
            return res;

        }

        public async Task<bool> DeleteEmployeeById(int empid)
        {
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            #region Serilog Logging the mesages 
            Log.Information($"EmployeeServices: DeleteEmployesById method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeServices: DeleteEmployesById method Inputparamter empid is{empid}");//here capture th empid 

            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: DeleteEmployeeById method Excution Ended");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeServices:DeleteEmployeeById method Inputparamter empid is:{empid}");//logg the message in database using custom logging factory
            #endregion
            await _employeeRepository.DeleteEmployeeById(empid);
            Log.Information($"EmployeeServices: DeleteEmployeeById method Excution Ended and Current Loggedin username:{userName}");//logg the message in text file using serilog
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: DeleteEmployeeById method Excution Ended");//logg the message in database using custom logging factory
            return true;


        }

        public async Task<EmployeeDto> GetEmployeeById(int empid)
        {
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            #region Serilog Logging the mesages 
            Log.Information($"EmployeeServices: GetEmployeeById method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeServices: GetEmployeeById method Inputparamter empid is{empid}");//here capture th empid 

            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: GetEmployeeById method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeServices: GetEmployeeById method Inputparamter empid is:{empid}");//logg the message in database using custom logging factory

            #endregion
            var res = await _employeeRepository.GetEmployeeById(empid);
            EmployeeDto empdto = new EmployeeDto();
            empdto.empid = res.empid;
            empdto.empname = res.empname;
            empdto.empsalary = res.empsalary;
            Log.Information($"EmployeeServices: GetEmployeeById method Excution Ended and Current Loggedin username:{userName}");//logg the message in text file using serilog
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: GetEmployeeById method Excution Ended");//logg the message in database using custom logging factory

            return empdto;

        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            Log.Information($"EmployeeServices: GetEmployees method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: GetEmployees method Excution Starts");//logg the message in database using custom logging factory
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
            Log.Information($"EmployeeServices: GetEmployees method Excution Ended and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: GetEmployees method Excution Ended");
            return lstempdto;
        }

        public async Task<bool> UpdateEmployee(EmployeeDto empdetail)
        {
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            #region Serilog Logging the mesages 
            Log.Information($"EmployeeServices: UpdateEmployee method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeServices: UpdateEmployee method Inputparamter empdto.empid is {empdetail.empid}");//here capture th empid 
            Log.Information($"EmployeeServices: UpdateEmployee method Inputparamter empdto.empname is {empdetail.empname}");//here capture th empid
            Log.Information($"EmployeeServices: UpdateEmployee method Inputparamter empdto.empsalary is{empdetail.empsalary}");//here capture th empid
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: UpdateEmployee method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeServices: UpdateEmployee method Inputparamter empdto.empid is:{empdetail.empid}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeServices: UpdateEmployee method Inputparamter empdto.empname is:{empdetail.empname}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeServices: UpdateEmployee method Inputparamter empdto.empsalary is:{empdetail.empsalary}");//logg the message in database using custom logging factory

            #endregion
            Employee emp = new Employee();
            emp.empid = empdetail.empid;
            emp.empsalary = empdetail.empsalary;
            emp.empname = empdetail.empname;
            await _employeeRepository.UpdateEmployee(emp);
            Log.Information($"EmployeeServices: UpdateEmployee method Excution Ended and Current Loggedin username:{userName}");//logg the message in text file using serilog
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeServices: UpdateEmployee method Excution Ended");//logg the message in database using custom logging factory
            return true;

        }
    }
}
