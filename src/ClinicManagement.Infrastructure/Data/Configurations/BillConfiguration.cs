using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bill");
        
        builder.HasKey(b => b.BillID);
        
        builder.Property(b => b.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(b => b.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(b => b.Appointment)
            .WithOne(a => a.Bill)
            .HasForeignKey<Bill>(b => b.AppointmentID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Patient)
            .WithMany(p => p.Bills)
            .HasForeignKey(b => b.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Doctor)
            .WithMany(d => d.Bills)
            .HasForeignKey(b => b.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
