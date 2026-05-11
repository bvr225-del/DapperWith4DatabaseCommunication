using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace DapperWith4DatabaseCommunication.Controllers
{
    //to provide the security to this controller api methods use [Authorize] attribute.
    //if you pass the token to this api calling ,then only you can access this employee controller api methods.
    //without token you can't access this employee controller api methods.
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        private readonly ILoggingFactory _loggingFactory;//this is used for log the messages for text file
        private readonly IHttpContextAccessor _httpContextAccessor;
        //IHttpContextAccessor used get the token related information.this interface is predefined.
        public EmployeeController(IEmployeeService employeeService, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _employeeService = employeeService;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post([FromBody] EmployeeDto empdto)
        {//dtos are used to transafer the data purpose used.
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: Post Api method Excution Starts and Current Loggedin username:", userName);
            Log.Information("EmployeeController: Post Api method Inputparamter EmployeeName:", empdto.empname);
            Log.Information("EmployeeController: Post Api method Inputparamter EmployeeSalary:", empdto.empsalary);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController: Post Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"Post Api method  Inputparamter EmployeeName:{empdto.empname}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"Post Api method Inputparamter EmployeeSalary:{empdto.empsalary}");//logg the message in database using custom logging factory
            #endregion
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
                Log.Information("EmployeeController: Post Api method Excution Ended and Current Loggedin username:", userName);//logg the message in text file using serilog
                await _loggingFactory.AddLoggingMessages(userName, "Information", "Post Api method Excution Ended");//logg the message in database using custom logging factory
                return StatusCode(StatusCodes.Status201Created, empdata);
            }
        }
        [HttpDelete]
        [Route("DeleteEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> delete(int empid)
        {
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: delete Api method Excution Starts and Current Loggedin username:", userName);//logg the message in text file using serilog
            Log.Information("EmployeeController: delete Api method Inputparamter empid is", empid);//here capture th empid 

            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController: Delete Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"delete Api method Inputparamter empid is:{empid}");//logg the message in database using custom logging factory

            #endregion

            if (empid < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            var empdata = await _employeeService.DeleteEmployeeById(empid);
            if (empdata == null)
            {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                Log.Information("EmployeeController: delete Api method Excution Starts and Current Loggedin username:", userName);//logg the message in text file using serilog
                await _loggingFactory.AddLoggingMessages(userName, "Information", "delete Api method Excution Ended");//logg the message in database using custom logging factory
                return StatusCode(StatusCodes.Status404NotFound, "empdata not  found");
            }
            else
            {
                Log.Information("EmployeeController: delete Api method Excution Starts and Current Loggedin username:", userName);//logg the message in text file using serilog
                await _loggingFactory.AddLoggingMessages(userName, "Information", "delete Api method Excution Ended");//logg the message in database using custom logging factory
                return StatusCode(StatusCodes.Status200OK, "deleted successfully");
            }

        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployees()
        {
            //To read  the token from postman/react/angular/mobile application we used below code 
            //===================First way of read token properties(Just understanding purpose)=================================================
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.Replace("Bearer ", "");


            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);
            Dictionary<string, string> obj = new Dictionary<string, string>();
            foreach (var claim in jwtToken.Claims)
            {
                obj.Add(claim.Type, claim.Value);
            }

            var totaldata = obj;

            //====================Second way of read token properties(Just  understanding purpose)===================
            var userName1 = User.FindFirst("UserName")?.Value;

            var email = User.FindFirst("EmailId")?.Value;

            var phone = User.FindFirst("PhoneNumber")?.Value;

            var address = User.FindFirst("Address")?.Value;

            var isActive = User.FindFirst("IsActive")?.Value;

            var role = User.FindFirst("Roles")?.Value;
            //=================================
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: GetEmployees Api method Excution Starts and Current Loggedin username:", userName);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController: GetEmployees Api method Excution Starts");//logg the message in database using custom logging factory
            #endregion
            var empdata = await _employeeService.GetEmployees();
            if (empdata == null)
            {
                Log.Information("EmployeeController: GetEmployees Api method Excution Ended and Current Loggedin username:", userName);//logg the message in text file using serilog
                await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController: GetEmployees Api method Excution Ended");//logg the message in database using custom logging factory
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            else
            {
                Log.Information("EmployeeController: GetEmployees Api method Excution Ended and Current Loggedin username:", userName);//logg the message in text file using serilog
                await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController: GetEmployees Api method Excution Ended");//logg the message in database using custom logging factory
                return StatusCode(StatusCodes.Status200OK, empdata);
            }
        }
        [HttpGet]
        [Route("GetEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> Get(int empid)
        {
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: Get Api method Excution Starts and Current Loggedin username:", userName);//logg the message in text file using serilog
            Log.Information("EmployeeController: Get Api method Inputparamter empid is", empid);//here capture th empid 

            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController: Get Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeController: Get Api method Inputparamter empid is:{empid}");//logg the message in database using custom logging factory

            #endregion

            if (empid < 0)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            var empdata = await _employeeService.GetEmployeeById(empid);
            Log.Information("EmployeeController: Get Api method Excution Ended and Current Loggedin username:", userName);//logg the message in text file using serilog
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController: Get Api method Excution Ended");//logg the message in database using custom logging factory
            return StatusCode(StatusCodes.Status200OK, empdata);
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> put([FromBody] EmployeeDto empdto)
        {
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            #region Serilog Logging the mesages 
            Log.Information("EmployeeController: put Api method Excution Starts and Current Loggedin username:", userName);//logg the message in text file using serilog
            Log.Information("EmployeeController: put Api method Inputparamter empdto.empid is", empdto.empid);//here capture th empid 
            Log.Information("EmployeeController: put Api method Inputparamter empdto.empname is", empdto.empname);//here capture th empid
            Log.Information("EmployeeController: put Api method Inputparamter empdto.empsalary is", empdto.empsalary);//here capture th empid
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController:  put Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeController: put Api method Inputparamter empdto.empid is:{empdto.empid}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeController: put Api method Inputparamter empdto.empname is:{empdto.empname}");//logg the message in database using custom logging factory
            await _loggingFactory.AddLoggingMessages(userName, "Information", $"EmployeeController: put Api method Inputparamter empdto.empsalary is:{empdto.empsalary}");//logg the message in database using custom logging factory

            #endregion
            if (!ModelState.IsValid)
            {

                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                var empdata = await _employeeService.UpdateEmployee(empdto);
                Log.Information("EmployeeController: put Api method Excution Ended and Current Loggedin username:", userName);//logg the message in text file using serilog
                await _loggingFactory.AddLoggingMessages(userName, "Information", "EmployeeController:put Api method Excution Ended");//logg the message in database using custom logging factory
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