using BancoChuSA.Domain.Entities;

namespace BancoChuSA.Domain.Repositories.Contas;
public interface IContaReadOnlyRepository
{
    Task<List<Conta>> GetAll();

    Task<Conta> Get(string numeroConta);

    Task<bool> ExistNumeroConta(string numeroConta);
}
