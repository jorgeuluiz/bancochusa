using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;

namespace BancoChuSA.Application.UseCases.Transferencias.Cadastrar;

public interface ICadastrarTransferenciaUseCase
{
    Task<ResponseCadastrarTransferenciaJson> Execute(RequestCadastrarTransferenciaJson request);
}
