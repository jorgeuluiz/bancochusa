namespace BancoChuSA.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
