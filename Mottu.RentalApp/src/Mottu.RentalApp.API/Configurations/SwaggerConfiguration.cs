using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mottu.RentalApp.Domain.Enums;
using System.Reflection;

namespace Mottu.RentalApp.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.UseAllOfToExtendReferenceSchemas();
                c.MapType<CnhType>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(CnhType))
                                 .Select(n => new Microsoft.OpenApi.Any.OpenApiString(n))
                                 .Cast<Microsoft.OpenApi.Any.IOpenApiAny>()
                                 .ToList()
                });
                c.OrderActionsBy(apiDesc =>
                {
                    // Define a ordem desejada das controllers
                    var order = new Dictionary<string, int>
                     {
                         { "Motorcycles", 1 },
                         { "Riders", 2 },
                         { "Rentals", 3 }
                     };

                    var controllerName = apiDesc.ActionDescriptor.RouteValues["controller"] ?? "";
                    return order.ContainsKey(controllerName) ? order[controllerName].ToString() : controllerName;
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sistema de Manutenção de Motos",
                    Version = "v1",
                    Description = "API de gerenciamento de motos - Desafio Técnico Mottu.",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe MT",
                        Email = "contato@MT.com.br",
                        Url = new Uri("https://mottu.com.br")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License"
                    }
                });

                c.EnableAnnotations();

                // Inclui comentários XML (resumos dos métodos)
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);

                // Agrupa os endpoints por controller
                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                        return new[] { api.GroupName };

                    var controllerName = api.ActionDescriptor.RouteValues["controller"];
                    return new[] { controllerName ?? "API" };
                });

                c.DocInclusionPredicate((name, api) => true);
            });


            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu Rental API v1");
                c.InjectJavascript("/swagger-ui/pt-pt.js");
                c.InjectStylesheet("/swagger-ui/custom-swagger.css");
                c.DocumentTitle = "Mottu Rental API - Swagger UI";
                c.DisplayRequestDuration();
            });

            return app;
        }
    }
}
