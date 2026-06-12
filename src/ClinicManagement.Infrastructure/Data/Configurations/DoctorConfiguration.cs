using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for Doctor
/// </summary>
public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");
        
        builder.HasKey(d => d.DoctorID);
        
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(d => d.Password)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(d => d.Phone)
            .IsRequired()
            .HasMaxLength(15);
        
        builder.Property(d => d.Gender)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(d => d.Address)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(d => d.Specialization)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(d => d.Qualification)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(d => d.Salary)
            .HasColumnType("decimal(18,2)");
        
        builder.Property(d => d.ChargesPerVisit)
            .HasColumnType("decimal(18,2)");
        
        builder.Property(d => d.Status)
            .HasDefaultValue(true);
        
        builder.Property(d => d.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        // Relationships
        builder.HasOne(d => d.Department)
            .WithMany(dept => dept.Doctors)
            .HasForeignKey(d => d.DeptNo)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Bills)
            .WithOne(b => b.Doctor)
            .HasForeignKey(b => b.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
