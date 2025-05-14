//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.Repository
{
    public interface IGoogleSignInRepository
    {
        Task<User> SaveUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
