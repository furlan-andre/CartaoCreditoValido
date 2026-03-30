namespace CartaoCreditoValido.Application.Commands.CriarCartaoCredito;

public record CriarCartaoCreditoRequest
{
    public string NomeCompletoTitular { get; init; }
    public DateOnly NascimentoTitular { get; init; }
    public long NumeroCartao { get; init; }
}