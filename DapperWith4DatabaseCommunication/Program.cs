using DapperWith4DatabaseCommunication.Data;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.MiddleWares;
using DapperWith4DatabaseCommunication.Repositories;
using DapperWith4DatabaseCommunication.Services;
using Serilog;

//this program.cs is divided into 2 sections.
//===========================================================
//section1:builder is the inbuilt depency injection conatiner.we need to register our all application/Project level depencies into our inbuilt depency injection container.
//================================================================================================================================================================
//this conatiner will load your depencies and then it will inject those depencies to the controller class by using constructor injection and then we can use those depencies in the controller class to perform the required CRUD operations.

#region inbuilt dependency injection containerSection.


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();//register the connection factory interface and its implementation in the dependency injection container of the application using the AddSingleton method   builder object. The AddSingleton method is used to register a service with a singleton lifetime, which means that a single instance of the service will be created and shared throughout the application's lifetime.
builder.Services.AddSingleton<ILoggingFactory, LoggingFactory>();//register the logging factory interface and its implementation in the dependency injection container of the application using the AddSingleton method   builder object. The AddSingleton method is used to register a service with a singleton lifetime, which means that a single instance of the service will be created and shared throughout the application's lifetime.

//register the dependency injection for the repository and service layers of the application in the program.cs file of the web api project using the AddScoped method   builder object. The AddScoped method is used to register a service with a scoped lifetime, which means that a new instance of the service will be created for each HTTP request and shared within that request.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.

//register the dependency injection for the repository and service layers of the application in the program.cs file of the web api project using the AddScoped method   builder object. The AddScoped method is used to register a service with a scoped lifetime, which means that a new instance of the service will be created for each HTTP request and shared within that request.
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IDepartmentService, DepartmentService>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.


//=========================================================================================
//=========================================================================================
//register the dependency injection for the repository and service layers of the application in the program.cs file of the web api project using the AddScoped method   builder object. The AddScoped method is used to register a service with a scoped lifetime, which means that a new instance of the service will be created for each HTTP request and shared within that request.
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IOrdersService, OrdersService>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.

//========================================================================================================
//=========================================================================================
//register the dependency injection for the repository and service layers of the application in the program.cs file of the web api project using the AddScoped method   builder object. The AddScoped method is used to register a service with a scoped lifetime, which means that a new instance of the service will be created for each HTTP request and shared within that request.
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IRestaurantService, RestaurantService>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.

//========================================================================================================
//======================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
//=====================
//======================
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IRolesService, RolesService>();
//=====================
//======================
builder.Services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
//=====================


//builder is the inbuilt dependency injection container which is used to register the services and the repositories in the dependency injection container of the application and then we are building the application and running it.
//if you run the program,first it will call program.cs and it will load all the depencies into the memory and then it will inject those depencies to the controller class by using constructor injection and then we can use those depencies in the controller class to perform the required operations
// If you want to add any depencencies to your Depencyinjection container. by using builder.services....we can register our dependicies to the container.

//============enabling the cors at program.cs file of the web api project using the AddCors method   builder object. The AddCors method is used to add Cross-Origin Resource Sharing (CORS) services to the application, which allows you to specify which origins are allowed to access the API and what HTTP methods and headers are permitted in cross-origin requests.
builder.Services.AddCors(options =>
{ //THIS CODE IS ACCESSING ALL ORIGINS,ALL METHODS,ALL HEADERS. IT IS NOT A GOOD PRACTICE TO ALLOW ALL ORIGINS,ALL METHODS,ALL HEADERS IN PRODUCTION ENVIRONMENT.BECAUSE IT CAN CAUSE SECURITY ISSUES IN YOUR APPLICATION. SO IN PRODUCTION ENVIRONMENT YOU SHOULD SPECIFY THE ORIGINS,METHODS,HEADERS THAT YOU WANT TO ALLOW IN YOUR APPLICATION.
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
//WE ARE GIVING PERMISSIONS TO SPECIFIC ORIGINS, METHODS, HEADERS IN THE CORS POLICY. IT IS A GOOD PRACTICE TO ALLOW ONLY THE ORIGINS, METHODS, HEADERS THAT YOU WANT TO ALLOW IN YOUR APPLICATION TO AVOID SECURITY ISSUES IN YOUR APPLICATION.
//string[] origins =
//{
//"https://ICICBANK.com",
//"https://AXISBANK.com",
//"https://HDFCBANK.com",
//};
//builder.Services.AddCors(options =>
//{//Addpolicy mens we can define multiple policies as per our requirement and we can specify the policy name and then we can use that policy name in the app.useCors() method to enable the CORS for that specific policy.
//    options.AddPolicy("bankPolicy", (builder) =>//this is the name of the policy, you can give any name to your policy as per your requirement and then you can use that policy name in the app.useCors() method to enable the CORS for that specific policy.
//    {
//        builder.WithOrigins(origins)
//            .AllowAnyHeader().WithMethods("*");//=>Here* means it will allow "GET", "POST", "PUT", "DELETE".WithExposedHeaders("*");     
//    });
//});

#endregion
//section2:app is the inbuilt request pipeline,heare we need to register our middlewares to application pipeline.
//===================================================================================================================
#region middlewaresConfigurationSection
var app = builder.Build();//app is requtest pipeline,it is created at runtime.
                          //all middleware we need to configure/register /adding to this request pipeline
                          //whenever if you add any middleware to request pipeleine order wise it will exceute.
                          //for inline middlewares all logic written in the app.use() method and for custom middlewares we need to create a class and write the logic in that class and then we need to register that custom middleware in the program.cs file using the app.UseMiddleware method.
                          // middleware naming convention starts with use keyword.                         
                          //we must register the middle wares to request pipleline.based on order you register.same order it will excute.
                          //**below code is for inline middlewares,here we are writing the logic of the middleware inside the app.use() method.**                         
                          //******inline middleware logic starts here********
                          //For inline middleware we are writing the logic of the middleware inside the app.use() method. we can write multiple inline middlewares in the program.cs file by using multiple app.use() methods. and also we need to call the next.invoke() method in the app.use() method to call the next middleware in the pipeline. if you are not calling next.invoke() method in the app.use() method, it will not call the next middleware in the pipeline and it will stop the execution of the middleware.
                          //app.Use(async (context, next) => {//app.Use for Inline middlewares
                          //    await context.Response.WriteAsync("Hello I am From use1");
                          //    await next.Invoke();//if you are not calling next.invoke() method in the app.use() method, it will not call the next middleware in the pipeline and it will stop the execution of the middleware.
                          //});
                          //app.Use(async (context, next) => {//app.Use for Inline middlewares
                          //    await context.Response.WriteAsync("Hello I am From use2");
                          //    await next.Invoke();
                          //});
                          //******inline middleware logic ended here********
/*
 * syntax:Adds a custom  middleware type to the application request Pipeline like below syntax .
 * =============================================================================================
  app.UseMiddleWare<CustomMiddlewareClassName>();we must register like this way.
//<>   we called its as placeholder  .or AngleBracktes
//In that PlaceHolder (<Custommiddlware classname>)Write here
//app.UseMiddleware<RequestLoggingMiddleware>();//Registering the  Custom Middleware to appliction pipeline like this way.
// Register the middlewares in  HTTP request pipeline.
*/
//custom middlewares we need to register in the program.cs file of the web api project using the UseMiddleware method   app object. The UseMiddleware method is used to add custom middleware components to the application's request processing pipeline. By adding the GlobalErrorHandlerMiddleware, you ensure that any unhandled exceptions that occur during the processing of HTTP requests will be caught and handled by this middleware, allowing you to return a standardized error response to the client and log the error details as needed.
app.UseMiddleware<GlobalErrorHandlerMiddleware>();//Registering the  Custom Middleware to appliction pipeline like this way.
app.UseMiddleware<RequestLoggingMiddleware>();//Registering the  Custom Middleware to appliction pipeline like this way.
//This line of code is used to add the GlobalErrorHandlerMiddleware to the application's request processing pipeline. The UseMiddleware method is an extension method that allows you to add custom middleware components to the pipeline. By adding the GlobalErrorHandlerMiddleware, you ensure that any unhandled exceptions that occur during the processing of HTTP requests will be caught and handled by this middleware, allowing you to return a standardized error response to the client and log the error details as needed.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();//Predefined Middlewares,created by the swagger team to generate the swagger documentation for the api.
    app.UseSwaggerUI();
}
//UseCors is a predefined middleware,created by the Microsoft team to handle the cross-origin resource sharing in the api level.
app.UseCors();//add the cors middleware to the application pipeline and specify the policy name that we defined in the AddCors method of the builder object in the dependency injection container section.
//app.UseCors("bankPolicy");//this cors is for specific policy,if you want to enable the cors for specific policy then you need to specify the policy name in the app.useCors() method like this way.
app.UseAuthorization();//Predefined Middlewares,created by the Microsoft team to handle the authorization in the api.

app.MapControllers();

app.Run();//App.run() is a termainal middleware,it ends the application pipeline without calling the next middleware.always app.run() is last in program.cs file
          //always it should be ending only.
          //(Here We removed App.Run() method,without app.run() if you run the application it will throw error like "WebServer failed to listen on port 5296")
          //After app.run () method if you write any code it will not exceute.because app.run() does not having next.due to that exceution is stopped in this  line.



#endregion
