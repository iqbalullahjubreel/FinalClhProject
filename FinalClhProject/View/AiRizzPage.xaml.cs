using FinalClhProject.Service;
using FinalClhProject.ViewModel;

namespace FinalClhProject.Views
{
    public partial class AiRizzPage : ContentPage
    {
        private readonly AiRizzViewModel _viewModel;
        private readonly ISpeechRecognitionService _speechService;
        private readonly CancellationToken cancellationToken;
        public AiRizzPage(IGroqAiService aiService, ISpeechRecognitionService speechService)
        {
            InitializeComponent();
            _viewModel = new AiRizzViewModel(aiService);
            _speechService = speechService;
            BindingContext = _viewModel;
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

        private async void OnStartSpeechToText(object sender, EventArgs e)
        {
            try
            {
                string recognizedText = await _speechService.RecognizeSpeechAsync(cancellationToken);

                if (!string.IsNullOrWhiteSpace(recognizedText))
                {
                    _viewModel.UserRequest = recognizedText;
                    await _viewModel.SendMessageCommand.ExecuteAsync(null);  
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Speech Error", $"Could not recognize speech: {ex.Message}", "OK");
            }
        }

    }
}
