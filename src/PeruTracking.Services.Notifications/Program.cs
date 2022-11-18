using MediatR;
using PeruTracking.Common.EventBus.Messaging;
using PeruTracking.Services.Notifications;
using PeruTracking.Services.Notifications.Messages.Events;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMailKit();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddRabbitMq(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseRabbitMq()
                .SubscribeEvent<CustomerCreated>(@namespace: "customers");

app.MapControllers();

app.Run();
