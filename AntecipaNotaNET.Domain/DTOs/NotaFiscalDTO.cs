namespace AntecipaNotaNET.Domain.DTOs;

public record NotaFiscalDTO(int numero, decimal valor, DateTime vencimento, string cnpj);
