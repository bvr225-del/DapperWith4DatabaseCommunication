using DapperWith4DatabaseCommunication.Dtos;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<RestaurantDto>> GetRestaurants();
        Task<RestaurantDto> GetRestaurantById(int restaurantid);
        Task<int> AddRestaurant(RestaurantDto restaurantdetail);
        Task<string> DeleteRestaurantById(int restaurantid);
        Task<string> UpdateRestaurant(RestaurantDto restaurantdetail);
    }
}
