﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
if (token == null)
    return;

var bot = new TelegramBotClient(token);
var me = await bot.GetMe();
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running...");
await Task.Delay(-1);

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text is null) return;	// we only handle Text messages here
    Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
    // let's echo back received text in the chat
    await bot.SendMessage(msg.Chat, $"{msg.From} said: {msg.Text}");
}