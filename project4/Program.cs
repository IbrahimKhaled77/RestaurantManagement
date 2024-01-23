using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.Implementation;
using RestaurantManagement_Repository.IRepository;
using Serilog;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RestaurantManagement API",
        Version = "v1",
        Description = "An API for Restaurant Management, allowing customers to place orders for menu items through a website. In general, it is a system designed for restaurant use.",
       TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Ibrahim Khaled",
            Email = "Theghost12345678902001@gmail.com",
            Url = new Uri("https://twitter.com/jwalkner"),
        }
    });
    /**/
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
   // c.IncludeXmlComments(xmlPath);


});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RestaurantManagementContext>(server => server.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnect")));
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeOrderRepository, EmployeeOrdersRepository>();

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


Serilog.Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.File(configuration.GetValue<string>("LoggerFilePaths"), rollingInterval: RollingInterval.Day)
    .MinimumLevel.Debug() 
    .CreateLogger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
try
{
    Log.Information("Starting web host");
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");

}
finally {

    Log.CloseAndFlush();
}