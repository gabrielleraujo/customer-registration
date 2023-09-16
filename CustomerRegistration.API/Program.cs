using CustomerRegistration.API.Configurations;
using CustomerRegistration.Application;
using CustomerRegistration.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerConfiguration();

builder.Services
    .AddApplicationModule()
    .AddInfrastructureModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMidleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
