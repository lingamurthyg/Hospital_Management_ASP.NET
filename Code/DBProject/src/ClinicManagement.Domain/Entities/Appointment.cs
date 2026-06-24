using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    /// <summary>
    /// Represents an appointment between a patient and doctor
    /// </summary>
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        public int FreeSlotID { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(30)]
        public string? Timings { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsCompleted { get; set; } = false;

        public bool FeedbackGiven { get; set; } = false;

        [MaxLength(30)]
        public string? Disease { get; set; }

        [MaxLength(50)]
        public string? Progress { get; set; }

        [MaxLength(60)]
        public string? Prescription { get; set; }

        // Navigation properties
        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }

        [ForeignKey("FreeSlotID")]
        public virtual FreeSlot? FreeSlot { get; set; }

        public virtual Bill? Bill { get; set; }
    }
}
