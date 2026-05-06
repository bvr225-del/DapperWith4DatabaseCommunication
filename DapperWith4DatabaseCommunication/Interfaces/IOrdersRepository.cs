using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IOrdersRepository
    {
        Task<List<Orders>> GetOrders();
        Task<Orders> GetOrderById(int orderid);
        Task<int> AddOrder(Orders orderdetail);
        Task<string> DeleteOrderById(int orderid);
        Task<string> UpdateOrder(Orders orderdetail);

    }
}
