namespace CartaoCreditoValido.Application.Commands.CriarCartaoCredito;

public sealed record CriarCartaoCreditoDto(
    long Id,
    string NomeCompletoTitular,
    DateOnly NascimentoTitular,
    long NumeroCartao
);