using System.Data;
using Microsoft.Data.Sqlite;

namespace Record.DataAccess;

public class ChatDataAccess : IDisposable
{
    private IDbConnection _dbConnection;
    
    public ChatDataAccess()
    {
        _dbConnection = new SqliteConnection(DataAccessConstants.ConnectionString);
        _dbConnection.Open();
    }
    
    public void InsertChatWithIdAndName(long id, string name)
    {
        var command = _dbConnection.CreateCommand();
        command.CommandText = $@"
            INSERT INTO chats (chat_id, chat_name) VALUES ({id}, '{name}')
        ";
        command.ExecuteNonQuery();
    }

    public bool IsChatWithIdExists(long id)
    {
        var command = _dbConnection.CreateCommand();
        command.CommandText = $@"
            SELECT EXISTS(SELECT 1 FROM chats WHERE chat_id={id});
        ";
        using var reader = command.ExecuteReader();
        reader.Read();
        return reader.GetInt32(0) == 1;
    }

    public IEnumerable<ChatResponse> GetAllWatchedChats()
    {
        var command = _dbConnection.CreateCommand();
        command.CommandText = $@"
            SELECT chat_id, chat_name
FROM chats";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new ChatResponse()
            {
                ChatId = reader.GetInt64(0),
                ChatName = reader.GetString(1)
            };
        }
        reader.Dispose();
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}

public struct ChatResponse
{
    public long ChatId;
    public string ChatName;
}