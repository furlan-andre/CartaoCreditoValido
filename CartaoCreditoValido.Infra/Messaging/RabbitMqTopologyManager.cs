using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CartaoCreditoValido.Infra.Messaging;

public sealed class RabbitMqTopologyManager
{
	private readonly RabbitMqOptions _options;

	public RabbitMqTopologyManager(IOptions<RabbitMqOptions> options)
	{
		_options = options.Value;
	}

	public void ConfigurarTopologia(Type eventType)
	{
		ConfigurarTopologia(BuildExchangeName(eventType), BuildQueueName(eventType));
	}

	public void ConfigurarTopologia(string exchangeName, string queueName)
	{
		var factory = new ConnectionFactory
		{
			HostName = _options.HostName,
			Port = _options.Port,
			UserName = _options.UserName,
			Password = _options.Password
		};

		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();

		channel.ExchangeDeclare(
			exchange: exchangeName,
			type: ExchangeType.Fanout,
			durable: true,
			autoDelete: false,
			arguments: null);

		channel.QueueDeclare(
			queue: queueName,
			durable: true,
			exclusive: false,
			autoDelete: false,
			arguments: null);

		channel.QueueBind(
			queue: queueName,
			exchange: exchangeName,
			routingKey: string.Empty);
	}

	public string BuildExchangeName(Type eventType)
	{
		var name = eventType.Name;
		if (name.EndsWith("Event", StringComparison.OrdinalIgnoreCase))
			name = name[..^5];

		var kebab = System.Text.RegularExpressions.Regex
			.Replace(name, "([a-z0-9])([A-Z])", "$1-$2")
			.ToLowerInvariant();

		return $"{_options.ExchangePrefix}.{kebab}";
	}

	public string BuildQueueName(Type eventType)
	{
		return $"{BuildExchangeName(eventType)}.queue";
	}
}

