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
using Mottu.RentalApp.API.Configurations; 
using Microsoft.Extensions.Options;

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


builder.Services.Configure<MinioSettings>(builder.Configuration.GetSection("MinioSettings"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MinioSettings>>().Value);
builder.Services.AddScoped<IFileStorageService, MinioFileStorageService>();


builder.Services.AddAutoMapper(config =>
{
    config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddSwaggerDocumentation();

builder.Services.AddPersistenceInfrastructure(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RentalDbContext>();
    db.Database.Migrate();
}

app.Run();
