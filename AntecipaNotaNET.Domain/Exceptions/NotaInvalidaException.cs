namespace AntecipaNotaNET.Domain.Exceptions;

public class NotaInvalidaException(int numeroNota)
    : Exception($"Nota fiscal número {numeroNota} não encontrada.") { }
