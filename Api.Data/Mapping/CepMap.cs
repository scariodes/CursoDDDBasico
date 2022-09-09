using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class CepMap : IEntityTypeConfiguration<CepEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CepEntity> builder)
        {
            builder.ToTable("Cep");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Cep);

            builder.HasOne(c => c.Municipio)
                    .WithMany(m => m.Ceps);        
        }
    }
}