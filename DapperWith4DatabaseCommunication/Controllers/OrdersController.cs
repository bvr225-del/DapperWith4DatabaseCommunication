using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> Post([FromBody] OrdersDto orderdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var orderData = await _ordersService.AddOrder(orderdto);
                    return StatusCode(StatusCodes.Status201Created, orderData);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
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
            try
            {
                var orderData = await _ordersService.DeleteOrderById(orderid);

                if (orderData == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "orderData not  found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrder()
        {
            try
            {
                var orderdata = await _ordersService.GetOrders();
                if (orderdata == null)//here null means if you are not getting any data from db then we will return this statuscode:Status404NotFound
                {
                    return StatusCode(StatusCodes.Status404NotFound, "orderData not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, orderdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
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
            try
            {
                var orderdata = await _ordersService.GetOrderById(orderid);
                return StatusCode(StatusCodes.Status200OK, orderdata);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> put([FromBody] OrdersDto orderdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var orderData = await _ordersService.UpdateOrder(orderdto);
                    return StatusCode(StatusCodes.Status200OK, orderData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

    }
}
