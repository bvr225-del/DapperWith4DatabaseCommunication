using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IUserRepository
    {
        Task<UserSignInResponse> UserResgistration(Users usersObj);
        Task<UserSignInResponse> UserRolesMapping(UserRole userRoleObj);


    }
}
