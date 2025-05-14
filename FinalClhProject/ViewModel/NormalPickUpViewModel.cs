using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FinalClhProject.Service;

namespace FinalClhProject.ViewModel
{
    public class NormalPickUpViewModel : INotifyPropertyChanged
    {
        private readonly IGroqAiService _aiService;

        public NormalPickUpViewModel(IGroqAiService aiService)
        {
            _aiService = aiService;
            GenerateNormalLinesCommand = new AsyncRelayCommand(GeneratePickUpLines);
        }

        private string _aiGeneratedLines;
        public string AiGeneratedLines
        {
            get => _aiGeneratedLines;
            set { _aiGeneratedLines = value; OnPropertyChanged(); }
        }

        public IAsyncRelayCommand GenerateNormalLinesCommand { get; }

        private async Task GeneratePickUpLines()
        {
            try
            {
                string hardcodedInput = "Generate 5 cute and wholesome pick-up lines someone could use at a coffee shop.";

                AiGeneratedLines = await _aiService.GenerateNormalPickUpLines(hardcodedInput);
            }
            catch (Exception ex)
            {
                AiGeneratedLines = $"Error: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
