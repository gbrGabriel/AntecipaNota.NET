using AntecipaNotaNET.Domain.Entities;
using AntecipaNotaNET.Domain.Interfaces.Repositories;
using AntecipaNotaNET.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AntecipaNotaNET.Infrastructure.Repositories;

public class RepisotoryEmpresa(ApplicationDbContext context) : IRepositoryEmpresa
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Empresa?> ObterEmpresaPorCnpj(string cnpj) =>
        await _context.Empresas.AsNoTracking().SingleOrDefaultAsync(e => e.Cnpj == cnpj);

    public async Task<Empresa> SalvarEmpresa(Empresa empresa)
    {
        try
        {
            _context.Empresas.Add(empresa);

            await _context.SaveChangesAsync();

            return empresa;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
