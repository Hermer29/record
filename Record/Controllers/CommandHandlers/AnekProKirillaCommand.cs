using Record.Services;
using Record.Services.Langchain;
using Telegram.Bot.Types;

namespace Record.Controllers.CommandHandlers;

public class ReceiveRequest : IClientCommand
{
    private readonly CreateJokeAboutKirill _jokeAboutKirill;
    private readonly BotService _service;

    public ReceiveRequest(CreateJokeAboutKirill jokeAboutKirill, BotService service)
    {
        _jokeAboutKirill = jokeAboutKirill;
        _service = service;
    }
    
    public string Command => "register";
    
    public async Task Execute(Message message)
    {
        await _service.Respond(await _jokeAboutKirill.Generate());
    }
}

public interface QuestionStep
{
    
}