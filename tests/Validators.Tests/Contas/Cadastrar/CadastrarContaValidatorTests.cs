using BancoChuSA.Application.UseCases.Contas;
using BancoChuSA.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Contas.Cadastrar;

public class CadastrarContaValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new ContaValidator();
        var request = RequestCadastrarContaJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    //[Theory]
    //[InlineData("")]
    //[InlineData("      ")]
    //[InlineData(null)]
    //public void Error_Nome_Empty(string nome)
    //{
    //    var validator = new ContaValidator();
    //    var request = RequestCadastrarContaJsonBuilder.Build();
    //    request.Nome = nome;

    //    var result = validator.Validate(request);

    //    result.IsValid.Should().BeFalse();
    //    result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NOME_REQUIRED));

    //}

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Error_NumeroConta_Empty(string numeroConta)
    {
        var validator = new ContaValidator();
        var request = RequestCadastrarContaJsonBuilder.Build();
        request.NumeroConta = numeroConta;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CONTA_REQUIRED));

    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Saldo_Invalido(decimal saldo)
    {
        var validator = new ContaValidator();
        var request = RequestCadastrarContaJsonBuilder.Build();
        request.Saldo = saldo;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.VALOR_MAIOR_QUE_ZERO));

    }       

}
