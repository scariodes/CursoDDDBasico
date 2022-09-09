using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para criar Migrações
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONECTION"));
            //optionsBuilder.UseSqlServer("Server=localhost\\ph;Database=CursoDDD2;User Id=SA;Password=@18P04au;");

            return new MyContext(optionsBuilder.Options);
        }
    }
}