using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Server_TCP
{
    public class Server
    {
        private static ConcurrentDictionary<string, StreamWriter> clientWriters = new ConcurrentDictionary<string, StreamWriter>();
        private static List<string> chatHistory = new List<string>();
        private static TcpListener server;

        public static async Task Main(string[] args)
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, 7777);
            server.Start();
            Console.WriteLine($"Server is running on {localAddr}:{7777}");
            Console.WriteLine("Waiting for connections...");

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine("Connected!");
                _ = HandleClientAsync(client);
            }
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            string username = null;
            try
            {
                using (var stream = client.GetStream())
                using (var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string command;
                    while ((command = await reader.ReadLineAsync()) != null)
                    {
                        if (command == "authenticate")
                        {
                            username = await reader.ReadLineAsync();
                            string password = await reader.ReadLineAsync();

                            bool isAuthenticated = await Authenticate(username, password);
                            await writer.WriteLineAsync(isAuthenticated.ToString());
                            if (!isAuthenticated)
                            {
                                Console.WriteLine($"Authentication failed for {username}");
                                return;
                            }

                            if (!clientWriters.TryAdd(username, writer))
                            {
                                await writer.WriteLineAsync("Username already in use.");
                                return;
                            }

                            Console.WriteLine($"{username} has joined the chat.");
                            await BroadcastUsers(clientWriters.Keys);
                            await SendChatHistory(writer);
                        }
                        else if (command == "request_users")
                        {
                            await BroadcastUsers(clientWriters.Keys);
                        }
                        else
                        {
                            Console.WriteLine($"{username}: {command}");
                            await BroadcastMessageAsync($"{username}: {command}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with {username}: {ex.Message}");
            }
            finally
            {
                if (username != null)
                {
                    if (clientWriters.TryRemove(username, out _))
                    {
                        Console.WriteLine($"{username} has left the chat.");
                        await BroadcastUsers(clientWriters.Keys);
                        await BroadcastMessageAsync($"{username} has left the chat.");
                    }
                }
                client.Close();
            }
        }



        private static bool UsernameExists(string username)
        {
            return clientWriters.ContainsKey(username);
        }

        private static async Task<bool> Authenticate(string username, string password)
        {
            // Здесь должен быть вызов вашего метода UserRepository.AuthorizeUser
            var userRepository = new UserRepository(new ApplicationContext());
            return await userRepository.AuthorizeUser(username, password);
        }


        private static bool IsUsernameInUse(string username)
        {
            return clientWriters.ContainsKey(username);
        }


        private static async Task BroadcastMessageAsync(string message, bool saveToHistory = true)
        {
            if (saveToHistory && chatHistory != null)
            {
                chatHistory.Add(message);
            }

            foreach (var writer in clientWriters.Values)
            {
                if (writer != null)
                {
                    try
                    {
                        await writer.WriteLineAsync(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error sending message: " + ex.Message);
                    }
                }
            }
        }

        private static async Task SendChatHistory(StreamWriter writer)
        {
            foreach (var message in chatHistory)
            {
                await writer.WriteLineAsync("HISTORY:" + message); // Добавляем префикс для истории
            }
        }





        public static async Task BroadcastUsers(IEnumerable<string> users)
        {
            string userList = "USERS:" + String.Join(",", users);
            foreach (var writer in clientWriters.Values)
            {
                await writer.WriteLineAsync(userList);
            }
        }

        public static async Task SendUserListAsync()
        {
            string userList = string.Join(",", clientWriters.Keys);
            await BroadcastMessageAsync("userlist:" + userList, saveToHistory: false);
        }

        public static void Start()
        {
            Server.Main(new string[] { }).Wait();
        }
    }
}
