using BancoChuSA.Domain.Repositories;

namespace BancoChuSA.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly BancoChuSADbContext _dbContext;

    public UnitOfWork(BancoChuSADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
    
}
