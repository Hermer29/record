using Telegram.Bot;

namespace Record.Services;

public class BotService
{
    private readonly ITelegramBotClient _botClient;
    private readonly long _chatId;

    public BotService(ITelegramBotClient botClient, long chatId)
    {
        _botClient = botClient;
        _chatId = chatId;
    }

    public async Task Respond(string message)
    {
        await _botClient.SendTextMessageAsync(_chatId, message);
    }
}