using backend_core.Application;
using backend_core.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

