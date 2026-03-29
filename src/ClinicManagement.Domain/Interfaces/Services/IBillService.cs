namespace ClinicManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for Bill operations
/// </summary>
public interface IBillService
{
    Task<IEnumerable<BillDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BillDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<BillDto> CreateAsync(BillCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, BillUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<BillDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<IEnumerable<BillDto>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<BillDto?> GetByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken = default);
}

public class BillDto
{
    public int BillID { get; set; }
    public int PatientID { get; set; }
    public string? PatientName { get; set; }
    public int AppointmentID { get; set; }
    public decimal Amount { get; set; }
    public DateTime BillDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}

public class BillCreateDto
{
    public int PatientID { get; set; }
    public int AppointmentID { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}

public class BillUpdateDto
{
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
}
