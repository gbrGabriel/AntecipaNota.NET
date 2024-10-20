namespace AntecipaNotaNET.Domain.Exceptions;

public class NotaCarrinhoAdicionadaException(int numeroNota)
    : Exception($"Nota fiscal {numeroNota} já adicionada ao carrinho.") { }
