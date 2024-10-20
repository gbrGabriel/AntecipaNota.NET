using AntecipaNotaNET.Domain.DTOs;
using AntecipaNotaNET.Domain.Entities;
using AntecipaNotaNET.Domain.Exceptions;
using AntecipaNotaNET.Domain.Interfaces.Repositories;
using AntecipaNotaNET.Domain.Interfaces.Services;

namespace AntecipaNotaNET.Application.Services;

public class ServiceRecebivel(
    IRepositoryEmpresa repositoryEmpresa,
    IRepositoryNotaFiscal repositoryNotaFiscal,
    IRepositoryCarrinho repositoryCarrinho
) : IServiceRecebivel
{
    private readonly IRepositoryEmpresa _repositoryEmpresa = repositoryEmpresa;
    private readonly IRepositoryNotaFiscal _repositoryNotaFiscal = repositoryNotaFiscal;
    private readonly IRepositoryCarrinho _repositoryCarrinho = repositoryCarrinho;

    public async Task<Empresa> AdicionarNovaEmpresa(EmpresaDTO empresaDto) =>
        await _repositoryEmpresa.SalvarEmpresa(
            new(empresaDto.nome, empresaDto.cnpj, empresaDto.faturamento, empresaDto.ramo)
        );

    public async Task<NotaFiscal> AdicionarNovaNotaFiscal(NotaFiscalDTO notaDto)
    {
        try
        {
            var empresa = await _repositoryEmpresa.ObterEmpresaPorCnpj(notaDto.cnpj);

            if (empresa == null)
                throw new EmpresaInexistenteException();

            return await _repositoryNotaFiscal.SalvarNotaFiscal(
                new(notaDto.numero, notaDto.valor, notaDto.vencimento, empresa.Id)
            );
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task AdicionarNotaAoCarrinho(int numeroNota)
    {
        var numeroNotaCarrinho = await _repositoryCarrinho.ObterNotaCarrinho(numeroNota);
        if (numeroNotaCarrinho != null)
            throw new NotaCarrinhoAdicionadaException(numeroNota);

        var notaFiscal = await _repositoryNotaFiscal.ObterNotaPorNumero(numeroNota);

        if (notaFiscal == null)
            throw new NotaInvalidaException(numeroNota);

        await _repositoryCarrinho.AdicionarAoCarrinho(notaFiscal.Numero);
    }

    public async Task RemoverNotaDoCarrinho(int numeroNota)
    {
        var numeroNotaCarrinho = await _repositoryCarrinho.ObterNotaCarrinho(numeroNota);
        if (numeroNotaCarrinho == null)
            throw new NotaCarrinhoInexistenteException(numeroNota);

        var notaFiscal = await _repositoryNotaFiscal.ObterNotaPorNumero(
            numeroNotaCarrinho.NotaFiscalId
        );

        if (notaFiscal == null)
            throw new NotaInvalidaException(numeroNota);

        await _repositoryCarrinho.RemoveDoCarrinho(notaFiscal.Numero);
    }

    public async Task<CarrinhoAntecipacaoDTO> CalcularAntecipacaoCarrinho(string cnpj)
    {
        try
        {
            var empresa = await _repositoryEmpresa.ObterEmpresaPorCnpj(cnpj);

            if (empresa == null)
                throw new EmpresaInexistenteException();

            empresa.CalcularLimite();

            var notas = await _repositoryCarrinho.ObterNotasPorEmpresa(empresa.Id);

            decimal totalBruto = notas.Sum(n => n.ValorBruto);

            if (totalBruto > empresa.Limite)
                throw new EmpresaValorLimiteExcedidoException(
                    empresa.Limite,
                    Math.Round(totalBruto, 2)
                );

            var notasComValores = new List<ValorNotaDTO>();
            decimal totalLiquido = 0;

            foreach (var nota in notas)
            {
                nota.CalcularAntecipacao();
                totalLiquido += nota.ValorLiquido;

                notasComValores.Add(
                    new ValorNotaDTO(nota.Numero, nota.ValorBruto, nota.ValorLiquido)
                );
            }

            return new(
                empresa.Nome,
                empresa.Cnpj,
                empresa.Limite,
                notasComValores,
                totalLiquido,
                totalBruto
            );
        }
        catch (Exception)
        {
            throw;
        }
    }
}
