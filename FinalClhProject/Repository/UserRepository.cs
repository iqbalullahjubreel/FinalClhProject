//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalClhProject.DataAccess.Data;
namespace FinalClhProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FinalClhProjDbContext _finalClhProjDbContext;
        public UserRepository(FinalClhProjDbContext finalClhProjDbContext)
        {
            _finalClhProjDbContext = finalClhProjDbContext;
        }

        public User AddUser(User user)
        {
            _finalClhProjDbContext.Users.Add(user);
            _finalClhProjDbContext.SaveChanges();
            return user;
        }

        public void DeleteUser(int userId)
        {
            var user = FindById(userId);
            if (user == null) throw new InvalidOperationException("User not found.");
            _finalClhProjDbContext.Users.Remove(user);
            _finalClhProjDbContext.SaveChanges();

        }

        public bool Exists(int id)
        {
            return _finalClhProjDbContext.Users.Any(e => e.Id == id);
        }

        public User FindByEmail(string userEmail)
        {
            var item = _finalClhProjDbContext.Users.SingleOrDefault(u => u.Email == userEmail);
            return item;
        }

        public User FindById(int userId)
        {
            User? item = _finalClhProjDbContext.Users.FirstOrDefault(u => u.Id == userId);
            return item;
        }

        public List<User> GetAllUser()
        {
            return _finalClhProjDbContext.Users.ToList();
        }

        public void SaveChanges()
        {
            _finalClhProjDbContext.SaveChanges();
        }

        public User UpdateUser(User user)
        {
            User User = FindById(user.Id);

            if (User == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            User.Email = user.Email;
            User.FirstName = user.FirstName;
            User.LastName = user.LastName;
            User.Password = user.Password;
            User.PasswordHash = user.PasswordHash;
            User.Decript = user.Decript;

            _finalClhProjDbContext.Users.Update(User);
            _finalClhProjDbContext.SaveChanges();

            return User;
        }
    }
}