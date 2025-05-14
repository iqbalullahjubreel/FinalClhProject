using Xunit;
using Moq;
using FinalClhProject.ViewModel;
using FinalClhProject.Service;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using System;
using FluentAssertions;

namespace FinalClhProject.UnitTesting
{
    class GetUserUnitTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly GetUserViewModel _viewModel;

        public GetUserUnitTest()
        {
            _mockUserService = new Mock<IUserService>();
            _viewModel = new GetUserViewModel(_mockUserService.Object);
        }

        [Fact]
        public void OnLoadUser_UserExists_ShouldSetUserAndSuccessMessage()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            };
            _mockUserService.Setup(s => s.GetUser(1)).Returns(user);

            // Act
            _viewModel.LoadUserCommand.Execute(1);

            // Assert
            _viewModel.User.Should().NotBeNull();
            _viewModel.User.FirstName.Should().Be("John");
            _viewModel.StatusMessage.Should().Be("User John Doe retrieved successfully!");
        }

        [Fact]
        public void OnLoadUser_UserNotFound_ShouldSetUserToNullAndErrorMessage()
        {
            // Arrange
            _mockUserService.Setup(s => s.GetUser(999)).Returns((User)null);

            // Act
            _viewModel.LoadUserCommand.Execute(999);

            // Assert
            _viewModel.User.Should().BeNull();
            _viewModel.StatusMessage.Should().Be("User not found.");
        }

        [Fact]
        public void OnLoadUser_ExceptionThrown_ShouldSetErrorMessage()
        {
            // Arrange
            string error = "Database connection failed!";
            _mockUserService.Setup(s => s.GetUser(It.IsAny<int>())).Throws(new Exception(error));

            // Act
            _viewModel.LoadUserCommand.Execute(1);

            // Assert
            _viewModel.User.Should().BeNull();
            _viewModel.StatusMessage.Should().Be($"Error: {error}");
        }

        [Fact]
        public void OnLoadUser_InvalidId_ShouldReturnUserNotFound()
        {
            // Arrange
            _mockUserService.Setup(s => s.GetUser(0)).Returns((User)null);

            // Act
            _viewModel.LoadUserCommand.Execute(0);

            // Assert
            _viewModel.User.Should().BeNull();
            _viewModel.StatusMessage.Should().Be("User not found.");
        }
    }
}
