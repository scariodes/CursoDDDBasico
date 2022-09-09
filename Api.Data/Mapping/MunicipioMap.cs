using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class MunicipioMap : IEntityTypeConfiguration<MunicipioEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MunicipioEntity> builder)
        {
            builder.ToTable("Municipio");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.CodIBGE)
                    .IsUnique();

            builder.HasOne(u => u.Uf)
                    .WithMany(m => m.Municipios);        
        }
    }
}