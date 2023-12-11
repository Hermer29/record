using Record.DataAccess;

namespace Record.Model.Sessions;

public class Session
{
    private readonly MessagesDataAccess _dataAccess;
    
    private string _messages;

    public Session(MessagesDataAccess dataAccess, string messages)
    {
        _dataAccess = dataAccess;
        _messages = messages;
    }

    public void Append(string message)
    {
        _messages += $"\n{message}";
    }
    
    public string GetMessagesBlob()
    {
        return _messages;
    }
}