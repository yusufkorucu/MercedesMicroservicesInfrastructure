using EmailService.Api.Consumer.Email;
using EmailService.Api.Core.Application.Services;
using RabbitMQ.Shared;
using MassTransit;
using EmailService.Api.Consumer.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailService, EmailService.Api.Core.Application.Services.EmailService>();

builder.Services.AddMassTransit(conf =>
{
    conf.AddConsumer<EmailSenderEventConsumer>();
    conf.AddConsumer<UserCreatedEventConsumer>();

    conf.UsingRabbitMq((context, _conf) =>
    {
        _conf.Host(builder.Configuration["RabbitMQ"]);
        _conf.ReceiveEndpoint(RabbitMQSettings.Email_EmailSenderEventQueue, e => e.ConfigureConsumer<EmailSenderEventConsumer>(context));
        _conf.ReceiveEndpoint(RabbitMQSettings.User_UserCreatedEventQueue, e => e.ConfigureConsumer<UserCreatedEventConsumer>(context));
        

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
