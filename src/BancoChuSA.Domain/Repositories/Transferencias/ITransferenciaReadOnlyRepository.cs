using BancoChuSA.Domain.Entities;

namespace BancoChuSA.Domain.Repositories.Transferencias;
public interface ITransferenciaReadOnlyRepository
{
    Task<List<Transferencia>> GetAll();

    Task<List<Transferencia>> FilterByMonth(DateTime date);
}
