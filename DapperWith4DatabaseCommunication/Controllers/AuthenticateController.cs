using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        [Route("UserSignInWithoutToken")]//Siginin/Login both are same.
        //Here i am verifyibg the username and password ,once it will valid i am returning the uservalid messge.
        public async Task<IActionResult> UserSignInWithoutToken([FromBody] LoginDTO loginDTOObj)
        {
            var res = await _authenticateService.UserSignIn(loginDTOObj);
            return Ok(res);
        }
        [HttpPost]
        [Route("UserSignIn")]//Siginin/Login both are same.
        //Here i am verifyibg the username and password ,once it will valid i am generting the token.
        public async Task<IActionResult> UserSignIn([FromBody] LoginDTO loginDTOObj)
        {
            UserLoginResponse loginResponseObj = new UserLoginResponse();
            //Below process is recomended to validate the apis filed for validation.realtime use this process.
            var validationMessages = ValidationMessages.UserSignIn(loginDTOObj);
            if (validationMessages.Length > 0)
            {

                return StatusCode(StatusCodes.Status400BadRequest, Convert.ToString(validationMessages));
            }
            //if (!ModelState.IsValid)
            //{//this way not rememendedway to implemt validations
            //    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            //}
            else
            {
                var res = await _authenticateService.UserSignIn(loginDTOObj);
                if (res.StatusCode == ConstantMessages.SuccessStatusCode)
                {//if user details are validated successfully and the response conatins success status code 200,we will fetch the userroles information by using username.
                    var userRolesObj = await _authenticateService.GetUserRolesInformation(loginDTOObj);
                    var claims = new[] {//here prepare the claims array and stored into your required information to prepare the token.
                        new Claim("UserName",userRolesObj.UserName),//here map your username 
                        new Claim("EmailId", userRolesObj.EmailId),//here map your emailid  
                        new Claim("PhoneNumber", userRolesObj.PhoneNumber),//here map your PhoneNumber 
                        new Claim("Address", userRolesObj.Address),//here map your Address 
                        new Claim("IsActive", Convert.ToString(userRolesObj.IsActive)),//here map your IsActive 
                        new Claim("Roles", userRolesObj.RoleName)//here map your RoleName 
                    };
                    //this symentric security key comming from Microsoft.IdentityModel.Tokens  namespace
                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    //this SigningCredentials comming from Microsoft.IdentityModel.Tokens  namespace
                    //HmacSha256 is the encryption alogrotham.
                    //create object for SigningCredentials and pass key and algoritham.
                    SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    //this JwtSecurityToken comming from System.IdentityModel.Tokens.Jwt namespace                                
                    JwtSecurityToken token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,//pass the claims object here.
                        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpriyTime"])),//SET THE TIME FOR  YOUR TOKEN 
                        signingCredentials: signIn);
                    loginResponseObj.UserMessage = res.StatusMessage;
                    loginResponseObj.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);//pass the token object here
                    return Ok(loginResponseObj);

                }
                else
                {
                    loginResponseObj.UserMessage = res.StatusMessage;
                    loginResponseObj.AccessToken = "Userisinvalid,due to that we are not provided token,please enter correct credentials";
                }
                return Ok();
            }
        }
    }
}
