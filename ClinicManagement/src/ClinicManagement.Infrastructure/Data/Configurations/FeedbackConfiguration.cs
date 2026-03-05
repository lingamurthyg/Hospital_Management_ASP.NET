using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedbacks");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Message).IsRequired();
        builder.Property(f => f.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(f => f.IsActive).HasDefaultValue(true);

        builder.HasOne(f => f.Patient)
            .WithMany(p => p.Feedbacks)
            .HasForeignKey(f => f.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
