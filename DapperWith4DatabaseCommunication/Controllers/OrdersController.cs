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
            #region Serilog Logging the mesages 
            Log.Information("OrdersController: Post Api method Excution Starts");
            Log.Information("OrdersController: Post Api method called with OrderName: {@ordername}", orderdto.ordername);
            Log.Information("OrdersController: Post Api method called with OrderLocation: {@orderlocation}", orderdto.orderlocation);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersController: Post Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"Post Api method called with OrderName:{orderdto.ordername}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"Post Api method called with OrderLocation:{orderdto.orderlocation}");//logg the message in database using custom logging factory
            #endregion

            try
            {
                //#region CustomError Raising Example
                ////int a = 10, b = 0;
                ////int result = a / b; //this will throw an exception because we are dividing by zero exception
                //#endregion

                //throw new Exception("Custom Exception: EmployeeController: Post Api method Excution Failed");

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var orderData = await _ordersService.AddOrder(orderdto);
                    Log.Information("OrdersController: Post Api method Excution Ends");
                    await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersController: Post Api method Excution Ends");//logg the message in database using custom logging factory
                    return StatusCode(StatusCodes.Status201Created, orderData);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "OrdersController: Post Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Error", $"OrdersController: Inside Post Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpDelete]
        [Route("DeleteOrderByOrderid/{orderid}")]
        public async Task<IActionResult> delete(int orderid)
        {
            #region Serilog Logging the mesages 
            Log.Information("OrdersController: delete Api method Excution Starts");
            Log.Information("OrdersController: delete Api method called with OrderId: {@orderid}", orderid);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersController: delete Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"delete Api method called with OrderId:{orderid}");//logg the message in database using custom logging factory
            #endregion


            if (orderid < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var orderData = await _ordersService.DeleteOrderById(orderid);

                if (orderData == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "orderData not  found");
                }
                else
                {
                    Log.Information("OrdersController: delete Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "delete Api method Execution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "OrdersController: delete Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Error", $"OrdersController: Inside delete Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory


                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrder()
        {
            #region Serilog Logging the mesages 
            Log.Information("OrdersController: get Api method Excution Starts");
            Log.Information("OrdersController: get Api method called ");
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersController: GET Api method Excution Starts");//log the message in database using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"GET Api method called");//log the message in database using custom logging factory
            #endregion


            try
            {
                var orderdata = await _ordersService.GetOrders();
                if (orderdata == null)//here null means if you are not getting any data from db then we will return this statuscode:Status404NotFound
                {
                    return StatusCode(StatusCodes.Status404NotFound, "orderData not found");
                }
                else
                {
                    Log.Information("OrdersController: Get Api method Excution Ended");//log the message in text file using serilog
                    await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "Get Api method Excution Ended");//log the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, orderdata);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
               "OrdersController: GET Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Error", $"OrdersController: Inside GET Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }
        [HttpGet]
        [Route("GetOrderByOrderid/{orderid}")]
        public async Task<IActionResult> Get(int orderid)
        {
            #region Serilog Logging the mesages 
            Log.Information("OrdersController: GetById Api method Excution Starts");
            Log.Information("OrdersController: GetById Api method called with OrderId: {@orderid}", orderid);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersController: GetById Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"GetById Api method called with OrderId:{orderid}");//logg the message in database using custom logging factory
            #endregion


            if (orderid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var orderdata = await _ordersService.GetOrderById(orderid);
                Log.Information("OrdersController: GetById Api method Excution Ended");//logg the message in text file using serilog
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "GetById Api method Execution Ended");//logg the message in database using custom logging factory


                return StatusCode(StatusCodes.Status200OK, orderdata);
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "OrdersController: delete Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Error", $"OrdersController: Inside GetById Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> put([FromBody] OrdersDto orderdto)
        {
            #region Serilog Logging the mesages 
            Log.Information("OrdersController: Put Api method Excution Starts");
            Log.Information("OrdersController: Put Api method called with OrderId: {@orderid}", orderdto.orderid);
            Log.Information("OrdersController: Put Api method called with OrderName: {@ordername}", orderdto.ordername);
            Log.Information("OrdersController: Put Api method called with OrderLocation: {@orderlocation}", orderdto.orderlocation);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "OrdersController: Put Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"Put Api method called with OrderId:{orderdto.orderid}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"Put Api method called with OrderName:{orderdto.ordername}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", $"Put Api method called with OrderLocation:{orderdto.orderlocation}");//logg the message in database using custom logging factory
            #endregion

            try
            {
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var orderData = await _ordersService.UpdateOrder(orderdto);
                    Log.Information("OrdersController: Put Api method Excution Ended");//logg the message in text file using serilog
                    await _loggingFactory.Add_OrderLoggingMessages("venkat", "Information", "put Api method Execution Ended");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, orderData);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "OrdersController: put Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_OrderLoggingMessages("venkat", "Error", $"OrdersController: Inside put Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

    }
}
