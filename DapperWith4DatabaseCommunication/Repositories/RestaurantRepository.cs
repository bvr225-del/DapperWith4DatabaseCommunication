using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RestaurantRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddRestaurant(Restaurant restaurantdetail)
        {
            using (IDbConnection con = _connectionFactory.RestaurantDBSqlConnectionString())
            {//Create object for DynamicParameters for storedure input parameter values binding purpose used.
                var p = new DynamicParameters();//DynamicParameters comming from Dapper package
                p.Add(StoredprocedureParameters.RestaurantName, restaurantdetail.RestaurantName);
                p.Add(StoredprocedureParameters.RestaurantLocation, restaurantdetail.RestaurantLocation);
                p.Add(StoredprocedureParameters.RestaurantInsertedvariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredprocedureNames.AddRestaurant, p, commandType: CommandType.StoredProcedure);
                int inserterdid = p.Get<int>(StoredprocedureParameters.RestaurantInsertedvariable);
                return inserterdid;
            }

        }

        public async Task<string> DeleteRestaurantById(int restaurantid)
        {
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
                    return deletedData;
                }
            }


        }

        public async Task<Restaurant> GetRestaurantById(int restaurantid)
        {
            Restaurant restaurant;
            using (IDbConnection con = _connectionFactory.RestaurantDBSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.ID, restaurantid);
                var result = await con.QueryAsync<Restaurant>(StoredprocedureNames.GetRestaurantById, p, commandType: CommandType.StoredProcedure);
                restaurant = result.FirstOrDefault();
                return restaurant;
            }

        }

        public async Task<List<Restaurant>> GetRestaurants()
        {
            List<Restaurant> res;
            using (IDbConnection conn = _connectionFactory.RestaurantDBSqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Restaurant>(StoredprocedureNames.GetRestaurant, CommandType.StoredProcedure);
                res = queryresult.ToList();
                return res;
            }

        }

        public async Task<string> UpdateRestaurant(Restaurant restaurantdetail)
        {
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
                    return UpdatedData;
                }
            }

        }
    }
}
