using CartaoCreditoValido.Domain.CartaoCredito.Validadores;
using CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;
using CartaoCreditoValido.Domain.Commons.Exceptions;

namespace CartaoCreditoValido.Domain.CartoesCredito.Services.Validadores;

public sealed class ValidadorNumeroCartao : IValidadorNumeroCartao
{
    private readonly IReadOnlyCollection<IRegraBandeira> _regras;

    public ValidadorNumeroCartao(IEnumerable<IRegraBandeira> regras)
    {
        _regras = regras.ToList().AsReadOnly();
    }

    public void Validar(long numeroCartao)
    {
        var regra = _regras.FirstOrDefault(r => r.Suporta(numeroCartao));

        if(regra is null)
            throw new DomainException("Cartão desconhecido.");
        
        if(!regra.Valido(numeroCartao))
            throw new DomainException("Quantidade de dígitos incorreta.");
        
        if(!AlgoritmoLuhn.Validar(numeroCartao))
            throw new DomainException("Cartão inválido.");
    }
}