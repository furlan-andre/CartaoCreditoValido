using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartaoCreditoValido.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartoesCredito",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCartao = table.Column<long>(type: "bigint", nullable: false),
                    NomeCompletoTitular = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NascimentoTitular = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartoesCredito", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartoesCredito");
        }
    }
}
