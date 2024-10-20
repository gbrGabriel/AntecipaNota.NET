using AntecipaNotaNET.Domain.Entities;
using AntecipaNotaNET.Domain.Interfaces.Repositories;
using AntecipaNotaNET.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AntecipaNotaNET.Infrastructure.Repositories;

public class RepositoryNotaFiscal(ApplicationDbContext context) : IRepositoryNotaFiscal
{
    private readonly ApplicationDbContext _context = context;

    public async Task<NotaFiscal> SalvarNotaFiscal(NotaFiscal nota)
    {
        try
        {
            _context.Notas.Add(nota);

            await _context.SaveChangesAsync();

            return nota;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<NotaFiscal?> ObterNotaPorNumero(int numeroNota) =>
        await _context.Notas.AsNoTracking().SingleOrDefaultAsync(e => e.Numero == numeroNota);
}
