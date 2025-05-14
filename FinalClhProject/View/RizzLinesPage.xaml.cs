using FinalClhProject.Service;
using FinalClhProject.ViewModel;

namespace FinalClhProject.Views
{
    public partial class RizzLinesPage : ContentPage
    {
        public RizzLinesPage(IGroqAiService aiService)
        {
            InitializeComponent();
            BindingContext = new RizzLinesViewModel(aiService);
        }
    }
}
