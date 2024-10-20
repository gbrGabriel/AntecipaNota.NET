using System.Text.Json.Serialization;
using AntecipaNotaNET.Domain.Exceptions;

namespace AntecipaNotaNET.Domain.Entities;

public class NotaFiscal
{
    public NotaFiscal(int numero, decimal valor, DateTime vencimento, int empresaId)
    {
        ValidarDataNota(vencimento);

        Numero = numero;
        ValorBruto = valor;
        DataVencimento = vencimento;
        EmpresaId = empresaId;

        CalcularAntecipacao();
    }

    private NotaFiscal() { }

    public int Numero { get; private set; }
    public decimal ValorBruto { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public decimal ValorLiquido { get; private set; }
    public decimal Desagio { get; private set; }

    [JsonIgnore]
    public int EmpresaId { get; private set; }

    private void ValidarDataNota(DateTime data)
    {
        if (data.Date <= DateTime.Today)
            throw new DataNotaInvalidaException();
    }

    private decimal CalcularDesagio(decimal valorBruto, int prazo, decimal taxa)
    {
        decimal fatorDesconto = (decimal)Math.Pow((double)(1 + taxa), prazo / 30.0);
        decimal valorLiquido = valorBruto / fatorDesconto;

        return Math.Round(valorBruto - valorLiquido, 2);
    }

    public void CalcularAntecipacao()
    {
        int prazo = (DataVencimento.Date - DateTime.Today).Days;

        decimal taxa = 0.0465m;

        Desagio = CalcularDesagio(ValorBruto, prazo, taxa);

        ValorLiquido = Math.Round(ValorBruto - Desagio, 2);
    }
}
