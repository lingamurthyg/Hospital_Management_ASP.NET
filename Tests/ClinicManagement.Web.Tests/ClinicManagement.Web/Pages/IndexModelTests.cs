using Xunit;
using ClinicManagement.Web.Pages;

namespace ClinicManagement.Web.Tests.Pages;

public class IndexModelTests
{
    [Fact]
    public void IndexModel_Constructor_CreatesInstance()
    {
        // Arrange & Act
        var model = new IndexModel();

        // Assert
        Assert.NotNull(model);
    }

    [Fact]
    public void IndexModel_OnGet_ExecutesWithoutError()
    {
        // Arrange
        var model = new IndexModel();

        // Act
        model.OnGet();

        // Assert - Method completes without exception
        Assert.True(true);
    }

    [Fact]
    public void IndexModel_InheritsFromPageModel()
    {
        // Arrange & Act
        var model = new IndexModel();

        // Assert
        Assert.IsAssignableFrom<Microsoft.AspNetCore.Mvc.RazorPages.PageModel>(model);
    }
}
