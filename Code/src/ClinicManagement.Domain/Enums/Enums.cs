namespace ClinicManagement.Domain.Enums;

/// <summary>
/// User type enumeration
/// </summary>
public enum UserType
{
    Patient = 1,
    Doctor = 2,
    Admin = 3
}

/// <summary>
/// Gender enumeration
/// </summary>
public enum Gender
{
    Male,
    Female,
    Other
}

/// <summary>
/// Appointment status enumeration
/// </summary>
public enum AppointmentStatus
{
    Pending = 0,
    Approved = 1,
    Completed = 2,
    Cancelled = 3
}

/// <summary>
/// Bill payment status enumeration
/// </summary>
public enum PaymentStatus
{
    Unpaid = 0,
    Paid = 1
}
