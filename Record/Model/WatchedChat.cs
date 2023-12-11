using Record.DataAccess;
using static Record.Model.WatchedChat.ChatCreationResult;

namespace Record.Model;

public class WatchedChat
{
    private readonly ChatDataAccess _chatDataAccess;
    
    private long _id;

    public WatchedChat(ChatDataAccess chatDataAccess)
    {
        _chatDataAccess = chatDataAccess;
    }

    public ChatCreationResult TryCreate(long id, string name)
    {
        if (_chatDataAccess.IsChatWithIdExists(id))
        {
            return ChatAlreadyExists;
        }

        _chatDataAccess.InsertChatWithIdAndName(id, name);
        _id = id;
        return Success;
    }

    public enum ChatCreationResult
    {
        Success,
        ChatAlreadyExists
    }

    public IEnumerable<ChatResponse> GetAllChats()
    {
        return _chatDataAccess.GetAllWatchedChats();
    }
}

