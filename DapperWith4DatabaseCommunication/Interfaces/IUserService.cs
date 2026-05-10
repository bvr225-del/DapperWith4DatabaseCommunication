using DapperWith4DatabaseCommunication.Dtos;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IUserService
    {
        Task<UserSignInResponse> UserResgistration(UsersDTO usersObj);
        Task<UserSignInResponse> UserRolesMapping(UserRoleDTO userRoleDTOObj);


    }
}
