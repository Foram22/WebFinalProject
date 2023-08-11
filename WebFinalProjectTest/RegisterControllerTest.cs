
using WebFinalProject.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.WebFinalProjectTest
{
    public class RegisterControllerTest
    {

        private RegisterController registerController;



        [SetUp]
        public void SetUp()
        {
            
            registerController = new RegisterController();
            registerController.ControllerContext = new ControllerContext();
        }


        [Test]
        public async Task Register_InvalidEmail_ReturnsViewResultWithModelError()
        {
            // Arrange
            var name = "John Doe";
            var email = "invalid_email"; // Invalid email format
            var password = "password123";

            // Act
            var result = await registerController.Register(name, email, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewName, Is.EqualTo(string.Empty)); // Ensure no specific view name
            Assert.IsTrue(registerController.ModelState.ContainsKey(nameof(email))); // Check for a model state error for the email field
        }

        [Test]
        public async Task Register_EmptyName_ReturnsViewResultWithModelError()
        {
            // Arrange
            var name = ""; // Empty name
            var email = "john@example.com";
            var password = "password123";

            // Act
            var result = await registerController.Register(name, email, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewName, Is.EqualTo(string.Empty)); // Ensure no specific view name
            Assert.IsTrue(registerController.ModelState.ContainsKey(nameof(name))); // Check for a model state error for the name field
        }
    }
}

