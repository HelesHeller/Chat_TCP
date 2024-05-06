using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server_TCP
{
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(string username, string password);
        Task<bool> AuthorizeUser(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            ValidateInput(username, password);

            if (await _context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower()))
            {
                throw new InvalidOperationException("User with this username already exists!");
            }


            return await CreateUser(username, password);
        }

        private void ValidateInput(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password must not be empty.");
            }
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(e => e.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<bool> CreateUser(string username, string password)
        {
            var salt = SecurityHelper.GenerateSalt(10101);
            var hashedPassword = SecurityHelper.HashPassword(password, salt, 10101, 70);

            var newUser = new User
            {
                UserName = username,
                Salt = salt,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(newUser);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // В реальном приложении здесь должен быть код логирования
                return false;
            }
        }

        public async Task<bool> AuthorizeUser(string username, string password)
        {
            // EF Core корректно обрабатывает оператор == для строковых полей
            var user = await _context.Users
                                     .FirstOrDefaultAsync(u => u.UserName == username);

            if (user != null)
            {
                var hashedPassword = SecurityHelper.HashPassword(password, user.Salt, 10101, 70);
                return hashedPassword == user.PasswordHash;
            }
            return false;
        }

    }
}
