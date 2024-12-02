using BancoChuSA.Application.UseCases.Users;
using BancoChuSA.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Users.Cadastrar;

public class CadastrarUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new CadastrarUserValidator();
        var request = RequestCadastrarUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("           ")]
    [InlineData(null)]
    public void Error_Name_Empty(string nome)
    {
        var validator = new CadastrarUserValidator();
        var request = RequestCadastrarUserJsonBuilder.Build();
        request.Nome = nome;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NOME_REQUIRED));
    }

    [Theory]
    [InlineData("")]
    [InlineData("           ")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        var validator = new CadastrarUserValidator();
        var request = RequestCadastrarUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_REQUIRED));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new CadastrarUserValidator();
        var request = RequestCadastrarUserJsonBuilder.Build();
        request.Email = "jorge.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var validator = new CadastrarUserValidator();
        var request = RequestCadastrarUserJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));
    }
}
