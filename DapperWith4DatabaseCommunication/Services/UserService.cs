using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserSignInResponse> UserResgistration(UsersDTO usersObj)
        {
            Users users = new Users();
            users.Id = usersObj.Id;
            users.UserName = usersObj.UserName;
            users.Password = usersObj.Password;
            users.EmailId = usersObj.EmailId;
            users.Address = usersObj.Address;
            users.PhoneNumber = usersObj.PhoneNumber;
            users.IsActive = usersObj.IsActive;
            var result = await _userRepository.UserResgistration(users);
            return result;
        }
        public async Task<UserSignInResponse> UserRolesMapping(UserRoleDTO userRoleDTOObj)
        {
            UserRole userRole = new UserRole();
            userRole.UserId = userRoleDTOObj.UserId;
            userRole.RoleId = userRoleDTOObj.RoleId;
            return await _userRepository.UserRolesMapping(userRole);
        }

    }
}
