using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace FinalClhProject.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnSignupClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(RegisterPage));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }

    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoggedIn;

        public string LoginButtonText => IsLoggedIn ? "Logout" : "Login";

        public ICommand LoginOrLogoutCommand { get; }

        public MainViewModel()
        {
            LoginOrLogoutCommand = new RelayCommand(OnLoginOrLogout);
        }

        private async void OnLoginOrLogout()
        {
            if (IsLoggedIn)
            {
                Preferences.Clear();
                IsLoggedIn = false;
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
                IsLoggedIn = true;
            }

            OnPropertyChanged(nameof(LoginButtonText)); // refresh button text
        }
    }
}
