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


                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeService.AddEmployees(empdto);
                    return StatusCode(StatusCodes.Status201Created, empdata);
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
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployees()
        {
            throw new Exception("Custom Exception: EmployeeController: get Api method Excution Failed");

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
        [HttpGet]
        [Route("GetEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> Get(int empid)
        {
            if (empid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
                var empdata = await _employeeService.GetEmployeeById(empid);
                return StatusCode(StatusCodes.Status200OK, empdata);
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> put([FromBody] EmployeeDto empdto)
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

    }
}
/*(*)How many ways we can log the messages in dotnet core?
 * 3 ways we can log the messages in dotnet core:
 * (1)By using Serilog we can log the messages in text file.
 * (2)By using your orm and custom logging factory we can log the messages in database.
 * (3)by using Azure Application Insights Service we can log the messages in azure cloud(for thi azure need to pay the money,very expensive).
 * ===================================================================================
 * 
 * 1.how to log the messages in text file using serilog:(or)How to log the messages in dotnet core?
 * =>By using Serilog we can log the messages in text file and 
 * also we can log the messages in database by using custom logging factory.
 * and also by using Azure Application Insights we can log the messages in azure cloud.
 * 1.First Install the serilog.Aspnetcore  nuget package in your project.
 * 2.Next We need to register Serilog to our dependency Injection Conatiner with below code.
 * ====================================================================
 * builder.Host.UseSerilog((context, configuration) =>
   configuration.ReadFrom.Configuration(context.Configuration));
 * ============================================================================
 * 3.Use the Log.Information(), Log.Error(), Log.Warning() methods to log the messages in your code.
 * 4)in appsettings.json file we need to add the serilog configuration code to specify the log file path and other settings for serilog.
 * #########################################################################################
 * 
 * 
 * 
 * 2.how to log the messages in database using custom logging factory:
 * =>We can also log the messages using serilog and customlogging factory.
 * 
 * 1.Create a logging factory class that implements an interface for logging.
 * 2.Inside the logging factory class, create a method that takes the log message and other relevant information as parameters and saves it to the database using Dapper or any other data access method.
 * 3.Inject the logging factory into your controller or service class where you want to log the messages and call the logging method with appropriate parameters whenever you want to log a message.
 */