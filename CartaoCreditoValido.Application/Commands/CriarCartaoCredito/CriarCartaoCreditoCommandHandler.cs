using CartaoCreditoValido.Application.Services;
using CartaoCreditoValido.Domain.CartaoCredito.Validadores;
using CartaoCreditoValido.Domain.CartoesCredito.Entidades;
using MediatR;

namespace CartaoCreditoValido.Application.Commands.CriarCartaoCredito;

public class CriarCartaoCreditoCommandHandler : IRequestHandler<CriarCartaoCreditoCommand, CriarCartaoCreditoDto>
{
    private readonly IValidadorNumeroCartao _validadorNumeroCartao;
    private readonly ICartaoCreditoService _service;

    public CriarCartaoCreditoCommandHandler(
        IValidadorNumeroCartao validadorNumeroCartao,
        ICartaoCreditoService service)
    {
        _validadorNumeroCartao = validadorNumeroCartao;
        _service = service;
    }

    public async Task<CriarCartaoCreditoDto?> Handle(CriarCartaoCreditoCommand request, CancellationToken cancellationToken)
    {
        _validadorNumeroCartao.Validar(request.NumeroCartao);

        var cartao = CartaoCredito.CriarComNumeroValidado(
            request.NumeroCartao,
            request.NomeCompletoTitular,
            request.NascimentoTitular);

        var resultado = await _service.Armazenar(cartao, cancellationToken);

        return resultado;
    }
}