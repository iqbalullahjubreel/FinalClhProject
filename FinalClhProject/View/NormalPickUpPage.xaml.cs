using FinalClhProject.Service;
using FinalClhProject.ViewModel;

namespace FinalClhProject.Views
{
    public partial class NormalPickUpPage : ContentPage
    {
        public NormalPickUpPage(IGroqAiService aiService)
        {
            InitializeComponent();
            BindingContext = new NormalPickUpViewModel(aiService);
        }
    }
}
