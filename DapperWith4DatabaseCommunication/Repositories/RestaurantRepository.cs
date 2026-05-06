using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using Serilog;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;

        public RestaurantRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
        }
        public async Task<int> AddRestaurant(Restaurant restaurantdetail)
        {
            Log.Information("RestaurantRepository: AddRestaurant method Excution Starts");
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: AddRestaurant method Excution Starts");//logg the message in database using custom logging factory

            using (IDbConnection con = _connectionFactory.RestaurantDBSqlConnectionString())
            {//Create object for DynamicParameters for storedure input parameter values binding purpose used.
                var p = new DynamicParameters();//DynamicParameters comming from Dapper package
                p.Add(StoredprocedureParameters.RestaurantName, restaurantdetail.RestaurantName);
                p.Add(StoredprocedureParameters.RestaurantLocation, restaurantdetail.RestaurantLocation);
                p.Add(StoredprocedureParameters.RestaurantInsertedvariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredprocedureNames.AddRestaurant, p, commandType: CommandType.StoredProcedure);
                int inserterdid = p.Get<int>(StoredprocedureParameters.RestaurantInsertedvariable);
                Log.Information("RestaurantRepository: AddRestaurant method Excution Ended");
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: AddRestaurant method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("RestaurantRepository: AddRestaurant method Excution Ended and Insertedrecord is{@Insertedrecord}", inserterdid);
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"RestaurantRepository: AddRestaurant method Excution Ended and Insertedrecord is {inserterdid}");//logg the message in database using custom logging factory
                return inserterdid;
            }

        }

        public async Task<string> DeleteRestaurantById(int restaurantid)
        {
            Log.Information("RestaurantRepository: DeleteRestaurantById method Excution Starts");
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: DeleteRestaurantById  method Excution Starts");//logg the message in database using custom logging factory

            using (IDbConnection con = _connectionFactory.RestaurantDBSqlConnectionString())
            {
                //first featch the data based on id and then delete the data based on id and return the deleted data as string format
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.ID, restaurantid);
                var result = await con.QueryAsync<Restaurant>(StoredprocedureNames.GetRestaurantById, p, commandType: CommandType.StoredProcedure);
                Restaurant restaurant = result.FirstOrDefault();
                if (restaurant == null)
                {
                    return $"restaurant with id {restaurantid} not found.";
                }
                else
                {
                    var deletedData = $"Deleted Restaurant: ID={restaurant.Id}, Name={restaurant.RestaurantName}, Location={restaurant.RestaurantLocation}";
                    await con.ExecuteScalarAsync(StoredprocedureNames.DeleteRestaurant, p, commandType: CommandType.StoredProcedure);
                    Log.Information("RestaurantRepository: DeleteRestaurantById method Excution Ended");
                    await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: DeleteRestaurantById method Excution Ended");//logg the message in database using custom logging factory

                    Log.Information("RestaurantRepository: DeleteRestaurantById method Excution Ended");
                    await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: DeleteRestaurantById method Excution Ended");//logg the message in database using custom logging factory

                    return deletedData;
                }
            }


        }

        public async Task<Restaurant> GetRestaurantById(int restaurantid)
        {
            Log.Information("RestaurantRepository: GetRestaurantById method Excution Starts");
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: GetRestaurantById  method Excution Starts");//logg the message in database using custom logging factory

            Restaurant restaurant;
            using (IDbConnection con = _connectionFactory.RestaurantDBSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.ID, restaurantid);
                var result = await con.QueryAsync<Restaurant>(StoredprocedureNames.GetRestaurantById, p, commandType: CommandType.StoredProcedure);
                restaurant = result.FirstOrDefault();
                Log.Information("RestaurantRepository: GetRestaurantById method Excution Ended");
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: GetRestaurantById method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("RestaurantRepository: GetRestaurantById method Excution Ended");
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: GetRestaurantById method Excution Ended");//logg the message in database using custom logging factory
                return restaurant;
            }

        }

        public async Task<List<Restaurant>> GetRestaurants()
        {
            Log.Information("RestaurantRepository: GetRestaurants method Excution Starts");
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: GetRestaurants  method Excution Starts");//logg the message in database using custom logging factory

            List<Restaurant> res;
            using (IDbConnection conn = _connectionFactory.RestaurantDBSqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Restaurant>(StoredprocedureNames.GetRestaurant, CommandType.StoredProcedure);
                res = queryresult.ToList();
                Log.Information("RestaurantRepository: GetRestaurants method Excution Ended");
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: GetRestaurants method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("RestaurantRepository: GetRestaurants method Excution Ended");
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: GetRestaurants method Excution Ended");//logg the message in database using custom logging factory

                return res;
            }

        }

        public async Task<string> UpdateRestaurant(Restaurant restaurantdetail)
        {
            Log.Information("RestaurantRepository: UpdateRestaurant method Excution Starts");
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository:UpdateRestaurant  method Excution Starts");//logg the message in database using custom logging factory

            using (IDbConnection con = _connectionFactory.RestaurantDBSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.ID, restaurantdetail.Id);
                var result = await con.QueryAsync<Restaurant>(StoredprocedureNames.GetRestaurantById, p, commandType: CommandType.StoredProcedure);
                Restaurant restaurant = result.FirstOrDefault();
                if (restaurant == null)
                {
                    return $"restaurant with id {restaurantdetail.Id} not found.";
                }
                else
                {
                    var UpdatedData = $"Updated Restaurant: ID={restaurantdetail.Id}, Name={restaurantdetail.RestaurantName}, Location={restaurantdetail.RestaurantLocation}";
                    var pu = new DynamicParameters();
                    pu.Add(StoredprocedureParameters.ID, restaurantdetail.Id);
                    pu.Add(StoredprocedureParameters.RestaurantName, restaurantdetail.RestaurantName);
                    pu.Add(StoredprocedureParameters.RestaurantLocation, restaurantdetail.RestaurantLocation);
                    await con.ExecuteReaderAsync(StoredprocedureNames.UpdateRestaurant, pu, commandType: CommandType.StoredProcedure);
                    Log.Information("RestaurantRepository: UpdateRestaurant method Excution Ended");
                    await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: UpdateRestaurant method Excution Ended");//logg the message in database using custom logging factory

                    Log.Information("RestaurantRepository: UpdateRestaurant method Excution Ended");
                    await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantRepository: UpdateRestaurant method Excution Ended");//logg the message in database using custom logging factory

                    return UpdatedData;
                }
            }

        }
    }
}
