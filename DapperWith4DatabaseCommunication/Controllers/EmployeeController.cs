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
            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: Post Api method Excution Starts");
            Log.Information("EmployeeController: Post Api method called with EmployeeName: {@EmployeeName}", empdto.empname);
            Log.Information("EmployeeController: Post Api method called with EmployeeSalary: {@EmployeeSalary}", empdto.empsalary);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "EmployeeController: Post Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", $"Post Api method called with EmployeeName:{empdto.empname}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", $"Post Api method called with EmployeeSalary:{empdto.empsalary}");//logg the message in database using custom logging factory
            #endregion

            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: EmployeeController: Post Api method Excution Failed");


                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeService.AddEmployees(empdto);
                    Log.Information("EmployeeController: Post Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "Post Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status201Created, empdata);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "EmployeeController: Post Api method", ex.Message, DateTime.Today);
                await _loggingFactory.AddLoggingMessages("venkat", "Error", $"EmployeeController: Inside Post Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpDelete]
        [Route("DeleteEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> delete(int empid)
        {
            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: delete Api method Excution Starts");
            Log.Information("EmployeeController: delete Api method called with EmployeeId: {@empid}",empid);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "EmployeeController: delete Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", $"delete Api method called with EmployeeId:{empid}");//logg the message in database using custom logging factory
            #endregion


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
                    Log.Information("EmployeeController: delete Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "delete Api method Execution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "EmployeeController: delete Api method", ex.Message, DateTime.Today);
                await _loggingFactory.AddLoggingMessages("venkat", "Error", $"EmployeeController: Inside delete Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployees()
        {
            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: get Api method Excution Starts");
            Log.Information("EmployeeController: get Api method called ");
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "EmployeeController: GET Api method Excution Starts");//log the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", $"GET Api method called");//log the message in database using custom logging factory
            #endregion

            try
            {
                var empdata = await _employeeService.GetEmployees();
                if (empdata == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    Log.Information("EmployeeController: Get Api method Excution Ended");//log the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "Get Api method Excution Ended");//log the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "EmployeeController: GET Api method", ex.Message, DateTime.Today);
                await _loggingFactory.AddLoggingMessages("venkat", "Error", $"EmployeeController: Inside GET Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }
        [HttpGet]
        [Route("GetEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> Get(int empid)
        {
            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: get Api method Excution Starts");
            Log.Information("EmployeeController: get Api method called with EmployeeId: {@empid}", empid);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "EmployeeController: get Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", $"get Api method called with EmployeeId:{empid}");//logg the message in database using custom logging factory
            #endregion


            if (empid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var empdata = await _employeeService.GetEmployeeById(empid);
                Log.Information("EmployeeController: get Api method Excution Ended");//logg the message in text file using serilog
                await _loggingFactory.AddLoggingMessages("venkat", "Information", "get Api method Execution Ended");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status200OK, empdata);
            }
            catch (Exception ex)
            {               
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "EmployeeController: get Api method", ex.Message, DateTime.Today);
                await _loggingFactory.AddLoggingMessages("venkat", "Error", $"EmployeeController: Inside get Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> put([FromBody] EmployeeDto empdto)
        {
            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: Put Api method Excution Starts");
            Log.Information("EmployeeController: Put Api method called with EmployeeID: {@empid}", empdto.empid);
            Log.Information("EmployeeController: Put Api method called with EmployeeName: {@EmployeeName}", empdto.empname);
            Log.Information("EmployeeController: Put Api method called with EmployeeSalary: {@EmployeeSalary}", empdto.empsalary);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "EmployeeController: Put Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", $"Put Api method called with EmployeeID:{empdto.empid}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", $"Put Api method called with EmployeeName:{empdto.empname}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages("venkat", "Information", $"Put Api method called with EmployeeSalary:{empdto.empsalary}");//logg the message in database using custom logging factory
            #endregion


            try
            {
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeService.UpdateEmployee(empdto);
                    Log.Information("EmployeeController: put Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "put Api method Execution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
               "EmployeeController: put Api method", ex.Message, DateTime.Today);
                await _loggingFactory.AddLoggingMessages("venkat", "Error", $"EmployeeController: Inside put Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory


                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
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