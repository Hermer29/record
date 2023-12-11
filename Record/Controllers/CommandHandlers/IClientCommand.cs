using Telegram.Bot.Types;

namespace Record.Controllers.CommandHandlers;

public interface IClientCommand
{
    public string Command { get; }

    public Task Execute(Message message);
}