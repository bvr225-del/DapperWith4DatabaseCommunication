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
        public async Task<bool> AddProjectLevelErrorlogAsync(string statusCode, string ErrorMessage, string StackTraceError, string InnerExceptionError, string username)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                //DynamicParameters used in dapper,to pass the values to storedprocedure parameters.
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredprocedureParameters.ErrorLog_StatusCode, statusCode);
                p.Add(StoredprocedureParameters.ErrorLog_ErrorMessage, ErrorMessage);
                p.Add(StoredprocedureParameters.ErrorLog_StackTraceError, StackTraceError);
                p.Add(StoredprocedureParameters.ErrorLog_InnerExceptionError, InnerExceptionError);
                p.Add(StoredprocedureParameters.ErrorLog_UserName, username);//Here pass the username to Storedprocedure.
                await con.ExecuteScalarAsync(StoredprocedureNames.AddProjectLevelErrorlog, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }
    }
}



    
