namespace ClinicManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for TimeSlot operations
/// </summary>
public interface ITimeSlotService
{
    Task<IEnumerable<TimeSlotDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TimeSlotDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TimeSlotDto> CreateAsync(TimeSlotCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, TimeSlotUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimeSlotDto>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimeSlotDto>> GetAvailableSlotsAsync(int doctorId, DateTime date, CancellationToken cancellationToken = default);
}

public class TimeSlotDto
{
    public int TimeSlotID { get; set; }
    public int DoctorID { get; set; }
    public string? DoctorName { get; set; }
    public DateTime SlotDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}

public class TimeSlotCreateDto
{
    public int DoctorID { get; set; }
    public DateTime SlotDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;
}

public class TimeSlotUpdateDto
{
    public DateTime SlotDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }
}
