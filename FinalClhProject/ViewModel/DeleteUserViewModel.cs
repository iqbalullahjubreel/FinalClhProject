using FinalClhProject.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class DeleteUserViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;

    public DeleteUserViewModel(IUserService userService)
    {
        _userService = userService;
        DeleteCommand = new Command(DeleteUser);
    }

    private int _id;
    public int Id
    {
        get => _id;
        set { _id = value; OnPropertyChanged(); }
    }

    private string _statusMessage;
    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }

    public Command DeleteCommand { get; }

    private async void DeleteUser()
    {
        try
        {
            if (Id <= 0)
            {
                StatusMessage = "❌ Invalid user ID.";
                return;
            }

            await _userService.Delete(Id); 

            StatusMessage = "✅ Account successfully deleted.";
            await Application.Current.MainPage.DisplayAlert("Tapped", $"Deleting user {Id}", "OK");
            await Shell.Current.GoToAsync("///SettingsPage");
        }
        catch (InvalidOperationException ex)
        {
            StatusMessage = $"❌ {ex.Message}";
        }
        catch (Exception ex)
        {
            StatusMessage = $"⚠️ Unexpected error: {ex.Message}";
        }
    }



    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
