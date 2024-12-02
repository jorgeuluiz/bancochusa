using BancoChuSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BancoChuSA.Infrastructure.DataAccess;

public class BancoChuSADbContext : DbContext
{
    public BancoChuSADbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Conta> Contas { get; set; }
    public DbSet<Transferencia> Transferencias { get; set; }   
    public DbSet<User> Users { get; set; }   

}
