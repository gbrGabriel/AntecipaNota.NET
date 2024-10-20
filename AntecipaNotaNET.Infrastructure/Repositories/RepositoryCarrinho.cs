using AntecipaNotaNET.Domain.Entities;
using AntecipaNotaNET.Domain.Interfaces.Repositories;
using AntecipaNotaNET.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AntecipaNotaNET.Infrastructure.Repositories;

public class RepositoryCarrinho(ApplicationDbContext context) : IRepositoryCarrinho
{
    private readonly ApplicationDbContext _context = context;

    public async Task AdicionarAoCarrinho(int numeroNota)
    {
        _context.Carrinhos.Add(new(numeroNota));

        await _context.SaveChangesAsync();
    }

    public async Task<Carrinho?> ObterNotaCarrinho(int numeroNota) =>
        await _context.Carrinhos.SingleOrDefaultAsync(e => e.NotaFiscalId == numeroNota);

    public async Task<List<NotaFiscal>> ObterNotasPorEmpresa(int empresaId)
    {
        try
        {
            var notas = new List<NotaFiscal>();

            var numerosNotas = await _context.Carrinhos.Select(e => e.NotaFiscalId).ToListAsync();

            if (numerosNotas.Count > 0)
            {
                notas = await _context
                    .Notas.AsNoTracking()
                    .Where(nota =>
                        numerosNotas.Contains(nota.Numero) && nota.EmpresaId == empresaId
                    )
                    .ToListAsync();
            }

            return notas;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task RemoveDoCarrinho(int numeroNota)
    {
        var notaCarrinho = await _context.Carrinhos.SingleOrDefaultAsync(e =>
            e.NotaFiscalId == numeroNota
        );

        if (notaCarrinho != null)
        {
            _context.Carrinhos.Remove(notaCarrinho);

            await _context.SaveChangesAsync();
        }
    }
}
