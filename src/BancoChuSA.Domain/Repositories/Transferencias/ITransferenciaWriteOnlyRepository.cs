using BancoChuSA.Domain.Entities;

namespace BancoChuSA.Domain.Repositories.Transferencias;

public interface ITransferenciaWriteOnlyRepository
{
    Task Add(Transferencia transferencia);
}
