using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using BancoChuSA.Domain.Repositories.User;
using BancoChuSA.Domain.Security.Cryptography;
using BancoChuSA.Domain.Security.Tokens;
using BancoChuSA.Exception.ExceptionsBase;

namespace BancoChuSA.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncrypter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncrypter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseCadastrarUserJson> Execute(RequestLoginJson request)
    {

        var user = await _repository.GetUserByEmail(request.Email);
        if (user is null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);
        if (!passwordMatch)
        {
            throw new InvalidLoginException();
        }

        return new ResponseCadastrarUserJson
        {
            Nome = user.Nome,
            Token = _accessTokenGenerator.Generate(user)
        };
    }

}
