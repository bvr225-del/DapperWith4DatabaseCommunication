using DapperWith4DatabaseCommunication.Dtos;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IAuthenticateRepository
    {
        Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj);
        Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj);

    }
}
