namespace Record.Model.Sessions;

public class InputMessagesController
{
    private readonly Session _session;

    public InputMessagesController(Session session)
    {
        _session = session;
    }
    
    public void ReceiveMessage(string message)
    {
        _session.Append(message);
    }
}