using BancoChuSA.Domain.Entities;

namespace BancoChuSA.Domain.Repositories.Contas;
public interface IContaWriteOnlyRepository
{
    Task Add(Conta conta);

    Task Update(Conta conta);
}
