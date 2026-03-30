using CartaoCreditoValido.Application.Services;
using MediatR;

namespace CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;

public sealed class ObterCartaoCreditoPorIdQueryHandler
    : IRequestHandler<ObterCartaoCreditoPorIdQuery, ObterCartaoCreditoPorIdResponse?>
{
    private readonly ICartaoCreditoService _cartaoCreditoService;

    public ObterCartaoCreditoPorIdQueryHandler(ICartaoCreditoService cartaoCreditoService)
    {
        _cartaoCreditoService = cartaoCreditoService;
    }

    public async Task<ObterCartaoCreditoPorIdResponse?> Handle(
        ObterCartaoCreditoPorIdQuery request,
        CancellationToken cancellationToken)
    {
        var cartao = await _cartaoCreditoService.ObterCartaoCredito(request.Id, cancellationToken);

        if (cartao == null)
            return null;
        
        return new ObterCartaoCreditoPorIdResponse(
            cartao.Id,
            cartao.NomeCompletoTitular,
            cartao.NascimentoTitular,
            cartao.NumeroCartao);
    }
}