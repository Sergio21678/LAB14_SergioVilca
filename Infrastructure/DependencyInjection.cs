using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar el AppDbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            
            // --- AÑADIR ESTA LÍNEA ---
            // Registra la interfaz. Cuando un Handler pide IAppDbContext,
            // el sistema le entregará la instancia de AppDbContext que ya se registró.
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            
            return services;
        }
    }
}