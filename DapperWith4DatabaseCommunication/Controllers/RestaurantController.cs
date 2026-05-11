using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILoggingFactory _loggingFactory;


        public RestaurantController(IRestaurantService restaurantService, ILoggingFactory loggingFactory)
        {
            _restaurantService = restaurantService;
            _loggingFactory = loggingFactory;
        }
        [HttpPost]
        [Route("AddRestaurant")]
        public async Task<IActionResult> Post([FromBody] RestaurantDto restaurantdto)
        {

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var restaurantData = await _restaurantService.AddRestaurant(restaurantdto);
                    Log.Information("RestaurantController: Post Api method Excution Ends");
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantController: Post Api method Excution Ends");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status201Created, restaurantData);
                }
        }
        [HttpDelete]
        [Route("DeleteRestaurantById/{restaurantid}")]
        public async Task<IActionResult> delete(int restaurantid)
        {
            if (restaurantid < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }

                var restaurantData = await _restaurantService.DeleteRestaurantById(restaurantid);

                if (restaurantData == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not  found");
                }
                else
                {
                    Log.Information("RestaurantController:delete Api method Excution Ends");
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantController: delete Api method Excution Ends");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
        }
        [HttpGet]
        [Route("GetRestaurants")]
        public async Task<IActionResult> GetRestaurants()
        {

                var restaurantData = await _restaurantService.GetRestaurants();
                if (restaurantData == null)//here null means if you are not getting any data from db then we will return this statuscode:Status404NotFound
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not found");
                }
                else
                {
                    Log.Information("RestaurantController:Get Api method Excution Ends");
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantController: Get Api method Excution Ends");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, restaurantData);
                }
        }
        [HttpGet]
        [Route("GetRestaurantById/{restaurantid}")]
        public async Task<IActionResult> Get(int restaurantid)
        {

            if (restaurantid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }

                var restaurantData = await _restaurantService.GetRestaurantById(restaurantid);
                if (restaurantData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not found");
                }
                else
                {
                    Log.Information("RestaurantController: GetById Api method Excution Ends");
                    await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantController: GetById Api method Excution Ends");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, restaurantData);
                }
                   
        }

        [HttpPut]
        [Route("UpdateRestaurant")]
        public async Task<IActionResult> put([FromBody] RestaurantDto restaurantdto)
        {           
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var restaurantData = await _restaurantService.UpdateRestaurant(restaurantdto);
                    if (restaurantData == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "restaurantData not found");
                    }
                    else
                    {
                        Log.Information("RestaurantController: Put Api method Excution Ends");
                        await _loggingFactory.AddLoggingMessages("venkat", "Information", "RestaurantController: Put Api method Excution Ends");//logg the message in database using custom logging factory

                        return StatusCode(StatusCodes.Status200OK, restaurantData);
                    }
                        
                }
        }

    }
}
