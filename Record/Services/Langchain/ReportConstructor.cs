namespace Record.Services.Langchain;

public class ReportConstructor
{
    private LangChainService _langChainService;
 
    public ReportConstructor()
    {
        _langChainService = new LangChainService();
    }

    public Task<string> ConstructReport(string messageBlob)
    {
        var template = "Придумай отчёт о проделанной работе за сегодня на основе текста сообщений из чата, " +
                       "которые пользователь присылает тебе без вопросов. Нужно формулировать выполненные пункты " +
                       "отвечая на вопрос \"что сделано?\". Не нужно писать о начале и конце сессии";

        return _langChainService.Generate(new[] { template }, new[] { messageBlob });
    }
}