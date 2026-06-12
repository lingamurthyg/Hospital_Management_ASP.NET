using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class OtherStaffConfiguration : IEntityTypeConfiguration<OtherStaff>
{
    public void Configure(EntityTypeBuilder<OtherStaff> builder)
    {
        builder.ToTable("OtherStaff");
        
        builder.HasKey(s => s.StaffID);
        
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.Phone)
            .IsRequired()
            .HasMaxLength(15);
        
        builder.Property(s => s.Gender)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(s => s.Designation)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(s => s.Qualification)
            .HasMaxLength(200);
        
        builder.Property(s => s.Salary)
            .HasColumnType("decimal(18,2)");
        
        builder.Property(s => s.IsActive)
            .HasDefaultValue(true);
        
        builder.Property(s => s.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
