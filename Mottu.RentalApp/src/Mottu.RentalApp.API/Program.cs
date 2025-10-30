using Microsoft.EntityFrameworkCore;
using Mottu.RentalApp.Infrastructure.Persistence.Context;
using Mottu.RentalApp.Application.Interfaces.Repositories;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Application.Services;
using FluentValidation.AspNetCore;
using Mottu.RentalApp.Infrastructure.Persistence.Repositories;
using Mottu.RentalApp.Infrastructure.Storage;
using Mottu.RentalApp.Infrastructure.Messaging;
using MassTransit;
using Mottu.RentalApp.CrossCutting.Settings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Mottu.RentalApp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RentalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IRiderRepository, RiderRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();

builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();
builder.Services.AddScoped<IRiderService, RiderService>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();
builder.Services.AddScoped<IFileStorageService, MinioFileStorageService>();
builder.Services.AddScoped<IRentalCalculatorService, RentalCalculatorService>();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitSettings = builder.Configuration.GetSection("RabbitMq");
        cfg.Host(rabbitSettings["Host"], "/", h =>
        {
            h.Username(rabbitSettings["Username"]);
            h.Password(rabbitSettings["Password"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});
// registra o publisher de eventos
builder.Services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();
builder.Services.AddAutoMapper(config =>
{
    // Aqui você registra manualmente os profiles se quiser
    config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

builder.Services.Configure<MinioSettings>(
    builder.Configuration.GetSection("MinioSettings")
);

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<Microsoft.Extensions.Options.IOptions<MinioSettings>>().Value
);

builder.Services.AddScoped<IFileStorageService, MinioFileStorageService>();

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container. 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddPersistenceInfrastructure(builder.Configuration);

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

