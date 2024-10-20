namespace AntecipaNotaNET.Domain.Entities;

public class Empresa
{
    public Empresa(string nome, string cnpj, decimal faturamento, string ramo)
    {
        Nome = nome;
        Cnpj = cnpj;
        Faturamento = faturamento;
        Ramo = ramo;
        CalcularLimite();
    }

    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Cnpj { get; private set; }
    public decimal Faturamento { get; private set; }
    public string Ramo { get; private set; }
    public decimal Limite { get; private set; }

    public void CalcularLimite()
    {
        if (Faturamento >= 100001)
            Limite = Math.Round(Ramo == "Serviços" ? Faturamento * 0.60m : Faturamento * 0.65m, 2);
        else if (Faturamento >= 50001 && Faturamento < 100001)
            Limite = Math.Round(Ramo == "Serviços" ? Faturamento * 0.55m : Faturamento * 0.60m, 2);
        else if (Faturamento >= 10000 && Faturamento < 50001)
            Limite = Math.Round(Faturamento * 0.50m, 2);
        else
            Limite = 0;
    }
}
