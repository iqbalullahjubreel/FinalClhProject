using System;
using Xunit;
using Moq;
using FinalClhProject.Service;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using FinalClhProject.Repository;
using FluentAssertions;

namespace FinalClhProject.UnitTesting
{
    class UpdateUserUnitTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UpdateUserUnitTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void Update_Should_Throw_When_User_Not_Found()
        {
            // Arrange
            var userToUpdate = new User { Id = 1, Email = "test@example.com" };
            _userRepositoryMock.Setup(r => r.FindById(userToUpdate.Id)).Returns((User)null);

            // Act
            Action act = () => _userService.Update(userToUpdate);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("User not found.");
        }

        [Fact]
        public void Update_Should_Update_User_When_Found()
        {
            // Arrange
            var existingUser = new User
            {
                Id = 1,
                Email = "old@example.com",
                FirstName = "Old",
                LastName = "Name",
                PasswordHash = "oldhash",
                Decript = "oldsalt"
            };

            var userToUpdate = new User
            {
                Id = 1,
                Email = "new@example.com",
                FirstName = "New",
                LastName = "User",
                Password = "newpassword"
            };

            _userRepositoryMock.Setup(r => r.FindById(userToUpdate.Id)).Returns(existingUser);

            // Act
            var result = _userService.Update(userToUpdate);

            // Assert
            result.Email.Should().Be(userToUpdate.Email);
            result.FirstName.Should().Be(userToUpdate.FirstName);
            result.LastName.Should().Be(userToUpdate.LastName);
            result.PasswordHash.Should().NotBe("oldhash");
            result.Decript.Should().NotBe("oldsalt");
            _userRepositoryMock.Verify(r => r.UpdateUser(existingUser), Times.Once);
            _userRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_Should_Not_Change_Password_If_Empty()
        {
            // Arrange
            var existingUser = new User
            {
                Id = 1,
                Email = "existing@example.com",
                FirstName = "First",
                LastName = "Last",
                PasswordHash = "originalHash",
                Decript = "originalSalt"
            };

            var userToUpdate = new User
            {
                Id = 1,
                Email = "updated@example.com",
                FirstName = "Updated",
                LastName = "User",
                Password = ""  
            };

            _userRepositoryMock.Setup(r => r.FindById(userToUpdate.Id)).Returns(existingUser);

            // Act
            var result = _userService.Update(userToUpdate);

            // Assert
            result.PasswordHash.Should().Be("originalHash");
            result.Decript.Should().Be("originalSalt");
        }
    }
}
