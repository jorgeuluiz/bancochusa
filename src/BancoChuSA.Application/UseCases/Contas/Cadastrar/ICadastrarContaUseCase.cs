using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;

namespace BancoChuSA.Application.UseCases.Contas.Cadastrar;

public interface ICadastrarContaUseCase
{
    Task<ResponseCadastrarContaJson> Execute(RequestCadastrarContaJson request);
}
