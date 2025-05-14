using CommunityToolkit.Mvvm.Input;
using FinalClhProject.Service;
using FinalClhProject.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalClhProject.Views
{
    public partial class DirtyRizzPage : ContentPage
    {
        public DirtyRizzPage(IGroqAiService aiService)
        {
            InitializeComponent();
            BindingContext = new CombinedRizzViewModel(aiService);
        }
        public async void OnLogoutAsync(object sender, EventArgs e)
        {

            try
            {
                Preferences.Clear();
                await Shell.Current.GoToAsync("///MainPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async void OnGoToSettingsAsync(object sender, EventArgs e)
        {

            try
            {
                await Shell.Current.GoToAsync("///SettingsPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async void OnGoToHomeAsync(object sender, EventArgs e)
        {

            try
            {
                await Shell.Current.GoToAsync("///MainPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    public class CombinedRizzViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> RizzLines { get; } = new();

        public IAsyncRelayCommand GenerateNormalCommand { get; }
        public IAsyncRelayCommand GenerateDirtyCommand { get; }

        private int _chatCredit = 10;
        public int ChatCredit
        {
            get => _chatCredit;
            set { _chatCredit = value; OnPropertyChanged(); }
        }

        private readonly IGroqAiService _aiService;

        public CombinedRizzViewModel(IGroqAiService aiService)
        {
            _aiService = aiService;

            GenerateNormalCommand = new AsyncRelayCommand(GenerateNormalLines);
            GenerateDirtyCommand = new AsyncRelayCommand(GenerateDirtyLines);
        }

        private async Task GenerateNormalLines()
        {

            var response = await _aiService.GenerateRizzLines("Give me 5 completely different, original, clever, and funny rizz lines. Each line should be short, bold, and unique. Do not repeat any style or structure from previous outputs. Separate each line with a newline.");

            RizzLines.Clear();
            foreach (var line in response.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                RizzLines.Add(line.Trim());

        }

        private async Task GenerateDirtyLines()
        {

            var response = await _aiService.GenerateDirtyRizzLines("Give me 5 completely different, original spicy and Flirty Rizz lines that are trending now Do not repeat any style or structure from previous outputs");
            RizzLines.Clear();
            foreach (var line in response.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                RizzLines.Add(line.Trim());
        }
       

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
