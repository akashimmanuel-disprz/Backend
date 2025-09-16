using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using BusinessLogicLayer.Interfaces;



var builder = WebApplication.CreateBuilder(args);

// 1. Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Register repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();

// 3. Register services
builder.Services.AddScoped<IEventService, EventService>();

// 4. Add controllers
builder.Services.AddControllers();

// 5. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
var provider = builder.Services.BuildServiceProvider();
var test = provider.GetService<IEventService>();
Console.WriteLine(test == null ? "Not resolved" : "Resolved");
app.Run();