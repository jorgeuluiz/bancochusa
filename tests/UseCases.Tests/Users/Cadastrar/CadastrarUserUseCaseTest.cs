using BancoChuSA.Exception.ExceptionsBase;
using BancoChuSA.Exception;
using FluentAssertions;
using CommonTestUtilities.Requests;
using BancoChuSA.Application.UseCases.Users.Cadastrar;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Token;
using CommonTestUtilities.Cryptography;

namespace UseCases.Tests.Users.Cadastrar;

public class CadastrarUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestCadastrarUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Nome.Should().Be(request.Nome);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestCadastrarUserJsonBuilder.Build();
        request.Nome = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NOME_REQUIRED));

    }

    [Fact]
    public async Task Error_Email_Already_Exist()
    {
        var request = RequestCadastrarUserJsonBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_REGISTERED));

    }

    private CadastrarUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = new PasswordEncrypterBuilder().Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();

        if (!string.IsNullOrWhiteSpace(email))
            readRepository.ExistActiveUserWithEmail(email);


        return new CadastrarUserUseCase(mapper, passwordEncripter, readRepository.Build(), unitOfWork, writeRepository, tokenGenerator);
    }
}
