﻿using System;
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
                    // Читаем команду от клиента
                    string command = await reader.ReadLineAsync();

                    if (command == "authenticate")
                    {
                        username = await reader.ReadLineAsync();
                        string password = await reader.ReadLineAsync();

                        // Обработка аутентификации
                        bool isAuthenticated = await Authenticate(username, password);
                        await writer.WriteLineAsync(isAuthenticated.ToString());
                        if (!isAuthenticated)
                        {
                            return; // Завершаем соединение, если аутентификация не удалась
                        }
                    }

                    // Проверяем, не используется ли уже имя пользователя
                    if (UsernameExists(username))
                    {
                        await writer.WriteLineAsync("Username already in use. Please try a different one.");
                        return; // Завершаем соединение, если имя пользователя уже используется
                    }

                    // Добавляем пользователя в словарь активных соединений
                    if (!clientWriters.TryAdd(username, writer))
                    {
                        await writer.WriteLineAsync("Failed to add username, please retry.");
                        return;
                    }

                    Console.WriteLine($"{username} has joined the chat.");
                    await BroadcastMessageAsync($"{username} has joined the chat.");

                    // Обработка входящих сообщений
                    string message;
                    while ((message = await reader.ReadLineAsync()) != null)
                    {
                        Console.WriteLine($"{username}: {message}");
                        await BroadcastMessageAsync($"{username}: {message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with {username}: {ex.Message}");
            }
            finally
            {
                if (username != null && clientWriters.TryRemove(username, out _))
                {
                    Console.WriteLine($"{username} has left the chat.");
                    await BroadcastMessageAsync($"{username} has left the chat.");
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


        public static async Task BroadcastMessageAsync(string message)
        {
            chatHistory.Add(message);
            var tasks = new List<Task>();

            foreach (var writer in clientWriters.Values)
            {
                tasks.Add(writer.WriteLineAsync(message));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error broadcasting message: {ex.Message}");
            }
        }

        public static void Start()
        {
            Server.Main(new string[] { }).Wait();
        }
    }
}
