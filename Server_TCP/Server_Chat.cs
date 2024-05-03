using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Reflection.PortableExecutable;

namespace Server_TCP
{
    public class Server_Chat
    {
        static Random random = new Random();
        static string[] chatSpeek = {
        "Чого мовчим?",
        "Я чекаю на розпусту...",
        "Якісь ви нудні, піду від вас!",
        "Зараз заспіваю!",
        "Що робить курка на футболі? Кур-ка-во.",
        "Що робиться з кораблем, якщо покласти у нього багато зерна? Він стає курчатником.",
        "Що говорить борщ, коли приходить холод? 'Бррррш'",
        "Де живуть огірки? У масиві згорблених огірків.",
        "Чому корова носить кросівки? Бо їй нічого боятися!",
        "Який буває кавун? Цільний.",
        "Як називають групу з п'яти лисиць? П'ятко.",
        "Як назвати салат, який не викликає підозр? Сальат.",
        "Що говорить кава коли гріється? 'Ммм, як добре!'",
        "Яка сучасна поговорка: 'Коли краб плаче - колись він пече.'",
        "Чому велосипедисти не бояться драконів? Бо вони вміють викручуватися.",
        "Як виглядає морозний шкаф, який весело дякує? Він щасливий і морозний.",
        "Що говорить ватрушка, коли вона біжить? 'Ватруш-ш-ш!'",
        "Яка рибка найбільш настійлива? Належить.",
        "Що робить квасолі, коли їй сумно? Плаче на плече.",
        "Який краб найбільш гумористичний? Жартівливий.",
        "Що говорить шоколад, коли його додають до випічки? 'Вау, це буде смачно!'",
        "Як виглядає стіл, який любить жартувати? Веселий і впередливий.",
        "Чому гіршки не бояться висоти? Бо вони мають хороший баланс.",
        "Яка кава найбільш забавна? Жартівлива.",
        "Що говорить яблуко, коли його ріжуть? 'Не болить, я цікавий!'",
        "Як назвати мишеня, яке вміє грати на піаніно? Музикант.",
        "Що робить каштан, коли йому соромно? Поховується у листя.",
        "Чому галочка не любить сідати на одиноке дерево? Вона боїться бути одинокою.",
        "Як виглядає печиво, яке любить вирушати у подорожі? Щасливий і приготований.",
        "Що говорить булочка, коли її пекуть? 'Ох, я така гаряча!'",
        "Який кролик найбільш веселий? Смішний.",
        "Чому сонце ніколи не соромиться своєї світлоти? Бо воно завжди вище.",
        "Яка рибка найбільш смішна? Риба-жартівничка.",
        "Що говорить кавун, коли його насміхаються? 'Я ще кращий внутрішній смак!'"
        };


        public static string GetRandomChat()
        {
            int index = random.Next(chatSpeek.Length);
            return chatSpeek[index];
        }

        public static async Task ChatBot(IPAddress localAddr)
        {
            TcpClient botClient = new TcpClient();

            try
            {
                await botClient.ConnectAsync(localAddr, 7777);
                NetworkStream stream = botClient.GetStream();
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                await writer.WriteLineAsync("Chat-Bot");
                await writer.WriteLineAsync("botochat");

                string response = await reader.ReadLineAsync();
                bool authorized = bool.Parse(response);

                if (authorized)
                {

                    string botUsername = "Chat-Bot";
                    await writer.WriteLineAsync(botUsername);
                    while (true)
                    {
                        string chatSpeek = GetRandomChat();
                        await Server.BroadcastMessageAsync($"{botUsername}: {chatSpeek}");
                        await Task.Delay(20000);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ChatBot Exception: {e.Message}");
            }
            finally
            {
                botClient.Close();
            }
        }
    }
}
