using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FinalClhProject.Service;

namespace FinalClhProject.ViewModel
{
    public class RizzLinesViewModel : INotifyPropertyChanged
    {
        private readonly IGroqAiService _aiService;

        public RizzLinesViewModel(IGroqAiService aiService)
        {
            _aiService = aiService;
            GenerateRizzLinesCommand = new AsyncRelayCommand(GenerateRizzLines);
            RizzLines = new ObservableCollection<string>();
        }

        public ObservableCollection<string> RizzLines { get; }

        private int _chatCredit = 10;
        public int ChatCredit
        {
            get => _chatCredit;
            set { _chatCredit = value; OnPropertyChanged(); }
        }

        public IAsyncRelayCommand GenerateRizzLinesCommand { get; }

        private async Task GenerateRizzLines()
        {
            if (ChatCredit <= 0)
            {
                RizzLines.Clear();
                RizzLines.Add("❌ Out of chat credits!");
                return;
        }
            try
            {
                string hardcodedRequest = "Give me 5 clever and funny rizz lines to impress someone on Instagram.";

                var RizzResponse = await _aiService.GenerateRizzLines(hardcodedRequest);
                RizzLines.Clear();
                foreach (var line in RizzResponse.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                {
                    RizzLines.Add(line.Trim());
                }

                ChatCredit--;
            }
            catch (Exception ex)
            {
                RizzLines.Clear();
                RizzLines.Add($"Error: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
