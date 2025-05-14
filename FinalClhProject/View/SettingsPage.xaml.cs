using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Storage;

namespace FinalClhProject.Views;

public partial class SettingsPage : ContentPage
{
    private const string DarkModeKey = "IsDarkMode";

    public SettingsPage()
    {
        InitializeComponent();

        ThemeSwitch.IsToggled = Preferences.Get(DarkModeKey, false);
    }

    private async void OnDeleteAccountClicked(object sender, EventArgs e)
    {
        try
        {
            string UserId = await SecureStorage.GetAsync("user_id");
            if (int.TryParse(UserId, out int currentUserId))
            {
                await Shell.Current.GoToAsync($"DeleteUserPage?userId={currentUserId}");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnEditProfileClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("///UpdateUserPage");
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

    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        bool isDarkMode = e.Value;

        Preferences.Set(DarkModeKey, isDarkMode);

        ((App)Application.Current).SetTheme(isDarkMode);  
    }
}
