using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Application.Services;
using Financas.Pessoais.Infrastructure.Interfaces;
using Financas.Pessoais.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Financas.Pessoais.Application;

namespace Financas.Pessoais.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var jwtSecret = configuration["JwtSettings:SecretKey"];

            // Application Layer
            services.AddScoped<IDespesasService, DespesasService>();
            services.AddScoped<IReceitasService, ReceitasService>();
            services.AddScoped<ICategoriasService, CategoriasService>();            
            services.AddScoped<AuthService>();
            services.AddScoped<UserContext>();

            // Domain Layer
            services.AddScoped<IDespesasRepository>(provider => new DespesasRepository(connectionString));
            services.AddScoped<IReceitasRepository>(provider => new ReceitasRepository(connectionString));
            services.AddScoped<ICategoriasRepository>(provider => new CategoriasRepository(connectionString));
            services.AddScoped<IUserRepository>(provider => new UserRepository(connectionString));
        }
    }
}
