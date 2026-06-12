using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class TreatmentHistoryConfiguration : IEntityTypeConfiguration<TreatmentHistory>
{
    public void Configure(EntityTypeBuilder<TreatmentHistory> builder)
    {
        builder.ToTable("TreatmentHistory");
        
        builder.HasKey(t => t.TreatmentID);
        
        builder.Property(t => t.Disease)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(t => t.Treatment)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(t => t.Prescription)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(t => t.Progress)
            .HasMaxLength(500);
        
        builder.Property(t => t.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(t => t.Patient)
            .WithMany(p => p.TreatmentHistories)
            .HasForeignKey(t => t.PatientID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
