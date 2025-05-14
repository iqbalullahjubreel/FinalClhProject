//using FinalClhProject.Data;
//using FinalClhProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalClhProject.DataAccess.Data;
using FinalClhProject.DataAccess;


namespace FinalClhProject.Repository
{
    public class GoogleSignInRepository : IGoogleSignInRepository
    {
        private readonly FinalClhProjDbContext _context;

        public GoogleSignInRepository(FinalClhProjDbContext context)
        {
            _context = context;
        }

        public async Task<User> SaveUserAsync(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    Email = user.Email
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return newUser;
            }

            return existingUser;  
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
