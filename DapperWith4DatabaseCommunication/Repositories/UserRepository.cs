using Dapper;
using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<UserSignInResponse> UserResgistration(Users usersObj)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var encryptText = EncryptionLibrary.EncryptText(usersObj.Password);
                var p = new DynamicParameters();
                p.Add("@UserName", usersObj.UserName);
                p.Add("@Password", encryptText);//here pass the encrypted string to store in database.password is secure
                p.Add("@EmailId", usersObj.EmailId);
                p.Add("@PhoneNumber", usersObj.PhoneNumber);
                p.Add("@Address", usersObj.Address);
                p.Add("@IsActive", usersObj.IsActive);
                var result = await con.QuerySingleAsync<UserSignInResponse>(StoredprocedureNames.Usp_UserResgistration, p, commandType: CommandType.StoredProcedure);
                return result;

            }
        }
    }
}
