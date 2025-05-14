using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FinalClhProject.Service;

namespace FinalClhProject.ViewModel
{
    public class DirtyPickUpViewModel : INotifyPropertyChanged
    {
        private readonly IGroqAiService _aiService;

        public DirtyPickUpViewModel(IGroqAiService aiService)
        {
            _aiService = aiService;
            GenerateLinesCommand = new AsyncRelayCommand(GeneratePickUpLines);
        }

        private string _aiGeneratedLines;
        public string AiGeneratedLines
        {
            get => _aiGeneratedLines;
            set { _aiGeneratedLines = value; OnPropertyChanged(); }
        }

        public IAsyncRelayCommand GenerateLinesCommand { get; }

        private async Task GeneratePickUpLines()
        {
            try
            {
                string hardcodedInput = "Give me 5 spicy and clever pick-up lines with a flirty twist, nothing too explicit.";

                AiGeneratedLines = await _aiService.GenerateDirtyPickUpLines(hardcodedInput);
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
