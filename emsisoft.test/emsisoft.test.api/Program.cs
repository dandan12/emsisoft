using emsisoft.test.api.Infrastructure.MessageQueue;
using emsisoft.test.core.Abstractions;
using emsisoft.test.infra.rabbitmq;
using emsisoft.test.infra.sql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;

builder.Services
    .RegisterRabbitMQInfrastructure(config)
    .RegisterSqlInfrastructure(config);

builder.Services.AddScoped<IRabbitMQSubscriptionBinder, RabbitMQSubscriptionBinder>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(IMediatrRequest<>).Assembly);
});

builder.Services.AddHostedService<RabbitMQSubscriptionService>();

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
