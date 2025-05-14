using FinalClhProject.ViewModel;
using FinalClhProject.Service;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalClhProject.Views
{
    public partial class DirtyPickUpPage : ContentPage
    {
        public DirtyPickUpPage(IGroqAiService aiService)
        {
            InitializeComponent();
            BindingContext = new CombinedPickupViewModel(aiService);
        }

        private async void OnRizzLinesClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(DirtyRizzPage));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
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
        private async void OnChatWithJobbaAiClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AiRizzPage));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    public class CombinedPickupViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> RizzLines { get; } = new();

        public IAsyncRelayCommand GenerateNormalLinesCommand { get; }
        public IAsyncRelayCommand GenerateDirtyLinesCommand { get; }

        private readonly IGroqAiService _aiService;

        public CombinedPickupViewModel(IGroqAiService aiService)
        {
            _aiService = aiService;

            GenerateNormalLinesCommand = new AsyncRelayCommand(GenerateNormalPickUpLines);
            GenerateDirtyLinesCommand = new AsyncRelayCommand(GenerateDirtyPickUpLines);
        }

        private async Task GenerateNormalPickUpLines()
        {

            var response = await _aiService.GenerateNormalPickUpLines("Generate exactly 5 short, spicy, and clever pick-up lines. Don't include explanations or extra text — just list the pick-up lines");
            //await Application.Current.MainPage.DisplayAlert("Response", response, "OK");

            RizzLines.Clear();
            foreach (var line in response.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                RizzLines.Add(line.Trim());
            OnPropertyChanged(nameof(RizzLines));
        }

        private async Task GenerateDirtyPickUpLines()
        {

            var response = await _aiService.GenerateDirtyPickUpLines("Generate exactly 5 short, spicy, and clever Dirty pick-up lines. Don't include explanations or extra text — just list the Flirty pick-up lines");
            RizzLines.Clear();
            foreach (var line in response.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                RizzLines.Add(line.Trim());
            OnPropertyChanged(nameof(RizzLines));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
