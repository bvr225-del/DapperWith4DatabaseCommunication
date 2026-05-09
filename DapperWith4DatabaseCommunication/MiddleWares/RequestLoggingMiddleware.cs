using Serilog;

namespace DapperWith4DatabaseCommunication.MiddleWares
{
    public class RequestLoggingMiddleware
    {
        //RequestDelegate Process The HttpRequest.
        private readonly RequestDelegate _next;

        // Middleware constructor takes the next RequestDelegate in the pipeline
        /*
         * Each middleware can:
          Process the request itself.
          Pass it to the next middleware.
          Process the response after the next middleware completes.
         * 
         */
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;//Constructr Injection
        }

        // Invoke method handles each request and passes control to the next middleware

        //here need to CREATE ONE INVOKEASYNC METHOD, this method is responsible for handling each incoming HTTP request.
        //It accepts an HttpContext object, which contains all the information about the current request and response.
        //Inside this method, you can perform any necessary processing on the request,
        //such as logging, authentication, or modifying the request before passing it to the next middleware in the pipeline. After processing the request, you call _next(context) to pass control to the next middleware. Once the next middleware has completed its processing, you can also perform actions on the response if needed.
        public async Task InvokeAsync(HttpContext context)
        {//HttpContext is predfeined class,it  represents all the information about an individual HTTP request and response.
            /*
            2. How Middleware Works Internally?
          In ASP.NET Core, each middleware:
          Accepts an HttpContext.
          Either processes the request or forwards it to the next middleware.
          Performs some action on the response (optional)
            */
            // Log the request path
            Log.Information($"Request Path: {context.Request.Path}");
            Log.Information($"Request Path: {context.Request.Path}");
            await _next(context); // Process the next middleware in the pipeline
                                  // After the next middleware has completed, log the status code
            Log.Information($"Response Status Code: {context.Response.StatusCode}");
            Log.Information($"Response Status Code: {context.Response.StatusCode}");
        }
    }
}
