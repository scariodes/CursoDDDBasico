using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class UfMap : IEntityTypeConfiguration<UfEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UfEntity> builder)
        {
            builder.ToTable("Uf");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Sigla)
                    .IsUnique();
        }
    }
}