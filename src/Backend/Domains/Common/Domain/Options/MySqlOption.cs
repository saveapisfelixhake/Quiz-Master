using MySqlConnector;

namespace Backend.Domains.Common.Domain.Options;

public class MySqlOption
{
    public required string Host { get; init; }
    public required uint Port { get; init; }
    public required string Database { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }

    public override string ToString()
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Port = Port,
            AllowUserVariables = true,
            Database = Database,
            Password = Password,
            Pipelining = true,
            Pooling = true,
            Server = Host,
            UserID = UserName,
            UseAffectedRows = true,
            UseCompression = true
        };
        
        return builder.ToString();
    }
}