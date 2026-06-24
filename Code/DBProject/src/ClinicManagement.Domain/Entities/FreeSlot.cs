using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    /// <summary>
    /// Represents available time slots for doctors
    /// </summary>
    public class FreeSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FreeSlotID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Timings { get; set; } = string.Empty;

        [Required]
        public DateTime SlotDate { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation properties
        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
