using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_TCP
{
    public class Server
    {
         static List<StreamWriter> clientStreams = new List<StreamWriter>();
         static List<string> usernames = new List<string>();
        static List<string> chatHistory = new List<string>();

        private static TcpListener server;
        private static IPAddress localAddr = GetLocalIPAddress();

        public static async Task Main(string[] args)
        {
            try
            {
                server = new TcpListener(/*localAddr*/IPAddress.Parse("127.0.0.1"), 7777);
                server.Start();
                Console.WriteLine("Server is running on {0}:{1}", localAddr, 7777);
                Console.WriteLine("Waiting for connections...");

                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    Console.WriteLine("Connected!");
                    HandleClientAsync(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            finally
            {
                server?.Stop();
            }
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (var stream = client.GetStream())
                using (var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string username = await reader.ReadLineAsync();
                    usernames.Add(username);
                    clientStreams.Add(writer);

                    Console.WriteLine($"{username} has joined the chat.");
                    BroadcastMessageAsync($"{username} has joined the chat.");

                    string message;
                    while ((message = await reader.ReadLineAsync()) != null)
                    {
                        Console.WriteLine($"{username}: {message}");
                        BroadcastMessageAsync($"{username}: {message}");
                    }

                    // Remove the user from the chat
                    int index = clientStreams.IndexOf(writer);
                    usernames.RemoveAt(index);
                    clientStreams.RemoveAt(index);

                    Console.WriteLine($"{username} has left the chat.");
                    BroadcastMessageAsync($"{username} has left the chat.");
                }
            }
            finally
            {
                client.Close();
            }
        }

        public static async Task BroadcastMessageAsync(string message)
        {
            List<StreamWriter> streams;
            lock (clientStreams)
            {
                streams = new List<StreamWriter>(clientStreams);
            }

            chatHistory.Add(message);

            List<Task> sendTasks = new List<Task>();

            foreach (StreamWriter writer in streams)
            {
                Task sendMessageTask = writer.WriteLineAsync(message);
                sendTasks.Add(sendMessageTask);

                Task sendUserListTask = writer.WriteLineAsync(string.Join(",", usernames));
                sendTasks.Add(sendUserListTask);
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Logs.Add(new Log { Message = message });
                await db.SaveChangesAsync();
            }

            await Task.WhenAll(sendTasks);
        }


        private static IPAddress GetLocalIPAddress()
        {
            var hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ipAddress in hostAddresses)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddress;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static void Start()
        {
            IPAddress localAddr = GetLocalIPAddress();

            DatabaseManager.CreateDB();

            Server.Main(new string[] { });

            Task chatBotTask = Task.Run(() => Server_Chat.ChatBot(localAddr));

            Console.ReadLine();
        }

        
    }
}
