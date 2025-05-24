using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var connectionString = configuration.GetConnectionString("DBLOCAL");

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

            services.AddScoped<IPermisosRepository, PermisoRepository>();
            services.AddScoped<ITipoPermisosRepository, TipoPermisoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
