using Moq;
using Xunit;
using FinalClhProject.ViewModel;
using FinalClhProject.Service;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FinalClhProject.Tests
{
    public class AiRizzViewModelTests
    {
        private readonly Mock<IGroqAiService> _mockAiService;
        private readonly AiRizzViewModel _viewModel;

        public AiRizzViewModelTests()
        {
            _mockAiService = new Mock<IGroqAiService>();
            _viewModel = new AiRizzViewModel(_mockAiService.Object);
        }

        [Fact]
        public async Task SendMessage_ValidRequest_AddsUserAndAiMessages()
        {
            // Arrange
            var userMessage = "Hello, AI!";
            var aiReply = "Hi there! How can I help?";

            // Mock the AI response
            _mockAiService.Setup(service => service.AiRizzResponse(It.IsAny<string>()))
                .ReturnsAsync(aiReply);

            // Act
            _viewModel.UserRequest = userMessage;
            await _viewModel.SendMessageCommand.ExecuteAsync(null);

            // Assert
            Assert.Equal(2, _viewModel.ChatHistory.Count);
            Assert.Equal(userMessage, _viewModel.ChatHistory[0].Content);
            Assert.Equal(aiReply, _viewModel.ChatHistory[1].Content);
            Assert.Equal(Messagers.MessageRoleType.User, _viewModel.ChatHistory[0].Role);
            Assert.Equal(Messagers.MessageRoleType.Assistant, _viewModel.ChatHistory[1].Role);
        }

        [Fact]
        public async Task SendMessage_EmptyRequest_DoesNotAddMessages()
        {
            // Act
            _viewModel.UserRequest = string.Empty;
            await _viewModel.SendMessageCommand.ExecuteAsync(null);

            // Assert
            Assert.Empty(_viewModel.ChatHistory);
        }

        [Fact]
        public async Task SendMessage_NullRequest_DoesNotAddMessages()
        {
            // Act
            _viewModel.UserRequest = null;
            await _viewModel.SendMessageCommand.ExecuteAsync(null);

            // Assert
            Assert.Empty(_viewModel.ChatHistory);
        }

        [Fact]
        public async Task SendMessage_ValidRequest_ClearsUserRequestAfterSend()
        {
            // Arrange
            var userMessage = "Hello, AI!";
            var aiReply = "Hi there! How can I help?";

            _mockAiService.Setup(service => service.AiRizzResponse(It.IsAny<string>()))
                .ReturnsAsync(aiReply);

            // Act
            _viewModel.UserRequest = userMessage;
            await _viewModel.SendMessageCommand.ExecuteAsync(null);

            // Assert
            Assert.Empty(_viewModel.UserRequest);   
        }
    }
}
