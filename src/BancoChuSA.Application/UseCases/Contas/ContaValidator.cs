using BancoChuSA.Communication.Requests;
using BancoChuSA.Exception;
using FluentValidation;

namespace BancoChuSA.Application.UseCases.Contas;

public class ContaValidator : AbstractValidator<RequestCadastrarContaJson>
{
    public ContaValidator()
    {        

        RuleFor(conta => conta.NumeroConta)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.CONTA_REQUIRED);

        RuleFor(conta => conta.Saldo)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.VALOR_MAIOR_QUE_ZERO);
    }
}