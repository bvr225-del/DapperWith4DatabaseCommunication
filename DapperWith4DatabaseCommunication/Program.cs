using DapperWith4DatabaseCommunication.Data;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Repositories;
using DapperWith4DatabaseCommunication.Services;
using Serilog;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
