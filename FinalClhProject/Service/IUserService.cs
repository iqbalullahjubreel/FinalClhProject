using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalClhProject.DataAccess;
//using FinalClhProject.Model;
using FinalClhProject.RequestModel;
namespace FinalClhProject.Service
{
    public interface IUserService
    {
        public void RegisterUser(RegisterUserRequestModel user);
        public User LoginUser(LoginRequestModel user);
        public User FindById(int Id);
        public User FindByEmail(string email);
        public IEnumerable<User> GetAllUser();
        public User Update(User user);
        public Task Delete(int id);
        public User GetUser(int id);
    }
}
