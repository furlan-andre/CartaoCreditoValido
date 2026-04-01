using System.Text;
using System.Text.Json;
using CartaoCreditoValido.Domain.CartoesCredito.Eventos;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CartaoCreditoValido.Infra.Messaging;

public sealed class RabbitMqCartaoCreditoEventPublisher : ICartaoCreditoEventPublisher
{
    private readonly RabbitMqOptions _options;
    private readonly RabbitMqTopologyManager _topologyManager;

    public RabbitMqCartaoCreditoEventPublisher(
        IOptions<RabbitMqOptions> options,
        RabbitMqTopologyManager topologyManager)
    {
        _options = options.Value;
        _topologyManager = topologyManager;
    }

    public Task PublicarCartaoCriadoAsync(CartaoCreditoCriadoEvent evento, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var exchangeName = BuildExchangeName(evento.GetType());
        var queueName = BuildQueueName(evento.GetType());

        _topologyManager.ConfigurarTopologia(exchangeName, queueName);

        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var payload = JsonSerializer.Serialize(evento);
        var body = Encoding.UTF8.GetBytes(payload);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(
            exchange: exchangeName,
            routingKey: string.Empty,
            basicProperties: properties,
            body: body);

        return Task.CompletedTask;
    }

    private string BuildExchangeName(Type eventType)
    {
        var name = eventType.Name;
        if (name.EndsWith("Event", StringComparison.OrdinalIgnoreCase))
            name = name[..^5];

        var kebab = System.Text.RegularExpressions.Regex
            .Replace(name, "([a-z0-9])([A-Z])", "$1-$2")
            .ToLowerInvariant();

        return $"{_options.ExchangePrefix}.{kebab}";
    }

    private string BuildQueueName(Type eventType)
    {
        return $"{BuildExchangeName(eventType)}.queue";
    }
}

