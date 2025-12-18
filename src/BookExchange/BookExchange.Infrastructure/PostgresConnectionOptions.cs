namespace BookExchange.Infrastructure;

public sealed class PostgresConnectionOptions
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;

    public string AsConnectionString()
    {
        // Формируем строку для подключения к базе
        return $"Host=localhost;Port={Port};Username={UserName};Password={Password};Database={Database}";
    }
}