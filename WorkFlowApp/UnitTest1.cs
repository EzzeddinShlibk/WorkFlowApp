using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using WorkFlowApp.Controllers;
using WorkFlowApp.Models.Entities;


namespace WorkFlowApp
{
    public class UnitTest1
    {
        [Fact]
        public async Task VerifyEmail_InvalidData_ReturnsNotFoundView()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(); // Adjust constructor parameters if needed
            var controller = new AccountController();
            string userId = null;
            string token = null;

            // Act
            var result = await controller.VerifyEmail(userId, token) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(controller.NotFound), result.ViewName);
        }


    }
}
