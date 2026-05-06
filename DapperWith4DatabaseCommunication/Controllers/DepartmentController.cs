using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILoggingFactory _loggingFactory;

        public DepartmentController(IDepartmentService departmentService, ILoggingFactory loggingFactory)
        {
            _departmentService = departmentService;
            _loggingFactory = loggingFactory;
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(DepartmentDto department)
        {
            #region Serilog Logging the mesages 
            Log.Information("DepartmentController: Post Api method Excution Starts");
            Log.Information("DepartmentController: Post Api method called with DeptName: {@DeptName}", department.deptname);
            Log.Information("DepartmentController: Post Api method called with DeptLocation: {@DeptLocation}", department.deptlocation);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentController: Post Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"Post Api method called with DeptName:{department.deptname}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"Post Api method called with DeptLocation: {department.deptlocation}");//logg the message in database using custom logging factory
            #endregion

            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: DepartmentController: Post Api method Excution Failed");

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var Dept = await _departmentService.AddDepartment(department);
                    Log.Information("DepartmentController: Post Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "Post Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status201Created, Dept);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "DepartmentController: Post Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Error", $"DepartmentController: Inside Post Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(DepartmentDto department)
        {
            #region Serilog Logging the mesages 
            Log.Information("DepartmentController put Api method Excution Starts");
            Log.Information("DepartemntController: Put Api method called with DeptId: {@deptid}", department.deptid);
            Log.Information("DepartmentController: Post Api method called with DeptName: {@DeptName}", department.deptname);
            Log.Information("DepartmentController: Post Api method called with DeptLocation: {@DeptLocation}", department.deptlocation);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentController: Post Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"Put Api method called with DeptId:{department.deptid}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"put Api method called with DeptName:{department.deptname}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"Put Api method called with DeptLocation: {department.deptlocation}");//logg the message in database using custom logging factory
            #endregion

            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: DepartmentController: Put Api method Excution Failed");

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var Dept = await _departmentService.UpdateDepartment(department);
                    Log.Information("DepartmentController: Put Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "Put Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, Dept);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "DepartmentController: Put Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Error", $"DepartmentController: Inside Put Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpDelete]
        [Route("Deletedepartment")]
        public async Task<IActionResult> Delete(int deptId)
        {
            #region Serilog Logging the mesages 
            Log.Information("DepartmentController delete Api method Excution Starts");
            Log.Information("DepartemntController: delete Api method called with DeptId: {@deptid}", deptId);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentController: delete Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"delete Api method called with DeptId:{deptId}");//logg the message in database using custom logging factory
            #endregion

            if (deptId < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad Request");
            }
            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: DepartmentController: Delete Api method Excution Failed");

                var deptdata = await _departmentService.DeleteDepartment(deptId);
                if (deptdata == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Department Not Found");
                }
                else
                {
                    Log.Information("DepartmentController: delete Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "delete Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, deptdata);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "DepartmentController: delete Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Error", $"DepartmentController: Inside delete Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> Getalldepartments()
        {
            #region Serilog Logging the mesages 
            Log.Information("DepartmentController: get Api method Excution Starts");
            Log.Information("DepartmentController: get Api method called ");
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentController: GET Api method Excution Starts");//log the message in database using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"GET Api method called");//log the message in database using custom logging factory
            #endregion
            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: DepartmentController: Get Api method Excution Failed");

                var res = await _departmentService.GetDepartments();
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Department Data Not Found");
                }
                else
                {
                    Log.Information("DepartmentController: get Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "get Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, res);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "DepartmentController: get Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Error", $"DepartmentController: Inside get Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");

            }
        }
        [HttpGet]
        [Route("GetDepartmentbyid/{Deptid}")]
        public async Task<IActionResult> Getdepartmentbyid(int Deptid)
        {
            #region Serilog Logging the mesages 
            Log.Information("DepartmentController getById Api method Excution Starts");
            Log.Information("DepartemntController: getById Api method called with DeptId: {@deptid}", Deptid);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentController: getById Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"getById Api method called with DeptId:{Deptid}");//logg the message in database using custom logging factory
            #endregion

            if (Deptid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            }
            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: DepartmentController: getById Api method Excution Failed");

                var res = await _departmentService.GetDepartmentById(Deptid);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Department Not Exists");
                }
                else
                {
                    Log.Information("DepartmentController: getById Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "getById Api method Excution Ended");//logg the message in database using custom logging factory


                    return StatusCode(StatusCodes.Status200OK, res);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
               "DepartmentController: getById Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Error", $"DepartmentController: Inside getById Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

    }
}
