using AntecipaNotaNET.Domain.Entities;

namespace AntecipaNotaNET.Domain.Interfaces.Repositories;

public interface IRepositoryEmpresa
{
    Task<Empresa> SalvarEmpresa(Empresa empresa);
    Task<Empresa?> ObterEmpresaPorCnpj(string cnpj);
}
