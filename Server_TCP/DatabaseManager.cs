using System.IO;
using System;

namespace Server_TCP
{
    internal class DatabaseManager
    {

        private readonly ApplicationContext _context;
        private const string logFilePath = "server_log.txt";
        public static void CreateDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                bool databaseExists = db.Database.CanConnect();

                if (!databaseExists)
                {
                    Console.WriteLine("Database creation status: " + db.Database.EnsureCreated());
                }

                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    sw.WriteLine("Database creation status: " + db.Database.EnsureCreated());
                }

                if (!db.Users.Any())
                {
                    string salt = SecurityHelper.GenerateSalt(10101);
                    string hashedPassword = SecurityHelper.HashPassword("botochat", salt, 10101, 70);

                    db.Users.Add(new User
                    {
                        UserName = "Chat-Bot",
                        Salt = salt,
                        PasswordHash = hashedPassword
                    });
                    db.SaveChanges();
                }
            }
        }

        public DatabaseManager(ApplicationContext context)
        {
            _context = context;
        }

        public void EnsureDatabaseCreation()
        {
            if (!_context.Database.CanConnect())
            {
                bool created = _context.Database.EnsureCreated();
                Log($"Database creation status: {created}");
            }
        }

        public void InitializeDefaultUsers()
        {
            if (!_context.Users.Any())
            {
                RegisterDefaultUser("Chat-Bot", "botochat");
            }
        }

        private void RegisterDefaultUser(string username, string password)
        {
            string salt = SecurityHelper.GenerateSalt(10101);
            string hashedPassword = SecurityHelper.HashPassword(password, salt, 10101, 70);
            var user = new User { UserName = username, Salt = salt, PasswordHash = hashedPassword };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Log(string message)
        {
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine($"{message} " + DateTime.Now.ToLongTimeString());
            }
        }
    }
}
