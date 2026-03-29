using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
{
    public void Configure(EntityTypeBuilder<TimeSlot> builder)
    {
        builder.ToTable("TimeSlot");
        builder.HasKey(ts => ts.TimeSlotID);

        builder.Property(ts => ts.IsAvailable)
            .HasDefaultValue(true);

        builder.Property(ts => ts.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ts => ts.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(ts => ts.Doctor)
            .WithMany(d => d.TimeSlots)
            .HasForeignKey(ts => ts.DoctorID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
