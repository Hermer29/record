using LangChain.Providers;
using LangChain.Providers.OpenAI;

namespace Record.Services.Langchain;

public class LangChainService
{
    private const string ApiKey = "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";

    public async Task<string> Generate(string[] systemMessages = null, string[] userMessages = null)
    {
        using var httpClient = new HttpClient();
        var model = new Gpt35TurboModel(ApiKey, httpClient);
        var systemBlob = systemMessages.Aggregate((a, x) => a + "." + x);
        var userBlob = userMessages.Aggregate((a, x) => a + "." + x);

        var response = await model.GenerateAsync(new ChatRequest(Messages: new []
        {
            systemBlob.AsSystemMessage(),
            userBlob.AsHumanMessage()
        }));
        return response.Messages.ElementAt(2).Content;
    }
}