namespace Record.Services.Langchain;

public class CreateJokeAboutKirill
{
    private LangChainService _langChainService;
 
    public CreateJokeAboutKirill()
    {
        _langChainService = new LangChainService();
    }

    public Task<string> Generate()
    {
        var template = "Придумай анекдот про Кирилла";

        return _langChainService.Generate(new[] { "" }, new[] { template });
    }
}