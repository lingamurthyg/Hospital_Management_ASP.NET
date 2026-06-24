using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    /// <summary>
    /// Represents a bill for an appointment
    /// </summary>
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillID { get; set; }

        [Required]
        public int AppointmentID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime BillDate { get; set; }

        public bool IsPaid { get; set; } = false;

        // Navigation properties
        [ForeignKey("AppointmentID")]
        public virtual Appointment? Appointment { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }
    }
}
