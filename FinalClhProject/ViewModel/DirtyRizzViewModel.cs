using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FinalClhProject.Service;

public class DirtyRizzViewModel : INotifyPropertyChanged
{
    private readonly IGroqAiService _aiService;

    public DirtyRizzViewModel(IGroqAiService aiService)
    {
        _aiService = aiService;
        GenerateCommand = new AsyncRelayCommand(GenerateDirtyRizzLines);
        RizzLines = new ObservableCollection<string>();
    }

    public ObservableCollection<string> RizzLines { get; }

    private int _chatCredit = 10;
    public int ChatCredit
    {
        get => _chatCredit;
        set { _chatCredit = value; OnPropertyChanged(); }
    }

    public IAsyncRelayCommand GenerateCommand { get; }

    private async Task GenerateDirtyRizzLines()
    {
        if (ChatCredit <= 0)
        {
            RizzLines.Clear();
            RizzLines.Add("❌ Out of chat credits!");
            return;
        }

        try
        {
            string hardcodedRequest = "Give me 5 spicy and dirty pick-up lines to use on Tinder.";
            var result = await _aiService.GenerateDirtyRizzLines(hardcodedRequest);

            RizzLines.Clear();
            foreach (var line in result.Split('\n', StringSplitOptions.RemoveEmptyEntries))
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
