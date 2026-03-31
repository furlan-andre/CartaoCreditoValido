namespace CartaoCreditoValido.Infra.Messaging;

public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMq";

    public string HostName { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; } 
    public string ExchangePrefix { get; set; }
}

