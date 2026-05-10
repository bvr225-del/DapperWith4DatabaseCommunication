using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Dependency Injection
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region User Registration
        /// <summary>  //Summary describes what this method will do, its parameters, and return type. It provides a clear understanding of the method's purpose and functionality to developers who may be using or maintaining the code in the future.
        /// This API is used to register a new user in the system. 
        /// It accepts a UsersDTO object containing user details such as username, password, email, address, phone number, and active status.
        /// The API validates the input and calls the UserResgistration method of the IUserService to perform the registration logic. If the registration is successful, it returns an OK response with the result; otherwise, it returns appropriate error responses based on the validation or exceptions encountered.
        /// </summary>
        /// <param name="usersDTO">The user details for registration.</param>
        /// <returns>An IActionResult indicating the result of the registration operation.</returns>
        [HttpPost]
        [Route("UserRegistartion")]
        public async Task<IActionResult> UserRegistartion([FromBody] UsersDTO usersDTO)
        {//Singup/Register both are same ,use this api fro user registration or user signup.
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = await _userService.UserResgistration(usersDTO);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region User RolesMapping
        [HttpPost]
        [Route("UserRolesMapping")]
        public async Task<IActionResult> UserRolesMapping([FromBody] UserRoleDTO userRoleDTOObj)
        {

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                var res = await _userService.UserRolesMapping(userRoleDTOObj);
                return Ok(res);
            }
        }
        #endregion
    }
}
