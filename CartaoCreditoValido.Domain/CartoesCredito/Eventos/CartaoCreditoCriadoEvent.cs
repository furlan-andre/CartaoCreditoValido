namespace CartaoCreditoValido.Domain.CartoesCredito.Eventos;

public sealed record CartaoCreditoCriadoEvent(
    long Id,
    string NomeCompletoTitular,
    DateOnly NascimentoTitular,
    long NumeroCartao);

