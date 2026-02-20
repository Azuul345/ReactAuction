// using Microsoft.EntityFrameworkCore;
// using ReactAuction.DTO.Services.Interfaces;
// using ReactAuction.DTO.Services.Implementations;
// using ReactAuction.Data;

using Microsoft.EntityFrameworkCore;
using ReactAuction.Data;
using ReactAuction.DTO.Services.Interfaces;
using ReactAuction.DTO.Services.Implementations;


var builder = WebApplication.CreateBuilder(args);

// Add controllers (enables attribute routing with [ApiController] and [Route]).
builder.Services.AddControllers();

// Swagger/OpenAPI for API documentation and testing.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//1
// Add AppDbContext and connect it to SQL Server using the connection string from appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the UserService so we can inject it into controllers.
builder.Services.AddScoped<IUserService, UserService>();



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authorization middleware (we'll configure it more when we add JWT).
app.UseAuthorization();

// Map attribute-routed controllers (like UserController).
app.MapControllers();
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
