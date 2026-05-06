using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Repositories;
using Serilog;

namespace DapperWith4DatabaseCommunication.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILoggingFactory _loggingFactory;

        public RestaurantService(IRestaurantRepository restaurantRepository, ILoggingFactory loggingFactory)
        {
            _restaurantRepository = restaurantRepository;
            _loggingFactory = loggingFactory;   
        }
        public async Task<int> AddRestaurant(RestaurantDto restaurantdetail)
        {
            Log.Information("RestaurantService: AddRestaurant method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: AddRestaurant method Excution Starts");//logg the message in database using custom logging factory

            Restaurant restaurant = new Restaurant();
            restaurant.Id = restaurantdetail.Id;
            if (restaurantdetail?.Flag == "Vizag")//Here Flag is used to apply the conditions.based on condition we are perming the opertions.
            {
                restaurant.RestaurantName = restaurantdetail.RestaurantName + '-' + restaurantdetail.RestaurantLocation;
            }
            else
            {//if you are not using the flag then you can directly assign the value to ordername without any condition as shown below.
                restaurant.RestaurantName = restaurantdetail.RestaurantName;
            }

            restaurant.RestaurantLocation = restaurantdetail.RestaurantLocation;
            //to pass the data to repository we are not pass the falg value,falg is used to check the condition purpose only
            var res = await _restaurantRepository.AddRestaurant(restaurant);
            Log.Information("RestaurantService: AddRestaurant method Excution Ended");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: AddRestaurant method Excution Ended");//logg the message in database using custom logging factory

            return res;


        }

        public async Task<string> DeleteRestaurantById(int restaurantid)
        {
            Log.Information("RestaurantService: DeleteRestaurantById method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: DeleteRestaurantById method Excution Starts");//logg the message in database using custom logging factory

            var res = await _restaurantRepository.DeleteRestaurantById(restaurantid);
            Log.Information("RestaurantService: DeleteRestaurantById method Excution Ended");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: DeleteRestaurantById method Excution Ended");//logg the message in database using custom logging factory
            return res;

        }

        public async Task<RestaurantDto> GetRestaurantById(int restaurantid)
        {
            Log.Information("RestaurantService: GetRestaurantById method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: GetRestaurantById method Excution Starts");//logg the message in database using custom logging factory

            var res = await _restaurantRepository.GetRestaurantById(restaurantid);
            RestaurantDto restaurantdto = new RestaurantDto();
            restaurantdto.Id = res.Id;
            restaurantdto.RestaurantName = res.RestaurantName;
            restaurantdto.RestaurantLocation = res.RestaurantLocation;
            Log.Information("RestaurantService: GetRestaurantById method Excution Ended");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: GetRestaurantById method Excution Ended");//logg the message in database using custom logging factory

            return restaurantdto;

        }

        public async Task<List<RestaurantDto>> GetRestaurants()
        {
            Log.Information("RestaurantService: GetRestaurants method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: GetRestaurants method Excution Starts");//logg the message in database using custom logging factory

            List<RestaurantDto> lstrestaurantdto = new List<RestaurantDto>();
            var res = await _restaurantRepository.GetRestaurants();
            foreach (Restaurant restaurant in res)
            {
                RestaurantDto restaurantdto = new RestaurantDto();
                restaurantdto.Id = restaurant.Id;
                restaurantdto.RestaurantName = restaurant.RestaurantName;
                restaurantdto.RestaurantLocation = restaurant.RestaurantLocation;
                lstrestaurantdto.Add(restaurantdto);//Add the restaurant to list here
                Log.Information("RestaurantService: GetRestaurants method Excution Ended");
                await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: GetRestaurants method Excution Ended");//logg the message in database using custom logging factory


            }
            return lstrestaurantdto;
        }

        public async Task<string> UpdateRestaurant(RestaurantDto restaurantdetail)
        {
            Log.Information("RestaurantService: UpdateRestaurant method Excution Starts");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService:UpdateRestaurant method Excution Starts");//logg the message in database using custom logging factory

            Restaurant obj = new Restaurant();
            obj.Id = restaurantdetail.Id;
            obj.RestaurantName = restaurantdetail.RestaurantName;
            obj.RestaurantLocation = restaurantdetail.RestaurantLocation;
            var res = await _restaurantRepository.UpdateRestaurant(obj);
            Log.Information("RestaurantService:UpdateRestaurant method Excution Ended");
            await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantService: UpdateRestaurant method Excution Ended");//logg the message in database using custom logging factory

            return res;

        }
    }
}
