using BancoChuSA.Communication.Requests;
using BancoChuSA.Exception;
using FluentValidation;

namespace BancoChuSA.Application.UseCases.Transferencias;

public class TransferenciaValidator : AbstractValidator<RequestCadastrarTransferenciaJson>
{
    public TransferenciaValidator()
    {
        RuleFor(conta => conta.Valor)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.VALOR_MAIOR_QUE_ZERO);

        RuleFor(conta => conta.NumeroConta)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.CONTA_REQUIRED);

        RuleFor(conta => conta.Data)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(ResourceErrorMessages.DATA_PASSADO)
            .Must(SerDiaUtil)
            .WithMessage(ResourceErrorMessages.DATA_INVALIDA);
    }

    private bool SerDiaUtil(DateTime data)
    {
        if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
            return false;

        // Verifica se a data é um feriado
        if (Feriados.GetFeriados().Contains(data.Date))
            return false;

        return true;
    }
}
