using CartaoCreditoValido.Domain.Commons.Exceptions;

namespace CartaoCreditoValido.Domain.CartoesCredito.Entidades;

public class CartaoCredito
{
    public long Id { get; set; }
    public long NumeroCartao { get; set; }
    public string NomeCompletoTitular { get; set; } = string.Empty;
    public DateOnly NascimentoTitular { get; set; }

    protected CartaoCredito()
    {
    }

    private CartaoCredito(long numeroCartao, string nomeCompletoTitular, DateOnly nascimentoTitular)
    {
        if (numeroCartao <= 0)
            throw new DomainException("O número do cartão é obrigatório.");

        if (nascimentoTitular == default || nascimentoTitular > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new DomainException("Data de nascimento inválida.");

        if (string.IsNullOrWhiteSpace(nomeCompletoTitular))
            throw new DomainException("O nome completo do titular é obrigatório.");

        NumeroCartao = numeroCartao;
        NomeCompletoTitular = nomeCompletoTitular.Trim();
        NascimentoTitular = nascimentoTitular;
    }

    public static CartaoCredito CriarComNumeroValidado(
        long numeroCartaoValidado,
        string nomeCompletoTitular,
        DateOnly nascimentoTitular)
    {
        return new CartaoCredito(numeroCartaoValidado, nomeCompletoTitular, nascimentoTitular);
    }

    public void AlterarNomeCompletoTitular(string nomeCompletoTitular)
    {
        if (string.IsNullOrWhiteSpace(nomeCompletoTitular))
            throw new DomainException("O nome completo do titular é obrigatório.");

        if (nomeCompletoTitular.Length > 150)
            throw new DomainException("O nome completo do titular deve ter no máximo 150 caracteres.");

        NomeCompletoTitular = nomeCompletoTitular.Trim();
    }

    public void AlterarNascimentoTitular(DateOnly nascimentoTitular)
    {
        if (nascimentoTitular == default || nascimentoTitular > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new DomainException("Data de nascimento inválida.");

        NascimentoTitular = nascimentoTitular;
    }
}