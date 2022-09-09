using System.Text;
using Api.Application;
using Api.CrossCutting.Mappings;
using Api.Data.Context;
using Api.Domain.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public MyContext MyContext { get; private set; }
        public HttpClient Client { get; private set; }
        public IMapper Mapper { get; set; }
        public string HostApi { get; set; }
        public HttpResponseMessage response { get; set; }

        public BaseIntegration()
        {
            HostApi = "http://localhost:5114/api/";
            var builder = new WebHostBuilder()
            .UseEnvironment("Testing")
            .UseStartup<Startup>();

            var server = new TestServer(builder);

            MyContext = server.Host.Services.GetService(typeof(MyContext)) as MyContext;
            MyContext.Database.Migrate();

            Mapper = new AutoMapperFixture().GetMapper();

            Client = server.CreateClient();

            //AdicionarToken();
        }

        public async Task AdicionarToken()
        {
            var loginDto = new LoginDto
            {
                Email = "scariodes1895@gmail.com"
            };

            var resultLogin = await PostJsonAsync(loginDto, $"{HostApi}login", Client);
            if (resultLogin != null && resultLogin.IsSuccessStatusCode)
            {
                var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
                var loginObject = JsonConvert.DeserializeObject<LoginResponseDto>(jsonLogin);

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginObject.AccessToken);
            }
            else
            {
                throw new Exception($"Ocorreu um erro no login do método de integração");
            }
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(object dataclass, string url, HttpClient client)
        {
            return await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(dataclass), Encoding.UTF8, "application/json"));
        }

        public void Dispose()
        {
            //MyContext.Database.EnsureDeleted();
            MyContext.Dispose();
            Client.Dispose();
        }
    }

    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });
            return config.CreateMapper();
        }

        public void Dispose() { }
    }
}