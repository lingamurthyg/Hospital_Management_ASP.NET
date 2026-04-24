using Xunit;
using ClinicManagement.Domain.Entities;
using System;

namespace ClinicManagement.Domain.Tests
{
    public class PatientHistoryTests
    {
        [Fact]
        public void PatientHistory_Id_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedId = 1;

            // Act
            history.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, history.Id);
        }

        [Fact]
        public void PatientHistory_PatientId_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedPatientId = 10;

            // Act
            history.PatientId = expectedPatientId;

            // Assert
            Assert.Equal(expectedPatientId, history.PatientId);
        }

        [Fact]
        public void PatientHistory_DoctorId_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedDoctorId = 5;

            // Act
            history.DoctorId = expectedDoctorId;

            // Assert
            Assert.Equal(expectedDoctorId, history.DoctorId);
        }

        [Fact]
        public void PatientHistory_VisitDate_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedDate = DateTime.UtcNow;

            // Act
            history.VisitDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, history.VisitDate);
        }

        [Fact]
        public void PatientHistory_Diagnosis_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedDiagnosis = "Hypertension";

            // Act
            history.Diagnosis = expectedDiagnosis;

            // Assert
            Assert.Equal(expectedDiagnosis, history.Diagnosis);
        }

        [Fact]
        public void PatientHistory_Treatment_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedTreatment = "Medication prescribed";

            // Act
            history.Treatment = expectedTreatment;

            // Assert
            Assert.Equal(expectedTreatment, history.Treatment);
        }

        [Fact]
        public void PatientHistory_Prescription_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedPrescription = "Aspirin 100mg daily";

            // Act
            history.Prescription = expectedPrescription;

            // Assert
            Assert.Equal(expectedPrescription, history.Prescription);
        }

        [Fact]
        public void PatientHistory_Prescription_CanBeNull()
        {
            // Arrange
            var history = new PatientHistory();

            // Act
            history.Prescription = null;

            // Assert
            Assert.Null(history.Prescription);
        }

        [Fact]
        public void PatientHistory_CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedDate = DateTime.UtcNow;

            // Act
            history.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, history.CreatedDate);
        }

        [Fact]
        public void PatientHistory_ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var expectedDate = DateTime.UtcNow;

            // Act
            history.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, history.ModifiedDate);
        }

        [Fact]
        public void PatientHistory_ModifiedDate_CanBeNull()
        {
            // Arrange
            var history = new PatientHistory();

            // Act
            history.ModifiedDate = null;

            // Assert
            Assert.Null(history.ModifiedDate);
        }

        [Fact]
        public void PatientHistory_IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();

            // Act
            history.IsActive = true;

            // Assert
            Assert.True(history.IsActive);
        }

        [Fact]
        public void PatientHistory_Patient_ShouldSetAndGetValue()
        {
            // Arrange
            var history = new PatientHistory();
            var patient = new Patient { Id = 1, Name = "John Doe" };

            // Act
            history.Patient = patient;

            // Assert
            Assert.NotNull(history.Patient);
            Assert.Equal(patient, history.Patient);
        }

        [Fact]
        public void PatientHistory_AllProperties_ShouldBeSettable()
        {
            // Arrange
            var patient = new Patient { Id = 1, Name = "Jane Smith" };

            // Act
            var history = new PatientHistory
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 5,
                VisitDate = DateTime.UtcNow,
                Diagnosis = "Diabetes Type 2",
                Treatment = "Insulin therapy",
                Prescription = "Metformin 500mg",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow.AddDays(1),
                IsActive = true,
                Patient = patient
            };

            // Assert
            Assert.Equal(1, history.Id);
            Assert.Equal(1, history.PatientId);
            Assert.Equal(5, history.DoctorId);
            Assert.Equal("Diabetes Type 2", history.Diagnosis);
            Assert.Equal("Insulin therapy", history.Treatment);
            Assert.Equal("Metformin 500mg", history.Prescription);
            Assert.True(history.IsActive);
            Assert.NotNull(history.Patient);
        }

        [Fact]
        public void PatientHistory_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var history = new PatientHistory();

            // Assert
            Assert.Equal(string.Empty, history.Diagnosis);
            Assert.Equal(string.Empty, history.Treatment);
            Assert.False(history.IsActive);
        }
    }
}
