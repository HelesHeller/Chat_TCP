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
        public bool RegisterUser(string username, string password)
        {
            if (_context.Users.Any(e => e.UserName == username))
            {
                Console.WriteLine("User with this username alredy exists!");
                return false;
            }
            string salt = SecurityHelper.GenerateSalt(10101);
            string hashedPassword = SecurityHelper.HashPassword(password, salt, 10101, 70);
            _context.Users.Add(new User
            {
                UserName = username,
                Salt = salt,
                PasswordHash = hashedPassword
            });
            _context.SaveChanges();
            Console.WriteLine($"Create user: {username}");
            return true;
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
