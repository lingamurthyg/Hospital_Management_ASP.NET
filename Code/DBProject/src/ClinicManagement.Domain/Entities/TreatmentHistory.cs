using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    /// <summary>
    /// Represents treatment history for patients
    /// </summary>
    public class TreatmentHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        public DateTime TreatmentDate { get; set; }

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
    }
}
