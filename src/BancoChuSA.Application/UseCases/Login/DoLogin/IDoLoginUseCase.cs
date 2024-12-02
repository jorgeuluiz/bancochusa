using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;

namespace BancoChuSA.Application.UseCases.Login.DoLogin;
public interface IDoLoginUseCase
{
    Task<ResponseCadastrarUserJson> Execute(RequestLoginJson request);
}
