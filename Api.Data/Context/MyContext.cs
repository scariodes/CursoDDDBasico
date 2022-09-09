using Api.Data.Mapping;
using Api.Data.Seeds;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

            modelBuilder.Entity<UfEntity>(new UfMap().Configure);
            modelBuilder.Entity<MunicipioEntity>(new MunicipioMap().Configure);
            modelBuilder.Entity<CepEntity>(new CepMap().Configure);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity{
                    Id = Guid.NewGuid(),
                    Name = "Adminitrador",
                    Email = "scariodes1895@gmail.com",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                }
            );
            
            UfSeeds.Ufs(modelBuilder);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UfEntity> Ufs { get; set; }
        public DbSet<MunicipioEntity> Municipios { get; set; }
        public DbSet<CepEntity> Ceps { get; set; }
    }
}