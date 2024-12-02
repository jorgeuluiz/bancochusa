using BancoChuSA.Communication.Responses;

namespace BancoChuSA.Application.UseCases.Transferencias.GetAll;
public interface IGetAllTransferenciaUseCase
{
    Task<ResponseTransferenciaJson> Execute();
}
