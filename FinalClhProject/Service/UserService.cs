using FinalClhProject.DataAccess;
using FinalClhProject.DataAccess.Data;

//using FinalClhProject.Model;
using FinalClhProject.Repository;
using FinalClhProject.RequestModel;
using System.Security.Cryptography;

namespace FinalClhProject.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        FinalClhProjDbContext FinalClhProjDbContext;
        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public User FindByEmail(string email)
        {
            return _userRepository.FindByEmail(email);
        }

        public User FindById(int Id)
        {
            return _userRepository.FindById(Id);
        }

        public IEnumerable<User> GetAllUser()
        {
            return _userRepository.GetAllUser();
        }

        public User GetUser(int id)
        {
            User stu = _userRepository.FindById(id);
            return stu;
        }

        public User LoginUser(LoginRequestModel user)
        {
            var users = _userRepository.FindByEmail(user.Email);

            if (users == null)
            {
                throw new ArgumentException("User not found.");
            }

            string hashedPassword = HashPassword(user.Password, users.Decript);

            if (users.PasswordHash.Equals(hashedPassword))
            {
                return users;
            }

            throw new ArgumentException("Invalid password.");
        }

        public void RegisterUser(RegisterUserRequestModel user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("Email and password are required");
            }

            if (_userRepository.FindByEmail(user.Email) != null)
            {
                throw new InvalidOperationException("The email is already in use. Please choose a different one.");
            }

            byte[] saltBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string saltString = Convert.ToBase64String(saltBytes);

            string hashedPassword = HashPassword(user.Password, saltString);

            User users = new User
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Password = user.Password,
                Email = user.Email,
                Decript = saltString,
                PasswordHash = hashedPassword,
            };

            _userRepository.AddUser(users);
            _userRepository.SaveChanges();
        }

        public User Update(User user)
        {
            User existingUser = _userRepository.FindById(user.Id);

            if (existingUser == null && existingUser.Id <= 0)
            {
                throw new InvalidOperationException("User not found or Invalid User Id.");
            }

            existingUser.Email = user.Email;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                string saltString = Convert.ToBase64String(salt);

                string hashedPassword = HashPassword(user.Password, saltString);

                existingUser.PasswordHash = hashedPassword;
                existingUser.Decript = saltString;
            }
            _userRepository.UpdateUser(existingUser);
            _userRepository.SaveChanges();
            return existingUser;
        }

        public async Task Delete(int id)
        {
            var user = await FinalClhProjDbContext.Users.FindAsync(id);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            FinalClhProjDbContext.Users.Remove(user);
            await FinalClhProjDbContext.SaveChangesAsync();
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            using (var rfc2898 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = rfc2898.GetBytes(32);  
                return Convert.ToBase64String(hash);
            }
        }

    }
}
