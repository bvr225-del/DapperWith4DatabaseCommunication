using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpPost]
        [Route("AddRestaurant")]
        public async Task<IActionResult> Post([FromBody] RestaurantDto restaurantdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var restaurantData = await _restaurantService.AddRestaurant(restaurantdto);
                    return StatusCode(StatusCodes.Status201Created, restaurantData);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
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
            try
            {
                var restaurantData = await _restaurantService.DeleteRestaurantById(restaurantid);

                if (restaurantData == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not  found");
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
        [Route("GetRestaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            try
            {
                var restaurantData = await _restaurantService.GetRestaurants();
                if (restaurantData == null)//here null means if you are not getting any data from db then we will return this statuscode:Status404NotFound
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, restaurantData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
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
            try
            {
                var restaurantData = await _restaurantService.GetRestaurantById(restaurantid);
                if (restaurantData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not found");
                }
                return StatusCode(StatusCodes.Status200OK, restaurantData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }

        [HttpPut]
        [Route("UpdateRestaurant")]
        public async Task<IActionResult> put([FromBody] RestaurantDto restaurantdto)
        {
            try
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
                    return StatusCode(StatusCodes.Status200OK, restaurantData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

    }
}
