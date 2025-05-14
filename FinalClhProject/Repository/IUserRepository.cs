//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.Repository
{
    public interface IUserRepository
    {
        public User AddUser(User user);
        public User FindById(int userId);
        public User FindByEmail(string userEmail);
        public List<User> GetAllUser();
        public void SaveChanges();
        public bool Exists(int id);
        public User UpdateUser(User user);
        public void DeleteUser(int userId);
    }
}
