using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Data
{
    public class LoggingFactory : ILoggingFactory
    {
        private readonly IConnectionFactory _connectionFactory;

        public LoggingFactory(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> AddLoggingMessages(string userName, string logLevel, string messageTemplate)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredprocedureParameters.Logging_UserName, userName);
                p.Add(StoredprocedureParameters.Logging_LogLevel, logLevel);
                p.Add(StoredprocedureParameters.Logging_MessageTemplate, messageTemplate);
                await con.ExecuteScalarAsync(StoredprocedureNames.AddLoggingMessages, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }


    }
}
