using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    /// <summary>
    /// Represents a doctor in the clinic management system
    /// </summary>
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public int DeptNo { get; set; }

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public int Experience { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public int ChargesPerVisit { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Specialization { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Qualification { get; set; } = string.Empty;

        public bool Status { get; set; } = true;

        public float ReputationIndex { get; set; } = 0;

        public int PatientsTreated { get; set; } = 0;

        public int Age => DateTime.Now.Year - BirthDate.Year;

        // Navigation properties
        [ForeignKey("DeptNo")]
        public virtual Department? Department { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<FreeSlot> FreeSlots { get; set; } = new List<FreeSlot>();
    }
}
