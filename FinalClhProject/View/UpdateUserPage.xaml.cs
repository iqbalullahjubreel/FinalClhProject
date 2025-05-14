using FinalClhProject.Service;
using FinalClhProject.ViewModel;

namespace FinalClhProject.Views
{
    public partial class UpdateUserPage : ContentPage
    {
        public UpdateUserPage(IUserService userService)
        {
            InitializeComponent();
            BindingContext = new UpdateUserViewModel(userService);
        }
    }
}
