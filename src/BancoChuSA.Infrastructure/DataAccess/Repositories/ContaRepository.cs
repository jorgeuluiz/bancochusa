using BancoChuSA.Domain.Entities;
using BancoChuSA.Domain.Repositories.Contas;
using Microsoft.EntityFrameworkCore;

namespace BancoChuSA.Infrastructure.DataAccess.Repositories;

internal class ContaRepository : IContaReadOnlyRepository, IContaWriteOnlyRepository
{
    private readonly BancoChuSADbContext _dbContext;

    public ContaRepository(BancoChuSADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Conta conta)
    {
       await _dbContext.Contas.AddAsync(conta);
    }

    public async Task<bool> ExistNumeroConta(string numeroConta)
    {
        return await _dbContext.Contas.AnyAsync(user => user.NumeroConta.Equals(numeroConta));
    }

    public async Task<Conta> Get(string numeroConta)
    {
        return await _dbContext.Contas.FirstAsync(user => user.NumeroConta == numeroConta);
    }

    public async Task<List<Conta>> GetAll()
    {
        return await _dbContext.Contas.AsNoTracking().ToListAsync();
    }

    public async Task Update(Conta conta)
    {
        _dbContext.Contas.Update(conta);
    }
}