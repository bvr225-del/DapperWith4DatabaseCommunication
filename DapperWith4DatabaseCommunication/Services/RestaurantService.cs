using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Repositories;

namespace DapperWith4DatabaseCommunication.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        public async Task<int> AddRestaurant(RestaurantDto restaurantdetail)
        {
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
            return res;


        }

        public async Task<string> DeleteRestaurantById(int restaurantid)
        {
            var res = await _restaurantRepository.DeleteRestaurantById(restaurantid);
            return res;

        }

        public async Task<RestaurantDto> GetRestaurantById(int restaurantid)
        {
            var res = await _restaurantRepository.GetRestaurantById(restaurantid);
            RestaurantDto restaurantdto = new RestaurantDto();
            restaurantdto.Id = res.Id;
            restaurantdto.RestaurantName = res.RestaurantName;
            restaurantdto.RestaurantLocation = res.RestaurantLocation;
            return restaurantdto;

        }

        public async Task<List<RestaurantDto>> GetRestaurants()
        {
            List<RestaurantDto> lstrestaurantdto = new List<RestaurantDto>();
            var res = await _restaurantRepository.GetRestaurants();
            foreach (Restaurant restaurant in res)
            {
                RestaurantDto restaurantdto = new RestaurantDto();
                restaurantdto.Id = restaurant.Id;
                restaurantdto.RestaurantName = restaurant.RestaurantName;
                restaurantdto.RestaurantLocation = restaurant.RestaurantLocation;
                lstrestaurantdto.Add(restaurantdto);//Add the restaurant to list here

            }
            return lstrestaurantdto;
        }

        public async Task<string> UpdateRestaurant(RestaurantDto restaurantdetail)
        {
            Restaurant obj = new Restaurant();
            obj.Id = restaurantdetail.Id;
            obj.RestaurantName = restaurantdetail.RestaurantName;
            obj.RestaurantLocation = restaurantdetail.RestaurantLocation;
            var res = await _restaurantRepository.UpdateRestaurant(obj);
            return res;

        }
    }
}
