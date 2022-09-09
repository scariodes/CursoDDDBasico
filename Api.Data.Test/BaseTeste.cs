using Microsoft.Extensions.DependencyInjection;
using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Teste;

public abstract class BaseTest
{
    
}

public class DbTeste: IDisposable
{
    private string _dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", "")}";
    public ServiceProvider ServiceProvider { get; private set; }

    public DbTeste()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<MyContext>(
            options => options.UseSqlServer($"Server=localhost\\ph;Database={_dataBaseName};User Id=SA;Password=@18P04au;"), 
            ServiceLifetime.Transient          
        );

        ServiceProvider = serviceCollection.BuildServiceProvider();
        using(var context = ServiceProvider.GetService<MyContext>())
        {
            context.Database.EnsureCreated();
        }
    }
       
    public void Dispose()
    {
         using(var context = ServiceProvider.GetService<MyContext>())
        {
            context.Database.EnsureDeleted();
        }
    }
}