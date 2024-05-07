using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_TCP
{
    public class Server_Chat
    {
        private static Random random = new Random();
        private static string[] chatSpeek = {
            "Чого мовчим?",
            // другие фразы...
            "Яка рибка найбільш смішна? Риба-жартівничка."
        };

        public static string GetRandomChat()
        {
            int index = random.Next(chatSpeek.Length);
            return chatSpeek[index];
        }

        public static async Task ChatBot(TcpClient botClient)
        {
            try
            {
                NetworkStream stream = botClient.GetStream();
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // Подписываемся под именем бота и авторизуемся
                await writer.WriteLineAsync("Chat-Bot");
                await writer.WriteLineAsync("botochat"); // Предполагается, что сервер принимает и проверяет эти данные

                while (true)
                {
                    string message = GetRandomChat();
                    await writer.WriteLineAsync(message);
                    Console.WriteLine($"ChatBot sent: {message}");
                    await Task.Delay(20000); // Пауза между сообщениями
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ChatBot Exception: {e.Message}");
                botClient.Close();
            }
        }
        


        public static async Task RunBot()
        {
            TcpClient botClient = new TcpClient();
            try
            {
                await botClient.ConnectAsync("127.0.0.1", 7777);
                await ChatBot(botClient);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to connect bot: {e.Message}");
            }
        }
    }
}
