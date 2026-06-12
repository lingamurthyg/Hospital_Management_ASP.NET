using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Department");
        
        builder.HasKey(d => d.DeptNo);
        
        builder.Property(d => d.DeptName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(d => d.Description)
            .HasMaxLength(500);
        
        builder.Property(d => d.IsActive)
            .HasDefaultValue(true);
        
        builder.Property(d => d.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasMany(d => d.Doctors)
            .WithOne(doc => doc.Department)
            .HasForeignKey(doc => doc.DeptNo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
