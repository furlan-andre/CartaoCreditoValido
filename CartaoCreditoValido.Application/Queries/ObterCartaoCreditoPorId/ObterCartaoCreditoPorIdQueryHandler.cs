using CartaoCreditoValido.Application.Services;
using MediatR;

namespace CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;

public sealed class ObterCartaoCreditoPorIdQueryHandler
    : IRequestHandler<ObterCartaoCreditoPorIdQuery, ObterCartaoCreditoDto?>
{
    private readonly ICartaoCreditoService _cartaoCreditoService;

    public ObterCartaoCreditoPorIdQueryHandler(ICartaoCreditoService cartaoCreditoService)
    {
        _cartaoCreditoService = cartaoCreditoService;
    }

    public async Task<ObterCartaoCreditoDto?> Handle(
        ObterCartaoCreditoPorIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _cartaoCreditoService.ObterCartaoCredito(request.Id, cancellationToken);

        return response;
    }
}