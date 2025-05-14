using Moq;
using Xunit;
using FinalClhProject.ViewModel;
using FinalClhProject.Service;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using System.Collections.Generic;
using FluentAssertions;
using System;

namespace FinalClhProject.UnitTesting
{
    class GetAllUsersUnitTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly GetAllUsersViewModel _viewModel;

        public GetAllUsersUnitTest()
        {
            _mockUserService = new Mock<IUserService>();
            _viewModel = new GetAllUsersViewModel(_mockUserService.Object);
        }

        [Fact]
        public void OnLoadUsers_ShouldSetUsers_WhenUsersAreRetrievedSuccessfully()
        {
            // Arrange
            var users = new List<User>
        {
            new User { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new User { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
        };

            _mockUserService.Setup(service => service.GetAllUser()).Returns(users);

            // Act
            _viewModel.LoadUsersCommand.Execute(null);

            // Assert
            _viewModel.Users.Should().BeEquivalentTo(users);   
            _viewModel.StatusMessage.Should().Be("2 users retrieved successfully!");
        }

        [Fact]
        public void OnLoadUsers_ShouldSetNoUsersFound_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<User>();

            _mockUserService.Setup(service => service.GetAllUser()).Returns(users);

            // Act
            _viewModel.LoadUsersCommand.Execute(null);

            // Assert
            _viewModel.Users.Should().BeEmpty();   
            _viewModel.StatusMessage.Should().Be("No users found.");
        }

        [Fact]
        public void OnLoadUsers_ShouldSetErrorMessage_WhenErrorOccurs()
        {
            // Arrange
            var errorMessage = "Database connection error.";
            _mockUserService.Setup(service => service.GetAllUser()).Throws(new Exception(errorMessage));

            // Act
            _viewModel.LoadUsersCommand.Execute(null);

            // Assert
            _viewModel.StatusMessage.Should().Be($"Error: {errorMessage}");
        }
    }
}
