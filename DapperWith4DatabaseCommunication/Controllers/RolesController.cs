using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        [HttpPost]
        [Route("RolesCreation")]
        public async Task<IActionResult> RolesCreation([FromBody] RolesDTO rolesDTOObj)
        {//Here we are creating the roles by using this api method.
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                var res = await _rolesService.RolesCreation(rolesDTOObj);
                return Ok(res);
            }
        }

    }
}
