using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(p=> p.Id);

            builder.HasIndex(p=> p.Email).IsUnique();

            builder.Property(u=> u.Name).IsRequired().HasMaxLength(60);
            builder.Property(u=> u.Email).IsRequired().HasMaxLength(100);
        }
    }
}
