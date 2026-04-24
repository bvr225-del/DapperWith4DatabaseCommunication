using DapperWith4DatabaseCommunication.Dtos;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IOrdersService
    {
        Task<List<OrdersDto>> GetOrders();
        Task<OrdersDto> GetOrderById(int orderid);
        Task<int> AddOrder(OrdersDto orderdetail);
        Task<string> DeleteOrderById(int orderid);
        Task<string> UpdateOrder(OrdersDto orderdetail);

    }
}
