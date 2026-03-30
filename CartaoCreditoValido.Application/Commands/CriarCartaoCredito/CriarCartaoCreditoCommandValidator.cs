using FluentValidation;

namespace CartaoCreditoValido.Application.Commands.CriarCartaoCredito;

public class CriarCartaoCreditoCommandValidator    : AbstractValidator<CriarCartaoCreditoCommand>
{
    public CriarCartaoCreditoCommandValidator()
    {
        RuleFor(x => x.NomeCompletoTitular)
            .NotEmpty()
            .WithMessage("O nome completo do titular é obrigatório.")
            .MaximumLength(150)
            .WithMessage("O nome completo do titular deve ter no máximo 150 caracteres.");

        RuleFor(x => x.NascimentoTitular)
            .NotEmpty()
            .WithMessage("A data de nascimento do titular é obrigatória.")
            .Must(data => data != default)
            .WithMessage("A data de nascimento precisa ser uma data válida.");

        RuleFor(x => x.NumeroCartao)
            .NotEmpty()
            .WithMessage("O número do cartão é obrigatório.")
            .Must(numero => numero > 0)
            .WithMessage("O número do cartão é obrigatório.")
            .Must(numero => numero.ToString().Length <= 16)
            .WithMessage("O número do cartão deve possuir no máximo 16 caracteres.")
            .Must(numero => numero.ToString().Length >= 13)
            .WithMessage("O número do cartão deve possuir no mínimo 13 caracteres.");
    }
}