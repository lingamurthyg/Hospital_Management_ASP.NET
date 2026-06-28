using Xunit;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicManagement.Web.Pages.Admin;

namespace ClinicManagement.Web.Tests.Pages.Admin
{
    public class AdminHomeModelTests
    {
        [Fact]
        public void Constructor_CreatesInstance()
        {
            // Arrange & Act
            var model = new AdminHomeModel();

            // Assert
            Assert.NotNull(model);
        }

        [Fact]
        public void OnGet_ExecutesSuccessfully()
        {
            // Arrange
            var model = new AdminHomeModel();

            // Act
            model.OnGet();

            // Assert - Method completes without exception
            Assert.NotNull(model);
        }

        [Fact]
        public void OnGet_ExecutesWithoutException()
        {
            // Arrange
            var model = new AdminHomeModel();

            // Act
            var exception = Record.Exception(() => model.OnGet());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void OnGet_CalledMultipleTimes_ExecutesSuccessfully()
        {
            // Arrange
            var model = new AdminHomeModel();

            // Act
            model.OnGet();
            model.OnGet();
            model.OnGet();

            // Assert
            Assert.NotNull(model);
        }

        [Fact]
        public void AdminHomeModel_InheritsFromPageModel()
        {
            // Arrange
            var model = new AdminHomeModel();

            // Assert
            Assert.IsAssignableFrom<PageModel>(model);
        }

        [Fact]
        public void AdminHomeModel_IsPublicClass()
        {
            // Arrange
            var type = typeof(AdminHomeModel);

            // Assert
            Assert.True(type.IsPublic);
        }

        [Fact]
        public void OnGet_IsPublicMethod()
        {
            // Arrange
            var type = typeof(AdminHomeModel);
            var method = type.GetMethod("OnGet");

            // Assert
            Assert.NotNull(method);
            Assert.True(method.IsPublic);
        }

        [Fact]
        public void OnGet_ReturnsVoid()
        {
            // Arrange
            var type = typeof(AdminHomeModel);
            var method = type.GetMethod("OnGet");

            // Assert
            Assert.NotNull(method);
            Assert.Equal(typeof(void), method.ReturnType);
        }

        [Fact]
        public void OnGet_HasNoParameters()
        {
            // Arrange
            var type = typeof(AdminHomeModel);
            var method = type.GetMethod("OnGet");

            // Assert
            Assert.NotNull(method);
            Assert.Empty(method.GetParameters());
        }

        [Fact]
        public void AdminHomeModel_HasParameterlessConstructor()
        {
            // Arrange
            var type = typeof(AdminHomeModel);
            var constructor = type.GetConstructor(System.Type.EmptyTypes);

            // Assert
            Assert.NotNull(constructor);
        }

        [Fact]
        public void AdminHomeModel_CanBeInstantiatedMultipleTimes()
        {
            // Arrange & Act
            var model1 = new AdminHomeModel();
            var model2 = new AdminHomeModel();
            var model3 = new AdminHomeModel();

            // Assert
            Assert.NotNull(model1);
            Assert.NotNull(model2);
            Assert.NotNull(model3);
            Assert.NotSame(model1, model2);
            Assert.NotSame(model2, model3);
        }

        [Fact]
        public void OnGet_DoesNotThrowException()
        {
            // Arrange
            var model = new AdminHomeModel();

            // Act & Assert
            var exception = Record.Exception(() => model.OnGet());
            Assert.Null(exception);
        }

        [Fact]
        public void AdminHomeModel_IsInCorrectNamespace()
        {
            // Arrange
            var type = typeof(AdminHomeModel);

            // Assert
            Assert.Equal("ClinicManagement.Web.Pages.Admin", type.Namespace);
        }
    }
}
