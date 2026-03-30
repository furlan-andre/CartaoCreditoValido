using CartaoCreditoValido.Domain.CartoesCredito.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CartaoCreditoValido.Infra.Data
{
    public class CartaoCreditoContext : DbContext
    {
        public CartaoCreditoContext(DbContextOptions<CartaoCreditoContext> options)
            : base(options)
        {
        }

        public DbSet<CartaoCredito> CartoesCredito { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureCartaoCredito(modelBuilder);
        }

        private static void ConfigureCartaoCredito(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartaoCredito>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.NumeroCartao)
                    .IsRequired();

                entity.Property(e => e.NomeCompletoTitular)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NascimentoTitular)
                    .IsRequired();

                entity.ToTable("CartoesCredito");
            });
        }
    }
}
