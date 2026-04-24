using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public DepartmentRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory= connectionFactory;
        }
        public async Task<int> AddDepartment(Department deptdetail)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {//Create object for DynamicParameters for storedure input parameter values binding purpose used.
                var p = new DynamicParameters();//DynamicParameters comming from Dapper package
                p.Add(StoredprocedureParameters.DeptName, deptdetail.deptname);
                p.Add(StoredprocedureParameters.DeptLocation, deptdetail.deptlocation);
                p.Add(StoredprocedureParameters.DeptinsertedVariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredprocedureNames.AddDepartment, p, commandType: CommandType.StoredProcedure);
                int inserterdid = p.Get<int>(StoredprocedureParameters.DeptinsertedVariable);
                return inserterdid;
            }

        }

        public async Task<string> DeleteDepartment(int departmentid)
        {
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
                    return deletedData;
                }

            }

        }

        public async Task<Department> GetDepartmentById(int deptid)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.DeptId, deptid);
                var result = await con.QueryAsync<Department>(StoredprocedureNames.GetDepartmentByDeptId, p, commandType: CommandType.StoredProcedure);
                Department dept = result.FirstOrDefault();
                return dept;
            }

        }

        public async Task<List<Department>> GetDepartments()
        {
            using (IDbConnection conn = _connectionFactory.Northwind_DBSqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Department>(StoredprocedureNames.GetDepartment, CommandType.StoredProcedure);
                List<Department> res = queryresult.ToList();
                return res;
            }

        }

        public async Task<string> UpdateDepartment(Department deptdetail)
        {
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
                    return UpdatedData;
                }

            }

        }
    }
}
