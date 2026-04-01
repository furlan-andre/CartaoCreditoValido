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

        var exchangeName = _topologyManager.BuildExchangeName(evento.GetType());
        var queueName = _topologyManager.BuildQueueName(evento.GetType());

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
}

