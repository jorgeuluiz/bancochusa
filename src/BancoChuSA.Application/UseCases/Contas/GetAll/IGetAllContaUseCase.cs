using BancoChuSA.Communication.Responses;

namespace BancoChuSA.Application.UseCases.Contas.GetAll;
public interface IGetAllContaUseCase
{
    Task<ResponseContaJson> Execute();
}
