using System;

namespace ClinicManagement.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Appointment
    /// </summary>
    public class AppointmentDto
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int DoctorID { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Timings { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public bool IsCompleted { get; set; }
        public string? Disease { get; set; }
        public string? Progress { get; set; }
        public string? Prescription { get; set; }
    }

    /// <summary>
    /// DTO for creating an appointment
    /// </summary>
    public class CreateAppointmentDto
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int FreeSlotID { get; set; }
    }
}
