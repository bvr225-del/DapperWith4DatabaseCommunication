using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using Serilog;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class OrdersRepository : IOrdersRepository
    { 
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;

        public OrdersRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
        }
        public async Task<int> AddOrder(Orders orderdetail)
        {
            Log.Information("OrdersRepository: AddOrder method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: AddOrder method Excution Starts");//logg the message in database using custom logging factory


            using (IDbConnection con = _connectionFactory.MidLandSqlConnectionString())
            {//Create object for DynamicParameters for storedure input parameter values binding purpose used.
                var p = new DynamicParameters();//DynamicParameters comming from Dapper package
                p.Add(StoredprocedureParameters.OrderName, orderdetail.ordername);
                p.Add(StoredprocedureParameters.OrderLocation, orderdetail.orderlocation);
                p.Add(StoredprocedureParameters.OrderInsertedvariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredprocedureNames.AddOrder, p, commandType: CommandType.StoredProcedure);
                int inserterdid = p.Get<int>(StoredprocedureParameters.OrderInsertedvariable);

                Log.Information("OrdersRepository: AddOrder method Excution Ended");
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: AddOrder method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("OrdersRepository: AddOrder method Excution Ended and Insertedrecord is{@Insertedrecord}", inserterdid);
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"OrdersRepository: AddOrder method Excution Ended and Insertedrecord is {inserterdid}");//logg the message in database using custom logging factory
                return inserterdid;
            }

        }

        public async Task<string> DeleteOrderById(int orderid)
        {
            Log.Information("OrdersRepository: DeleteOrderById method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: DeleteOrderById method Excution Starts");//logg the message in database using custom logging factory

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
                    Log.Information("OrdersRepository: DeleteEmployeeById method Excution Ended");
                    await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: DeleteOrderById method Excution Ended");//logg the message in database using custom logging factory

                    Log.Information("OrdersRepository: DeleteOrderById method Excution Ended");
                    await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: DeleteOrderById method Excution Ended");//logg the message in database using custom logging factory
                    return deletedData;
                }
            }


        }

        public async Task<Orders> GetOrderById(int orderid)
        {
            Log.Information("OrdersRepository: GetOrderById method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: GetOrderById method Excution Starts");//logg the message in database using custom logging factory

            Orders order;
            using (IDbConnection con = _connectionFactory.MidLandSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.OrderId, orderid);
                var result = await con.QueryAsync<Orders>(StoredprocedureNames.GetOrderByOrderId, p, commandType: CommandType.StoredProcedure);
                order = result.FirstOrDefault();
                Log.Information("OrdersRepository: GetOrderById method Excution Ended");
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: GetOrderById method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("OrdersRepository: GetOrderById method Excution Ended");
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: GetOrderById method Excution Ended");//logg the message in database using custom logging factory

                return order;
            }

        }

        public async Task<List<Orders>> GetOrders()
        {
            Log.Information("OrdersRepository: GetOrders method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: GetOrders method Excution Starts");//logg the message in database using custom logging factory

            List<Orders> res;
            using (IDbConnection conn = _connectionFactory.MidLandSqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Orders>(StoredprocedureNames.GetOrder, CommandType.StoredProcedure);
                res = queryresult.ToList();
                Log.Information("OrdersRepository: GetOrders method Excution Ended");
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: GetOrders method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("OrdersRepository: GetOrders method Excution Ended");
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: GetOrders method Excution Ended");//logg the message in database using custom logging factory

                return res;
            }

        }

        public async Task<string> UpdateOrder(Orders orderdetail)
        {
            Log.Information("OrdersRepository: UpdateOrder method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository:UpdateOrder method Excution Starts");//logg the message in database using custom logging factory

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
                    Log.Information("OrdersRepository: UpdateOrder method Excution Ended");
                    await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: UpdateOrder method Excution Ended");//logg the message in database using custom logging factory

                    Log.Information("OrdersRepository: UpdateOrder method Excution Ended");
                    await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersRepository: UpdateOrder method Excution Ended");//logg the message in database using custom logging factory
                    return UpdatedData;
                }
            }
        }

    }
}

