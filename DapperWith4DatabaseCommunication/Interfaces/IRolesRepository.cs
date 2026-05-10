using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Models;
using System.Data;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IRolesRepository
    {
        Task<UserSignInResponse> RolesCreation(Roles rolesObj);

    }
}
