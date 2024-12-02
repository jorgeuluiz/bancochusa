using BancoChuSA.Application.UseCases.Transferencias;
using BancoChuSA.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Transferencias.Cadastrar;

public class CadastrarTransferenciaValidatorTests
{

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Saldo_Invalido(decimal valor)
    {
        var validator = new TransferenciaValidator();
        var request = RequestCadastrarTransferenciaJsonBuilder.Build();
        request.Valor = valor;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.VALOR_MAIOR_QUE_ZERO));

    }

    [Fact]
    public void Error_Date_Past()
    {
        var validator = new TransferenciaValidator();
        var request = RequestCadastrarTransferenciaJsonBuilder.Build();
        request.Data = DateTime.UtcNow.AddDays(-3);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DATA_PASSADO));

    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Error_NumeroConta_Empty(string nome)
    {
        var validator = new TransferenciaValidator();
        var request = RequestCadastrarTransferenciaJsonBuilder.Build();
        request.NumeroConta = nome;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CONTA_REQUIRED));

    }
}
