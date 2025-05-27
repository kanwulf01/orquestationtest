using Application.Interfaces;
using Application.Services;
using Infrastructure.Elastic;
using Infrastructure.Messaging;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, 
            IConfiguration configuration
        ) 
        {
            var connectionString = configuration.GetConnectionString("PermisosDB");
            Console.WriteLine($"[%] ConnectionString en uso: {connectionString}");

            if (String.IsNullOrEmpty(connectionString)) {
                throw new InvalidOperationException("La conexion a la DB no existe");
            }

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationDbContext>(
                    optionBuilder =>
                    {
                        optionBuilder.UseSqlServer(connectionString);
                    }
                );


            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<IElasticClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var elasticUrl = configuration["Elasticsearch:Url"] ?? "localhost:9200";
                var indexName = config["Elasticsearch:DefaultIndex"] ?? "tu-indice";

                var settings = new ConnectionSettings(new Uri(elasticUrl))
                    .DefaultIndex(indexName);
                return new ElasticClient(settings);
            });


            services.AddScoped<IPermisosRepository, PermisoRepository>();
            services.AddScoped<ITipoPermisosRepository, TipoPermisoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPermisosAppService, PermisosAppService>();
            services.AddScoped<IElasticService, ElasticService>();
            //Kafka Service Producer
            services.AddScoped<IKafkaServices, KafkaProducer>();

            return services;
        }
    }
}
