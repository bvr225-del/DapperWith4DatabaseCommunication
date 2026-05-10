using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;
        public AuthenticateController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("UserSignIn")]
        public async Task<IActionResult> UserSignIn([FromBody] LoginDTO loginDTOObj)
        {
            var res = await _authenticateService.UserSignIn(loginDTOObj);
            return Ok(res);
        }

    }
}
