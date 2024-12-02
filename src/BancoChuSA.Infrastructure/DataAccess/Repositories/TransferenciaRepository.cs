using BancoChuSA.Domain.Entities;
using BancoChuSA.Domain.Repositories.Transferencias;
using Microsoft.EntityFrameworkCore;

namespace BancoChuSA.Infrastructure.DataAccess.Repositories;

internal class TransferenciaRepository : ITransferenciaReadOnlyRepository, ITransferenciaWriteOnlyRepository
{
    private readonly BancoChuSADbContext _dbContext;

    public TransferenciaRepository(BancoChuSADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Transferencia transferencia)
    {
        await _dbContext.Transferencias.AddAsync(transferencia);
    }    

    public async Task<List<Transferencia>> GetAll()
    {
        return await _dbContext.Transferencias.AsNoTracking().ToListAsync();
    }

    public async Task<List<Transferencia>> FilterByMonth(DateTime date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day:1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
                .Transferencias
                .AsNoTracking()
                .Where(transferencia => transferencia.Data >= startDate && transferencia.Data <= endDate)
                .OrderBy(transferencia => transferencia.Data)               
                .ToListAsync();
    }
}
