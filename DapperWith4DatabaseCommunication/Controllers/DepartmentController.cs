using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Authorize]
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
                //throw new Exception("Custom Exception: DepartmentController: Post Api method Excution Failed");

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var Dept = await _departmentService.AddDepartment(department);
                    Log.Information("DepartmentController: Post Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "Post Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status201Created, Dept);
                }
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(DepartmentDto department)
        {

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var Dept = await _departmentService.UpdateDepartment(department);
                    Log.Information("DepartmentController: Put Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "Put Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, Dept);
                }
        }
        [HttpDelete]
        [Route("Deletedepartment")]
        public async Task<IActionResult> Delete(int deptId)
        {
            if (deptId < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad Request");
            }
                var deptdata = await _departmentService.DeleteDepartment(deptId);
                if (deptdata == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Department Not Found");
                }
                else
                {
                    Log.Information("DepartmentController: delete Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "delete Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, deptdata);
                }
        }
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> Getalldepartments()
        {

                var res = await _departmentService.GetDepartments();
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Department Data Not Found");
                }
                else
                {
                    Log.Information("DepartmentController: get Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "get Api method Excution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, res);
                }
        }
        [HttpGet]
        [Route("GetDepartmentbyid/{Deptid}")]
        public async Task<IActionResult> Getdepartmentbyid(int Deptid)
        {
            if (Deptid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            }

                var res = await _departmentService.GetDepartmentById(Deptid);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Department Not Exists");
                }
                else
                {
                    Log.Information("DepartmentController: getById Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "getById Api method Excution Ended");//logg the message in database using custom logging factory


                    return StatusCode(StatusCodes.Status200OK, res);
                }
        }

    }
}
