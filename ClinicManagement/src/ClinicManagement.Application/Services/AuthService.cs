using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public interface IAuthService
{
    Task<LoginResultDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default);
    Task<LoginResultDto> RegisterAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken = default);
}

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository userRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IStaffRepository staffRepository,
        IMapper mapper,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _staffRepository = staffRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LoginResultDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _userRepository.ValidateLoginAsync(loginDto.Email, loginDto.Password, cancellationToken);

            if (user == null)
            {
                var emailExists = await _userRepository.EmailExistsAsync(loginDto.Email, cancellationToken);
                return new LoginResultDto
                {
                    Success = false,
                    Message = emailExists ? "Incorrect password" : "Email not found"
                };
            }

            _logger.LogInformation("User {UserId} logged in successfully", user.Id);

            return new LoginResultDto
            {
                Success = true,
                Message = "Login successful",
                UserId = user.Id,
                UserType = user.UserType,
                Name = user.Name
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email {Email}", loginDto.Email);
            return new LoginResultDto
            {
                Success = false,
                Message = "An error occurred during login"
            };
        }
    }

    public async Task<LoginResultDto> RegisterAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var emailExists = await _userRepository.EmailExistsAsync(userCreateDto.Email, cancellationToken);
            if (emailExists)
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            var user = _mapper.Map<User>(userCreateDto);
            user = await _userRepository.AddAsync(user, cancellationToken);

            if (user.UserType == 1)
            {
                var patient = new Patient
                {
                    UserId = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    CreatedBy = "System"
                };
                await _patientRepository.AddAsync(patient, cancellationToken);
            }

            _logger.LogInformation("User {UserId} registered successfully", user.Id);

            return new LoginResultDto
            {
                Success = true,
                Message = "Registration successful",
                UserId = user.Id,
                UserType = user.UserType,
                Name = user.Name
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for email {Email}", userCreateDto.Email);
            return new LoginResultDto
            {
                Success = false,
                Message = "An error occurred during registration"
            };
        }
    }
}
