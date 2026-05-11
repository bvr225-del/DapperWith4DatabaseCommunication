using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly ILoggingFactory _loggingFactory;

        public OrdersController(IOrdersService ordersService, ILoggingFactory loggingFactory)
        {
            _ordersService = ordersService;
            _loggingFactory = loggingFactory;
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> Post([FromBody] OrdersDto orderdto)
        {


                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var orderData = await _ordersService.AddOrder(orderdto);
                    Log.Information("OrdersController: Post Api method Excution Ends");
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "OrdersController: Post Api method Excution Ends");//logg the message in database using custom logging factory
                    return StatusCode(StatusCodes.Status201Created, orderData);
                }
        }
        [HttpDelete]
        [Route("DeleteOrderByOrderid/{orderid}")]
        public async Task<IActionResult> delete(int orderid)
        {

            if (orderid < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }

                var orderData = await _ordersService.DeleteOrderById(orderid);

                if (orderData == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "orderData not  found");
                }
                else
                {
                    Log.Information("OrdersController: delete Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "delete Api method Execution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrder()
        {
            
            
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: OrdersController: get Api method Excution Failed");
                throw new Exception("Custom Exception: OrdersController: GetOrders Api method Excution Failed");

                var orderdata = await _ordersService.GetOrders();
                if (orderdata == null)//here null means if you are not getting any data from db then we will return this statuscode:Status404NotFound
                {
                    return StatusCode(StatusCodes.Status404NotFound, "orderData not found");
                }
                else
                {
                    Log.Information("OrdersController: Get Api method Excution Ended");//log the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "Get Api method Excution Ended");//log the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, orderdata);
                }
            

        }
        [HttpGet]
        [Route("GetOrderByOrderid/{orderid}")]
        public async Task<IActionResult> Get(int orderid)
        {


            if (orderid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }

                var orderdata = await _ordersService.GetOrderById(orderid);
                Log.Information("OrdersController: GetById Api method Excution Ended");//logg the message in text file using serilog
                await _loggingFactory.AddLoggingMessages("venkat", "Information", "GetById Api method Execution Ended");//logg the message in database using custom logging factory


                return StatusCode(StatusCodes.Status200OK, orderdata);
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> put([FromBody] OrdersDto orderdto)
        {

                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var orderData = await _ordersService.UpdateOrder(orderdto);
                    Log.Information("OrdersController: Put Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "put Api method Execution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, orderData);
                }
        }

    }
}
