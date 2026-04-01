using CartaoCreditoValido.Domain.CartoesCredito.Eventos;

namespace CartaoCreditoValido.Infra.Messaging;

public sealed class RabbitMqTopologyInitializer
{
    private readonly RabbitMqTopologyManager _topologyManager;

    public RabbitMqTopologyInitializer(RabbitMqTopologyManager topologyManager)
    {
        _topologyManager = topologyManager;
    }

    public Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _topologyManager.ConfigurarTopologia(typeof(CartaoCreditoCriadoEvent));

        return Task.CompletedTask;
    }
}


