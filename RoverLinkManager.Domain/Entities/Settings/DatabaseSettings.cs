using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RoverLinkManager.Domain.Entities.Settings;
public enum DbType
{
    [EnumMember(Value = "unknown")]
    Unknown,
    [EnumMember(Value = "sqlserver")]
    SqlServer,
    [EnumMember(Value = "postgres")]
    PostgreSql
}

public class DatabaseSettings
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("engine")]
    public string? Engine { get; set; }

    public DbType EngineType
    {
        get
        {
            return Engine switch
            {
                "sqlserver" => DbType.SqlServer,
                "postgres" => DbType.PostgreSql,
                _ => DbType.Unknown
            };
        }
        set
        {
            switch (value)
            {
                case DbType.SqlServer:
                    Engine = "sqlserver";
                    break;
                case DbType.PostgreSql:
                    Engine = "postgres";
                    break;
                default:
                    Engine = "unknown";
                    break;
            }
        }
    }

    [JsonPropertyName("host")]
    public string? Host { get; set; }

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("dbInstanceIdentifier")]
    public string? DbInstanceIdentifier { get; set; }

    [JsonPropertyName("database")]
    public string? Database { get; set; }

    public bool Encrypt { get; set; } = true;
    public int Timeout { get; set; } = 30;

    // PostgreSql connection string supported keywords = https://www.npgsql.org/doc/connection-string-parameters.html
    public string ToConnectionString()
    {
        if (EngineType == DbType.SqlServer)
            return $"Server={Host}, {Port};Database={Database};User Id={Username};Password={Password};Encrypt={Encrypt};Trusted_Connection=True;MultipleActiveResultSets=true;Timeout={Timeout}";
        else if (EngineType == DbType.PostgreSql)
            return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};Timeout={Timeout}";
        else return String.Empty;
    }
}