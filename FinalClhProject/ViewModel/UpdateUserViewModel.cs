using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using FinalClhProject.Service;
namespace FinalClhProject.ViewModel
{
    public class UpdateUserViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public UpdateUserViewModel(IUserService userService)
        {
            _userService = userService;
            UpdateCommand = new Command(OnUpdateUser);
        }

        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
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

        public ICommand UpdateCommand { get; }

        private async void OnUpdateUser()
        {
            try
            {
                var updatedUser = new User
                {
                    Id = Id,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password
                };

                var result = _userService.Update(updatedUser);

                if (result != null)
                {
                    StatusMessage = "User updated successfully!";
                    await Shell.Current.GoToAsync("///SettingsPage");

                }
                else
                {
                    StatusMessage = "Failed to update user.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}