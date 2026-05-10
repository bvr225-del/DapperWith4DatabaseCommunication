using Dapper;
using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RolesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<UserSignInResponse> RolesCreation(Roles rolesObj)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add("@RoleName", rolesObj.RoleName);
                p.Add("@IsActive", rolesObj.IsActive);
                var result = await con.QuerySingleAsync<UserSignInResponse>(StoredprocedureNames.Usp_RolesResgistration, p, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
