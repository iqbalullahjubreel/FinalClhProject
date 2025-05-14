using FinalClhProject.ViewModel;

namespace FinalClhProject.Views;

public partial class SpeechToTextPage : ContentPage
{
    public SpeechToTextPage(WindowsSpeechToTextViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
