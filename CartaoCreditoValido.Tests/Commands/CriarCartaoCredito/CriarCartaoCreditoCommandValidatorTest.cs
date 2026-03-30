using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using FluentValidation.TestHelper;

namespace CartaoCreditoValido.Tests.Commands.CriarCartaoCredito;

public class CriarCartaoCreditoCommandValidatorTest
{
    private readonly CriarCartaoCreditoCommandValidator _validator = new();

    [Fact]
    public void NaoDevePermitirCommandSemNomeCompletoTitular()
    {
        // Arrange
        var command = new CriarCartaoCreditoCommand(String.Empty,
            new DateOnly(1990, 1, 1),
            1234567890123);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NomeCompletoTitular)
            .WithErrorMessage("O nome completo do titular é obrigatório.");
    }

    [Fact]
    public void NaoDevePermitirCommandComTamanhoMaiorQue150Caracteres()
    {
        // Arrange
        var nomeCompletoTitular = new string('A', 151);
        var command = new CriarCartaoCreditoCommand(nomeCompletoTitular,
            new DateOnly(1990, 1, 1),
            1234567890123);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NomeCompletoTitular)
            .WithErrorMessage("O nome completo do titular deve ter no máximo 150 caracteres.");
    }

    [Fact]
    public void NaoDevePermitirCommandComNascimentoTitularInvalido()
    {
        // Arrange
        var command = new CriarCartaoCreditoCommand("João Silva",
            new DateOnly(),
            1234567890123);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NascimentoTitular)
            .WithErrorMessage("A data de nascimento precisa ser uma data válida.");
    }

    [Fact]
    public void NaoDevePermitirCommandComNumeroCartaoInvalido()
    {
        // Arrange
        var command = new CriarCartaoCreditoCommand("João Silva",
            new DateOnly(),
            0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NumeroCartao)
            .WithErrorMessage("O número do cartão é obrigatório.");
    }

    [Fact]
    public void NaoDevePermitirCommandComNumeroCartaoMenorQue13Digitos()
    {
        // Arrange
        var command = new CriarCartaoCreditoCommand("João Silva",
            new DateOnly(),
            1234567);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NumeroCartao)
            .WithErrorMessage("O número do cartão deve possuir no mínimo 13 caracteres.");
    }

    [Fact]
    public void NaoDevePermitirCommandComNumeroCartaoMaiorQue16Digitos()
    {
        // Arrange
        var command = new CriarCartaoCreditoCommand("João Silva",
            new DateOnly(),
            12345678901234567);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NumeroCartao)
            .WithErrorMessage("O número do cartão deve possuir no máximo 16 caracteres.");
    }
}

