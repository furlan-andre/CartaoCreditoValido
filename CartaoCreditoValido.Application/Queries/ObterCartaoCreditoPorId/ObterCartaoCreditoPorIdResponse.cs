namespace CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;

public sealed record ObterCartaoCreditoPorIdResponse(
    long Id,
    string NomeCompletoTitular,
    DateOnly NascimentoTitular,
    long NumeroCartao
);