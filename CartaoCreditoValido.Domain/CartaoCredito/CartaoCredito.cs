namespace CartaoCreditoValido.Domain.CartaoCredito
{
    public class CartaoCredito
    {
        long Id { get; set; }
        public int Numero { get; set; }
        public string NomeCompletoTitular { get; set; }
        public DateTime NascimentoTitular { get; set; }
    }
}
