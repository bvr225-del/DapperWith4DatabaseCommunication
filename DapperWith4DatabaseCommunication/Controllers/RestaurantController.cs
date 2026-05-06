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
            #region Serilog Logging the mesages 
            Log.Information("RestaurantController: Post Api method Excution Starts");
            Log.Information("RestaurantController: Post Api method called with RestaurantName: {@restaurantname}", restaurantdto.RestaurantName);
            Log.Information("RestaurantController: Post Api method called with RestaurantLocation: {@RestaurantLocation}", restaurantdto.RestaurantLocation);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: Post Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"Post Api method called with RestaurantName:{restaurantdto.RestaurantName}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"Post Api method called with RestaurantLocation: {restaurantdto.RestaurantLocation}");//logg the message in database using custom logging factory
            #endregion

            try
            {                
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: RestaurantController: Post Api method Excution Failed");

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var restaurantData = await _restaurantService.AddRestaurant(restaurantdto);
                    Log.Information("RestaurantController: Post Api method Excution Ends");
                    await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: Post Api method Excution Ends");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status201Created, restaurantData);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "RestaurantController: Post Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Error", $"RestaurantController: Inside Post Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpDelete]
        [Route("DeleteRestaurantById/{restaurantid}")]
        public async Task<IActionResult> delete(int restaurantid)
        {
            #region Serilog Logging the mesages 
            Log.Information("RestaurantController: delete Api method Excution Starts");
            Log.Information("RestaurantController: delete Api method called with RestaurantId: {@restaurantid}", restaurantid);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: delete Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"delete Api method called with RestaurantId:{restaurantid}");//logg the message in database using custom logging factory
            #endregion

            if (restaurantid < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: RestaurantController: delete Api method Excution Failed");

                var restaurantData = await _restaurantService.DeleteRestaurantById(restaurantid);

                if (restaurantData == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not  found");
                }
                else
                {
                    Log.Information("RestaurantController:delete Api method Excution Ends");
                    await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: delete Api method Excution Ends");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "RestaurantController: delete Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Error", $"RestaurantController: Inside delete Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetRestaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            #region Serilog Logging the mesages 
            Log.Information("RestaurantController: Get Api method Excution Starts");
            Log.Information("RestaurantController: Get Api method called ");
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: Get Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: Get Api method called ");//logg the message in database using custom logging factory
            #endregion

            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: RestaurantController: get Api method Excution Failed");

                var restaurantData = await _restaurantService.GetRestaurants();
                if (restaurantData == null)//here null means if you are not getting any data from db then we will return this statuscode:Status404NotFound
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not found");
                }
                else
                {
                    Log.Information("RestaurantController:Get Api method Excution Ends");
                    await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: Get Api method Excution Ends");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, restaurantData);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "RestaurantController: Get Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Error", $"RestaurantController: Inside Get Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }
        [HttpGet]
        [Route("GetRestaurantById/{restaurantid}")]
        public async Task<IActionResult> Get(int restaurantid)
        {
            #region Serilog Logging the mesages 
            Log.Information("RestaurantController: GetById Api method Excution Starts");
            Log.Information("RestaurantController: GetById Api method called with RestaurantId: {@restaurantid}", restaurantid);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: GetById Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"RestaurantController: GetById Api method called with RestaurantId:{restaurantid}");//logg the message in database using custom logging factory
            #endregion

            if (restaurantid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: RestaurantController: getbyid Api method Excution Failed");

                var restaurantData = await _restaurantService.GetRestaurantById(restaurantid);
                if (restaurantData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurantData not found");
                }
                else
                {
                    Log.Information("RestaurantController: GetById Api method Excution Ends");
                    await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: GetById Api method Excution Ends");//logg the message in database using custom logging factory

                    return StatusCode(StatusCodes.Status200OK, restaurantData);
                }
                   
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "RestaurantController:  GetById Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Error", $"RestaurantController: Inside GetById Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }

        [HttpPut]
        [Route("UpdateRestaurant")]
        public async Task<IActionResult> put([FromBody] RestaurantDto restaurantdto)
        {           
            #region Serilog Logging the mesages 
            Log.Information("RestaurantController: Put Api method Excution Starts");
            Log.Information("RestaurantController: Put Api method called with RestaurantId: {@restaurantid}", restaurantdto.Id);
            Log.Information("RestaurantController: Put Api method called with RestaurantName: {@restaurantname}", restaurantdto.RestaurantName);
            Log.Information("RestaurantController: Put Api method called with RestaurantLocation: {@RestaurantLocation}", restaurantdto.RestaurantLocation);
            Log.Information("RestaurantController: Put Api method called with CreationDate: {@CreationDate}", restaurantdto.CreationDate);
            #endregion

            #region Database Logging the mesages using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: Put Api method Excution Starts");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"RestaurantController: Put Api method called with RestaurantId:{restaurantdto.Id}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"RestaurantController: Put Api method called with RestaurantName:{restaurantdto.RestaurantName}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"RestaurantController: Put Api method called with RestaurantLocation: {restaurantdto.RestaurantLocation}");//logg the message in database using custom logging factory
            await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", $"RestaurantController: Put Api method called with CreationDate: {restaurantdto.CreationDate}");//logg the message in database using custom logging factory

            #endregion

            try
            {
                #region CustomError Raising Example
                //int a = 10, b = 0;
                //int result = a / b; //this will throw an exception because we are dividing by zero exception
                #endregion
                //throw new Exception("Custom Exception: RestaurantController: Put Api method Excution Failed");

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
                        await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Information", "RestaurantController: Put Api method Excution Ends");//logg the message in database using custom logging factory

                        return StatusCode(StatusCodes.Status200OK, restaurantData);
                    }
                        
                }
            }
            catch (Exception ex)
            {
                Log.Error("Custom Failure: {@RequestName}, {@Error}, {@DateTimeUtc}",
                "RestaurantController:  Put Api method", ex.Message, DateTime.Today);
                await _loggingFactory.Add_RestaurantLoggingMessages("venkat", "Error", $"RestaurantController: Inside Put Api method Error Occured,Errormessage is:({ex.Message})-errorStacktrace:({ex.StackTrace})-error Innerexeception:({ex.InnerException})");//logg the message in database using custom logging factory

                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

    }
}
