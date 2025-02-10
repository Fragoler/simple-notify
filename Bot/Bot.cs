using System.Threading.Tasks;
using SNotefier.TelegramBot.Behavior;
using Telegram.Bot;
using Telegram.Bot.Polling;


namespace SNotefier.TelegramBot;

public sealed partial class Bot
{
  public string User = "";
  private TelegramBotClient _bot;
  private CancellationToken _cToken;

  public Bot(string token, string user)
  {
    User = user;
    _bot = new(token);
    Console.WriteLine($"Bot {_bot.GetMe().Result.FirstName} is running");

    var cts = new CancellationTokenSource();
    _cToken = cts.Token;

    var debug = new DebugBehavior(this);
    var auth = new AuthBehavior(this);
    auth.SetNext(debug);
    _rootBehavior = auth;
  }

  public void Start()
  {
    var receiverOptions = new ReceiverOptions
    {
        AllowedUpdates = { }, // receive all update types
    };

    _bot.StartReceiving(
        HandleUpdate,
        HandleError,
        receiverOptions,
        _cToken
    );
    _rootBehavior.Start().Wait();
  }
}