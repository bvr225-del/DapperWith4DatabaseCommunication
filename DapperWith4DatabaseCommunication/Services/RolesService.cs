using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }
        public async Task<UserSignInResponse> RolesCreation(RolesDTO rolesObj)
        {
            Roles roles = new Roles();
            roles.RoleName = rolesObj.RoleName;
            roles.IsActive = rolesObj.IsActive;
            var result = await _rolesRepository.RolesCreation(roles);
            return result;
        }
    }
}
