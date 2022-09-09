using Api.Data;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(this IServiceCollection services, IConfiguration configuration)
        {          

            if (Environment.GetEnvironmentVariable("DATABASE").ToUpper() == "SQLSERVER")
            {
                services.AddDbContext<MyContext>(options =>
                {
                    options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONECTION"));
                });
            }


            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserImplementation>();
        }
    }
}