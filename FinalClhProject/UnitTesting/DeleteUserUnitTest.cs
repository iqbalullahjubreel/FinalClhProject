using Xunit;
using Moq;
using FinalClhProject.Service;
using FinalClhProject.ViewModel;
using System;

namespace FinalClhProject.UnitTesting
{
    class DeleteUserUnitTest
    {
        [Fact]
        public void DeleteCommand_WithValidId_DeletesUserAndSetsSuccessMessage()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var viewModel = new DeleteUserViewModel(mockService.Object)
            {
                Id = 1
            };

            // Act
            viewModel.DeleteCommand.Execute(null);

            // Assert
            mockService.Verify(x => x.Delete(1), Times.Once);
            Assert.Equal("User deleted successfully!", viewModel.StatusMessage);
        }

        [Fact]
        public void DeleteCommand_WithInvalidId_ThrowsExceptionAndSetsErrorMessage()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new InvalidOperationException("User not found."));

            var viewModel = new DeleteUserViewModel(mockService.Object)
            {
                Id = 999  
            };

            // Act
            viewModel.DeleteCommand.Execute(null);

            // Assert
            mockService.Verify(x => x.Delete(999), Times.Once);
            Assert.Equal("Error: User not found.", viewModel.StatusMessage);
        }

        [Fact]
        public void DeleteCommand_WhenUnexpectedExceptionOccurs_SetsGenericErrorMessage()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception("Something went wrong"));

            var viewModel = new DeleteUserViewModel(mockService.Object)
            {
                Id = 2
            };

            // Act
            viewModel.DeleteCommand.Execute(null);

            // Assert
            Assert.Equal("Error: Something went wrong", viewModel.StatusMessage);
        }

        [Fact]
        public void DeleteCommand_WithZeroOrNegativeId_ShouldStillCallDelete()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var viewModel = new DeleteUserViewModel(mockService.Object)
            {
                Id = 0  
            };

            // Act
            viewModel.DeleteCommand.Execute(null);

            // Assert
            mockService.Verify(x => x.Delete(0), Times.Once);
            Assert.Equal("User deleted successfully!", viewModel.StatusMessage);
        }
    }
}
