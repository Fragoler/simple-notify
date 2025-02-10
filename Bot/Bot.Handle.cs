using Telegram.Bot;
using Telegram.Bot.Types;


namespace SNotefier.TelegramBot;

public sealed partial class Bot
{
    private IBotBehavior _rootBehavior;
    public event EventHandler? BotBehaviorReset;


    public void Reset()
    {
        BotBehaviorReset?.Invoke(this, new());
    }

    private async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cToken)
    {   
        await _rootBehavior.HandleUpdate(botClient, update, cToken);
    }

    private async Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cToken)
    {
        await _rootBehavior.HandleError(botClient, exception, cToken);
    }
}