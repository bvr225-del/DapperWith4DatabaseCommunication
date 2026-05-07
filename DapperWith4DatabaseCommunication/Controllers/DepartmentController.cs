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
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                var Dept = await _departmentService.AddDepartment(department);
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
                return StatusCode(StatusCodes.Status200OK, deptdata);
            }

        }
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> Getalldepartments()
        {
            throw new Exception("Custom Exception: DepartmentController: Get Api method Excution Failed");

            var res = await _departmentService.GetDepartments();
            if (res == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Department Data Not Found");
            }
            else
            {
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
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }

    }
}
