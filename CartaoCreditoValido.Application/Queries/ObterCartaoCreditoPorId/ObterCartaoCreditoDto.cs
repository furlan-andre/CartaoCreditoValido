namespace CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;

public sealed record ObterCartaoCreditoDto(
    long Id,
    string NomeCompletoTitular,
    DateOnly NascimentoTitular,
    long NumeroCartao
);