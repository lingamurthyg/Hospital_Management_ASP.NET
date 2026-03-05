using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PhoneNo).IsRequired().HasMaxLength(20);
        builder.Property(u => u.Gender).IsRequired().HasMaxLength(10);
        builder.Property(u => u.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(u => u.IsActive).HasDefaultValue(true);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
