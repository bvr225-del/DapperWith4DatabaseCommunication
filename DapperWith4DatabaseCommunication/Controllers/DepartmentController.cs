using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService= departmentService;
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(DepartmentDto department)
        {
            try
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
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(DepartmentDto department)
        {
            try
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
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
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
            try
            {
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
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
            try
            {
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

    }
}
