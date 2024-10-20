namespace AntecipaNotaNET.Domain.Exceptions;

public class DataNotaInvalidaException : Exception
{
    public DataNotaInvalidaException()
        : base("A data fornecida não pode ser menor ou igual a data atual.") { }
}
