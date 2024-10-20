namespace AntecipaNotaNET.Domain.Exceptions;

public class EmpresaInexistenteException : Exception
{
    public EmpresaInexistenteException()
        : base("Empresa não encontrada.") { }
}
