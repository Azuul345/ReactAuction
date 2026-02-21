using Microsoft.EntityFrameworkCore;
using ReactAuction.Data;
using ReactAuction.DTO.Services.Interfaces;
using ReactAuction.DTO.Services.Implementations;
using ReactAuction.DTO.Repositories.Interfaces;
using ReactAuction.DTO.Repositories.Implementations;



var builder = WebApplication.CreateBuilder(args);

// Add controllers (enables attribute routing with [ApiController] and [Route]).
builder.Services.AddControllers();

// Swagger/OpenAPI for API documentation and testing.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//1
// Add AppDbContext and connect it to SQL Server using the connection string from appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the UserReposirory and UserService so we can inject it into controllers.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


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


app.Run();


