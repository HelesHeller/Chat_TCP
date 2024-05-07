using Server_TCP;



public class Program
{
    public static void Main(string[] args)
    {
        using (var context = new ApplicationContext())
        {
            context.Database.EnsureCreated();
        }
    Server.Start();
        // Остальной код запуска сервера
    }
}
