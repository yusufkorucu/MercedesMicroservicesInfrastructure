using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Api.Core.Application.Repository;
using IdentityService.Api.Core.Application.Services;
using IdentityService.Api.Core.Domain;
using IdentityService.Api.Infrastructure.Context;
using IdentityService.Api.Infrastructure.Validators;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Shared;
using RabbitMQ.Shared.Events.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IdentityApiDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();


builder.Services.AddMassTransit(conf =>
{

    conf.UsingRabbitMq((context, _conf) =>
    {
        _conf.Host(builder.Configuration["RabbitMQ"]);
        _conf.Publish<UserCreatedEvent>(p =>
        {
            p.AutoDelete = false;
            p.Durable = true;
        });

    });
});

#region FluentValidation

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

#endregion

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
