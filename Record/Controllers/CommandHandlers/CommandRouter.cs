using Record.Model.Sessions;
using Record.Services;
using Record.Services.Langchain;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Record.Controllers.CommandHandlers;

public class CommandRouter
{
    private readonly ITelegramBotClient _bot;
    private readonly Session _session;

    private List<Func<BotService, IClientCommand>> _commands = new()
    {
        //client => new EndSessionCommand(client, new LangChainService()),
        //client => new StartSessionCommand(client),
        client => new ReceiveRequest(new CreateJokeAboutKirill(), client),
    };

    public CommandRouter(ITelegramBotClient bot, Session session)
    {
        _bot = bot;
        _session = session;
    }

    public void Route(Message message)
    {
        var service = new BotService(_bot, message.Chat.Id);
        foreach (var handlerCreator in _commands)
        {
            HandleMessage(message, handlerCreator, service);
        }
    }

    private static async void HandleMessage(Message message, Func<BotService, IClientCommand> handlerCreator, BotService service)
    {
        var currentHandler = handlerCreator(service);
        if (message.Text.ToLower() == $"/{currentHandler.Command}")
        {
            await currentHandler.Execute(message);
        }
    }
}