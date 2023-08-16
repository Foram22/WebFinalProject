using WebFinalProject.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.WebFinalProjectTest.Tests
{
    public class RoleControllerTest
    {

        private RoleController roleController;



        [Test]
        public void RolePost_ValidRole_SelectedRoleIsSet()
        {
            // Arrange
            var roleController = new RoleController();

            // Act
            var result = roleController.ViewBag as ViewResult; // Passing a valid role name

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ViewData["SelectedRole"], Is.EqualTo("Please select any role."));
        }

        // Add more tests for other scenarios if needed
    }
}

