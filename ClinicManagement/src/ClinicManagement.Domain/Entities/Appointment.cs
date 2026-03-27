using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities;

public class Appointment : BaseEntity
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int TimeSlotId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
    public bool IsFeedbackGiven { get; set; }

    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public TimeSlot? TimeSlot { get; set; }
    public Bill? Bill { get; set; }
    public Feedback? Feedback { get; set; }
}
