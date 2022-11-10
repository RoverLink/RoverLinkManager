using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoverLinkManager.Infrastructure.Secrets.AWS.Models;
public enum DbType
{
    SqlServer,
    PostgreSql
}

public class DbCredentialsSecret
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("engine")]
    public string? Engine { get; set; }

    [JsonPropertyName("host")]
    public string? Host { get; set; }

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("dbInstanceIdentifier")]
    public string? DbInstanceIdentifier { get; set; }

    [JsonPropertyName("database")]
    public string? Database { get; set; }

    // PostgreSql connection string supported keywords = https://www.npgsql.org/doc/connection-string-parameters.html
    public string ToConnectionString()
    {
        if (Engine == "sqlserver")
            return $"Server={Host}, {Port};Database={Database};User Id={Username};Password={Password};Encrypt=true;Trusted_Connection=True;MultipleActiveResultSets=true;Timeout=30";
        else if (Engine == "postgres")
            return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};Timeout=30";
        else return String.Empty;
    }
}