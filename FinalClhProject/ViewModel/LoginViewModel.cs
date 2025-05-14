using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FinalClhProject.Model;
using FinalClhProject.Service;
using CommunityToolkit.Mvvm.Input;
using FinalClhProject.RequestModel;
using FinalClhProject.Views;

namespace FinalClhProject.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new RelayCommand(Login);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        private async void Login()
        {
            try
            {
                var user = new LoginRequestModel
                {
                    Email = this.Email,
                    Password = this.Password
                };

                var result = _userService.LoginUser(user);

                if (result != null)
                {
                    await Shell.Current.GoToAsync(nameof(DirtyPickUpPage));

                    StatusMessage = $"Welcome back, {result.FirstName}!";
                }
                else
                {
                    StatusMessage = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Login failed: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
