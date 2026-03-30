namespace CartaoCreditoValido.Domain.CartoesCredito.Services.Validadores;

public static class AlgoritmoLuhn
{
    public static bool Validar(long numeroCartao)
    {
        var numeroCartaoString = numeroCartao.ToString();
        var soma = 0;
        var multiplicador = 1;

        for (int i = numeroCartaoString.Length - 1; i >= 0; i--)
        {
            var digito = int.Parse(numeroCartaoString[i].ToString());
            var produto = digito * multiplicador;

            if (produto > 9)
                produto -= 9;

            soma += produto;
            multiplicador = multiplicador == 1 ? 2 : 1;
        }

        return soma % 10 == 0;
    }
}