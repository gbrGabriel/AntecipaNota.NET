namespace AntecipaNotaNET.Domain.Entities;

public class Carrinho
{
    public Carrinho(int numeroNota)
    {
        NotaFiscalId = numeroNota;
    }

    private Carrinho() { }

    public int Id { get; private set; }
    public int NotaFiscalId { get; private set; }
}
