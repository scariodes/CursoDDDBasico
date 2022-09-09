using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class UserMap:IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email)
                    .IsUnique();
            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            builder.Property(x => x.Email)
                    .HasMaxLength(100);          
        }
    } 
}