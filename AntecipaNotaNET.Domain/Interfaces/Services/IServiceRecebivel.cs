using AntecipaNotaNET.Domain.DTOs;
using AntecipaNotaNET.Domain.Entities;

namespace AntecipaNotaNET.Domain.Interfaces.Services;

public interface IServiceRecebivel
{
    Task<Empresa> AdicionarNovaEmpresa(EmpresaDTO empresa);
    Task<NotaFiscal> AdicionarNovaNotaFiscal(NotaFiscalDTO notaDto);
    Task<CarrinhoAntecipacaoDTO> CalcularAntecipacaoCarrinho(string cnpj);
    Task AdicionarNotaAoCarrinho(int numeroNota);
    Task RemoverNotaDoCarrinho(int numeroNota);
}
