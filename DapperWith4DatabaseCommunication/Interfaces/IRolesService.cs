using DapperWith4DatabaseCommunication.Dtos;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IRolesService
    {
        Task<UserSignInResponse> RolesCreation(RolesDTO rolesObj);

    }
}
