using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class OrdersRepository : IOrdersRepository
    { 
        private readonly IConnectionFactory _connectionFactory;
        public OrdersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddOrder(Orders orderdetail)
        {
            using (IDbConnection con = _connectionFactory.MidLandSqlConnectionString())
            {//Create object for DynamicParameters for storedure input parameter values binding purpose used.
                var p = new DynamicParameters();//DynamicParameters comming from Dapper package
                p.Add(StoredprocedureParameters.OrderName, orderdetail.ordername);
                p.Add(StoredprocedureParameters.OrderLocation, orderdetail.orderlocation);
                p.Add(StoredprocedureParameters.OrderInsertedvariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredprocedureNames.AddOrder, p, commandType: CommandType.StoredProcedure);
                int inserterdid = p.Get<int>(StoredprocedureParameters.OrderInsertedvariable);
                return inserterdid;
            }

        }

        public async Task<string> DeleteOrderById(int orderid)
        {
            using (IDbConnection con = _connectionFactory.MidLandSqlConnectionString())
            {
                //first featch the data based on id and then delete the data based on id and return the deleted data as string format
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.OrderId, orderid);
                var result = await con.QueryAsync<Orders>(StoredprocedureNames.GetOrderByOrderId, p, commandType: CommandType.StoredProcedure);
                Orders order = result.FirstOrDefault();
                if (order == null)
                {
                    return $"order with id {orderid} not found.";
                }
                else
                {
                    var deletedData = $"Deleted Order: ID={order.orderid}, Name={order.ordername}, Location={order.orderlocation}";

                    await con.ExecuteScalarAsync(StoredprocedureNames.DeleteOrder, p, commandType: CommandType.StoredProcedure);
                    return deletedData;
                }
            }


        }

        public async Task<Orders> GetOrderById(int orderid)
        {
            Orders order;
            using (IDbConnection con = _connectionFactory.MidLandSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.OrderId, orderid);
                var result = await con.QueryAsync<Orders>(StoredprocedureNames.GetOrderByOrderId, p, commandType: CommandType.StoredProcedure);
                order = result.FirstOrDefault();
                return order;
            }

        }

        public async Task<List<Orders>> GetOrders()
        {
            List<Orders> res;
            using (IDbConnection conn = _connectionFactory.MidLandSqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Orders>(StoredprocedureNames.GetOrder, CommandType.StoredProcedure);
                res = queryresult.ToList();
                return res;
            }

        }

        public async Task<string> UpdateOrder(Orders orderdetail)
        {
            using (IDbConnection con = _connectionFactory.MidLandSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.OrderId, orderdetail.orderid);
                var result = await con.QueryAsync<Orders>(StoredprocedureNames.GetOrderByOrderId, p, commandType: CommandType.StoredProcedure);
                Orders order = result.FirstOrDefault();
                if (order == null)
                {
                    return $"order with id {orderdetail.orderid} not found.";
                }
                else
                {
                    var UpdatedData = $"Updated Order: ID={orderdetail.orderid}, Name={orderdetail.ordername}, Location={orderdetail.orderlocation}";
                    var pu = new DynamicParameters();
                    pu.Add(StoredprocedureParameters.OrderId, orderdetail.orderid);
                    pu.Add(StoredprocedureParameters.OrderName, orderdetail.ordername);
                    pu.Add(StoredprocedureParameters.OrderLocation, orderdetail.orderlocation);
                    await con.ExecuteReaderAsync(StoredprocedureNames.UpdateOrder, pu, commandType: CommandType.StoredProcedure);
                    return UpdatedData;
                }
            }
        }

    }
}

