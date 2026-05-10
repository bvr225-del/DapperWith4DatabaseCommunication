using Dapper;
using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public AuthenticateRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var encryptText = EncryptionLibrary.EncryptText(loginDTOObj.Password);
                var p = new DynamicParameters();
                p.Add("@UserName", loginDTOObj.UserName);
                p.Add("@Password", encryptText);
                //var queryResult = await conn.QueryAsync<Hotel>(StoredProcedureStaticMessages.GetHotelDetails, CommandType.StoredProcedure);
                var result = await con.QueryAsync<UserSignInResponse>(StoredprocedureNames.SignIn, p, commandType: CommandType.StoredProcedure);
                var status = result.FirstOrDefault();
                return status;
            }
        }
        public async Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add("@UserName", loginDTOObj.UserName);
                //var queryResult = await conn.QueryAsync<Hotel>(StoredProcedureStaticMessages.GetHotelDetails, CommandType.StoredProcedure);
                var result = await con.QueryAsync<UserRolesInformationResponse>(StoredprocedureNames.GetUserRolesInformation, p, commandType: CommandType.StoredProcedure);
                var status = result.FirstOrDefault();
                return status;
            }
        }
    }
}
