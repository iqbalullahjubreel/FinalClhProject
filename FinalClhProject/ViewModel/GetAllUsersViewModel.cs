using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using FinalClhProject.Service;

namespace FinalClhProject.ViewModel
{
    public class GetAllUsersViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public GetAllUsersViewModel(IUserService userService)
        {
            _userService = userService;
            LoadUsersCommand = new Command(OnLoadUsers);
        }

        private List<User> _users;
        public List<User> Users
        {
            get => _users;
            set { _users = value; OnPropertyChanged(); }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoadUsersCommand { get; }

        private void OnLoadUsers()
        {
            try
            {
                Users = new List<User>(_userService.GetAllUser());
                if (Users.Count == 0)
                {
                    StatusMessage = "No users found.";
                }
                else
                {
                    StatusMessage = $"{Users.Count} users retrieved successfully!";
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