using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using Serilog;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;

        public DepartmentRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory    )
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
        }
        public async Task<int> AddDepartment(Department deptdetail)
        {
            Log.Information("DepartmentRepository:AddDepartment method Excution Starts");
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: AddDepartment  method Excution Starts");//logg the message in database using custom logging factory

            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {//Create object for DynamicParameters for storedure input parameter values binding purpose used.
                var p = new DynamicParameters();//DynamicParameters comming from Dapper package
                p.Add(StoredprocedureParameters.DeptName, deptdetail.deptname);
                p.Add(StoredprocedureParameters.DeptLocation, deptdetail.deptlocation);
                p.Add(StoredprocedureParameters.DeptinsertedVariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredprocedureNames.AddDepartment, p, commandType: CommandType.StoredProcedure);
                int inserterdid = p.Get<int>(StoredprocedureParameters.DeptinsertedVariable);
                Log.Information("DepartmentRepository: AddDepartment method Excution Ended");
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: AddDepartment  method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("DepartmentRepository: AddDepartment  method Excution Ended and Insertedrecord is{@Insertedrecord}", inserterdid);
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", $"DepartmentRepository: AddDepartment  method Excution Ended and Insertedrecord is {inserterdid}");//logg the message in database using custom logging factory

                return inserterdid;
            }

        }

        public async Task<string> DeleteDepartment(int departmentid)
        {
            Log.Information("DepartmentRepository: DeleteDepartment method Excution Starts");
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: DeleteDepartment method Excution Starts");//logg the message in database using custom logging factory

            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {//first featch the data based on id and then delete the data based on id and return the deleted data as string format
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.DeptId, departmentid);
                var result = await con.QueryAsync<Department>(StoredprocedureNames.GetDepartmentByDeptId, p, commandType: CommandType.StoredProcedure);
                Department dept = result.FirstOrDefault();
                if (dept == null)
                {
                    return $"Department with id {departmentid} not found.";
                }
                else
                {
                    var deletedData = $"Deleted Department: ID={dept.deptid}, Name={dept.deptname}, Location={dept.deptlocation}";

                    await con.ExecuteScalarAsync(StoredprocedureNames.DeleteDepartment, p, commandType: CommandType.StoredProcedure);
                    Log.Information("DepartmentRepository: DeleteDepartment method Excution Ended");
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: DeleteDepartment method Excution Ended");//logg the message in database using custom logging factory

                    Log.Information("DepartmentRepository: DeleteDepartment method Excution Ended");
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: DeleteDepartment method Excution Ended");//logg the message in database using custom logging factory
                    return deletedData;
                }

            }

        }

        public async Task<Department> GetDepartmentById(int deptid)
        {
            Log.Information("DepartmentRepository: GetDepartmentById method Excution Starts");
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: GetDepartmentById method Excution Starts");//logg the message in database using custom logging factory

            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.DeptId, deptid);
                var result = await con.QueryAsync<Department>(StoredprocedureNames.GetDepartmentByDeptId, p, commandType: CommandType.StoredProcedure);
                Department dept = result.FirstOrDefault();
                Log.Information("DepartmentRepository: GetDepartmentById method Excution Ended");
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: GetDepartmentById method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("DepartmentRepository:GetDepartmentById method Excution Ended");
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: GetDepartmentById method Excution Ended");//logg the message in database using custom logging factory

                return dept;
            }

        }

        public async Task<List<Department>> GetDepartments()
        {
            Log.Information("DepartmentRepository: GetDepartments method Excution Starts");
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: GetDepartments method Excution Starts");//logg the message in database using custom logging factory

            using (IDbConnection conn = _connectionFactory.Northwind_DBSqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Department>(StoredprocedureNames.GetDepartment, CommandType.StoredProcedure);
                List<Department> res = queryresult.ToList();
                Log.Information("DepartmentRepository: GetDepartments method Excution Ended");
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: GetDepartments method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("DepartmentRepository:GetDepartments method Excution Ended");
                await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: GetDepartments method Excution Ended");//logg the message in database using custom logging factory

                return res;
            }

        }

        public async Task<string> UpdateDepartment(Department deptdetail)
        {
            Log.Information("DepartmentRepository: UpdateDepartment method Excution Starts");
            await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: UpdateDepartment method Excution Starts");//logg the message in database using custom logging factory

            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {//first featch the data based on id and then delete the data based on id and return the deleted data as string format
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.DeptId, deptdetail.deptid);
                var result = await con.QueryAsync<Department>(StoredprocedureNames.GetDepartmentByDeptId, p, commandType: CommandType.StoredProcedure);
                Department dept = result.FirstOrDefault();
                if (dept == null)
                {
                    return $"Department with id {deptdetail.deptid} not found.";
                }
                else
                {
                    var UpdatedData = $"Updated Department: ID={deptdetail.deptid}, Name={deptdetail.deptname}, Location={deptdetail.deptlocation}";
                    var up = new DynamicParameters();
                    up.Add("@deptid", deptdetail.deptid);
                    up.Add("@deptname", deptdetail.deptname);
                    up.Add("@deptlocation", deptdetail.deptlocation);
                    await con.ExecuteScalarAsync(StoredprocedureNames.UpdateDepartment, up, commandType: CommandType.StoredProcedure);
                    Log.Information("DepartmentRepository: UpdateDepartment method Excution Ended");
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: UpdateDepartment method Excution Ended");//logg the message in database using custom logging factory

                    Log.Information("DepartmentRepository:UpdateDepartment method Excution Ended");
                    await _loggingFactory.Add_DepartmentLoggingMessages("venkat", "Information", "DepartmentRepository: UpdateDepartment method Excution Ended");//logg the message in database using custom logging factory

                    return UpdatedData;
                }

            }

        }
    }
}
