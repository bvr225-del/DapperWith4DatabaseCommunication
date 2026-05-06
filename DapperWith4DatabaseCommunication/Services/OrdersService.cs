using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using Serilog;

namespace DapperWith4DatabaseCommunication.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ILoggingFactory _loggingFactory;

        public OrdersService(IOrdersRepository ordersRepository, ILoggingFactory loggingFactory)
        {
            _ordersRepository = ordersRepository;
            _loggingFactory = loggingFactory;
        }
        public async  Task<int> AddOrder(OrdersDto orderdetail)
        {
            Log.Information("OrdersService: AddOrder method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersService: AddOrder method Excution Starts");//logg the message in database using custom logging factory

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
            Log.Information("OrdersService: AddOrder method Excution Ended");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersService: AddOrder method Excution Ended");//logg the message in database using custom logging factory
            return res;

        }

        public async Task<string> DeleteOrderById(int orderid)
        {
            Log.Information("OrdersService: DeleteOrderById method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersService: DeleteOrderById method Excution Starts");//logg the message in database using custom logging factory

            var res = await _ordersRepository.DeleteOrderById(orderid);
            Log.Information("OrdersServices: DeleteOrderById method Excution Ended");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersServices: DeleteOrderById method Excution Ended");//logg the message in database using custom logging factory

            return res;

        }

        public async Task<OrdersDto> GetOrderById(int orderid)
        {
            Log.Information("OrdersService: GetOrderById method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersService: GetOrderById method Excution Starts");//logg the message in database using custom logging factory

            var res = await _ordersRepository.GetOrderById(orderid);
            OrdersDto orderdto = new OrdersDto();
            orderdto.orderid = res.orderid;
            orderdto.ordername = res.ordername;
            orderdto.orderlocation = res.orderlocation;
            Log.Information("OrdersServices: GetOrderById method Excution Ended");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersServices: GetOrderById method Excution Ended");//logg the message in database using custom logging factory

            return orderdto;

        }

        public async Task<List<OrdersDto>> GetOrders()
        {
            Log.Information("OrdersService: GetOrders method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersService: GetOrders method Excution Starts");//logg the message in database using custom logging factory

            List<OrdersDto> lstorderdto = new List<OrdersDto>();
            var res = await _ordersRepository.GetOrders();
            foreach (Orders order in res)
            {
                OrdersDto ordersDto = new OrdersDto();
                ordersDto.orderid = order.orderid;
                ordersDto.ordername = order.ordername;
                ordersDto.orderlocation = order.orderlocation;
                lstorderdto.Add(ordersDto);//Add the orders to list here
                Log.Information("OrdersServices: GetOrders method Excution Ended");
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersServices: GetOrders method Excution Ended");//logg the message in database using custom logging factory


            }
            return lstorderdto;

        }

        public async Task<string> UpdateOrder(OrdersDto orderdetail)
        {
            Log.Information("OrdersService: UpdateOrder method Excution Starts");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersService: UpdateOrder method Excution Starts");//logg the message in database using custom logging factory

            Orders obj = new Orders();
            obj.orderid = orderdetail.orderid;
            obj.ordername = orderdetail.ordername;
            obj.orderlocation = orderdetail.orderlocation;
            var res = await _ordersRepository.UpdateOrder(obj);
            Log.Information("OrdersServices: UpdateOrder method Excution Ended");
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersServices: UpdateOrder method Excution Ended");//logg the message in database using custom logging factory

            return res;

        }
    }
}
