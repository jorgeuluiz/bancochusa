using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;

namespace BancoChuSA.Application.UseCases.Users.Cadastrar;

public interface ICadastrarUserUseCase
{
    Task<ResponseCadastrarUserJson> Execute(RequestCadastrarUserJson request);
}
