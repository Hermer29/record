using System.Data;
using Microsoft.Data.Sqlite;

namespace Record.DataAccess;

public class RestoreSchema
{    
    public void Execute()
    {
        using (IDbConnection connection = new SqliteConnection(DataAccessConstants.ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS chats (
            id INTEGER PRIMARY KEY AUTOINCREMENT, 
            chat_id INTEGER NOT NULL,
            interested TEXT,
            chat_name TEXT
)";
            command.ExecuteNonQuery();
        }
        
    }
}