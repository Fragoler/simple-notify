using Microsoft.AspNetCore.Mvc.Diagnostics;
using SNotefier.TelegramBot;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;

namespace SNotefier.TelegramBot.Behavior;

public sealed partial class AuthBehavior : ChainBotBehavior
{
    public override async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cToken)
    {
		if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
			return;
	    
		var message = update.Message;
		if (message == null)
			return;

		if (message.From?.Username != _bot?.User || _bot?.User == null)
		{
			await botClient.SendMessage(message.Chat, "К сожалению, вы не владелец бота, но спасибо, что попытались!", cancellationToken: cToken);
			return;
		}

		_next?.HandleUpdate(botClient, update, cToken);
    }

	public AuthBehavior(Bot bot) : base(bot)
	{
	}
}