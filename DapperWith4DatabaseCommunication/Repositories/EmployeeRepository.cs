using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public EmployeeRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory= connectionFactory;
        }
        public async Task<int> AddEmployees(Employee empdetail)
        {
            using(IDbConnection con=_connectionFactory.HotelmanagementsqlConnectionString())
            {
                //In Dapper we will use the DynamicParameters class to pass data to the stored procedure input parameters.
                //The DynamicParameters class allows us to define parameters and their values in a flexible way, making it easier to work with stored procedures that require multiple parameters or output parameters.
                //Create object for DynamicParameters class  for Passing  data to Storedprocedure input paramaters..
                //The first argument is the name of the parameter as defined in the stored procedure, and the second argument is the value you want to pass to that parameter.
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredprocedureParameters.EmployeeName, empdetail.empname);
                parameters.Add(StoredprocedureParameters.EmployeeSalary, empdetail.empsalary);
                parameters.Add(StoredprocedureParameters.Insertedvariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredprocedureNames.AddEmployee, parameters, commandType: CommandType.StoredProcedure);
                int inserterdid = parameters.Get<int>(StoredprocedureParameters.Insertedvariable);
                return inserterdid;

            }

        }

        public async Task<bool> DeleteEmployeeById(int empid)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredprocedureParameters.EmployeeID, empid);
                await con.ExecuteScalarAsync(StoredprocedureNames.DeleteEmployee, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }

        public async Task<Employee> GetEmployeeById(int empid)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.EmployeeID, empid);
                var result = await con.QueryAsync<Employee>(StoredprocedureNames.GetEmployeeByEmpid, p, commandType: CommandType.StoredProcedure);
                Employee emp = result.FirstOrDefault();//FirstOrDefault() it will return the first element of the sequence or a default value if the sequence contains no elements. In this case, it will return the first Employee object from the result set or null if there are no matching records.
                return emp;
            }

        }

        public async Task<List<Employee>> GetEmployees()
        {
            using (IDbConnection conn = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Employee>(StoredprocedureNames.GetEmployee, CommandType.StoredProcedure);
                List<Employee> res = queryresult.ToList();
                return res;
            }

        }

        public async Task<bool> UpdateEmployee(Employee empdetail)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.EmployeeID, empdetail.empid);
                p.Add(StoredprocedureParameters.EmployeeName, empdetail.empname);
                p.Add(StoredprocedureParameters.EmployeeSalary, empdetail.empsalary);
                await con.ExecuteReaderAsync(StoredprocedureNames.UpdateEmployee, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }
    }
}
