using Microsoft.Net.Http.Headers;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace SNotefier.TelegramBot;

public abstract class ChainBotBehavior : BotBehavior, IChainBotBehavior
{
	protected IBotBehavior? _next;

	public void SetNext(IBotBehavior next)
	{
		_next = next;
	}

	public ChainBotBehavior(Bot bot) : base(bot)
	{
	}

}

public abstract class BotBehavior : IBotBehavior
{
	protected Bot? _bot;

	public BotBehavior(Bot bot)
	{
		_bot = bot;
		_bot.BotBehaviorReset += OnReset;
	}

    protected virtual void OnReset(object? sender, EventArgs e)
    {
    }

	public virtual async Task Start()
	{
		await Task.Yield();
	}

	public abstract Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cToken);

	public virtual async Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cToken)
	{
		Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        await Task.Yield();
	}
}

public interface IBotBehavior
{
	public Task Start();
	public Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cToken);
	public Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cToken);
}

public interface IChainBotBehavior
{
	public void SetNext(IBotBehavior next);
}