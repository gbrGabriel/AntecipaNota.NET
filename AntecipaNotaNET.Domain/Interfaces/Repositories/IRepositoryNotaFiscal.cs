using AntecipaNotaNET.Domain.Entities;

namespace AntecipaNotaNET.Domain.Interfaces.Repositories;

public interface IRepositoryNotaFiscal
{
    Task<NotaFiscal> SalvarNotaFiscal(NotaFiscal nota);
    Task<NotaFiscal?> ObterNotaPorNumero(int numeroNota);
}
