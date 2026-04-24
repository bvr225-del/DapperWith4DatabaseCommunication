using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        public async  Task<int> AddOrder(OrdersDto orderdetail)
        {
            Orders order = new Orders();
            order.orderid = orderdetail.orderid;
            if (orderdetail?.Flag == "Hyderabad")//Here Flag is used to apply the conditions.based on condition we are perming the opertions.
            {
                order.ordername = orderdetail.ordername + '-' + orderdetail.orderlocation;
            }
            else
            {//if you are not using the flag then you can directly assign the value to ordername without any condition as shown below.
                order.ordername = orderdetail.ordername;
            }

            order.orderlocation = orderdetail.orderlocation;
            //to pass the data to repository we are not pass the falg value,falg is used to check the condition purpose only
            var res = await _ordersRepository.AddOrder(order);
            return res;

        }

        public async Task<string> DeleteOrderById(int orderid)
        {
            var res = await _ordersRepository.DeleteOrderById(orderid);
            return res;

        }

        public async Task<OrdersDto> GetOrderById(int orderid)
        {
            var res = await _ordersRepository.GetOrderById(orderid);
            OrdersDto orderdto = new OrdersDto();
            orderdto.orderid = res.orderid;
            orderdto.ordername = res.ordername;
            orderdto.orderlocation = res.orderlocation;
            return orderdto;

        }

        public async Task<List<OrdersDto>> GetOrders()
        {
            List<OrdersDto> lstorderdto = new List<OrdersDto>();
            var res = await _ordersRepository.GetOrders();
            foreach (Orders order in res)
            {
                OrdersDto ordersDto = new OrdersDto();
                ordersDto.orderid = order.orderid;
                ordersDto.ordername = order.ordername;
                ordersDto.orderlocation = order.orderlocation;
                lstorderdto.Add(ordersDto);//Add the orders to list here

            }
            return lstorderdto;

        }

        public async Task<string> UpdateOrder(OrdersDto orderdetail)
        {
            Orders obj = new Orders();
            obj.orderid = orderdetail.orderid;
            obj.ordername = orderdetail.ordername;
            obj.orderlocation = orderdetail.orderlocation;
            var res = await _ordersRepository.UpdateOrder(obj);
            return res;

        }
    }
}
