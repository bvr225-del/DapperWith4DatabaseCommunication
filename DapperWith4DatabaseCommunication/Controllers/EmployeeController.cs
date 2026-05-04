using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILoggingFactory _loggingFactory;

        public EmployeeController(IEmployeeService employeeService, ILoggingFactory loggingFactory)
        {
            _employeeService = employeeService;
            _loggingFactory = loggingFactory;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post([FromBody] EmployeeDto empdto)
        {//dtos are used to transafer the data purpose used.
            Log.Information("EmployeeController: Post Api method Excution Starts");
            Log.Information("EmployeeController: Post Api method called with EmployeeName: {@EmployeeName}", empdto.empname);
            Log.Information("EmployeeController: Post Api method called with EmployeeSalary: {@EmployeeSalary}", empdto.empsalary);
            await _loggingFactory.AddLoggingMessages("chandu", "Information", "EmployeeController: Post Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("chandu", "Information", $"Post Api method called with EmployeeName:{empdto.empname}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("chandu", "Information", $"Post Api method called with EmployeeSalary:{empdto.empsalary}");//logg the message in database using custom logging factory

            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeService.AddEmployees(empdto);
                    Log.Information("EmployeeController: Post Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("chandu", "Information", "Post Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status201Created, empdata);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "EmployeeController: Post Api method", ex.Message, DateTime.Today);
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpDelete]
        [Route("DeleteEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> delete(int empid)
        {
            if (empid < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var empdata = await _employeeService.DeleteEmployeeById(empid);
                if (empdata == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "empdata not  found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var empdata = await _employeeService.GetEmployees();
                if (empdata == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }
        [HttpGet]
        [Route("GetEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> Get(int empid)
        {
            if (empid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var empdata = await _employeeService.GetEmployeeById(empid);
                return StatusCode(StatusCodes.Status200OK, empdata);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> put([FromBody] EmployeeDto empdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeService.UpdateEmployee(empdto);
                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

    }
}
