using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IRestaurantRepository
    {
            Task<List<Restaurant>> GetRestaurants();
            Task<Restaurant> GetRestaurantById(int restaurantid);
            Task<int> AddRestaurant(Restaurant restaurantdetail);
            Task<string> DeleteRestaurantById(int restaurantid);
            Task<string> UpdateRestaurant(Restaurant restaurantdetail);
    }
}
