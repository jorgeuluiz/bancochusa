using BancoChuSA.Communication.Requests;
using BancoChuSA.Exception;
using FluentValidation;

namespace BancoChuSA.Application.UseCases.Users;

public class CadastrarUserValidator : AbstractValidator<RequestCadastrarUserJson>
{
    public CadastrarUserValidator()
    {
        RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_REQUIRED);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_REQUIRED)
            .EmailAddress()
            .When(user => !string.IsNullOrWhiteSpace(user.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestCadastrarUserJson>());


    }
}
