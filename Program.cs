using SNotefier.TelegramBot;

namespace SNotefier
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Start(args);
        }

        internal static void Start(string[] args)
        {
            var bot = new Bot(GetBotToken(), GetBotUser());
                        
            bot.Start();

            Task.Delay(-1).Wait();
        }

        private static string GetBotToken()
        {
            var token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
            if (token == null)
                throw new SystemException("Can't find Bot token in enviroment variables");
        
            return token;
        }

        private static string GetBotUser()
        {
            var user = Environment.GetEnvironmentVariable("BOT_USER");
            if (user == null)
                throw new SystemException("Can't find Bot user in enviroment variables");
        
            return user;
        }
    }
}