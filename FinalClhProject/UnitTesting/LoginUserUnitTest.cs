// Unit Tests for Registration and Login Functionality
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using FinalClhProject.RequestModel;
using FinalClhProject.Service;
using FinalClhProject.ViewModel;
using Moq;
using System;
using Xunit;
namespace FinalClhProject.UnitTesting
{
    class LoginUserUnitTest
    {
        [Fact]
        public void LoginUser_Successful()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.LoginUser(It.IsAny<LoginRequestModel>())).Returns(new User { FirstName = "John", Email = "john@example.com" });
            var viewModel = new LoginViewModel(mockService.Object)
            {
                Email = "john@example.com",
                Password = "pass1234"
            };

            viewModel.LoginCommand.Execute(null);

            Assert.Contains("Welcome back ", viewModel.StatusMessage);
        }

        [Fact]
        public void LoginUser_InvalidCredentials()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.LoginUser(It.IsAny<LoginRequestModel>())).Returns((User)null);
            var viewModel = new LoginViewModel(mockService.Object)
            {
                Email = "wrong@example.com",
                Password = "wrongpass"
            };

            viewModel.LoginCommand.Execute(null);

            Assert.Equal("Invalid email or password.", viewModel.StatusMessage);
        }
    }
}
