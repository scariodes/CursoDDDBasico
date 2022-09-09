using System.Reflection;
using Api.CrossCutting;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Data;
using Api.Data.Context;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Api.Application
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private IWebHostEnvironment Env { get; set; }       

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {            
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    Title = "Curso de API com DDD",
                    Description = "Arquitetura de API com DDD",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Paulo Henrique",
                        Email = "scariodes1895@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/paulo-henrique-de-souza-a8a8b8b4/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // Adiciona o botão do login
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o Token JWT: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Adiciona o Bearer ao Scopes => faz o uso do token
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            if (Env.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("DB_CONECTION", "Server=localhost\\ph;Database=CursoDDDTeste;User Id=SA;Password=@18P04au;");
                Environment.SetEnvironmentVariable("DATABASE", "SQLSERVER");
                Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
                Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
                Environment.SetEnvironmentVariable("Issuer", "ExemploIssuer");
                Environment.SetEnvironmentVariable("Seconds", "900");
                // app.Environment.SetEnvironmentVariable("teste","sfg");
                // link da Aula: https://www.udemy.com/course/aspnet-core-22-c-api-com-arquitetura-ddd-na-pratica/learn/lecture/21216918#overview
            }else{
                Environment.SetEnvironmentVariable("DB_CONECTION", "Server=localhost\\ph;Database=CursoDDD2;User Id=SA;Password=@18P04au;");
                Environment.SetEnvironmentVariable("DATABASE", "SQLSERVER");
                Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
                Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
                Environment.SetEnvironmentVariable("Issuer", "ExemploIssuer");
                Environment.SetEnvironmentVariable("Seconds", "900");
            }



            // Configura as dependencias
            services.ConfigureDependenciesRepository(Configuration);
            services.ConfigureDependenciesService();

            ConfigDatabase.Conexao = Configuration.GetConnectionString("builder");

            // Configura AutoMapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            // var tokenConfigurations = new TokenConfiguration();
            // new ConfigureFromConfigurationOptions<TokenConfiguration>(
            // Configuration.GetSection("TokenConfiguration")
            // ).Configure(tokenConfigurations);
            // services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                var paramsValidation = bearer.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.RoutePrefix = string.Empty;
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Api V1");
                });
            }               

            if (Environment.GetEnvironmentVariable("MIGRATION").ToUpper() == "APLICAR")
            {
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = service.ServiceProvider.GetService<MyContext>())
                    {
                        if (context != null)
                        {
                            context.Database.Migrate();
                        }
                    }
                }
            }

            app.UseAuthentication();
            app.UseRouting(); 
            app.UseAuthorization();               
            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });
        }
    }
}