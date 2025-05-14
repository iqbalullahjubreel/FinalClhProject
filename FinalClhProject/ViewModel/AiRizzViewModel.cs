using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using FinalClhProject.Service;

namespace FinalClhProject.ViewModel
{
    public class AiRizzViewModel : INotifyPropertyChanged
    {
        private readonly IGroqAiService _aiService;
        public ObservableCollection<Messagers> ChatHistory { get; } = new ObservableCollection<Messagers>();

        public AiRizzViewModel(IGroqAiService aiService)
        {
            _aiService = aiService;
            SendMessageCommand = new AsyncRelayCommand(SendMessage);
        }

        private string _userRequest;
        public string UserRequest
        {
            get => _userRequest;
            set { _userRequest = value; OnPropertyChanged(); }
        }

        public IAsyncRelayCommand SendMessageCommand { get; }

        public async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(UserRequest))
                return;

            var userMessage = new Messagers
            {
                Content = UserRequest,
                Role = Messagers.MessageRoleType.User
            };

            ChatHistory.Add(userMessage);

            var aiReply = await _aiService.AiRizzResponse(UserRequest);

            var aiMessage = new Messagers
            {
                Content = aiReply,
                Role = Messagers.MessageRoleType.Assistant
            };

            ChatHistory.Add(aiMessage);

            UserRequest = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
