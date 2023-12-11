using System.Data;
using Microsoft.Data.Sqlite;

namespace Record.DataAccess;

public class FeedbackInterestedDataAccess : IDisposable
{
    private IDbConnection _connection = new SqliteConnection(DataAccessConstants.ConnectionString);

    public FeedbackInterestedDataAccess()
    {
        _connection.Open();
    }
    
    public string[] GetInterestedForChat(long chatId)
    {
        var command = _connection.CreateCommand();
        command.CommandText = $@"SELECT interested
FROM chats
WHERE chat_id={chatId}";
        var reader = command.ExecuteReader();
        reader.Read();
        return reader.GetString(0).Split(' ');
    }

    public void SetInterestedForChat(long chatId, params string[] interested)
    {
        var command = _connection.CreateCommand();
        var concated = interested.Select(x => x.Trim('@')).Aggregate((a, x) =>
        {
            return a + " " + x;
        });
        command.CommandText = $@"UPDATE chats
SET interested = {concated}
FROM chats
WHERE chat_id={chatId}";
        command.ExecuteNonQuery();
    }
    
    
    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}