using AntecipaNotaNET.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AntecipaNotaNET.Infrastructure.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<NotaFiscal> Notas { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        base.OnConfiguring(optionsBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override void Dispose() => base.Dispose();
}
