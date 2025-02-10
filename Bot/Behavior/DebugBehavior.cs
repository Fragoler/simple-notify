using SNotefier.TelegramBot;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;

namespace SNotefier.TelegramBot.Behavior;

public sealed partial class DebugBehavior : BotBehavior
{
	private bool _firstMessage;

    protected override void OnReset(object? sender, EventArgs e)
    {
        _firstMessage = true;
    }

    public override async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cToken)
    {
		Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
			return;

		var message = update.Message;
		if (message == null)
			return;

		if (_firstMessage)
		{
			_firstMessage = false;
			await botClient.SendMessage(message.Chat, "Добро пожаловать на борт!", cancellationToken: cToken);
			return;
		}

		if (message.Text == "/reset")
		{
            _bot?.Reset();
			await botClient.SendMessage(message!.Chat, "Reset!", cancellationToken: cToken);
			return;
		}

		await botClient.SendMessage(message!.Chat, "Привет-привет!", cancellationToken: cToken);    	
     }

	public DebugBehavior(Bot bot) : base(bot)
	{
		_firstMessage = true;
	}
}