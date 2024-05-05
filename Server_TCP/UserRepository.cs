using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_TCP
{
    internal class UserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password must not be empty.");
            }

            // Check if user already exists, using case-insensitive comparison
            if (await _context.Users.AnyAsync(e => e.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("User with this username already exists!");
            }

            // Generate salt and hash the password
            string salt = SecurityHelper.GenerateSalt(10101);
            string hashedPassword = SecurityHelper.HashPassword(password, salt, 10101, 70);

            // Create and add the new user
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
                // Log exception details here using your logging framework
                return false;
            }
        }

        public bool AutorizeUser(string userName, string password)
        {
            var currentUser = _context.Users.FirstOrDefault(e => e.UserName.Equals(userName));
            if (currentUser != null)
            {
                string hashedPassword = SecurityHelper.HashPassword(password, currentUser.Salt, 10101, 70);
                return hashedPassword.Equals(currentUser.PasswordHash);
            }
            return false;
        }
    }
}
