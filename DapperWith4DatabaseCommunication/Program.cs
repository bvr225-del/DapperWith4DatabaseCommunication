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


var app = builder.Build();
//custom middlewares we need to register in the program.cs file of the web api project using the UseMiddleware method   app object. The UseMiddleware method is used to add custom middleware components to the application's request processing pipeline. By adding the GlobalErrorHandlerMiddleware, you ensure that any unhandled exceptions that occur during the processing of HTTP requests will be caught and handled by this middleware, allowing you to return a standardized error response to the client and log the error details as needed.
app.UseMiddleware<GlobalErrorHandlerMiddleware>();//This line of code is used to add the GlobalErrorHandlerMiddleware to the application's request processing pipeline. The UseMiddleware method is an extension method that allows you to add custom middleware components to the pipeline. By adding the GlobalErrorHandlerMiddleware, you ensure that any unhandled exceptions that occur during the processing of HTTP requests will be caught and handled by this middleware, allowing you to return a standardized error response to the client and log the error details as needed.


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
