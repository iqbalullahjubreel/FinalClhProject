using FinalClhProject.Service;
using FinalClhProject.ViewModel;

namespace FinalClhProject.Views;

public partial class GetAllUsersPage : ContentPage
{
    public GetAllUsersPage(IUserService userService)
    {
        InitializeComponent();
        BindingContext = new GetAllUsersViewModel(userService);
    }
}
