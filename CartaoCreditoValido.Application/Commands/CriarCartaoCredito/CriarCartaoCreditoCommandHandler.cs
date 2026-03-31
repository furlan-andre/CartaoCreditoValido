using CartaoCreditoValido.Application.Services;
using CartaoCreditoValido.Domain.CartaoCredito.Validadores;
using CartaoCreditoValido.Domain.CartoesCredito.Eventos;
using CartaoCreditoValido.Domain.CartoesCredito.Entidades;
using MediatR;

namespace CartaoCreditoValido.Application.Commands.CriarCartaoCredito;

public class CriarCartaoCreditoCommandHandler : IRequestHandler<CriarCartaoCreditoCommand, CriarCartaoCreditoDto>
{
    private readonly IValidadorNumeroCartao _validadorNumeroCartao;
    private readonly ICartaoCreditoService _cartaoCreditoService;
    private readonly ICartaoCreditoEventPublisher _cartaoCreditoEventPublisher;

    public CriarCartaoCreditoCommandHandler(
        IValidadorNumeroCartao validadorNumeroCartao,
        ICartaoCreditoService cartaoCreditoService,
        ICartaoCreditoEventPublisher cartaoCreditoEventPublisher)
    {
        _validadorNumeroCartao = validadorNumeroCartao;
        _cartaoCreditoService = cartaoCreditoService;
        _cartaoCreditoEventPublisher = cartaoCreditoEventPublisher;
    }

    public async Task<CriarCartaoCreditoDto?> Handle(CriarCartaoCreditoCommand request, CancellationToken cancellationToken)
    {
        _validadorNumeroCartao.Validar(request.NumeroCartao);

        var cartao = CartaoCredito.CriarComNumeroValidado(
            request.NumeroCartao,
            request.NomeCompletoTitular,
            request.NascimentoTitular);

        var resultado = await _cartaoCreditoService.Armazenar(cartao, cancellationToken);

        if (resultado is not null)
        {
            var evento = new CartaoCreditoCriadoEvent(
                resultado.Id,
                resultado.NomeCompletoTitular,
                resultado.NascimentoTitular,
                resultado.NumeroCartao);

            await _cartaoCreditoEventPublisher.PublicarCartaoCriadoAsync(evento, cancellationToken);
        }

        return resultado;
    }
}