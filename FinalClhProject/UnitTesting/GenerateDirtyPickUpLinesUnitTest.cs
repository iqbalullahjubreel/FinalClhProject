using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;
using FinalClhProject.Service;
using FinalClhProject.ViewModel;
using GroqSharp.Models;
using GroqSharp;

namespace FinalClhProject.UnitTesting
{
    class GenerateDirtyPickUpLinesUnitTest
    { 
        [Fact]
        public async Task GenerateDirtyPickUpLines_ShouldReturnResponse_WhenRequestIsValid()
        {
            // Arrange
            var request = "Tell me something spicy.";
            var expectedResponse = "Here are 5 spicy lines!";
            var mockClient = new Mock<IGroqClient>();

            mockClient.Setup(x => x.CreateChatCompletionAsync(It.IsAny<Message[]>()))
                      .ReturnsAsync(expectedResponse);

            var aiService = new GroqAiService(mockClient.Object);

            // Act
            var result = await aiService.GenerateDirtyPickUpLines(request);

            // Assert
            result.Should().Be(expectedResponse);
        }

        [Fact]
        public async Task GenerateDirtyPickUpLines_ShouldThrowException_WhenRequestIsNull()
        {
            // Arrange
            var mockClient = new Mock<IGroqClient>();
            var aiService = new GroqAiService(mockClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => aiService.GenerateDirtyPickUpLines(null));
        }

        [Fact]
        public async Task GenerateDirtyPickUpLines_ShouldReturnErrorMessage_OnException()
        {
            // Arrange
            var request = "Tell me a line.";
            var mockClient = new Mock<IGroqClient>();

            mockClient.Setup(x => x.CreateChatCompletionAsync(It.IsAny<Message[]>()))
                      .ThrowsAsync(new Exception("Internal API error"));

            var aiService = new GroqAiService(mockClient.Object);

            // Act
            var result = await aiService.GenerateDirtyPickUpLines(request);

            // Assert
            result.Should().Contain("An error occurred");
        }
 
        [Fact]
        public async Task DirtyPickUpViewModel_ShouldSetAiGeneratedLines_WhenServiceReturnsText()
        {
            // Arrange
            var fakeResponse = "Here's your flirty text!";
            var mockService = new Mock<IGroqAiService>();
            mockService.Setup(s => s.GenerateDirtyPickUpLines(It.IsAny<string>()))
                       .ReturnsAsync(fakeResponse);

            var viewModel = new DirtyPickUpViewModel(mockService.Object);

            // Act
            await viewModel.GenerateLinesCommand.ExecuteAsync(null);

            // Assert
            viewModel.AiGeneratedLines.Should().Be(fakeResponse);
        }

        [Fact]
        public async Task DirtyPickUpViewModel_ShouldHandleServiceException_Gracefully()
        {
            // Arrange
            var mockService = new Mock<IGroqAiService>();
            mockService.Setup(s => s.GenerateDirtyPickUpLines(It.IsAny<string>()))
                       .ThrowsAsync(new Exception("Service crash"));

            var viewModel = new DirtyPickUpViewModel(mockService.Object);

            // Act
            await viewModel.GenerateLinesCommand.ExecuteAsync(null);

            // Assert
            viewModel.AiGeneratedLines.Should().Contain("Error: Service crash");
        }
    }
}
