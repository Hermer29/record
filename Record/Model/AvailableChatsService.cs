using Record.DataAccess;

namespace Record.Model;

public class AvailableChatsService
{
    
    public bool TryAdd(long chat, string name)
    {
        var chatModel = new WatchedChat(new ChatDataAccess());
        return chatModel.TryCreate(chat, name) == WatchedChat.ChatCreationResult.Success;
    }
}