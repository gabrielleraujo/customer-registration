using CustomerRegistration.API.Configurations;
using CustomerRegistration.Application;
using CustomerRegistration.Domain.Messaging;
using CustomerRegistration.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerConfiguration();

var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

builder.Services
    .Configure<MessageBusConnectionConfigModel>(configuration!.GetSection("RabbitMQ"));

builder.Services
    .AddApplicationModule()
    .AddInfrastructureModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.Services.UseMessageBusSetup();

// Aplicar migrations ao iniciar a aplicação
app.UseMigration(configuration);

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
