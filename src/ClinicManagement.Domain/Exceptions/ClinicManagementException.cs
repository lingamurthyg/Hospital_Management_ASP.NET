namespace ClinicManagement.Domain.Exceptions;

/// <summary>
/// Base exception for all clinic management domain exceptions
/// </summary>
public class ClinicManagementException : Exception
{
    public ClinicManagementException()
    {
    }

    public ClinicManagementException(string message)
        : base(message)
    {
    }

    public ClinicManagementException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
