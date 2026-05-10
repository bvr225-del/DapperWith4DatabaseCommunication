using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;

namespace DapperWith4DatabaseCommunication.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepository _repository;
        public AuthenticateService(IAuthenticateRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj)
        {
            var res = await _repository.UserSignIn(loginDTOObj);
            return res;
        }
        public async Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj)
        {
            var res = await _repository.GetUserRolesInformation(loginDTOObj);
            return res;
        }
    }
}
