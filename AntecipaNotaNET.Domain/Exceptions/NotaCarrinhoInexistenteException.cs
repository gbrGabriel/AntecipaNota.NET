namespace AntecipaNotaNET.Domain.Exceptions;

public class NotaCarrinhoInexistenteException(int numeroNota)
    : Exception($"Nota fiscal {numeroNota} não foi encontrada no carrinho.") { }
