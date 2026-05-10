using DapperWith4DatabaseCommunication.Dtos;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IAuthenticateService
    {
        Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj);
        Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj);

    }
}
