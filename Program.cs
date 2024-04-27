using backend_core.Data;
using backend_core.Interfaces;
using backend_core.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnection = builder.Configuration.GetConnectionString("mysqlConnection");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
});

builder.Services.AddScoped<ICategoryRepository , CategoryRepository>();


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

app.Run();
