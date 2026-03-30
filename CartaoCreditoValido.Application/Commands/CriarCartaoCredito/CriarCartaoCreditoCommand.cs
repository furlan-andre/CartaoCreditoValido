using MediatR;

namespace CartaoCreditoValido.Application.Commands.CriarCartaoCredito;

public sealed record CriarCartaoCreditoCommand(
    string NomeCompletoTitular,
    DateOnly NascimentoTitular,
    long NumeroCartao) : IRequest<long>;