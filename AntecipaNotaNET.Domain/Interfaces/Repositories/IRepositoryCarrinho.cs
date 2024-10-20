using AntecipaNotaNET.Domain.Entities;

namespace AntecipaNotaNET.Domain.Interfaces.Repositories;

public interface IRepositoryCarrinho
{
    Task AdicionarAoCarrinho(int numeroNota);
    Task RemoveDoCarrinho(int numeroNota);
    Task<List<NotaFiscal>> ObterNotasPorEmpresa(int empresaId);
    Task<Carrinho?> ObterNotaCarrinho(int numeroNota);
}
