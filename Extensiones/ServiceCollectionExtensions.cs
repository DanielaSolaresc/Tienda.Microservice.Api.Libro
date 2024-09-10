using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservice.Api.Libro.Aplicacion;
using Tienda.Microservice.Api.Libro.Persistencia;

namespace Tienda.Microservice.Api.Libro.Extensiones
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddDbContext<ContextoLibreria>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")
            ));

            //Agregar Servicios de CORS
            services.AddCors(options =>
            {
                
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "https://another-example.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
                
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            //Agregamos MediatR como servicio
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));

            return services;
        }
    }
}
