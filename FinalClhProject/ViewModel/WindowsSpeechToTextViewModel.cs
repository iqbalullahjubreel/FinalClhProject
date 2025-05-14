using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalClhProject.Service;
using System.Threading.Tasks;
using System.Threading;

public partial class WindowsSpeechToTextViewModel : ObservableObject
{
    private readonly ISpeechRecognitionService _speechService;

    [ObservableProperty]
    private string recognizedText;

    [ObservableProperty]
    private string toggleButtonText = "Start";

    private bool isRecording = false;

    public WindowsSpeechToTextViewModel(ISpeechRecognitionService speechService)
    {
        _speechService = speechService;
    }

    [RelayCommand]
    public async Task ToggleSpeechAsync()
    {
        if (!isRecording)
        {
            ToggleButtonText = "Pause";
            isRecording = true;
            RecognizedText = "Listening...";
            var result = await _speechService.RecognizeSpeechAsync(CancellationToken.None);
            RecognizedText = result;
            ToggleButtonText = "Resume";
        }
        else
        {
            isRecording = false;
            ToggleButtonText = "Resume";
            RecognizedText += "\n[Paused]";
        }
    }

    [RelayCommand]
    public void StopSpeech()
    {
        RecognizedText = string.Empty;
        ToggleButtonText = "Start";
        isRecording = false;
    }
}
