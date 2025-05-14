using Xunit;
using Moq;
using FinalClhProject.Service;
using FinalClhProject.Repository;
using FinalClhProject.RequestModel;
//using FinalClhProject.Model;  
using FinalClhProject.DataAccess;
using System.Security.Cryptography;

namespace FinalClhProject.UnitTesting
{
    class RegisterUserUnitTest
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserService _service;

        public RegisterUserUnitTest()
        {
            _mockRepo = new Mock<IUserRepository>();
            _service = new UserService(_mockRepo.Object);
        }

        [Fact]
        public void RegisterUser_WithValidData_CreatesUser()
        {
            // Arrange
            var request = new RegisterUserRequestModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = "securepassword"
            };

            _mockRepo.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns((User)null);

            // Act
            _service.RegisterUser(request);

            // Assert
            _mockRepo.Verify(x => x.AddUser(It.Is<User>(u =>
                u.Email == request.Email &&
                u.FirstName == request.FirstName &&
                u.LastName == request.LastName &&
                !string.IsNullOrEmpty(u.PasswordHash)
            )), Times.Once);

            _mockRepo.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void RegisterUser_WithMissingEmail_ThrowsArgumentException()
        {
            var request = new RegisterUserRequestModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "",
                Password = "pass"
            };

            Assert.Throws<ArgumentException>(() => _service.RegisterUser(request));
        }

        [Fact]
        public void RegisterUser_WithExistingEmail_ThrowsInvalidOperationException()
        {
            var request = new RegisterUserRequestModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "existing@example.com",
                Password = "pass"
            };

            _mockRepo.Setup(r => r.FindByEmail("existing@example.com")).Returns(new User());

            Assert.Throws<InvalidOperationException>(() => _service.RegisterUser(request));
        }
    }
}
