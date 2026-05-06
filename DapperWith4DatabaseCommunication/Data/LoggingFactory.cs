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

        public async Task<bool> Add_OrderLoggingMessages(string userName, string logLevel, string messageTemplate)
        {
            using (IDbConnection con = _connectionFactory.MidLandSqlConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredprocedureParameters.Logging_UserName, userName);
                p.Add(StoredprocedureParameters.Logging_LogLevel, logLevel);
                p.Add(StoredprocedureParameters.Logging_MessageTemplate, messageTemplate);
                await con.ExecuteScalarAsync(StoredprocedureNames.AddLoggingMessages, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }
        public async Task<bool> Add_DepartmentLoggingMessages(string userName, string logLevel, string messageTemplate)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredprocedureParameters.Logging_UserName, userName);
                p.Add(StoredprocedureParameters.Logging_LogLevel, logLevel);
                p.Add(StoredprocedureParameters.Logging_MessageTemplate, messageTemplate);
                await con.ExecuteScalarAsync(StoredprocedureNames.AddLoggingMessages, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }
        public async Task<bool> Add_RestaurantLoggingMessages(string userName, string logLevel, string messageTemplate)
        {
            using (IDbConnection con = _connectionFactory.RestaurantDBSqlConnectionString())
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
