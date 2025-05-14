using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using FinalClhProject.Service;
using System.Windows.Input;

namespace FinalClhProject.ViewModel
{
    public class GetUserViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public GetUserViewModel(IUserService userService)
        {
            _userService = userService;
            LoadUserCommand = new Command<int>(OnLoadUser);
        }

        private User _user;
        public User User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(); }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoadUserCommand { get; }

        private void OnLoadUser(int id)
        {
            try
            {
                User = _userService.GetUser(id);
                if (User == null)
                {
                    StatusMessage = "User not found.";
                }
                else
                {
                    StatusMessage = $"User {User.FirstName} {User.LastName} retrieved successfully!";
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